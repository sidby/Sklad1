using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportContractorModel
    {
        public ContractorGridSimpleModel Client { get; set; }
        public ContractorGridSimpleModel Factory { get; set; }
        public decimal Shipment { get; set; }
        public decimal Refunds { get; set; }

        public int QuantityShipment { get; set; }
        public int QuantityRefunds { get; set; }
    }
}
