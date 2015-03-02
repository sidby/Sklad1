using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidBy.Sklad.Domain
{
    public class DocumentCounter
    {
        public int DocumentCounterId { get; set; }
        public int Counter { get; set; }
        public int Year { get; set; }
        public int DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}
