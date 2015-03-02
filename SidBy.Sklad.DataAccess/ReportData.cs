using SidBy.Sklad.Domain.Enums;
using SidBy.Sklad.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.ComponentModel;

namespace SidBy.Sklad.DataAccess
{
    public class ReportData
    {
        private string ConnectionString { get; set; }
        private readonly ILog logger;
        public ReportData(string connectionString)
        {
            ConnectionString = connectionString;
            logger = LogManager.GetLogger(GetType());
        }


        private SalesReportModelItem GetTotalReport(int? contractorId, string yearMonthFrom, string yearMonthTo,
            EntityEnum.DocumentTypeEnum doctype, int employeeId)
        {
            var result = new SalesReportModelItem();
            using (SqlConnection connection = SqlHelper.GetConnection(ConnectionString))
            {
                string contractorStr = " ";
                if (contractorId != null)
                { 
                    contractorStr = " AND ContractorId = " + contractorId + " ";
                }

                using (SqlCommand command = connection.GetCommand(" " +
                    "SELECT " +
                    "SUM([Sum]) AS PurchaseSum, SUM([SaleSum]) AS SaleSum " +
                    "FROM Document " +
                    "WHERE DocumentTypeId = " + (int)doctype + GetEmployeeIdWhereString(employeeId) + 
                    "AND IsCommitted = 1 " + contractorStr +
                    "AND CreatedOf >= '" + yearMonthFrom + "' AND CreatedOf < '" + yearMonthTo + "' ", CommandType.Text))
               
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //decimal purchaseSum = 0, saleSum = 0;
                            if (reader["PurchaseSum"] != DBNull.Value)
                                result.PurchaseSum = Convert.ToDecimal(reader["PurchaseSum"]);

                            if (reader["SaleSum"] != DBNull.Value)
                                result.SaleSum = Convert.ToDecimal(reader["SaleSum"]);
                          
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private DateTime GetDate(SqlDataReader reader, EntityEnum.ReportTypeEnum reportType)
        {
            if (reportType == EntityEnum.ReportTypeEnum.ByDay)
            {
                return Convert.ToDateTime(reader["CreatedOf"]);
            }
            else if (reportType == EntityEnum.ReportTypeEnum.ByMonth)
            {
                int year = Convert.ToInt32(reader["Year"]);
                int month = Convert.ToInt32(reader["Month"]);
                return new DateTime(year,month,1,0,0,0,0);
            }

            throw new ArgumentException();
        }

        private SalesReportModelItem GetReportByDate(DateTime date,
            EntityEnum.DocumentTypeEnum docType, EntityEnum.ReportTypeEnum reportType, int employeeId)
        {
            var result = new SalesReportModelItem();
            string datePartFrom = "", datePartTo = "";
            if (reportType == EntityEnum.ReportTypeEnum.ByMonth)
            {
                DateTime dateTo = date.AddMonths(1);
                datePartTo = SqlHelper.GetDateString(dateTo.Month, dateTo.Year);
            }
            else if (reportType == EntityEnum.ReportTypeEnum.ByDay)
            {
                DateTime dateTo = date.AddDays(1);
                datePartTo = SqlHelper.GetDateString(dateTo.Day, dateTo.Month, dateTo.Year);
            }
            datePartFrom = SqlHelper.GetDateString(date.Day, date.Month, date.Year);
            result = GetTotalReport(null, datePartFrom, datePartTo, docType, employeeId);

            return result;
        }

        private static string GetEmployeeIdWhereString(int employeeId)
        {
            bool includeEmployee = (employeeId > 0) ? true : false;
            if (includeEmployee)
                return " AND EmployeeId = " + employeeId + " ";
            else
                return " ";
        }

        private T GetGrandTotal<T>(int yearFromNumber, int yearToNumber,
           int monthFromNumber, int monthToNumber,
           SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, bool getQuantity, int managerId)
        {
            if (monthFromNumber > 12 || monthToNumber > 12 || monthToNumber < 1 || monthFromNumber < 1 ||
                    yearFromNumber < 1901 || yearToNumber < 1901)
                throw new ArgumentException();

            string yearMonthFrom = SqlHelper.GetDateString(monthFromNumber, yearFromNumber);
            string yearMonthTo = SqlHelper.GetDateString(monthToNumber, yearToNumber, true);

            var thisType = default(T);
            var typeCode = thisType.GetType();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            string responsibleFilter = String.Empty;
            if (managerId >= 1) {
                responsibleFilter = " AND Contractor.ResponsibleId=" + managerId + " ";
            }

            using (SqlConnection connection = SqlHelper.GetConnection(ConnectionString))
            {
                string sumString = (getQuantity) ? "ProductLine.Quantity" : "ProductLine.SaleSum";
                using (SqlCommand command = connection.GetCommand("" +
                        "SELECT SUM(" + sumString + ") AS SaleGrandTotal " +
                        "FROM ProductLine " + " RIGHT JOIN Contractor ON [SupplierId] = Contractor.ContractorId " +responsibleFilter +
                        " INNER JOIN " +
                        " Document ON ProductLine.DocumentId = Document.DocumentId " +
                        " WHERE  Document.IsCommitted = 1 AND Document.CreatedOf >= '" + yearMonthFrom + "' AND Document.CreatedOf <='" + yearMonthTo + "' " +
                        " AND Document.DocumentTypeId =" + (int)type, CommandType.Text))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["SaleGrandTotal"] != null)
                            {
                                if (getQuantity){
                                    int returnValueInt;
                                    Int32.TryParse(reader["SaleGrandTotal"].ToString(), out returnValueInt);
                                    return (T)Convert.ChangeType(returnValueInt, typeCode);
                                }
                                else
                                {
                                    decimal returnValueDecimal;
                                    Decimal.TryParse(reader["SaleGrandTotal"].ToString(), out returnValueDecimal);
                                    return (T)Convert.ChangeType(returnValueDecimal, typeCode);
                                    //return (T)converter.ConvertFrom(returnValueDecimal);
                                }
                              
                            }
                        }
                    }
                }
            }

            return (T)converter.ConvertFrom(0);
        }

        private decimal GetGrandTotalSum(int yearFromNumber, int yearToNumber,
            int monthFromNumber, int monthToNumber, 
            SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, int managerId)
        {
            return GetGrandTotal<decimal>(yearFromNumber, yearToNumber,
             monthFromNumber, monthToNumber,
             type, false, managerId);
        }

        private int GetGrandTotalQuantity(int yearFromNumber, int yearToNumber,
            int monthFromNumber, int monthToNumber,
            SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, int managerId)
        {
            return GetGrandTotal<int>(yearFromNumber, yearToNumber,
             monthFromNumber, monthToNumber,
             type, true, managerId);
        }

        private List<SalesReportContractorModel> GetSalesSumReportByContractorList(int yearFromNumber, int yearToNumber,
            int monthFromNumber, int monthToNumber, 
            SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, int managerId)
        {
            return GetSalesReportByContractorList(yearFromNumber, yearToNumber,
         monthFromNumber, monthToNumber, type, false, managerId);
        }

        private List<SalesReportContractorModel> GetSalesReportByContractorList(int yearFromNumber, int yearToNumber,
          int monthFromNumber, int monthToNumber,
          SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, bool getQuantity, int managerId)
        {
         var result = new List<SalesReportContractorModel>();

            if (monthFromNumber > 12 || monthToNumber > 12 || monthToNumber < 1 || monthFromNumber < 1 ||
                  yearFromNumber < 1901 || yearToNumber < 1901)
                throw new ArgumentException();

            string yearMonthFrom = SqlHelper.GetDateString(monthFromNumber, yearFromNumber);
            string yearMonthTo = SqlHelper.GetDateString(monthToNumber, yearToNumber, true);

            using (SqlConnection connection = SqlHelper.GetConnection(ConnectionString))
            {
                string sumString = (getQuantity) ? "[Quantity]" : "[SaleSum]";

                string responsibleStr0 = String.Empty;
              
                if (managerId >= 1)
                    responsibleStr0 = "  AND Contractor.ResponsibleId =  " + managerId + " ";

                using (SqlCommand command = connection.GetCommand(" " +
                "SELECT SupplierId, refunds.SupplierCode AS SupplierCode,SupplierRegion, "+
                "RefundsSaleSum AS SaleSum,refunds.ContractorId AS ClientId, Contractor.Code AS ClientCode , Contractor.Region AS ClientRegion " +
                "FROM ( " +
                "SELECT  " +
                   "ProductLine.[SupplierId], Contractor.Code AS SupplierCode, Contractor.Region AS SupplierRegion " +
                 " ,SUM(ProductLine." + sumString + ") AS RefundsSaleSum, Document.ContractorId AS ContractorId " +
                  "FROM [ProductLine] " +
                  " RIGHT JOIN Contractor ON [SupplierId] = Contractor.ContractorId  " + responsibleStr0 +
                  "INNER JOIN " +
                    "Document ON ProductLine.DocumentId = Document.DocumentId " +
                  "WHERE Document.IsCommitted = 1 AND Document.CreatedOf >= '" + yearMonthFrom + "' AND Document.CreatedOf <='" + yearMonthTo + "' " +
                  "AND Document.DocumentTypeId = " + (int)type  +
                  " GROUP BY [SupplierId],Contractor.Code ,Document.ContractorId , Contractor.Region" +
                  ") refunds " +
                  "INNER JOIN Contractor On refunds.ContractorId = Contractor.ContractorId " 
                    , CommandType.Text))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new SalesReportContractorModel
                            {
                                Client = new ContractorGridSimpleModel
                                {
                                    Code = reader["ClientCode"].ToString(),
                                    ContractorId = Int32.Parse(reader["ClientId"].ToString()),
                                    Region = reader["ClientRegion"].ToString()
                                },
                                Factory = new ContractorGridSimpleModel
                                {
                                    Code = reader["SupplierCode"].ToString(),
                                    ContractorId = Int32.Parse(reader["SupplierId"].ToString()),
                                    Region = reader["SupplierRegion"].ToString()
                                },

                            };
                            if (type == EntityEnum.DocumentTypeEnum.Refunds)
                            {
                                if (getQuantity)
                                    item.QuantityRefunds = Int32.Parse(reader["SaleSum"].ToString());
                                else
                                    item.Refunds = decimal.Parse(reader["SaleSum"].ToString());
                            }
                            else if (type == EntityEnum.DocumentTypeEnum.Shipment) {
                                if (getQuantity)
                                    item.QuantityShipment = Int32.Parse(reader["SaleSum"].ToString());
                                else
                                    item.Shipment = decimal.Parse(reader["SaleSum"].ToString());
                            }
                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        private List<SalesReportContractorModel> GetSalesQuantityReportByContractorList(int yearFromNumber, int yearToNumber,
          int monthFromNumber, int monthToNumber,
          SidBy.Sklad.Domain.Enums.EntityEnum.DocumentTypeEnum type, int managerId)
        {
            return GetSalesReportByContractorList(yearFromNumber, yearToNumber,
         monthFromNumber, monthToNumber, type, true, managerId);
        }

        public SalesReportContractorGridModel GetSalesReportByContractor(int yearFromNumber, int yearToNumber,
            int monthFromNumber, int monthToNumber, int managerId)
        {
            var result = new SalesReportContractorGridModel();

            // Отгрузки
            var shipment = GetSalesQuantityReportByContractorList(yearFromNumber, yearToNumber, monthFromNumber, monthToNumber, EntityEnum.DocumentTypeEnum.Shipment, managerId);
            // Возвраты
            var refunds = GetSalesQuantityReportByContractorList(yearFromNumber, yearToNumber, monthFromNumber, monthToNumber, EntityEnum.DocumentTypeEnum.Refunds, managerId);

            // merge shipment in refunds
            // Присоеденим все возвраты к отгрузкам
            foreach (var refundsItem in refunds)
            {
                var shipmentItem = shipment.Where(x => x.Factory.ContractorId == refundsItem.Factory.ContractorId &&
                    x.Client.ContractorId == refundsItem.Client.ContractorId).FirstOrDefault();
                if (shipmentItem != null)
                    shipmentItem.QuantityRefunds = refundsItem.QuantityRefunds;
            }

            result.Data = shipment;
          
            result.Clients = result.Data.GroupBy(x => x.Client.ContractorId).Select(k => 
                 result.Data.Where(x => x.Client.ContractorId == k.Key).First().Client  
            ).ToList();

            result.Factories = result.Data.GroupBy(x => x.Factory.ContractorId).Select(k =>
               result.Data.Where(x => x.Factory.ContractorId == k.Key).First().Factory
            ).OrderBy(x => x.Code).ToList();

            result.RefundsQuantityGrandTotal = GetGrandTotalQuantity(yearFromNumber, yearToNumber, monthFromNumber, monthToNumber, EntityEnum.DocumentTypeEnum.Refunds, managerId);
            result.ShipmentQuantityGrandTotal = GetGrandTotalQuantity(yearFromNumber, yearToNumber, monthFromNumber, monthToNumber, EntityEnum.DocumentTypeEnum.Shipment, managerId);

            return result;
        }

        public SalesReportDataModel GetSalesReport(int yearFromNumber, int yearToNumber,
            int monthFromNumber, int monthToNumber, 
            EntityEnum.DocumentTypeEnum docType, EntityEnum.ReportTypeEnum reportType, int employeeId)
        {
            var result = new List<SalesReportModel>();
            var data = new SalesReportDataModel();
            List<SalesReportPeriodModel> periods = new List<SalesReportPeriodModel>();
            if(monthFromNumber >12 || monthToNumber > 12 || monthToNumber < 1 || monthFromNumber < 1 ||
                yearFromNumber < 1901 || yearToNumber < 1901)
                throw new ArgumentException();

            string yearMonthFrom = SqlHelper.GetDateString(monthFromNumber,yearFromNumber);
            string yearMonthTo = SqlHelper.GetDateString(monthToNumber, yearToNumber);

            using (SqlConnection connection = SqlHelper.GetConnection(ConnectionString))
            {
                string datePartInSelect = " ", datePartInGroupBy = " ", datePartInOrderBy = " ",
                    datePartCreatedOf = ",  [CreatedOf] "/*, employeeStr = ""*/;

                if (reportType == EntityEnum.ReportTypeEnum.ByMonth)
                {
                    datePartInSelect = " DATEPART(Year, CreatedOf) Year, DATEPART(Month, CreatedOf) Month, ";
                    datePartInGroupBy = " DATEPART(Year, CreatedOf), DATEPART(Month, CreatedOf), ";
                    datePartInOrderBy = ", Year, Month ";
                    datePartCreatedOf = " ";
                }

                using (SqlCommand command = connection.GetCommand(" " +
                    "SELECT " + datePartInSelect + " [Document].[ContractorId] AS DocumentContractorId, " +
                    "[Contractor].Code AS ContractorCode " + datePartCreatedOf + ", SUM([Sum]) AS PurchaseSum, " +
                    "SUM([SaleSum]) AS SaleSum " +
                    "FROM [Document] " +
                    "INNER JOIN Contractor on Document.ContractorId = Contractor.ContractorId " +
                    "WHERE DocumentTypeId = " + (int)docType + " AND IsCommitted = 1 " + GetEmployeeIdWhereString(employeeId) +
                    "AND CreatedOf >= '" + yearMonthFrom + "' AND CreatedOf < '" + yearMonthTo + "' " +
                    "GROUP BY " + datePartInGroupBy + " [Contractor].Code " + datePartCreatedOf + ",[Document].[ContractorId] " +
                    "ORDER BY [Document].[ContractorId] ASC " + datePartInOrderBy +" " + datePartCreatedOf 
                    , CommandType.Text))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        SalesReportModel item = null;
                   
                        while (reader.Read())
                        {
                            DateTime createdOf = GetDate(reader,reportType);
                           
                            decimal purchaseSum = Convert.ToDecimal(reader["PurchaseSum"]);
                            decimal saleSum = Convert.ToDecimal(reader["SaleSum"]);
                            int contractorId = Int32.Parse(reader["DocumentContractorId"].ToString());
                            string contractorCode = reader["ContractorCode"].ToString();

                            if (periods.Where(x => x.Period == createdOf).FirstOrDefault()==null)
                            {
                                periods.Add(
                                    new SalesReportPeriodModel
                                    {
                                        Period = createdOf,
                                        ReportItem = GetReportByDate(createdOf,docType, reportType,employeeId),
                                    });
                            }

                            if(item == null)
                                item = new SalesReportModel{ 
                                    ContractorModel = new ContractorCodeIdModel{ ContractorCode = contractorCode,
                                     ContractorId = contractorId,
                                    },
                                    Refunds = GetTotalReport(contractorId, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Refunds, employeeId),
                                    SubTotal = GetTotalReport(contractorId, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Shipment, employeeId),
                                };
                            if (item.ContractorModel.ContractorId != contractorId)
                            {
                                result.Add(item);
                                item = new SalesReportModel
                                {
                                    ContractorModel = new ContractorCodeIdModel
                                    {
                                        ContractorCode = contractorCode,
                                        ContractorId = contractorId
                                    },
                                    Refunds = GetTotalReport(contractorId, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Refunds, employeeId),
                                    SubTotal = GetTotalReport(contractorId, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Shipment, employeeId),
                                    ReportItems = new List<SalesReportModelItem>()
                                };
                            }

                            if (item.ReportItems == null)
                                item.ReportItems = new List<SalesReportModelItem>();

                            item.ReportItems.Add(new SalesReportModelItem
                            {
                                CreatedOf = createdOf,
                                PurchaseSum = purchaseSum,
                                SaleSum = saleSum
                            });
                        }
                        // Check if contractor already exists

                        result.Add(item);
                    }
                }
            }
            /*
               string yearMonthFrom = SqlHelper.GetDateString(monthFromNumber,yearFromNumber);
            string yearMonthTo = SqlHelper.GetDateString(monthToNumber, yearToNumber);
             */
            data.RefundsGrandTotal = GetTotalReport(null, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Refunds, employeeId);
            data.GrandTotal = GetTotalReport(null, yearMonthFrom, yearMonthTo, EntityEnum.DocumentTypeEnum.Shipment, employeeId);

            data.ReportModel = result;
            //List<DateTime> datesList = dates.ToList();
            //datesList.Sort();
            data.Periods = periods.OrderBy(x => x.Period).ToList();
            return data;
        }
    }
}
