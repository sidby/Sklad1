using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class Contract
    {
        public int ContractId { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOf { get; set; }
        public int ContractorId { get; set; }
        public int LegalEntityId { get; set; }
        public decimal Sum { get; set; }
        /// <summary>
        /// Оплачено
        /// </summary>
        public decimal Paid { get; set; }
        /// <summary>
        /// Выполнено
        /// </summary>
        public decimal CarriedOut { get; set; }

        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
