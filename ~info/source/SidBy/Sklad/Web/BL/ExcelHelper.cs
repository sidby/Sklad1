namespace SidBy.Sklad.Web.BL
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Domain.Enums;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public static class ExcelHelper
    {
        public static int ExcelColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            columnName = columnName.ToUpperInvariant();
            int num = 0;
            for (int i = 0; i < columnName.Length; i++)
            {
                num *= 0x1a;
                num += (columnName[i] - 'A') + 1;
            }
            return num;
        }

        public static List<ProductLine> GetProductsData(string fileName, string fileExtension, string sheetName)
        {
            ISheet sheet;
            string str;
            IRow row3;
            int num5;
            decimal num6;
            decimal num7;
            string str3;
            try
            {
                FileStream stream;
                if (fileExtension.ToLower() == ".xls")
                {
                    HSSFWorkbook workbook;
                    using (stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        workbook = new HSSFWorkbook(stream);
                    }
                    sheet = workbook.GetSheet(sheetName);
                }
                else
                {
                    XSSFWorkbook workbook2;
                    using (stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        workbook2 = new XSSFWorkbook(stream);
                    }
                    sheet = workbook2.GetSheet(sheetName);
                }
            }
            finally
            {
                File.Delete(fileName);
            }
            List<ProductLine> list = new List<ProductLine>();
            if (sheet == null)
            {
                return null;
            }
            IEnumerator rowEnumerator = sheet.GetRowEnumerator();
            int num = sheet.get_LastRowNum();
            IRow row = null;
            for (int i = 0; i < 10; i++)
            {
                if (row != null)
                {
                    break;
                }
                row = sheet.GetRow(i);
            }
            TemplateEnum.ExcelFileTypeEnum enum2 = 1;
            int num3 = row.get_LastCellNum();
            for (int j = 0; j < num3; j++)
            {
                ICell cell = row.GetCell(j);
                if (cell != null)
                {
                    if (cell.ToString().Contains("Код фабрики"))
                    {
                        enum2 = 1;
                    }
                    else
                    {
                        enum2 = 2;
                    }
                    break;
                }
            }
            bool flag = rowEnumerator.MoveNext();
            if (enum2 == 1)
            {
                bool flag2 = rowEnumerator.MoveNext();
                IRow current = (XSSFRow) rowEnumerator.Current;
                str = current.GetCell(1).ToString();
                flag = rowEnumerator.MoveNext();
                while (rowEnumerator.MoveNext())
                {
                    row3 = (XSSFRow) rowEnumerator.Current;
                    ICell cell3 = row3.GetCell(1);
                    if (cell3 != null)
                    {
                        if (string.IsNullOrEmpty(cell3.ToString()))
                        {
                            break;
                        }
                        num5 = 0;
                        if (row3.GetCell(2) != null)
                        {
                            int.TryParse(row3.GetCell(2).ToString(), out num5);
                        }
                        num6 = 0M;
                        if (row3.GetCell(3) != null)
                        {
                            string str2 = row3.GetCell(3).ToString().Trim().Replace('.', ',');
                            if (!string.IsNullOrEmpty(str2))
                            {
                                num6 = Convert.ToDecimal(str2);
                            }
                        }
                        num7 = 0M;
                        if (row3.GetCell(4) != null)
                        {
                            str3 = row3.GetCell(4).ToString().Trim().Replace('.', ',');
                            if (!string.IsNullOrEmpty(str3))
                            {
                                num7 = Convert.ToDecimal(str3);
                            }
                        }
                        ProductLine item = new ProductLine();
                        item.set_ProductArticle(cell3.ToString().Trim());
                        item.set_Quantity(num5);
                        item.set_PurchasePrice(num6);
                        item.set_Discount(num7);
                        item.set_Comment((row3.GetCell(5) == null) ? string.Empty : row3.GetCell(5).ToString().Trim());
                        item.set_SupplierCode(str);
                        list.Add(item);
                    }
                }
            }
            else if (enum2 == 2)
            {
                int num8 = 0;
                Stack<ProductLine> stack = new Stack<ProductLine>();
                str = string.Empty;
                flag = rowEnumerator.MoveNext();
                flag = rowEnumerator.MoveNext();
                while (rowEnumerator.MoveNext())
                {
                    ProductLine line3;
                    row3 = (XSSFRow) rowEnumerator.Current;
                    ICell cell4 = row3.GetCell(1);
                    if (cell4 != null)
                    {
                        if (string.IsNullOrEmpty(cell4.ToString().Trim()))
                        {
                            goto Label_05A7;
                        }
                        num8 = 0;
                        ICell cell5 = row3.GetCell(0);
                        if ((cell5 != null) && !string.IsNullOrEmpty(cell5.ToString()))
                        {
                            str = cell5.ToString();
                        }
                        num5 = 0;
                        if (row3.GetCell(2) != null)
                        {
                            int.TryParse(row3.GetCell(2).ToString(), out num5);
                        }
                        num6 = 0M;
                        if (row3.GetCell(3) != null)
                        {
                            num6 = Convert.ToDecimal(Math.Round(row3.GetCell(3).get_NumericCellValue(), 2));
                        }
                        num7 = 0M;
                        if (row3.GetCell(4) != null)
                        {
                            str3 = row3.GetCell(4).ToString().Trim().Replace('.', ',');
                            if (!string.IsNullOrEmpty(str3))
                            {
                                string[] strArray = str3.Split(new char[] { '+' });
                                if ((strArray != null) && (strArray.GetLength(0) > 1))
                                {
                                    num7 = Convert.ToDecimal(strArray[1]);
                                }
                            }
                        }
                        ProductLine line2 = new ProductLine();
                        line2.set_ProductArticle(cell4.ToString().Trim());
                        line2.set_Quantity(num5);
                        line2.set_MarginAbs(num7);
                        line2.set_PurchasePrice(num6);
                        line2.set_SalePrice(num6 + num7);
                        stack.Push(line2);
                    }
                    continue;
                Label_0536:
                    line3 = stack.Pop();
                    ProductLine line4 = new ProductLine();
                    line4.set_SupplierCode(str);
                    line4.set_ProductArticle(line3.get_ProductArticle());
                    line4.set_Quantity(line3.get_Quantity());
                    line4.set_SalePrice(line3.get_SalePrice());
                    line4.set_PurchasePrice(line3.get_PurchasePrice());
                    line4.set_MarginAbs(line3.get_MarginAbs());
                    list.Add(line4);
                Label_05A7:
                    if (stack.Count > 0)
                    {
                        goto Label_0536;
                    }
                    if (num8 > 3)
                    {
                        break;
                    }
                    str = string.Empty;
                    num8++;
                }
            }
            sheet = null;
            return list;
        }
    }
}

