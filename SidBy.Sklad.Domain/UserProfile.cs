using System.Collections.Generic;

namespace SidBy.Sklad.Domain
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string UserEmail { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }
        
        public int? ContactTypeId { get; set; }

        public int? LegalEntityId { get; set; }

        public string NewPassword { get; set; }

        public string ContactTypeName
        {
            get { return Type == null ? "" : Type.Name; }
        }

        public virtual ContactType Type { get; set; }

        public virtual LegalEntity Job { get; set; }

        public virtual ICollection<Contractor> ResponsibleContractors { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
