using System.Collections.Generic;

namespace SidBy.Sklad.Domain
{
    public class ContractorTag
    {
        public int ContractorTagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contractor> ContractorList { get; set; }
    }
}
