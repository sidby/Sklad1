using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using SidBy.Sklad.Domain;
using System.IO;
using System.Collections;
using System.Data;
using SidBy.Sklad.Domain.Enums;

namespace SidBy.Sklad.Web.BL
{
    public static class ExcelHelper
    {
        public static List<ProductLine> GetProductsData(string fileName, string fileExtension, string sheetName)
        {

            ISheet sheet;
            try
            {
                if (fileExtension.ToLower() == ".xls")
                {
                    HSSFWorkbook hssfwb;

                    // Create temp XLSX file
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        hssfwb = new HSSFWorkbook(fileStream);
                    }

                    sheet = hssfwb.GetSheet(sheetName);
                }
                else
                {
                    XSSFWorkbook hssfwb;
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        hssfwb = new XSSFWorkbook(fileStream);
                    }

                    sheet = hssfwb.GetSheet(sheetName);
                }

            }
            finally
            {
                // Make sure temp file is deleted
                File.Delete(fileName);
            }

            var productLines = new List<ProductLine>();

            if(sheet != null)
            {
               // DataTable dt = new DataTable();
              //  IRow headerRow = sheet.GetRow(0);
                IEnumerator rows = sheet.GetRowEnumerator();

                int rowCount = sheet.LastRowNum;
                IRow firstRow = null;

                // try to get first row
                for (int i=0; i < 10; i++)
                {
                    if (firstRow == null)
                        firstRow = sheet.GetRow(i);
                    else
                        break;
                }

                TemplateEnum.ExcelFileTypeEnum templateType = TemplateEnum.ExcelFileTypeEnum.Type1;

                int colCount = firstRow.LastCellNum;

                for (int c = 0; c < colCount; c++)
                {
                    ICell cell = firstRow.GetCell(c);
                    if(cell != null)
                    {
                        if (cell.ToString().Contains("Код фабрики"))
                        {
                            templateType = TemplateEnum.ExcelFileTypeEnum.Type1;
                            break;
                        }
                        else
                        {
                            templateType = TemplateEnum.ExcelFileTypeEnum.Type2;
                            break;
                        }
                    }
                }

                bool skipReadingHeaderRow = rows.MoveNext();

                if (templateType == TemplateEnum.ExcelFileTypeEnum.Type1)
                {
                    bool readSupplierRow = rows.MoveNext();
                    IRow rowSupplier = (XSSFRow)rows.Current;
                    ICell supplierCell = rowSupplier.GetCell(1);

                    string supplierName = supplierCell.ToString();
                    skipReadingHeaderRow = rows.MoveNext();
                    while (rows.MoveNext())
                    {
                        IRow row = (XSSFRow)rows.Current;

                        ICell cell1 = row.GetCell(1);
                        if (cell1 != null)
                        {
                            if (String.IsNullOrEmpty(cell1.ToString()))
                                // all rows was read
                                break;
                            else
                            {
                                int quantity = 0;
                                if (row.GetCell(2) != null)
                                    Int32.TryParse(row.GetCell(2).ToString(), out quantity);

                                decimal price = 0;
                                if (row.GetCell(3) != null)
                                {
                                    string priceStr = row.GetCell(3).ToString().Trim().Replace('.', ',');
                                    if(!String.IsNullOrEmpty(priceStr))
                                        price = Convert.ToDecimal(priceStr);
                                }

                                decimal discount = 0;
                                if (row.GetCell(4) != null)
                                {
                                    string discountStr = row.GetCell(4).ToString().Trim().Replace('.', ',');
                                    if (!String.IsNullOrEmpty(discountStr))
                                        discount = Convert.ToDecimal(discountStr);
                                }

                                productLines.Add(
                                 new ProductLine {
                                  ProductArticle = cell1.ToString().Trim(),
                                  Quantity = quantity,
                                  PurchasePrice = price,
                                  Discount = discount,
                                  Comment = (row.GetCell(5) == null ? String.Empty: row.GetCell(5).ToString().Trim()),
                                  SupplierCode = supplierName
                                 }
                                );
                            }
                        }
                    }
                }
                else if (templateType == TemplateEnum.ExcelFileTypeEnum.Type2)
                {
                    int emptyRowLimit = 0;

                    Stack<ProductLine> tempProducts = new Stack<ProductLine>();
                    string supplierName = String.Empty;
                    skipReadingHeaderRow = rows.MoveNext();
                    skipReadingHeaderRow = rows.MoveNext();
                    while (rows.MoveNext())
                    {
                        IRow row = (XSSFRow)rows.Current;

                        ICell productArticleCell = row.GetCell(1);
                        if (productArticleCell != null)
                        {
                            if (!String.IsNullOrEmpty(productArticleCell.ToString().Trim()))
                            {
                                emptyRowLimit = 0;
                                // check supplierName
                                ICell supplierNameCell = row.GetCell(0);
                                if (supplierNameCell != null)
                                {
                                    if (!String.IsNullOrEmpty(supplierNameCell.ToString()))
                                        supplierName = supplierNameCell.ToString();
                                }

                                int quantity = GetIntCellValue(row.GetCell(2));
                              
                                // Закупочная цена
                                decimal price = GetDecimalCellValue(row.GetCell(3));

                                // Цена
                                decimal salePrice = GetDecimalCellValue(row.GetCell(4));
                                
                                decimal discount = GetDecimalCellValue(row.GetCell(7));

                                tempProducts.Push(
                                    new ProductLine {
                                        ProductArticle = productArticleCell.ToString().Trim(),
                                        Quantity = quantity,
                                        MarginAbs = salePrice -price,
                                        PurchasePrice = price,
                                        Discount = discount,
                                        SalePrice = salePrice
                                    });
                            }
                            else
                            { 
                                // dump products from stack
                                while(tempProducts.Count > 0)
                                {
                                    var tempProduct = tempProducts.Pop();
                                    productLines.Add(
                                        new ProductLine { 
                                         SupplierCode = supplierName,
                                         ProductArticle = tempProduct.ProductArticle,
                                         Quantity = tempProduct.Quantity,
                                         Discount = tempProduct.Discount,
                                         SalePrice = tempProduct.SalePrice,
                                         PurchasePrice = tempProduct.PurchasePrice,
                                         MarginAbs = tempProduct.MarginAbs
                                        }
                                    );
                                }

                                // empty row
                                if (emptyRowLimit > 3)
                                    break;
                                else
                                {
                                    supplierName = String.Empty;
                                    emptyRowLimit++;
                                }
                            }
                        }
                    }
                
                }

                sheet = null;

                return productLines;
            }
            else
                return null;
        }

        private static int GetIntCellValue(ICell cell)
        {
            int val = 0;
            if (cell != null)
            {
                if (cell.CellType == CellType.Numeric || cell.CellType == CellType.Formula)
                {
                    double numVal = cell.NumericCellValue;
                    val = Convert.ToInt32(Math.Round(numVal, 0));
                }
                else if (cell.CellType == CellType.String)
                {
                    System.Int32.TryParse(cell.StringCellValue, out val);
                }
            }

            return val;
        }

        private static decimal GetDecimalCellValue(ICell cell)
        {
            decimal val = 0;
            if (cell != null)
            {
                if (cell.CellType == CellType.Numeric || cell.CellType ==  CellType.Formula)
                {
                    double numVal = cell.NumericCellValue;
                    val = Convert.ToDecimal(Math.Round(numVal, 2));
                }
                else if (cell.CellType == CellType.String)
                {
                    System.Decimal.TryParse(cell.StringCellValue, out val);
                    val = Math.Round(val, 2);
                }
            }

            return val;
        }

        /// <summary>
        /// Translate Excel column letter to number,
        /// e.g. C to 3 (R3)
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int ExcelColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }
    }
}