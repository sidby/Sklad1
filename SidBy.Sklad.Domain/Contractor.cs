using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string ContactPersonName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ActualAddress { get; set; }
        public string Comment { get; set; }
        public int? ResponsibleId { get; set; }
        public bool IsArchived { get; set; }

        /// <summary>
        /// Торговая наценка
        /// </summary>
        public decimal MarginAbs { get; set; }
        //public int? RegionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ContractorTypeId { get; set; }

        public string Region { get; set; }

        public string ResponsibleName
        {
            get { return Responsible == null ? "" : Responsible.DisplayName; }
        }

        public string ContractorTypeName
        {
            get { return ContractorType == null ? "" : ContractorType.Name; }
        }

        public virtual ICollection<ContractorTag> Tags { get; set; }
        public virtual UserProfile Responsible { get; set; }
        public virtual ContractorType ContractorType { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductLine> ProductLines { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
