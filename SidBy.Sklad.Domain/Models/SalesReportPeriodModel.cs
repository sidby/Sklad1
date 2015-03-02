using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportPeriodModel
    {
        public DateTime Period { get; set; }
        public SalesReportModelItem ReportItem { get; set; }
    }
}
