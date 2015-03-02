using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentCounter> DocumentCounters { get; set; }
    }
}
