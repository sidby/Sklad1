using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportDataModel
    {
        public List<SalesReportModel> ReportModel { get; set; }
        public List<SalesReportPeriodModel> Periods { get; set; }
        /// <summary>
        /// Итого возвраты покупателей
        /// </summary>
        public SalesReportModelItem RefundsGrandTotal { get; set; }
        public SalesReportModelItem GrandTotal { get; set; }
    }
}
