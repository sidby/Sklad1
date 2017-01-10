﻿using log4net;
using SidBy.Sklad.DataAccess;
using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SidBy.Sklad.Web.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "admin,employee")]
    public class AdministrationController : Controller
    {
        private readonly ILog logger;

        public AdministrationController()
        {
            logger = LogManager.GetLogger(GetType());
        }

        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        private void DeleteBackup(string filename)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~"), ConfigurationManager.AppSettings["BackupFolder"]);
                string filePath = Path.Combine(path, filename);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                logger.InfoFormat("Файл резервной копии БД '{0}' был удалён", filename);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления резервной копии БД ", ex);
            }
        }

        private void RestoreDatabase(string filename)
        {
            string path = Path.Combine(Server.MapPath("~"), ConfigurationManager.AppSettings["BackupFolder"]);
            string filePath = Path.Combine(path, filename);
            BackupDB("-before-restore");

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            builder.ConnectTimeout = 10;
            var service = new BackupService(builder.ConnectionString, path);
            if (System.IO.File.Exists(filePath))
                service.RestoreDatabase(database, filePath);
        }

        public JsonResult OperateBackup(int operation, string filename)
        {
            if (String.IsNullOrEmpty(filename))
                return null;

            if (operation == 0)
            {
                DeleteBackup(filename);
            }
            else if (operation == 1)
                RestoreDatabase(filename);

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = 1 };
            //logger.InfoFormat("Создание полной резервной копии БД завершено");

            return jsonResult;
        }

        private void BackupDB(string prefix = "")
        {
            string path = Path.Combine(Server.MapPath("~"), ConfigurationManager.AppSettings["BackupFolder"]);
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;

            var service = new BackupService(connectionString, path);
            service.BackupDatabase(database, prefix);
        }

        public JsonResult CreateFullBackup(int employeeId)
        {
            logger.InfoFormat("Создание полной резервной копии БД");

            BackupDB();

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = 1 };
            logger.InfoFormat("Создание полной резервной копии БД завершено");

            return jsonResult;
        }

        public JsonResult GetBackupFiles()
        {
            string backupFolder = ConfigurationManager.AppSettings["BackupFolder"];
            string path = Path.Combine(Server.MapPath("~"), backupFolder);
            //var files = Directory.GetFiles(path,"*");

            DirectoryInfo info = new DirectoryInfo(path);
            var files = info.GetFiles().OrderByDescending(p => p.LastWriteTime);
            
            var result = new BackupModel();
            result.RelativeUrl = backupFolder;
            result.Files = new List<BackupFileModel>();
            foreach (FileInfo file in files)
            {
                if(file.Extension == ".bak")
                    result.Files.Add(new BackupFileModel { FileName = file.Name, Created = file.CreationTime, Modified = file.LastWriteTime });
            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = result };

            return jsonResult;
        }
    }
}