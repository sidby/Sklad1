using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain.Models
{
    public class SalesReportContractorGridModel
    {
        public List<SalesReportContractorModel> Data { get; set; }

        public List<ContractorGridSimpleModel> Clients { get; set; }
        public List<ContractorGridSimpleModel> Factories { get; set; }

        public decimal RefundsGrandTotal { get; set; }
        public decimal ShipmentGrandTotal { get; set; }

        public int RefundsQuantityGrandTotal { get; set; }
        public int ShipmentQuantityGrandTotal { get; set; }
    }
}
