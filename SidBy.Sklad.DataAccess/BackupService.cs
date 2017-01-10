using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.DataAccess
{
    public class BackupService
    {
        private readonly string _connectionString;
        private readonly string _backupFolderFullPath;
        private readonly string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };

        public BackupService(string connectionString, string backupFolderFullPath)
        {
            _connectionString = connectionString;
            _backupFolderFullPath = backupFolderFullPath;
        }

        public void BackupAllUserDatabases()
        {
            foreach (string databaseName in GetAllUserDatabases())
            {
                BackupDatabase(databaseName);
            }
        }

        /*
         USE master;
GO
ALTER DATABASE AMOD SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
Once you've finished restoring and the database is ready for use again:

ALTER DATABASE AMOD SET MULTI_USER;
             */

        private void SingleUserOwn(string databaseName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // WITH INIT - backup file should be overwritten 
                    var query = String.Format(@"ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", databaseName);

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var fd = ex;
            }
        }

        private void MultiUserOwn(string databaseName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // WITH INIT - backup file should be overwritten 
                    var query = String.Format(@"

ALTER DATABASE [{0}] SET MULTI_USER", databaseName);

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                var df = ex;
            }
        }

        /// <summary>
        /// RestoreDatabase.
        /// Login should be dbcreator server role
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="filePath"></param>
        public void RestoreDatabase(string databaseName, string filePath)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    using (var command = new SqlCommand())
                    {
                        command.CommandText = String.Format(@"ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE [{0}] FROM DISK = @filePath  WITH REPLACE; ALTER DATABASE [{0}] SET MULTI_USER ", databaseName);
                        command.Parameters.Add("@filePath", SqlDbType.NVarChar, 4000).Value = filePath;
                        command.CommandType = CommandType.Text;
                        command.Connection = connection;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
              
            }

            //MultiUserOwn(databaseName);
        }

        public void BackupDatabase(string databaseName, string prefix = "")
        {
            string filePath = BuildBackupPathWithFilename(databaseName, prefix);

            using (var connection = new SqlConnection(_connectionString))
            {
                // WITH INIT - backup file should be overwritten 
                var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}' WITH INIT", databaseName, filePath);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<string> GetAllUserDatabases()
        {
            var databases = new List<String>();

            DataTable databasesTable;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                databasesTable = connection.GetSchema("Databases");

                connection.Close();
            }

            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();

                if (_systemDatabaseNames.Contains(databaseName))
                    continue;

                databases.Add(databaseName);
            }

            return databases;
        }

        private string BuildBackupPathWithFilename(string databaseName, string prefix)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName + prefix, DateTime.Now.ToString("yyyy-MM-dd"));

            return Path.Combine(_backupFolderFullPath, filename);
        }
    }
}
