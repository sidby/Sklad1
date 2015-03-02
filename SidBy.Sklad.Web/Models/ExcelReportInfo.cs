using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SidBy.Sklad.Web.Models
{
    public class ExcelReportInfo
    {
        public string TitleLeft { get; set; }
        public string TitleCenter { get; set; }
        public string TitleRight { get; set; }
        public DateTime CreatedOf { get; set; }
        public string SheetName { get; set; }
        public string DocumentSubject { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        /// DateTime createdOf, string subject, string path, string fileName
    }
}