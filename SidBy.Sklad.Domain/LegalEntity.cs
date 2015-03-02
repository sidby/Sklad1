using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SidBy.Sklad.Domain
{
    public class LegalEntity
    {
        public int LegalEntityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        //[EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(ValidationStrings))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsVATPayer { get; set; }
        public string ActualAddress { get; set; }
        public string Comment { get; set; }
        public string Director { get; set; }
        public string ChiefAccountant { get; set; }

        public virtual ICollection<UserProfile> Employees { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
