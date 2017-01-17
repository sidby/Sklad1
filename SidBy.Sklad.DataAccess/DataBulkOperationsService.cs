using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.DataAccess
{
    public class DataBulkOperationsService
    {
        private readonly string _connectionString;
        public DataBulkOperationsService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DeleteDocumentsByPeriod(int monthFromNumber, int yearFromNumber,
          int monthToNumber, int yearToNumber)
        {
            if (monthFromNumber > 12 || monthToNumber > 12 || monthToNumber < 1 || monthFromNumber < 1 ||
               yearFromNumber < 1901 || yearToNumber < 1901)
                throw new ArgumentException();

            string yearMonthFrom = SqlHelper.GetDateString(monthFromNumber, yearFromNumber);
            string yearMonthTo = SqlHelper.GetDateString(monthToNumber, yearToNumber);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    using (var command = new SqlCommand())
                    {

                        //The DELETE statement conflicted with the SAME TABLE REFERENCE constraint "FK_dbo.Document_dbo.Document_ParentDocumentId". The conflict occurred in database "Sklad", table "dbo.Document", column 'ParentDocumentId'.


                        command.CommandText = String.Format(@"
                         UPDATE Document SET Comment ='deleted' WHERE CreatedOf >= '" + yearMonthFrom + "' AND CreatedOf < '" + yearMonthTo + "' ");
                        //    "SELECT AND CreatedOf >= '" + yearMonthFrom + "' AND CreatedOf < '" + yearMonthTo + "' " +

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
        }
    }
}
