using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SidBy.Sklad.Web
{
    public static class Constants
    {
        public const string ErrorUrl = "/Error";

        public const string DocumentReportPath = "Reports/Document";
        public const string RefundsReportPath = "Reports/Refunds";
        public const string SalesByContractorReportPath = "Reports/SalesByContractor";
        public const string DocumentReportPrefix = "Report-";
        public const string RefundsReportPrefix = "RefundsReport-";
        public const string SalesByContractorReportPrefix = "SalesByContractorReport";
    }
}