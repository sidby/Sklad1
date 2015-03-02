using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportModelItem
    {
        public DateTime CreatedOf { get; set; }
        /// <summary>
        /// Наша
        /// </summary>
        public decimal PurchaseSum { get; set; }
        /// <summary>
        /// Продажа
        /// </summary>
        public decimal SaleSum { get; set; }

        /// <summary>
        /// Разница
        /// </summary>
        public decimal Profit
        {
            get
            {
                return (SaleSum - PurchaseSum);
            }
        }
    }
}
