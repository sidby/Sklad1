using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SidBy.Sklad.Web.Models
{
    public class ProductArticlePriceModel
    {
        public int ProductId { get; set; }

        public string Article { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}