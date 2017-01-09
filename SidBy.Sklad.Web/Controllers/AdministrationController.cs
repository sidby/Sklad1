using log4net;
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

        public JsonResult CreateFullBackup(int employeeId)
        {
            logger.InfoFormat("Создание полной резервной копии БД");

            string path = Path.Combine(Server.MapPath("~"), ConfigurationManager.AppSettings["BackupFolder"]);
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;

            BackupService service = new BackupService(connectionString, path);
            service.BackupDatabase(database);
          
            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = 1 };
            logger.InfoFormat("Создание полной резервной копии БД завершено");

            return jsonResult;
        }

        public JsonResult GetBackupFiles()
        {
            string path = Path.Combine(Server.MapPath("~"), ConfigurationManager.AppSettings["BackupFolder"]);
            //var files = Directory.GetFiles(path,"*");

            DirectoryInfo info = new DirectoryInfo(path);
            var files = info.GetFiles().OrderBy(p => p.LastWriteTime);
            
            var result = new BackupModel();
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