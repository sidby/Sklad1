using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Profiles { get; set; }
    }
}
