using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Article { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal VAT { get; set; }
        public int ContractorId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Для остатков
        public int Remains { get; set; }
        /// <summary>
        /// В резерве
        /// </summary>
        public int Reserve { get; set; }
        /// <summary>
        /// В ожидании
        /// </summary>
        public int Pending { get; set; }
        /// <summary>
        /// Доступно
        /// </summary>
        public int Available { get; set; }

        public string ContractorName
        {
            get { return Supplier == null ? String.Empty : Supplier.Code; }
        }

        public string Code
        {
            get { return Supplier == null ? Article : String.Format("{0}-{1}", Supplier.Code, Article); }
        }

        public virtual Contractor Supplier { get; set; }
        public virtual ICollection<ProductLine> Products { get; set; }
    }
}
