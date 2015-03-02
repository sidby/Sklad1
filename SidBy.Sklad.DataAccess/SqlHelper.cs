using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.DataAccess
{
    public static class SqlHelper
    {

        public static string GetDateString(int monthNumber, int yearNumber)
        {
            return GetDateString(monthNumber, yearNumber, false);
        }
        public static string GetDateString(int monthNumber, int yearNumber, bool monthPlusOne)
        {
            DateTime date = new DateTime(yearNumber,monthNumber,1);
            if (monthPlusOne)
                date = date.AddMonths(1);
            return String.Format("{0}-{1}-01", date.Year, date.Month.ToString("D2"));
        }

        public static string GetDateString(int dayNumber, int monthNumber, int yearNumber)
        {
            return String.Format("{0}-{1}-{2}", yearNumber, monthNumber.ToString("D2"), dayNumber.ToString("D2"));
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            { 
                // log error
                if (connection != null)
                    connection.Dispose();
            }

            return connection;
        }

        public static SqlCommand GetCommand(this SqlConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = connection.ConnectionTimeout;
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        public static void AddParameter(this SqlCommand command, string parameterName, object parameterValue, SqlDbType parameterSqlType)
        {
            if (!parameterName.StartsWith("@"))
                parameterName = "@" + parameterName;

            command.Parameters.Add(parameterName, parameterSqlType);
            command.Parameters[parameterName].Value = parameterValue;
        }
    }
}
