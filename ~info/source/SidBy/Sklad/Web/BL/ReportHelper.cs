namespace SidBy.Sklad.Web.BL
{
    using NPOI.HPSF;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using SidBy.Sklad.Domain;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class ReportHelper
    {
        private static HSSFWorkbook hssfworkbook;

        private static void CreateApplyStyleToCell(IRow row, int columnId, ICellStyle style, string value)
        {
            ICell cell = row.GetCell(columnId) ?? row.CreateCell(columnId);
            cell.SetCellValue(value);
            cell.set_CellStyle(style);
        }

        private static void CreateHeader(ISheet sheet, int rowId)
        {
            IRow row = sheet.CreateRow(rowId);
            row.set_Height(800);
            ICellStyle style = CreateTableStyle(true, false);
            CreateApplyStyleToCell(row, 0, style, "Название");
            CreateApplyStyleToCell(row, 1, style, "Артикул");
            CreateApplyStyleToCell(row, 2, style, "Кол-во");
            CreateApplyStyleToCell(row, 3, style, "Зак. цена");
            CreateApplyStyleToCell(row, 4, style, "Цена");
            CreateApplyStyleToCell(row, 5, style, "Зак. сумма");
            CreateApplyStyleToCell(row, 6, style, "Сумма");
            sheet.CreateFreezePane(0, rowId + 1);
        }

        private static ICellStyle CreateLeftBottomBorderStyle()
        {
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.set_BorderBottom(7);
            style.set_BottomBorderColor(IndexedColors.Black.get_Index());
            style.set_BorderLeft(7);
            style.set_LeftBorderColor(IndexedColors.Black.get_Index());
            return style;
        }

        private static ICellStyle CreateTableStyle(bool alignCenter, bool bold)
        {
            ICellStyle style = hssfworkbook.CreateCellStyle();
            if (alignCenter)
            {
                style.set_Alignment(2);
                style.set_VerticalAlignment(1);
            }
            style.set_BorderRight(7);
            style.set_RightBorderColor(IndexedColors.Black.get_Index());
            style.set_BorderBottom(7);
            style.set_BottomBorderColor(IndexedColors.Black.get_Index());
            style.set_BorderLeft(7);
            style.set_LeftBorderColor(IndexedColors.Black.get_Index());
            style.set_BorderTop(7);
            style.set_TopBorderColor(IndexedColors.Black.get_Index());
            if (bold)
            {
                IFont font = hssfworkbook.CreateFont();
                font.set_Boldweight(700);
                style.SetFont(font);
            }
            return style;
        }

        public static void GenerateSalesReport(Document document, string path, string fileName)
        {
            InitializeWorkbook(string.Concat(new object[] { "Отчёт №", document.get_Number(), " от ", document.get_CreatedOf() }));
            ISheet sheet = hssfworkbook.CreateSheet("Report-" + document.get_CreatedOf().ToString("dd.MM.yyyy"));
            sheet.SetColumnWidth(0, 0x1388);
            ICellStyle style = hssfworkbook.CreateCellStyle();
            IFont font = hssfworkbook.CreateFont();
            font.set_Boldweight(700);
            style.SetFont(font);
            ICellStyle style2 = CreateTableStyle(true, false);
            ICellStyle style3 = CreateTableStyle(false, true);
            ICellStyle style4 = CreateTableStyle(false, false);
            ICellStyle style5 = CreateLeftBottomBorderStyle();
            IRow row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.set_CellStyle(style);
            cell.SetCellValue(document.get_Contractor().get_Code());
            cell.get_RichStringCellValue().ApplyFont(0, document.get_Contractor().get_Code().Length, font);
            row.CreateCell(1).SetCellValue(document.get_CreatedOf().ToString("dd.MM.yyyy"));
            cell.set_CellStyle(style);
            row.CreateCell(3).SetCellValue("Док. №" + document.get_Number() + " от " + document.get_CreatedOf().ToString("dd.MM.yyyy"));
            CreateHeader(sheet, 2);
            var enumerable = from prod in document.get_Products()
                group prod by prod.get_SupplierId() into grouping
                select new { Key = grouping.Key, grouping = grouping, Count = grouping.Count<ProductLine>(), TotalQuantity = grouping.Sum<ProductLine>((Func<ProductLine, int>) (p => p.get_Quantity())), TotalPurchasePrice = grouping.Sum<ProductLine>((Func<ProductLine, decimal>) (p => p.get_PurchasePrice())), TotalSalePrice = grouping.Sum<ProductLine>((Func<ProductLine, decimal>) (p => p.get_SalePrice())), TotalSum = grouping.Sum<ProductLine>((Func<ProductLine, decimal>) (p => p.get_Sum())), TotalSaleSum = grouping.Sum<ProductLine>((Func<ProductLine, decimal>) (p => p.get_SaleSum())) };
            int num = 3;
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<ProductLine, bool> predicate = null;
                    var grp = enumerator.Current;
                    bool flag = true;
                    int num2 = num;
                    foreach (ProductLine line in grp.grouping)
                    {
                        IRow row2 = sheet.CreateRow(num);
                        if (flag)
                        {
                            string str = "Фабрика не известна";
                            if (grp.Key.HasValue)
                            {
                                if (predicate == null)
                                {
                                    predicate = x => x.get_SupplierId() == grp.Key;
                                }
                                str = document.get_Products().Where<ProductLine>(predicate).FirstOrDefault<ProductLine>().get_SupplierCode();
                            }
                            ICell cell3 = row2.CreateCell(0);
                            cell3.SetCellValue(str);
                            cell3.set_CellStyle(style2);
                        }
                        else
                        {
                            (row2.GetCell(0) ?? row2.CreateCell(0)).set_CellStyle(style2);
                        }
                        ICell cell5 = row2.CreateCell(1);
                        cell5.SetCellValue(line.get_ProductArticle());
                        cell5.set_CellStyle(style4);
                        ICell cell6 = row2.CreateCell(2);
                        cell6.SetCellValue((double) line.get_Quantity());
                        cell6.set_CellStyle(style4);
                        ICell cell7 = row2.CreateCell(3);
                        cell7.SetCellValue((double) line.get_PurchasePrice());
                        cell7.set_CellStyle(style4);
                        ICell cell8 = row2.CreateCell(4);
                        cell8.SetCellValue((double) line.get_SalePrice());
                        cell8.set_CellStyle(style4);
                        ICell cell9 = row2.CreateCell(5);
                        cell9.SetCellValue((double) line.get_Sum());
                        cell9.set_CellStyle(style4);
                        ICell cell10 = row2.CreateCell(6);
                        cell10.SetCellValue((double) line.get_SaleSum());
                        cell10.set_CellStyle(style4);
                        row2.CreateCell(7).SetCellValue(line.get_Comment());
                        num++;
                        flag = false;
                    }
                    CellRangeAddress address = new CellRangeAddress(num2, grp.Count + num2, 0, 0);
                    sheet.AddMergedRegion(address);
                    IRow row3 = sheet.CreateRow(num);
                    (row3.GetCell(0) ?? row3.CreateCell(0)).set_CellStyle(style5);
                    row3.CreateCell(1).set_CellStyle(style3);
                    ICell cell13 = row3.CreateCell(2);
                    cell13.SetCellValue((double) grp.TotalQuantity);
                    cell13.set_CellStyle(style3);
                    row3.CreateCell(3).set_CellStyle(style3);
                    row3.CreateCell(4).set_CellStyle(style3);
                    ICell cell16 = row3.CreateCell(5);
                    cell16.SetCellValue((double) grp.TotalSum);
                    cell16.set_CellStyle(style3);
                    ICell cell17 = row3.CreateCell(6);
                    cell17.SetCellValue((double) grp.TotalSaleSum);
                    cell17.set_CellStyle(style3);
                    num += 2;
                }
            }
            WriteToFile(Path.Combine(path, fileName));
        }

        private static void InitializeWorkbook(string subject)
        {
            hssfworkbook = new HSSFWorkbook();
            DocumentSummaryInformation information = PropertySetFactory.CreateDocumentSummaryInformation();
            information.set_Company("Трикотажный ряд");
            hssfworkbook.set_DocumentSummaryInformation(information);
            SummaryInformation information2 = PropertySetFactory.CreateSummaryInformation();
            information2.set_Subject(subject + " (sid.by - складской учёт)");
            hssfworkbook.set_SummaryInformation(information2);
        }

        private static void WriteToFile(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(stream);
            stream.Close();
        }
    }
}

