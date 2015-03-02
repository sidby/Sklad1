using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Contractor> Contractors { get; set; }
    }
}
