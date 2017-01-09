using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SidBy.Sklad.Web.Models
{
    public class BackupFileModel
    {
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}