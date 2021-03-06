﻿using log4net;
using SidBy.Sklad.DataAccess;
using SidBy.Sklad.Domain;
using SidBy.Sklad.Domain.Enums;
using SidBy.Sklad.Web.BL;
using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;

namespace SidBy.Sklad.Web.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "admin,employee")]
    public class SaleController : Controller
    {
        private readonly ILog logger;

        public SaleController()
        {
            logger = LogManager.GetLogger(GetType());
        }

        #region CustomerOrders

        public ActionResult CustomerOrders()
        {
            ViewBag.Title = "Заказы покупателя";
            // Get the model (setup) of the grid defined in the /Models folder.
            var gridModel = new SkladJqGridModel();
            var grid = gridModel.DocumentGrid;

            //// NOTE: you need to call this method in the action that fetches the data as well,
            //// so that the models match
            CustomerOrdersSetupGrid(grid);
            /*
            public int? ContractId { get; set; }
            public int? EmployeeId { get; set; } 
             */

            // Pass the custmomized grid model to the View
            return View(gridModel);
        }

        // This method is called when the grid requests data
        public JsonResult CustomerOrdersSearchGridDataRequested()
        {
            //// Get both the grid Model and the data Model
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();
            // customize the default grid model with our custom settings
            CustomerOrdersSetupGrid(gridModel.DocumentGrid);
            return GetDocumentList(gridModel, datacontextModel, EntityEnum.DocumentTypeEnum.CustomerOrders);
        }

        private JsonResult GetDocumentList(SkladJqGridModel gridModel, 
            SkladDataContext datacontextModel, EntityEnum.DocumentTypeEnum docType)
        {
            string contractorIdStr = Request.QueryString["ContractorId"];
            if (!String.IsNullOrEmpty(contractorIdStr))
            {
                var contractor = datacontextModel.Contractors.Where(x =>
                    x.Code.ToLower() == contractorIdStr.ToLower()).FirstOrDefault();
                if (contractor != null)
                {
                    // ContractorName
                    return gridModel.DocumentGrid.DataBind(datacontextModel
                        .Documents.Include("Contractor").Where(x => x.ContractorId == contractor.ContractorId && 
                           x.DocumentTypeId == (int)docType));
                }
            }

            return gridModel.DocumentGrid.DataBind(datacontextModel.Documents.Include("Contractor").Where(x =>
                x.DocumentTypeId == (int)docType));
        }

        private void CustomerOrdersSetupGrid(JQGrid grid)
        {
            SetUpGrid(grid, "CustomerOrdersGrid1",
                Url.Action("CustomerOrdersSearchGridDataRequested"),
                Url.Action("DocumentItemEditRows", "Home"));

            SetUpContractorEmployeeEditDropDown(grid);
            // +планируемая дата отгрузки
        }

        #endregion

        #region Shipment

        public ActionResult Shipment()
        {
            ViewBag.Title = "Отгрузки";
            // Get the model (setup) of the grid defined in the /Models folder.
            var gridModel = new SkladJqGridModel();
            var grid = gridModel.DocumentGrid;

            //// NOTE: you need to call this method in the action that fetches the data as well,
            //// so that the models match
            ShipmentSetupGrid(grid);

            // Pass the custmomized grid model to the View
            return View(gridModel);
        }

        // This method is called when the grid requests data
        public JsonResult ShipmentSearchGridDataRequested()
        {
            // Get both the grid Model and the data Model
            // The data model in our case is an autogenerated linq2sql database based on Northwind.
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();
            // customize the default grid model with our custom settings
            ShipmentSetupGrid(gridModel.DocumentGrid);

            return GetDocumentList(gridModel, datacontextModel, EntityEnum.DocumentTypeEnum.Shipment);
        }

        private void ShipmentSetupGrid(JQGrid grid)
        {
            SetUpGrid(grid, "ShipmentGrid1",
                Url.Action("ShipmentSearchGridDataRequested"),
                Url.Action("DocumentItemEditRows", "Home"));
            SetUpContractorEmployeeEditDropDown(grid);
        }

        #endregion

        #region Refunds

        public ActionResult Refunds()
        {
            ViewBag.Title = "Возвраты покупателя";
            // Get the model (setup) of the grid defined in the /Models folder.
            var gridModel = new SkladJqGridModel();
            var grid = gridModel.DocumentGrid;

            //// NOTE: you need to call this method in the action that fetches the data as well,
            //// so that the models match
            RefundsSetupGrid(grid);

            // Создать grid со списком товаров с возвратом за определённый день
            // Можно сделать просто полный список всех товаров из проведённых
            // документов возвратов
            var gridProducts = gridModel.ProductLineGrid;
            RefundsProductSetupGrid(gridProducts);
            ViewBag.GridProducts = gridProducts;

            // Pass the custmomized grid model to the View
            return View(gridModel);
        }

        private void RefundsProductSetupGrid(JQGrid grid)
        {
            SetUpGrid(grid, "RefundsProductGrid1",
                Url.Action("RefundsProductSearchGridDataRequested"),
                Url.Action("DocumentEditRows", "Home"));

            grid.ToolBarSettings.ShowAddButton = false;
    
            SetUpRefundsProductLineColumns(grid);
            SetUpRefundsProductLineGroupingGrandTotal(grid);
        }

        private List<ProductLine> FilterProductLinesByManager(SkladDataContext context, int managerId, List<ProductLine> prods)
        {
            var filteredProducts = new List<ProductLine>();
            foreach (var product in prods)
            { 
                var factory = context.Contractors.Where(x => x.Code.ToLower() == product.SupplierCode.ToLower()).FirstOrDefault();
                // failed to find factory. In this case - dont miss product
                if (factory == null)
                    filteredProducts.Add(product);
                else if(factory.ResponsibleId == managerId)
                    filteredProducts.Add(product);
            }

            return filteredProducts;
        }

        private IQueryable<ProductLine> GetRefundsProductLines(SkladDataContext context, DateTime day, int? contractorId, int? managerId)
        {
            List<Document> docs = null;
            if (contractorId.HasValue && contractorId.Value > 0)
            {
                docs = context.Documents
                .Where(x => x.CreatedOf == day && x.IsCommitted == true && x.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds
                && x.ContractorId == contractorId.Value).ToList();
            }
            else {
                docs = context.Documents
                  .Where(x => x.CreatedOf == day && x.IsCommitted == true && x.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds).ToList();
            }
            if (docs != null)
            {
                if (docs.Count > 0)
                {
                    var docIds = docs.Select(x => x.DocumentId).ToArray();
                    var products = context.ProductLines.Where(x => docIds.Contains(x.DocumentId));

                    if (managerId.HasValue && managerId.Value > 0)
                        products = FilterProductLinesByManager(context, managerId.Value, products.ToList()).AsQueryable();
                    if (contractorId.HasValue && contractorId.Value > 0)
                        return products;
                    else
                    {
                        // в комментариях указать клиента
                        foreach (var product in products)
                        {
                            var docsP = context.Documents.Where(x => x.DocumentId == product.DocumentId).FirstOrDefault();
                            if (docsP != null) {
                                product.Comment = "Возврат от: " + docsP.Contractor.Code + ". " + product.Comment;
                            }
                        }

                        return products.AsQueryable();
                    }
                }
            }

            return new List<ProductLine>().AsQueryable();
        }

        public JsonResult RefundsProductSearchGridDataRequested(DateTime? refundsReportDate)
        {
            if (!refundsReportDate.HasValue)
                refundsReportDate = DateTime.Now;
        
            // Get both the grid Model and the data Model
            // The data model in our case is an autogenerated linq2sql database based on Northwind.
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();
            SetUpRefundsProductLineColumns(gridModel.ProductLineGrid);
            SetUpRefundsProductLineGroupingGrandTotal(gridModel.ProductLineGrid);
            // customize the default grid model with our custom settings
            //JQGridState gridState = gridModel.ProductLineGrid.GetState();

            // Need this to enable ExportToExcelRefundsByDay
            // Session["refundsProductLinesGridState"] = gridState;
            int contractorId = 0;
            if (Request.Params["contractorId"] != null)
                Int32.TryParse(Request.Params["contractorId"].ToString(), out contractorId);

            int managerId = 0;
            if (Request.Params["managerId"] != null)
                Int32.TryParse(Request.Params["managerId"].ToString(), out managerId);

            return gridModel.ProductLineGrid.DataBind(GetRefundsProductLines(datacontextModel, refundsReportDate.Value, contractorId, managerId));
        }

        public string GetRefundsReportFileLink(DateTime refundsReportDate, int contractorId, int managerId)
        {
            string relativePath = "../" + Constants.RefundsReportPath;
            string rootPath = Server.MapPath(relativePath);
            string reportFile = String.Empty;
             
            // Check if file already exists. 
            // Than check Modified date of the file in file name
            string docPath = Path.Combine(rootPath, contractorId.ToString(), refundsReportDate.ToString("yyyy-MM-dd"));

            if(!Directory.Exists(docPath))
                Directory.CreateDirectory(docPath);

            string dateSeparator = "-";
            string extension = ".xls";
            string mask = String.Format("{0}*{1}", Constants.RefundsReportPrefix, extension);

            var directory = new DirectoryInfo(docPath);
            // get last created file in directory
            var existingFile = directory.GetFiles(mask).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

            // check if file is actual upon document modified date
            if (existingFile != null)
            {
                // Cache 1 minute
                if (existingFile.CreationTime.AddSeconds(10) > DateTime.Now)
                {
                    // return cached file
                    return String.Format("{0}/{1}/{2}/{3}", relativePath, contractorId.ToString(), refundsReportDate.ToString("yyyy-MM-dd"),
                         existingFile.Name);
                }
                else
                { 
                    // delete outdate file
                    existingFile.Delete();
                }
            }

            var context = new SkladDataContext();
            // Get refunds products by day
            var products = GetRefundsProductLines(context, refundsReportDate, contractorId, managerId);
            if (products.Count() > 0)
            {
                string fileName = String.Format("{0}{1}{4}{2}{4}{3}{5}", Constants.RefundsReportPrefix,
                    refundsReportDate.Year, refundsReportDate.ToString("MM"), refundsReportDate.ToString("dd"),
                    dateSeparator, extension);

                string clientName = String.Empty;
                if (contractorId > 0)
                {
                    var contractor = context.Contractors.Where(x => x.ContractorId == contractorId).FirstOrDefault();
                    if (contractor != null)
                        clientName = contractor.Code;
                }
                else
                    clientName = "По всем клиентам";

                string titleRight =  "Возврат от " + refundsReportDate.ToString("dd.MM.yyyy");
                if (managerId > 0)
                { 

                    var user = context.UserProfiles.Where(x => x.UserId == managerId).FirstOrDefault();
                    if (user != null)
                        titleRight += ". Менеджер: " + user.DisplayName;
                }

                // create report
                ExcelReportInfo reportInfo = new ExcelReportInfo
                {
                    CreatedOf = refundsReportDate,
                    FileName = fileName,
                    FilePath = docPath,
                    DocumentSubject = "Возврат от " + refundsReportDate.ToString("dd.MM.yyyy"),
                    SheetName = Constants.RefundsReportPrefix + refundsReportDate.ToString("dd.MM.yyyy"),
                    TitleLeft = clientName,
                    TitleCenter = refundsReportDate.ToString("dd.MM.yyyy"),
                    TitleRight = titleRight
                };

                ReportHelper.GenerateProductLinesReport(products.ToList(), reportInfo);
                // ../Reports/Document/1093/e4fmt/Report-2014-10-09.xls
                //  /Reports/Refunds/RefundsReport-2014-10-09.xls
                reportFile = String.Format("{0}/{1}/{2}/{3}", relativePath, contractorId.ToString(), refundsReportDate.ToString("yyyy-MM-dd"),
                   fileName);
            }
            return reportFile;
        }

        /// <summary>
        /// Example of Dirty export to Excel. Just Create Session["refundsProductLinesGridState"] = gridState;
        /// in RefundsProductSearchGridDataRequested() method
        /// </summary>
        /// <param name="refundsReportDate"></param>
        /// <returns></returns>
        [Obsolete("Use custom Excel export method")]
        public ActionResult ExportToExcelRefundsByDay(DateTime? refundsReportDate)
        {
            if(!refundsReportDate.HasValue)
                refundsReportDate = DateTime.Now;

            var gridModel = new SkladJqGridModel();
            var skladModel = new SkladDataContext();
            var grid = gridModel.ProductLineGrid;

            // Get the last grid state the we saved before in Session in the DataRequested action
            JQGridState gridState = Session["refundsProductLinesGridState"] as JQGridState;

            // Need to set grid options again
            SetUpRefundsProductLineColumns(grid);
            SetUpRefundsProductLineGroupingGrandTotal(grid);

            //if (String.IsNullOrEmpty(exportType))
            //    exportType = "1";
            // refundsReportDate
            //switch (exportType)
            //{
            //    case "1":

           // grid.ExportToExcel(GetRefundsProductLines(skladModel, refundsReportDate.Value), "refundsByDateGrid.xls", gridState);
                    //break;
            //    case "2":
            //        grid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
            //        grid.ExportToExcel(northWindModel.Orders, "grid.xls", gridState);
            //        break;
            //    case "3":
            //        grid.ExportSettings.ExportDataRange = ExportDataRange.FilteredAndPaged;
            //        grid.ExportToExcel(northWindModel.Orders, "grid.xls", gridState);
            //        break;
            //}

            return View();
        }


        public void SetUpRefundsProductLineGroupingGrandTotal(JQGrid grid)
        {
            grid.DataResolved += new JQGridDataResolvedEventHandler(RefundsProductLineGroupingGrandTotal_DataResolved);
            grid.AppearanceSettings.ShowFooter = true;
        }

        void RefundsProductLineGroupingGrandTotal_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            if (Request.Params["refundsReportDate"] != null)
            {
                DateTime reportDate = DateTime.Now;
                int contractorId = 0;
                DateTime.TryParse(Request.Params["refundsReportDate"].ToString(), out reportDate);
                Int32.TryParse(Request.Params["contractorId"].ToString(), out contractorId);

                if (reportDate != DateTime.Now)
                {
                    try
                    {
                        JQGridColumn saleSumColumn = e.GridModel.Columns.Find(c => c.DataField == "SaleSum");
                        JQGridColumn sumColumn = e.GridModel.Columns.Find(c => c.DataField == "Sum");
                        JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
                        // Записывать клиентов от которых были возвраты
                        JQGridColumn commentColumn = e.GridModel.Columns.Find(c => c.DataField == "Comment");
                        var datacontextModel = new SkladDataContext();

                        // TODO: Найти все документы возврата за указанный день и выбрать по ним сумму
                        string saleSum = "0", sum = "0", quantity = "0";
                        List<Document> docs = null;
                        if (contractorId > 0)
                            docs = datacontextModel.Documents.Where(x => x.CreatedOf == reportDate && x.IsCommitted == true 
                                && x.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds && x.ContractorId == contractorId).ToList();
                        else
                           docs = datacontextModel.Documents.Where(x => x.CreatedOf == reportDate && x.IsCommitted == true && x.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds).ToList();
                        
                        
                        if (docs != null)
                        {
                            if (docs.Count > 0)
                            {
                                var docIds = docs.Select(x => x.DocumentId).ToArray();
                                commentColumn.FooterValue = "Возвраты от: " +  String.Join(", ",docs.Select(x => x.Contractor.Code).Distinct().ToArray());
                                // ids.Contains(e.SuchID)
                                saleSum = (from o in datacontextModel.ProductLines where docIds.Contains(o.DocumentId) select o.SaleSum)
                                .Sum().ToString();

                                sum = (from o in datacontextModel.ProductLines where docIds.Contains(o.DocumentId) select o.Sum)
                                    .Sum().ToString();

                                quantity = (from o in datacontextModel.ProductLines where docIds.Contains(o.DocumentId) select o.Quantity)
                                .Sum().ToString();
                            }
                        }

                        saleSumColumn.FooterValue = "Итого: " + saleSum;
                        sumColumn.FooterValue = "Итого: " + sum;
                        quantityColumn.FooterValue = "Итого: " + quantity;
                     
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Ошибка при подсчёте итоговых сумм: " + ex.Message);
                    }
                }
            }
        }

        private void SetUpRefundsProductLineColumns(JQGrid itemGrid)
        {
            JQGridColumn salePriceColumn = itemGrid.Columns.Find(c => c.DataField == "SalePrice");
            salePriceColumn.Visible = true;
            salePriceColumn.Editable = false;

            JQGridColumn saleSumColumn = itemGrid.Columns.Find(c => c.DataField == "SaleSum");
            saleSumColumn.Visible = true;

            JQGridColumn marginabsColumn = itemGrid.Columns.Find(c => c.DataField == "MarginAbs");
            marginabsColumn.Visible = true;
            marginabsColumn.Editable = true;
        }

        // This method is called when the grid requests data
        public JsonResult RefundsSearchGridDataRequested()
        {
            // Get both the grid Model and the data Model
            // The data model in our case is an autogenerated linq2sql database based on Northwind.
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();
            // customize the default grid model with our custom settings
            RefundsSetupGrid(gridModel.DocumentGrid);

            return GetDocumentList(gridModel, datacontextModel, EntityEnum.DocumentTypeEnum.Refunds);
        }

        private void RefundsSetupGrid(JQGrid grid)
        {
            SetUpGrid(grid, "RefundsGrid1",
                Url.Action("RefundsSearchGridDataRequested"),
                Url.Action("DocumentItemEditRows", "Home"));

            SetUpContractorEmployeeEditDropDown(grid);
        }

        #endregion

        private void SetUpContractorEmployeeEditDropDown(JQGrid itemGrid)
        {
       
            JQGridColumn employeeNameColumn = itemGrid.Columns.Find(c => c.DataField == "EmployeeName");
            employeeNameColumn.Visible = true;

            JQGridColumn contractorNameColumn = itemGrid.Columns.Find(c => c.DataField == "ContractorName");
            contractorNameColumn.Visible = true;
            JQGridColumn contractorIdColumn = itemGrid.Columns.Find(c => c.DataField == "ContractorId");
            contractorIdColumn.Visible = true;

            JQGridColumn saleSumColumn = itemGrid.Columns.Find(c => c.DataField == "SaleSum");
            saleSumColumn.Visible = true;

            // setup the grid search criteria for the columns
            JQGridColumn employeeIdColumn = itemGrid.Columns.Find(c => c.DataField == "EmployeeId");
            employeeIdColumn.Visible = true;

            // Populate the search dropdown only on initial request, in order to optimize performance
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                var skladModel = new SkladDataContext();

                // Выбрать только Сотрудников и менеджеров
                var usersList = (from m in skladModel.UserProfiles 
                                 where m.ContactTypeId == (int)EntityEnum.ContactTypeEnum.Employee select m).AsEnumerable()
                               .Select(x => new SelectListItem
                               {
                                   Text = x.DisplayName,
                                   Value = x.UserId.ToString()
                               });

                employeeIdColumn.EditList.AddRange(usersList.ToList<SelectListItem>());
                employeeIdColumn.SearchList = usersList.ToList<SelectListItem>();
                employeeIdColumn.SearchList.Insert(0, new SelectListItem { Text = "Все", Value = "" });
            }
        }

        private void SetUpGrid(JQGrid grid, string gridID, string dataUrl, string editUrl)
        {
            // Customize/change some of the default settings for this model
            // ID is a mandatory field. Must by unique if you have several grids on one page.
            grid.ID = gridID;

            // Setting the DataUrl to an action (method) in the controller is required.
            // This action will return the data needed by the grid
            grid.DataUrl = dataUrl;
            grid.EditUrl = editUrl;
            // show the search toolbar
            grid.ToolBarSettings.ShowSearchToolBar = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.ToolBarSettings.ShowAddButton = false;
            grid.AddDialogSettings.CloseAfterAdding = true;
        }
    }
}
