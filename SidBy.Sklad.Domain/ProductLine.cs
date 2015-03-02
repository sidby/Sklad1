using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class ProductLine
    {
        public int ProductLineId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }
        /// <summary>
        /// Артикул товара
        /// </summary>
        public string ProductArticle { get; set; }
        /// <summary>
        /// Код поставщика
        /// </summary>
        public string SupplierCode { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Резерв
        /// </summary>
        public int Reserve { get; set; }
        /// <summary>
        /// Отгружено (отображается Заказ покупателя)
        /// </summary>
        public int Shipped { get; set; }
        /// <summary>
        /// Доступно
        /// </summary>
        public int Available { get; set; }
        /// <summary>
        /// Цена закупки
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// Торговая наценка
        /// </summary>
        public decimal MarginAbs { get; set; }

        /// <summary>
        /// Размер скидки
        /// </summary>
        public decimal Discount { get; set; }
        public decimal Sum { get; set; }
        public decimal SaleSum { get; set; }
        public decimal VAT { get; set; }
        public bool IsPriceIncludesVAT { get; set; }
        public int DocumentId { get; set; }
        public string Comment { get; set; }

        public virtual Document Document { get; set; }
        public virtual Product Product { get; set; }
        public virtual Contractor Supplier { get; set; }
    }
}
