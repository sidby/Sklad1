using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportModel
    {
        /// <summary>
        /// Клиент
        /// </summary>
        public ContractorCodeIdModel ContractorModel { get; set; }
        public List<SalesReportModelItem> ReportItems { get; set; }
        public SalesReportModelItem Refunds { get; set; }
        public SalesReportModelItem SubTotal { get; set; }
        public SalesReportModelItem SubTotalProfit {
            get
            {
                var result = new SalesReportModelItem { SaleSum = 0, PurchaseSum = 0 };
                if (SubTotal != null)
                {
                    if (Refunds == null)
                    { 
                        result.SaleSum = SubTotal.SaleSum;
                        result.PurchaseSum = SubTotal.PurchaseSum;
                    }
                    else
                    {
                        result.SaleSum = SubTotal.SaleSum - Refunds.SaleSum;
                        result.PurchaseSum = SubTotal.PurchaseSum - Refunds.PurchaseSum;
                    }
                }

                return result;
            }
        }
    }
}
