using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace SidBy.Sklad.Web.Models
{
    public class SheetStyle
    {
        public HSSFWorkbook Workbook { get; set; }
        public ISheet Sheet { get; set; }
        public ICellStyle StyleBold { get; set; }
        public IFont Font { get; set; }
        public ICellStyle StyleAlignCenter { get; set; }
        public ICellStyle StyleTableBold { get; set; }
        public ICellStyle StyleTable { get; set; }
        public ICellStyle StyleLeftBottom { get; set; }
        public IRow FirstRow { get; set; }
        public int CurrentRowNum { get; set; }
    }
}