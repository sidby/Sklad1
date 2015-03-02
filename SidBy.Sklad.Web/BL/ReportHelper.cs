using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SidBy.Sklad.Domain;
using System.IO;
using SidBy.Common.Helpers;
using System.Collections;
using NPOI.SS.Util;
using SidBy.Sklad.Web.Models;

namespace SidBy.Sklad.Web.BL
{
    public static class ReportHelper
    {
       // static HSSFWorkbook hssfworkbook;

        private static ICellStyle CreateLeftBottomBorderStyle(HSSFWorkbook workbook)
        {
            ICellStyle tableStyle = workbook.CreateCellStyle();
            tableStyle.BorderBottom = BorderStyle.Hair;
            tableStyle.BottomBorderColor = (IndexedColors.Black.Index);
            tableStyle.BorderLeft = BorderStyle.Hair;
            tableStyle.LeftBorderColor = (IndexedColors.Black.Index);
            return tableStyle;
        }

        private static ICellStyle CreateTableStyle(HSSFWorkbook workbook, bool alignCenter, bool bold)
        {
            ICellStyle tableStyle = workbook.CreateCellStyle();
            if (alignCenter)
            {
                tableStyle.Alignment = HorizontalAlignment.Center;
                tableStyle.VerticalAlignment = VerticalAlignment.Top;
            }
            tableStyle.BorderRight = BorderStyle.Hair;
            tableStyle.RightBorderColor = (IndexedColors.Black.Index);
            tableStyle.BorderBottom = BorderStyle.Hair;
            tableStyle.BottomBorderColor = (IndexedColors.Black.Index);
            tableStyle.BorderLeft = BorderStyle.Hair;
            tableStyle.LeftBorderColor = (IndexedColors.Black.Index);
            tableStyle.BorderTop = BorderStyle.Hair;
            tableStyle.TopBorderColor = (IndexedColors.Black.Index);

            if (bold)
            {
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                tableStyle.SetFont(font);
            }

            return tableStyle;
        }

        private static SheetStyle InitializeWorkbookAndGenerateStandartHeader(ExcelReportInfo reportInfo)
        {
            SheetStyle result = new SheetStyle();
            result.Workbook = InitializeWorkbook(reportInfo.DocumentSubject);
            result.Sheet = result.Workbook.CreateSheet(reportInfo.SheetName);
            result.Sheet.SetColumnWidth(0, 5000);
            result.StyleBold = result.Workbook.CreateCellStyle();
            result.Font = result.Workbook.CreateFont();
            result.Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            result.StyleBold.SetFont(result.Font);
            result.StyleAlignCenter = CreateTableStyle(result.Workbook, true, false);
            result.StyleTableBold = CreateTableStyle(result.Workbook, true, true);
            result.StyleTable = CreateTableStyle(result.Workbook, true, false);
            result.StyleLeftBottom = CreateLeftBottomBorderStyle(result.Workbook);

            result.FirstRow = result.Sheet.CreateRow(0);

            if (!String.IsNullOrEmpty(reportInfo.TitleLeft))
            {
                ICell contractorCodeCell = result.FirstRow.CreateCell(0);
                contractorCodeCell.CellStyle = result.StyleBold;
                contractorCodeCell.SetCellValue(reportInfo.TitleLeft);
                contractorCodeCell.RichStringCellValue.ApplyFont(0, reportInfo.TitleLeft.Length, result.Font);
                contractorCodeCell.CellStyle = result.StyleBold;
            }

            if (!String.IsNullOrEmpty(reportInfo.TitleCenter))
            {
                ICell dateCreatedCell = result.FirstRow.CreateCell(1);
                dateCreatedCell.SetCellValue(reportInfo.TitleCenter);
            }

            if (!String.IsNullOrEmpty(reportInfo.TitleRight))
                result.FirstRow.CreateCell(3).SetCellValue(reportInfo.TitleRight);

            CreateHeader(result.Workbook, result.Sheet, 2);
            result.CurrentRowNum = 3;
            return result;
        }

        private static void GenerateDocumentInfoRow(Document document, SheetStyle style)
        {
            string documentInfo = "Док. №"  + document.Number + ". "+ document.Contractor.Code + " от " + document.CreatedOf.ToString("dd.MM.yyyy");
            IRow row = style.Sheet.CreateRow(style.CurrentRowNum);
            ICell contractorCodeCell = row.CreateCell(0);
            contractorCodeCell.CellStyle = style.StyleBold;
            contractorCodeCell.SetCellValue(documentInfo);
            contractorCodeCell.RichStringCellValue.ApplyFont(0, documentInfo.Length, style.Font);
            contractorCodeCell.CellStyle = style.StyleBold;

            style.CurrentRowNum += 1;
        }

        public static void GenerateContractorSalesReport(List<Document> documents, ExcelReportInfo reportInfo)
        {
            SheetStyle style = InitializeWorkbookAndGenerateStandartHeader(reportInfo);

            foreach (Document document in documents)
            {
                GenerateDocumentInfoRow(document, style);
                CreateProductLineTable(style, document.ProductsFilteredList, false);
                string pathToFile = Path.Combine(reportInfo.FilePath, reportInfo.FileName);
                WriteToFile(style.Workbook, pathToFile);
            }
        }

        private static void CreateProductLineTable(SheetStyle style, ICollection<ProductLine> products, bool showGrandTotal = true)
        {
            var priceQuery =
                from prod in products
                group prod by prod.SupplierId into grouping
                select new
                {
                    grouping.Key,
                    grouping,
                    Count = grouping.Count(),
                    TotalQuantity = grouping.Sum(p => p.Quantity),
                    TotalPurchasePrice = grouping.Sum(p => p.PurchasePrice),
                    TotalSalePrice = grouping.Sum(p => p.SalePrice),
                    TotalSum = grouping.Sum(p => p.Sum),
                    TotalSaleSum = grouping.Sum(p => p.SaleSum)
                };

           // int rowCounter = 3;

            double grandTotalSum = 0, grandTotalSaleSum = 0;
            int grandTotalQuantity = 0;

            foreach (var grp in priceQuery)
            {
                bool firstRow = true;
                int supplierRowId = style.CurrentRowNum;
                foreach (var gr in grp.grouping)
                {
                    //supplierId;
                    IRow rowData = style.Sheet.CreateRow(style.CurrentRowNum);
                    if (firstRow)
                    {
                        string supplierCode = "Фабрика не известна";
                        if (grp.Key != null)
                            supplierCode = products.Where(x => x.SupplierId == grp.Key).FirstOrDefault().SupplierCode;

                        ICell supplierCell = rowData.CreateCell(0);
                        supplierCell.SetCellValue(supplierCode);
                        supplierCell.CellStyle = style.StyleAlignCenter;

                        // sheet1.AddMergedRegion(new CellRangeAddress(rowCounter, grp.Count, 0, 0));
                    }
                    else
                    {
                        var cell = rowData.GetCell(0) ?? rowData.CreateCell(0);
                        cell.CellStyle = style.StyleAlignCenter;
                    }

                    ICell productArticleCell = rowData.CreateCell(1);
                    productArticleCell.SetCellValue(gr.ProductArticle);
                    productArticleCell.CellStyle = style.StyleTable;

                    ICell quantityCell = rowData.CreateCell(2);
                    quantityCell.SetCellValue(gr.Quantity);
                    quantityCell.CellStyle = style.StyleTable;

                    ICell purchasePriceCell = rowData.CreateCell(3);
                    purchasePriceCell.SetCellValue((double)gr.PurchasePrice);
                    purchasePriceCell.CellStyle = style.StyleTable;

                    ICell salePriceCell = rowData.CreateCell(4);
                    salePriceCell.SetCellValue((double)gr.SalePrice);
                    salePriceCell.CellStyle = style.StyleTable;

                    ICell sumCell = rowData.CreateCell(5);
                    sumCell.SetCellValue((double)gr.Sum);
                    sumCell.CellStyle = style.StyleTable;

                    ICell saleSumCell = rowData.CreateCell(6);
                    saleSumCell.SetCellValue((double)gr.SaleSum);
                    saleSumCell.CellStyle = style.StyleTable;

                    rowData.CreateCell(7).SetCellValue(gr.Comment);

                    style.CurrentRowNum++;
                    firstRow = false;
                }

                CellRangeAddress region = new CellRangeAddress(supplierRowId, grp.Count + supplierRowId, 0, 0);
                style.Sheet.AddMergedRegion(region);

                IRow rowTotal = style.Sheet.CreateRow(style.CurrentRowNum);

                ICell totalCell0 = rowTotal.GetCell(0) ?? rowTotal.CreateCell(0);
                totalCell0.CellStyle = style.StyleLeftBottom;

                ICell totalCell2 = rowTotal.CreateCell(1);
                totalCell2.CellStyle = style.StyleTableBold;

                grandTotalQuantity += grp.TotalQuantity;
                ICell quantityTotalCell = rowTotal.CreateCell(2);
                quantityTotalCell.SetCellValue(grp.TotalQuantity);
                quantityTotalCell.CellStyle = style.StyleTableBold;

                ICell totalCell3 = rowTotal.CreateCell(3);
                totalCell3.CellStyle = style.StyleTableBold;

                ICell totalCell4 = rowTotal.CreateCell(4);
                totalCell4.CellStyle = style.StyleTableBold;


                grandTotalSum += (double)grp.TotalSum;
                ICell sumTotalCell = rowTotal.CreateCell(5);
                sumTotalCell.SetCellValue((double)grp.TotalSum);
                sumTotalCell.CellStyle = style.StyleTableBold;

                grandTotalSaleSum += (double)grp.TotalSaleSum;
                ICell sumSaleTotalCell = rowTotal.CreateCell(6);
                sumSaleTotalCell.SetCellValue((double)grp.TotalSaleSum);
                sumSaleTotalCell.CellStyle = style.StyleTableBold;

                // итоги
                style.CurrentRowNum = style.CurrentRowNum + 2;
            }
            if (showGrandTotal)
            {
                // итоговые суммы
                IRow rowGrandTotal = style.Sheet.CreateRow(style.CurrentRowNum);
                ICell granTotalCell0 = rowGrandTotal.GetCell(0) ?? rowGrandTotal.CreateCell(0);
                granTotalCell0.SetCellValue("Итого:");
                granTotalCell0.CellStyle = style.StyleTableBold;

                ICell quantityGrandTotalCell = rowGrandTotal.CreateCell(2);
                quantityGrandTotalCell.SetCellValue(grandTotalQuantity);
                quantityGrandTotalCell.CellStyle = style.StyleTableBold;

                ICell sumGrandTotalCell = rowGrandTotal.CreateCell(5);
                sumGrandTotalCell.SetCellValue(grandTotalSum);
                sumGrandTotalCell.CellStyle = style.StyleTableBold;

                ICell sumGrandSaleTotalCell = rowGrandTotal.CreateCell(6);
                sumGrandSaleTotalCell.SetCellValue(grandTotalSaleSum);
                sumGrandSaleTotalCell.CellStyle = style.StyleTableBold;
            }
        }

        /// <summary>
        /// Отчёт по товарам
        /// </summary>
        /// <param name="products"></param>
        /// <param name="createdOf"></param>
        /// <param name="subject"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void GenerateProductLinesReport(ICollection<ProductLine> products, ExcelReportInfo reportInfo)
        {
            SheetStyle style = InitializeWorkbookAndGenerateStandartHeader(reportInfo);
            //InitializeWorkbook(reportInfo.DocumentSubject);

            //ISheet sheet1 = hssfworkbook.CreateSheet(reportInfo.SheetName);
            //sheet1.SetColumnWidth(0, 5000);

            //ICellStyle styleBold = hssfworkbook.CreateCellStyle();
            ////create a font style
            //IFont font = hssfworkbook.CreateFont();

            //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            //styleBold.SetFont(font);

            //ICellStyle styleAlignCenter = CreateTableStyle(true, false);
            //ICellStyle styleTableBold = CreateTableStyle(false, true);
            //ICellStyle styleTable = CreateTableStyle(false, false);
            //ICellStyle styleLeftBottom = CreateLeftBottomBorderStyle();

            //IRow row1 = sheet1.CreateRow(0);

            //if (!String.IsNullOrEmpty(reportInfo.TitleLeft))
            //{
            //    ICell contractorCodeCell = row1.CreateCell(0);
            //    contractorCodeCell.CellStyle = styleBold;
            //    contractorCodeCell.SetCellValue(reportInfo.TitleLeft);
            //    contractorCodeCell.RichStringCellValue.ApplyFont(0, reportInfo.TitleLeft.Length, font);
            //    contractorCodeCell.CellStyle = styleBold;
            //}

            //if (!String.IsNullOrEmpty(reportInfo.TitleCenter))
            //{
            //    ICell dateCreatedCell = row1.CreateCell(1);
            //    dateCreatedCell.SetCellValue(reportInfo.TitleCenter);
            //}

            //if (!String.IsNullOrEmpty(reportInfo.TitleRight))
            //    row1.CreateCell(3).SetCellValue(reportInfo.TitleRight);

            //CreateHeader(sheet1, 2);

           
            CreateProductLineTable(style, products);
            //var priceQuery =
            //    from prod in products
            //    group prod by prod.SupplierId into grouping
            //    select new
            //    {
            //        grouping.Key,
            //        grouping,
            //        Count = grouping.Count(),
            //        TotalQuantity = grouping.Sum(p => p.Quantity),
            //        TotalPurchasePrice = grouping.Sum(p => p.PurchasePrice),
            //        TotalSalePrice = grouping.Sum(p => p.SalePrice),
            //        TotalSum = grouping.Sum(p => p.Sum),
            //        TotalSaleSum = grouping.Sum(p => p.SaleSum)
            //    };

            ////int rowCounter = 3;

            //double grandTotalSum = 0, grandTotalSaleSum = 0;
            //int grandTotalQuantity = 0;

            //foreach (var grp in priceQuery)
            //{
            //    bool firstRow = true;
            //    int supplierRowId = rowCounter;
            //    foreach (var gr in grp.grouping)
            //    {
            //        //supplierId;
            //        IRow rowData = style.Sheet.CreateRow(rowCounter);
            //        if (firstRow)
            //        {
            //            string supplierCode = "Фабрика не известна";
            //            if (grp.Key != null)
            //                supplierCode = products.Where(x => x.SupplierId == grp.Key).FirstOrDefault().SupplierCode;

            //            ICell supplierCell = rowData.CreateCell(0);
            //            supplierCell.SetCellValue(supplierCode);
            //            supplierCell.CellStyle = style.StyleAlignCenter;

            //            // sheet1.AddMergedRegion(new CellRangeAddress(rowCounter, grp.Count, 0, 0));
            //        }
            //        else
            //        {
            //            var cell = rowData.GetCell(0) ?? rowData.CreateCell(0);
            //            cell.CellStyle = style.StyleAlignCenter;
            //        }

            //        ICell productArticleCell = rowData.CreateCell(1);
            //        productArticleCell.SetCellValue(gr.ProductArticle);
            //        productArticleCell.CellStyle = style.StyleTable;

            //        ICell quantityCell = rowData.CreateCell(2);
            //        quantityCell.SetCellValue(gr.Quantity);
            //        quantityCell.CellStyle = style.StyleTable;

            //        ICell purchasePriceCell = rowData.CreateCell(3);
            //        purchasePriceCell.SetCellValue((double)gr.PurchasePrice);
            //        purchasePriceCell.CellStyle = style.StyleTable;

            //        ICell salePriceCell = rowData.CreateCell(4);
            //        salePriceCell.SetCellValue((double)gr.SalePrice);
            //        salePriceCell.CellStyle = style.StyleTable;

            //        ICell sumCell = rowData.CreateCell(5);
            //        sumCell.SetCellValue((double)gr.Sum);
            //        sumCell.CellStyle = style.StyleTable;

            //        ICell saleSumCell = rowData.CreateCell(6);
            //        saleSumCell.SetCellValue((double)gr.SaleSum);
            //        saleSumCell.CellStyle = style.StyleTable;

            //        rowData.CreateCell(7).SetCellValue(gr.Comment);

            //        rowCounter++;
            //        firstRow = false;
            //    }

            //    CellRangeAddress region = new CellRangeAddress(supplierRowId, grp.Count + supplierRowId, 0, 0);
            //    style.Sheet.AddMergedRegion(region);

            //    IRow rowTotal = style.Sheet.CreateRow(rowCounter);

            //    ICell totalCell0 = rowTotal.GetCell(0) ?? rowTotal.CreateCell(0);
            //    totalCell0.CellStyle = style.StyleLeftBottom;

            //    ICell totalCell2 = rowTotal.CreateCell(1);
            //    totalCell2.CellStyle = style.StyleTableBold;

            //    grandTotalQuantity += grp.TotalQuantity;
            //    ICell quantityTotalCell = rowTotal.CreateCell(2);
            //    quantityTotalCell.SetCellValue(grp.TotalQuantity);
            //    quantityTotalCell.CellStyle = style.StyleTableBold;

            //    ICell totalCell3 = rowTotal.CreateCell(3);
            //    totalCell3.CellStyle = style.StyleTableBold;

            //    ICell totalCell4 = rowTotal.CreateCell(4);
            //    totalCell4.CellStyle = style.StyleTableBold;


            //    grandTotalSum += (double)grp.TotalSum;
            //    ICell sumTotalCell = rowTotal.CreateCell(5);
            //    sumTotalCell.SetCellValue((double)grp.TotalSum);
            //    sumTotalCell.CellStyle = style.StyleTableBold;

            //    grandTotalSaleSum += (double)grp.TotalSaleSum;
            //    ICell sumSaleTotalCell = rowTotal.CreateCell(6);
            //    sumSaleTotalCell.SetCellValue((double)grp.TotalSaleSum);
            //    sumSaleTotalCell.CellStyle = style.StyleTableBold;

            //    // итоги
            //    rowCounter = rowCounter + 2;
            //}

            //// итоговые суммы
            //IRow rowGrandTotal = style.Sheet.CreateRow(rowCounter);
            //ICell granTotalCell0 = rowGrandTotal.GetCell(0) ?? rowGrandTotal.CreateCell(0);
            //granTotalCell0.SetCellValue("Итого:");
            //granTotalCell0.CellStyle = style.StyleTableBold;

            //ICell quantityGrandTotalCell = rowGrandTotal.CreateCell(2);
            //quantityGrandTotalCell.SetCellValue(grandTotalQuantity);
            //quantityGrandTotalCell.CellStyle = style.StyleTableBold;

            //ICell sumGrandTotalCell = rowGrandTotal.CreateCell(5);
            //sumGrandTotalCell.SetCellValue(grandTotalSum);
            //sumGrandTotalCell.CellStyle = style.StyleTableBold;

            //ICell sumGrandSaleTotalCell = rowGrandTotal.CreateCell(6);
            //sumGrandSaleTotalCell.SetCellValue(grandTotalSaleSum);
            //sumGrandSaleTotalCell.CellStyle = style.StyleTableBold;

            //for (int i = 0; i <= rowCounter; i++)
            //{
            //    IRow row = sheet1.GetRow(i) ?? sheet1.CreateRow(i);
            //    var cell = row.GetCell(0) ?? row.CreateCell(0);
            //    cell.CellStyle = styleAlignCenter;

            //}

            string pathToFile = Path.Combine(reportInfo.FilePath, reportInfo.FileName);

            WriteToFile(style.Workbook,pathToFile);
        }

 

        private static void CreateHeader(HSSFWorkbook workbook, ISheet sheet, int rowId)
        {

            IRow rowHeader = sheet.CreateRow(rowId);
            rowHeader.Height = 40 * 20;
            ICellStyle style = CreateTableStyle(workbook, true, false);

            CreateApplyStyleToCell(rowHeader, 0, style, "Название");
            CreateApplyStyleToCell(rowHeader, 1, style, "Артикул");
            CreateApplyStyleToCell(rowHeader, 2, style, "Кол-во");
            CreateApplyStyleToCell(rowHeader, 3, style, "Зак. цена");
            CreateApplyStyleToCell(rowHeader, 4, style, "Цена");
            CreateApplyStyleToCell(rowHeader, 5, style, "Зак. сумма");
            CreateApplyStyleToCell(rowHeader, 6, style, "Сумма");

            sheet.CreateFreezePane(0, rowId + 1);
        }

        private static void CreateApplyStyleToCell(IRow row, int columnId, ICellStyle style, string value)
        {
            var cell = row.GetCell(columnId) ?? row.CreateCell(columnId);
            cell.SetCellValue(value);
            cell.CellStyle = style;
        }

        static HSSFWorkbook InitializeWorkbook(string subject)
        {
            var hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Трикотажный ряд";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = subject + " (sid.by - складской учёт)";
            hssfworkbook.SummaryInformation = si;

            return hssfworkbook;
        }

        static void WriteToFile(HSSFWorkbook hssfworkbook, string path)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
    }
}