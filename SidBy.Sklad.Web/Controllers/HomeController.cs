using log4net;
using SidBy.Sklad.DataAccess;
using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SidBy.Sklad.Domain.Enums;
using SidBy.Sklad.Domain;
using Trirand.Web.Mvc;
using SidBy.Sklad.Web.BL;
using System.Web.Configuration;


namespace SidBy.Sklad.Web.Controllers
{
    using SidBy.Sklad.Web;
    using System.IO;
    using SidBy.Common.Crypt;

    [System.Web.Mvc.Authorize(Roles = "admin,employee")]
    public class HomeController : Controller
    {
        private readonly ILog logger;

        public HomeController()
        {
            logger = LogManager.GetLogger(GetType());
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [HttpPost]
        public ActionResult Document(IEnumerable<HttpPostedFileBase> files, int? documentId,
            int doctypeId)
        {
            int? contractorId =null;
           
            string contractorIdStr = Request.Params["contractorId"].ToString();

            if (!String.IsNullOrEmpty(contractorIdStr))
            {
                int contractorIdInt = 0;
                Int32.TryParse(contractorIdStr, out contractorIdInt);
                if (contractorIdInt <= 0)
                    return Json(new { message = "Сначала выберите контрагента!", documentId = -1 });
                contractorId = contractorIdInt;
            }

            string docNumber = Request.Params["docNumber"];
            DateTime docCreatedOf = DateTime.Now;
                
            if(Request.Params["docCreatedOf"] != null)
            {
                docCreatedOf = Convert.ToDateTime(Request.Params["docCreatedOf"].ToString());
            }


            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    //Stream uploadFileStream = file.InputStream;

                    // If the file uploaded is "XLSX", convert it's Sheet1 to a NPOI document
                    if (file.FileName.EndsWith("xlsx"))
                    {
                        //logger.InfoFormat("Загружается файл:{0}", file.FileName);

                        string extension = ".xlsx";
                        string uploadsPath = Server.MapPath("~/uploads");
                        var filename = Path.Combine(uploadsPath, Session.LCID.ToString() + extension);
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        file.SaveAs(filename);
                        //logger.InfoFormat("Загружен файл:{0}", filename);

                        var products = ExcelHelper.GetProductsData(filename, extension, "ProductLine-Template");

                        if (products != null)
                        {
                            // TODO: dublicated code. Merge with code in AddNewProductLine() method
                            var datacontextModel = new SkladDataContext();

                            // Add DocumentId if null
                            Document document = null;

                            if (documentId == null || documentId <= 0)
                            {
                                DocumentOperation docOp = new DocumentOperation();

                                docNumber = String.IsNullOrEmpty(docNumber) ?
                                    docOp.GetDocumentNumber(datacontextModel, DateTime.Now.Year, doctypeId).ToString()
                                    : docNumber.Trim().Replace(" ", "");

                                string secureFolderName = GenerateFolderName(String.Empty);
                                string commonFolderName = GenerateFolderName(secureFolderName);
                                // Create new document
                                document = new Document
                                {
                                    DocumentTypeId = doctypeId,
                                    CreatedOf = docCreatedOf,
                                    Comment = String.Empty,
                                    CreatedAt = DateTime.Now,
                                    Number = docNumber,
                                    IsCommitted = false,
                                    SecureFolderName = secureFolderName,
                                    CommonFolderName = commonFolderName,
                                    IsReportOutdated = true,
                                };
                                datacontextModel.Documents.Add(document);
                                datacontextModel.SaveChanges();

                                document = datacontextModel.Documents.Where(x => x.Number == docNumber &&
                                    x.DocumentTypeId == doctypeId
                                    && x.CreatedAt.Year == document.CreatedAt.Year
                                    && x.CreatedAt.Month == document.CreatedAt.Month
                                    && x.CreatedAt.Day == document.CreatedAt.Day
                                    && x.CreatedAt.Hour == document.CreatedAt.Hour
                                    && x.CreatedAt.Minute == document.CreatedAt.Minute).FirstOrDefault();
                            }
                            else
                            {
                                document = datacontextModel.Documents.Where(x => x.DocumentId == documentId).FirstOrDefault();
                            }

                            if (document == null)
                                return Json(new { message = String.Format("Не найден документ {0}!", documentId), documentId = -1 });

                            var contractor = datacontextModel.Contractors.Where(x => x.ContractorId == contractorId).FirstOrDefault();

                            if (contractor == null)
                                return Json(new { message = String.Format("Не найден контрагент {0}!", contractorId), documentId = -1 });

                            foreach (var productLine in products)
                            {
                                string article = productLine.ProductArticle.Trim().ToLower().Replace(" ", "");

                                var supplier = datacontextModel.Contractors.Where(x => x.Code == productLine.SupplierCode.Trim() &&
                                        x.ContractorTypeId == (int)EntityEnum.ContractorTypeEnum.Factory).FirstOrDefault();

                                if (supplier == null)
                                    return Json(new { message = String.Format("Фабрика {0} не найдена!", productLine.SupplierCode.Trim()), documentId = -1 });

                                decimal marginAbs = 0;

                                if (productLine.MarginAbs > 0)
                                    marginAbs = productLine.MarginAbs;
                                else if (productLine.SalePrice > 0)
                                {
                                    marginAbs = Math.Abs(productLine.SalePrice - productLine.PurchasePrice);

                                    if (marginAbs == 0)
                                        marginAbs = contractor.MarginAbs;
                                }
                                else
                                    marginAbs = contractor.MarginAbs;

                                Product product = GetOrCreateProduct(datacontextModel,
                                    supplier.ContractorId,
                                    productLine.PurchasePrice,
                                    marginAbs,
                                    article, String.Empty);

                                // Check if this productline already exists in document. if so, just
                                // change it value
                                ProductLine line = datacontextModel.ProductLines.Where(x => x.ProductId == product.ProductId &&
                                    x.DocumentId == document.DocumentId).FirstOrDefault();

                                string comment = productLine.Comment;
                                if (comment != null)
                                    comment = comment.Trim();

                                if (line == null)
                                {
                                    // Attach productLine to document
                                    line = new ProductLine
                                    {
                                        ProductArticle = article,
                                        SupplierCode = supplier.Code,
                                        SupplierId = supplier.ContractorId,
                                        Quantity = productLine.Quantity,
                                        PurchasePrice = productLine.PurchasePrice,
                                        SalePrice = productLine.PurchasePrice + marginAbs,
                                        MarginAbs = marginAbs,
                                        Discount = productLine.Discount,
                                        Comment = comment,
                                        ProductId = product.ProductId,
                                        DocumentId = document.DocumentId,
                                        Sum = productLine.Quantity * productLine.PurchasePrice,
                                        SaleSum = productLine.Quantity * (productLine.PurchasePrice + marginAbs)
                                    };

                                    if (document.Products == null)
                                        document.Products = new List<ProductLine>();

                                    document.Products.Add(line);
                                }
                                else
                                {
                                    line.ProductArticle = article;
                                    line.SupplierCode = supplier.Code;
                                    line.SupplierId = supplier.ContractorId;
                                    line.Quantity = productLine.Quantity;
                                    line.PurchasePrice = productLine.PurchasePrice;
                                    line.SalePrice = productLine.PurchasePrice + marginAbs;
                                    line.MarginAbs = marginAbs;
                                    line.Comment = comment;
                                    line.Discount = productLine.Discount;
                                    line.ProductId = product.ProductId;
                                    line.Sum = productLine.Quantity * productLine.PurchasePrice;
                                    line.SaleSum = productLine.Quantity * (productLine.PurchasePrice + marginAbs);
                                }
                                document.IsReportOutdated = true;
                                datacontextModel.SaveChanges();
                            }

                            document.IsReportOutdated = true;
                            datacontextModel.SaveChanges();

                            return Json(new { message = "Документ успешно обновлен/добавлен", documentId = document.DocumentId });
                        }
                    }
                    else
                    {
                        return Json(new { message = "Вы можете загружать только файлы Excel 2007-2010!", documentId = -1 });
                    }
                }
            }
            return Json(new { message = "Возникла критическая ошибка!", documentId = -1 });
        }

        public ActionResult Document(int? documentTypeId, int? documentId)
        {
            // Get the model (setup) of the grid defined in the /Models folder.
            var gridModel = new SkladJqGridModel();
            var grid = gridModel.ProductLineGrid;

            var datacontextModel = new SkladDataContext();

            DocumentProductLineModel model = new DocumentProductLineModel
            {
                WarehouseList = datacontextModel.Warehouses.ToList(),
                OurCompanyList = datacontextModel.LegalEntities.ToList(),
                EmployeeList = datacontextModel.UserProfiles.Where(x => x.ContactTypeId == (int)EntityEnum.ContactTypeEnum.Employee).ToList(),
                //DocumentItem = (documentTypeId != null || documentTypeId >= 0) ?
                //(new Document { DocumentTypeId = (int)documentTypeId, 
                //    DocumentType = datacontextModel.DocumentTypes.Where(x =>x.DocumentTypeId == documentTypeId).First(),
                //    CreatedOf = DateTime.Now
                //}) :
                //(datacontextModel.Documents
                //.Include("DocumentType")
                //.Include("FromWarehouse")
                //.Include("ToWarehouse")
                //.Include("Contract")
                //.Include("Employee")
                //.Where(x => x.DocumentId == documentId)).First(),
                Products = grid,
            };

            if (documentTypeId != null)
            { 
                model.DocumentItem = new Document { DocumentTypeId = (int)documentTypeId, 
                    DocumentType = datacontextModel.DocumentTypes.Where(x =>x.DocumentTypeId == documentTypeId).First(),
                    CreatedOf = DateTime.Now,
                    IsCommitted = true, IsReportOutdated = true,
                };
            }
            else if (documentId != null)
            {
                model.DocumentItem = (datacontextModel.Documents
                .Include("DocumentType")
                .Include("FromWarehouse")
                .Include("ToWarehouse")
                .Include("Contract")
                .Include("Employee")
                .Include("Contractor")
                .Where(x => x.DocumentId == documentId)).First();
            }

            if (model.DocumentItem != null)
            {
                ViewBag.Title = model.DocumentItem.DocumentType.Name;
            }
            model.JGridName = "DocumentGrid1";

            // NOTE: you need to call this method in the action that fetches the data as well,
            // so that the models match
            DocumentSetupGrid(grid, documentId);

            return View(model);
        }
        /*
          "docContractorId": $("#ContractorDocId").val(),
                        "docEmployeeId": $("#DocumentItem_Employee_UserId").val(),
                        "docPlanDate": $("#planDateDatepicker").datepicker("getDate").toJSONLocal(),
         */
        public string SaveDocument(int documentId, string docNumber,
            DateTime docCreatedOf, string docComment,
            bool docIsComitted, int? docContractorId, int? docEmployeeId,
            DateTime? docPlanDate, bool createRelatedDoc, string selectedRowsIds
          )
        {
            if (documentId <= 0)
                return Constants.ErrorUrl;

            var datacontextModel = new SkladDataContext();

            var document = datacontextModel.Documents.Include("Products").Where(x => x.DocumentId == documentId).FirstOrDefault();

            if (document == null)
                return Constants.ErrorUrl;

            if (document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Posting
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Cancellation)
            {
                docContractorId = null;
                docEmployeeId = null;
                docPlanDate = null;
            }

            if (document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Shipment)
            { 
                if(docContractorId == null || docEmployeeId == null)
                    return Constants.ErrorUrl;
            }

            if(!String.IsNullOrEmpty(docNumber))
                document.Number = docNumber;

            document.CreatedOf = docCreatedOf;
            document.Comment = docComment;
            document.ContractorId = docContractorId;
            document.EmployeeId = docEmployeeId;
            document.PlannedDate = docPlanDate;
            document.IsReportOutdated = true;

            // calculate sum
            document.Sum = datacontextModel.ProductLines
                .Where(x => x.DocumentId == document.DocumentId).Sum(x => x.Sum);

            document.SaleSum = datacontextModel.ProductLines
                .Where(x => x.DocumentId == document.DocumentId).Sum(x => x.SaleSum);

            DocumentOperation docOperation = new DocumentOperation();
            docOperation.UpdateDocument(datacontextModel, document.DocumentId);

            if (createRelatedDoc)
                docIsComitted = true;

            document.IsCommitted = docIsComitted;

            datacontextModel.SaveChanges();

            // TODO: if there was ParentDocumentId than check all productLines
            // and update Shipped.
            if (document.ParentDocumentId != null)
            { 
                // get parent document
                var parentDoc = datacontextModel.Documents.Include("Products").Where(x => x.DocumentId == document.ParentDocumentId)
                    .FirstOrDefault();
                if (parentDoc != null)
                { 
                    // TODO: Check all ProductLines that were shipped
                    if (parentDoc.Products != null)
                    {
                        foreach (var product in parentDoc.Products)
                        { 
                            int? shipped = 0;
                            shipped = datacontextModel.ProductLines
                                .Where(x => x.ProductId == product.ProductId && 
                                    x.DocumentId == documentId)
                                .Select(x => x.Quantity).FirstOrDefault();
                            if (shipped != null)
                                product.Shipped = (int)shipped;
                        }

                        datacontextModel.SaveChanges();
                    }
                }
            }

            if (document.Products != null)
                docOperation.UpdateDocumentProducts(document.Products.ToList());

            if (createRelatedDoc)
                return CreateRelatedDocument(datacontextModel, documentId, selectedRowsIds);

            return DocumentOperation.UrlToDocumentList(document.DocumentTypeId);
        }

        private string CreateRelatedDocument(SkladDataContext datacontextModel, int documentId, string selectedRowsIds)
        {
            Document parentDoc = datacontextModel.Documents.Include("Products").Where(x => x.DocumentId == documentId)
                 .FirstOrDefault();

            if(parentDoc == null)
                return Constants.ErrorUrl;

            // TODO: split selectedRowsIds to array of int. Transfer to a new document only items with specified ids.

            int newDocumentTypeId = 0;
            if(parentDoc.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders)
            {
                newDocumentTypeId = (int)EntityEnum.DocumentTypeEnum.Shipment;
            }

            if(parentDoc.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Shipment)
            {
                newDocumentTypeId = (int)EntityEnum.DocumentTypeEnum.Refunds;
            }
            if(newDocumentTypeId == 0)
                return Constants.ErrorUrl;

            DocumentOperation operation = new DocumentOperation();

            string secureFolderName = GenerateFolderName(String.Empty);
            string commonFolderName = GenerateFolderName(secureFolderName);
            
            Document newDocument = new Document
            {
                Number = parentDoc.Number + "-"
                        + operation.GetDocumentNumber(datacontextModel, DateTime.Now.Year,newDocumentTypeId).ToString(),
                CreatedOf = parentDoc.CreatedOf,
                ContractorId = parentDoc.ContractorId,
                EmployeeId = parentDoc.EmployeeId,
                CreatedAt = DateTime.Now,
                FromWarehouseId = parentDoc.FromWarehouseId,
                ToWarehouseId = parentDoc.ToWarehouseId,
                DocumentTypeId = newDocumentTypeId,
                ParentDocumentId = parentDoc.DocumentId,
                SecureFolderName = secureFolderName,
                CommonFolderName = commonFolderName,
                IsReportOutdated = true,
            };

            datacontextModel.Documents.Add(newDocument);

            datacontextModel.SaveChanges();

            newDocument = datacontextModel.Documents.Where(x => x.ParentDocumentId == parentDoc.DocumentId &&
                  x.DocumentTypeId == newDocumentTypeId
                  && x.Number == newDocument.Number
                  && x.CreatedAt.Year == newDocument.CreatedAt.Year
                  && x.CreatedAt.Month == newDocument.CreatedAt.Month
                  && x.CreatedAt.Day == newDocument.CreatedAt.Day
         
                  ).FirstOrDefault();

            //Clone list
            newDocument.Products = new List<ProductLine>();
                //parentDoc.Products.Where(x => x.Shipped < x.Quantity)
                //.Select(c => { c.DocumentId = newDocument.DocumentId; c.Document = null;
                //c.Product = null;
                //    return c; }).ToList()
                //);
            List<ProductLine> productsToCopy = null;
            if (!String.IsNullOrEmpty(selectedRowsIds))
            {
                string[] idsStr = selectedRowsIds.Split(',');
                idsStr = idsStr.Where(x => !String.IsNullOrEmpty(x)).ToArray();
                int[] ids = Array.ConvertAll<string, int>(idsStr, int.Parse);
                productsToCopy = parentDoc.Products.Where(x => x.Shipped < x.Quantity
                    && ids.Contains(x.ProductLineId))
                .ToList();
            }
            else
            {
                productsToCopy = parentDoc.Products.Where(x => x.Shipped < x.Quantity)
              .ToList();
            }
            
            /*
             * string values = "1,2,3,4,5,6,7,8,9,10";
string[] tokens = values.Split(',');

int[] convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse);


             * 
             int[] ids = // populate ids
var query = from e in db.SomeTable
        where ids.Contains(e.SuchID)
        select e
             */
           

            foreach (var product in productsToCopy)
            {
                newDocument.Products.Add(new ProductLine
                {
                    DocumentId = newDocument.DocumentId,
                    ProductId = product.ProductId,
                    SupplierId = product.SupplierId,
                    ProductArticle = product.ProductArticle,
                    SupplierCode = product.SupplierCode,
                    Quantity = product.Quantity,
                    Discount = product.Discount,
                    Reserve = product.Reserve,
                    Shipped = product.Shipped,
                    Available = product.Available,
                    PurchasePrice = product.PurchasePrice,
                    SalePrice = product.SalePrice,
                    MarginAbs = product.MarginAbs,
                    Sum = product.Sum,
                    SaleSum = product.SaleSum,
                    VAT = product.VAT,
                    IsPriceIncludesVAT = product.IsPriceIncludesVAT,
                    Comment = product.Comment
                });
            }

            datacontextModel.SaveChanges();

            return "/Home/Document?documentId=" + newDocument.DocumentId;
            // Cre

            /*
                   case (int)EntityEnum.DocumentTypeEnum.CustomerOrders:
                    return "/Sale/CustomerOrders";
                case (int)EntityEnum.DocumentTypeEnum.Shipment:
                    return "/Sale/Shipment";
             */
        }

        private string GenerateFolderName(string dublicate)
        {
            int randomLength = 5;

            if (dublicate == null)
                dublicate = String.Empty;

            string folderName = RandomGenerator.GenerateRandomText(randomLength);
            if (String.Compare(folderName, dublicate, true) == 0)
                folderName = RandomGenerator.GenerateRandomText(randomLength + 1);

            return folderName;
        }

        public int AddNewProductLine(int? documentId, int doctypeId, string docNumber,
            DateTime docCreatedOf, string docComment,
            int supplierId,
            string article,
            decimal purchaseprice, decimal marginabs, decimal discount, int quantity, int quantityW, string comment)
        {
            if (supplierId <= 0 || String.IsNullOrEmpty(article) || purchaseprice < 0
                || doctypeId <= 0
                )
                return -1;

            var datacontextModel = new SkladDataContext();
            // check for valid supplier
            var supplier = datacontextModel.Contractors.Where(x => x.ContractorId == supplierId &&
                x.ContractorTypeId == (int)EntityEnum.ContractorTypeEnum.Factory).FirstOrDefault();

            if (supplier == null)
                return -1;

            article = article.Trim().Replace(" ","").ToLower();
            comment = comment.Trim();
            // get product
            Product product = GetOrCreateProduct(datacontextModel, supplierId, purchaseprice, marginabs, article, comment);
            
            // Add DocumentId if null
            Document document = null;

            if (documentId == null || documentId <= 0)
            {
                DocumentOperation docOp = new DocumentOperation();

                docNumber = String.IsNullOrEmpty(docNumber) ?
                    docOp.GetDocumentNumber(datacontextModel, DateTime.Now.Year, doctypeId).ToString()
                    : docNumber.Trim().Replace(" ", "");

                string secureFolderName = GenerateFolderName(String.Empty);
                string commonFolderName = GenerateFolderName(secureFolderName);
                // Create new document
                document = new Document
                {
                    DocumentTypeId = doctypeId,
                    CreatedOf = docCreatedOf,
                    Comment = docComment,
                    CreatedAt = DateTime.Now,
                    Number = docNumber,
                    IsCommitted = false,
                    SecureFolderName = secureFolderName,
                    CommonFolderName = commonFolderName,
                    IsReportOutdated = true,
                };
                datacontextModel.Documents.Add(document);
                datacontextModel.SaveChanges();
               
                document = datacontextModel.Documents.Where(x => x.Number == docNumber &&
                    x.DocumentTypeId == doctypeId 
                    && x.CreatedAt.Year == document.CreatedAt.Year
                    && x.CreatedAt.Month == document.CreatedAt.Month
                    && x.CreatedAt.Day == document.CreatedAt.Day
                    && x.CreatedAt.Hour == document.CreatedAt.Hour
                    && x.CreatedAt.Minute == document.CreatedAt.Minute).FirstOrDefault();
            }
            else
            {
                document = datacontextModel.Documents.Where(x => x.DocumentId == documentId).FirstOrDefault();
            }

            if (document == null)
            {
                logger.ErrorFormat("Ошибка создания документа № {0} от {1}", docNumber, docCreatedOf);
                return -1;
            }

            document.IsReportOutdated = true;

            // Check if this productline already exists in document. if so, just
            // change it value
            ProductLine line = datacontextModel.ProductLines.Where(x => x.ProductId == product.ProductId &&
                 x.DocumentId == document.DocumentId).FirstOrDefault();

            string productComment = comment;
          //  int productQuantity = quantity;
            if (quantityW > 0)
            {
                productComment += "со склада [" + quantityW + "] ед. ";
            }

            if (line == null)
            {
                // Attach productLine to document
                line = new ProductLine
                {
                    ProductArticle = article,
                    SupplierCode = supplier.Code,
                    SupplierId = supplier.ContractorId,
                    Quantity = quantity + quantityW,
                    PurchasePrice = purchaseprice,
                    SalePrice = purchaseprice + marginabs,
                    MarginAbs = marginabs,
                    Discount = discount,
                    Comment = productComment,
                    ProductId = product.ProductId,
                    DocumentId = document.DocumentId,
                    Sum = (quantity + quantityW) * purchaseprice,
                    SaleSum = (quantity + quantityW) * (purchaseprice + marginabs)
                };

                if (document.Products == null)
                    document.Products = new List<ProductLine>();

                document.Products.Add(line);
            }
            else
            { 
                line.ProductArticle = article;
                line.SupplierCode = supplier.Code;
                line.SupplierId = supplier.ContractorId;
                line.Quantity = quantity + quantityW;
                line.PurchasePrice = purchaseprice;
                line.MarginAbs = marginabs;
                line.Discount = discount;
                line.SalePrice = purchaseprice + marginabs;
                line.Comment = productComment;
                line.ProductId = product.ProductId;
                line.Sum = (quantity + quantityW) * purchaseprice;
                line.SaleSum = (quantity + quantityW) * (purchaseprice + marginabs);
            }

            // обновить документы оприходования где имеется этот товар
            RemoveProductFromPostingDocument(datacontextModel, product.ProductId, quantityW);

            datacontextModel.SaveChanges();

            return document.DocumentId;
        }

        /// <summary>
        /// Удалить позиции из оприходования
        /// </summary>
        /// <param name="datacontextModel"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        private void RemoveProductFromPostingDocument(SkladDataContext datacontextModel, 
            int productId, int quantity)
        {
            var plines = datacontextModel.ProductLines.Where(x => x.ProductId == productId).ToList();

            foreach (var pline in plines)
            { 
                // выбирать только из документа оприходование
                var doc = datacontextModel.Documents.Where(x => x.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Posting &&
                    x.DocumentId == pline.DocumentId && x.IsCommitted == true).FirstOrDefault();
                if(doc != null)
                {
                    if (pline.Quantity > quantity)
                    { 
                        // just update product line
                        pline.Quantity = pline.Quantity - quantity;

                        break;
                    }
                    else if (pline.Quantity == quantity)
                    {
                        datacontextModel.ProductLines.Remove(pline);
                        break;
                    }
                    else
                    {
                        datacontextModel.ProductLines.Remove(pline);
                      
                    }
                }
            }
            datacontextModel.SaveChanges();
        }

        public ActionResult DocumentItemEditRows(Document editedItem)
        {
            // Get the grid and database models
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();

            // If we are in "Edit" mode
            if (gridModel.DocumentGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                return RedirectToAction("Document", new { documentId = editedItem.DocumentId });
            }

            if (gridModel.DocumentGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                Document item = (from x in datacontextModel.Documents
                                    where x.DocumentId == editedItem.DocumentId
                                    select x)
                               .First<Document>();

                DocumentOperation operation = new DocumentOperation();
                operation.DeleteDocument(datacontextModel, item);

                datacontextModel.Documents.Remove(item);
                datacontextModel.SaveChanges();
            
                logger.InfoFormat("удален документ {0} от {1}", editedItem.Number, editedItem.CreatedOf);
            }

            return RedirectToDocumentList(editedItem.DocumentTypeId);
        }
        public ActionResult RedirectToDocumentList(int documentId)
        {
            // string 
            switch (documentId)
            {
                case (int)EntityEnum.DocumentTypeEnum.Posting:
                    return RedirectToAction("Posting", "Warehouse");
                case (int)EntityEnum.DocumentTypeEnum.Cancellation:
                    return RedirectToAction("Cancellation", "Warehouse");
                case (int)EntityEnum.DocumentTypeEnum.CustomerOrders:
                    return RedirectToAction("CustomerOrders", "Sale");
                case (int)EntityEnum.DocumentTypeEnum.Shipment:
                    return RedirectToAction("Shipment", "Sale"); ;
                case (int)EntityEnum.DocumentTypeEnum.Refunds:
                    return RedirectToAction("Refunds", "Sale"); ;
                default:
                    return RedirectToAction("Index", "Indicators"); ;
            }
        }

        public JsonResult GetProductByArticle(string term, int supplierId)
        {
            if (supplierId <= 0)
                return null;

            var datacontextModel = new SkladDataContext();

            var result = (from u in datacontextModel.Products
                          where u.Article.ToLower().Contains(term.ToLower()) && u.ContractorId ==
                          supplierId
                          select new ProductArticlePriceModel { Article = u.Article, ProductId = u.ProductId, PurchasePrice = u.PurchasePrice, Description = u.Description }).ToList();
           
            if(result != null) 
            {
                if(result.Count > 0)
                {
                    // Check if this product exists in warehouse
                    var inWarehouseDocIds = (from u in datacontextModel.Documents
                                       where u.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Posting && u.IsCommitted == true
                                       select u.DocumentId ).ToList();


                    if (inWarehouseDocIds != null)
                    {
                        if (inWarehouseDocIds.Count > 0)
                        { 
                            var productIds = result.Select(x => x.ProductId).ToList();
                            var result2 = (from u in datacontextModel.ProductLines
                                           where inWarehouseDocIds.Contains(u.DocumentId) && productIds.Contains(u.ProductId.Value)
                                           select new ProductArticlePriceModel {  ProductId = u.ProductId.Value, Quantity = u.Quantity, Description = u.Comment }).ToList();
                            if (result2 != null)
                            {
                                if (result2.Count > 0)
                                { 
                                    // может быть несколько документов с одинаковым товаром. Поэтому их нужно объединить
                                    List<ProductArticlePriceModel> summed = result2.GroupBy(row => row.ProductId)
                                          .Select(g => new ProductArticlePriceModel()
                                          {
                                              ProductId = g.Key,
                                              Quantity = g.Sum(x => x.Quantity),

                                          }).ToList();

                                    foreach (var item in summed)
                                    {
                                        var it = result.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                                        if (it != null)
                                        {
                                            it.Quantity = item.Quantity;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = result };
        }

        public JsonResult GetDocumentsByProduct(int productId, int documentTypeId)
        {
            if (productId <= 0)
                return null;

            var datacontextModel = new SkladDataContext();

            // Get product Id

            var result = (from u in datacontextModel.ProductLines.Include("Document")
                          
                          where u.ProductId == productId
                          select new { u.DocumentId, 
                              u.Document.IsCommitted, u.Document.Number,
                              u.Document.DocumentTypeId,
                              u.Document.CreatedOf,
                                }).Where(x => x.DocumentTypeId == documentTypeId).OrderByDescending(u => u.CreatedOf).ToList();

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = result };
        }

        // This method is called when the grid requests data
        public JsonResult DocumentSearchGridDataRequested(int? documentId)
        {
            // Get both the grid Model and the data Model
            // The data model in our case is an autogenerated linq2sql database based on Northwind.
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();
            // customize the default grid model with our custom settings
            DocumentSetupGrid(gridModel.ProductLineGrid, documentId);

            if (documentId == null)
                documentId = 0;

            // return the result of the DataBind method, passing the datasource as a parameter
            // jqGrid for ASP.NET MVC automatically takes care of paging, sorting, filtering/searching, etc
            return gridModel.ProductLineGrid.DataBind(datacontextModel.ProductLines.Where(x => x.DocumentId == (int)documentId));
        }

        private void DocumentSetupGrid(JQGrid grid, int? documentId)
        {
            SetUpGrid(grid, "DocumentGrid1",
                Url.Action("DocumentSearchGridDataRequested", new { documentId = documentId }),
                Url.Action("DocumentEditRows"));

            grid.ToolBarSettings.ShowAddButton = false;

            //if (document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders
            //   || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds
            //   || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Shipment)
            //{
                SetUpProductLineColumns(grid, documentId);
                SetUpProductLineGroupingGrandTotal(grid);
           // }
        }

        public void SetUpProductLineGroupingGrandTotal(JQGrid grid)
        {
            grid.DataResolved += new JQGridDataResolvedEventHandler(GroupingGrandTotal_DataResolved);
            grid.AppearanceSettings.ShowFooter = true;

        }

        void GroupingGrandTotal_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            if (Request.Params["documentId"] != null)
            {
                int documentId = 0;
                Int32.TryParse(Request.Params["documentId"].ToString(), out documentId);

                if (documentId > 0)
                {
                    try
                    {
                        JQGridColumn saleSumColumn = e.GridModel.Columns.Find(c => c.DataField == "SaleSum");
                        JQGridColumn sumColumn = e.GridModel.Columns.Find(c => c.DataField == "Sum");
                        JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
                        var datacontextModel = new SkladDataContext();

                        string saleSum = (from o in datacontextModel.ProductLines where o.DocumentId == documentId select o.SaleSum)
                            .Sum().ToString();

                        string sum = (from o in datacontextModel.ProductLines where o.DocumentId == documentId select o.Sum)
                            .Sum().ToString();

                        string quantity = (from o in datacontextModel.ProductLines where o.DocumentId == documentId select o.Quantity)
                        .Sum().ToString();

                        saleSumColumn.FooterValue = "Итого: " + saleSum;
                        sumColumn.FooterValue = "Итого: " + sum;
                        quantityColumn.FooterValue = "Итого: " + quantity;
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Ошибка при подсчёте итоговых сумм: " + ex.Message);
                    }
                }
                //shipCityColumn.FooterValue = "Grand Total: ";
            }
        }


        private void SetUpProductLineColumns(JQGrid itemGrid, int? documentId)
        {
            int documentTypeId = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["documentTypeId"]))
            {
                Int32.TryParse(Request.QueryString["documentTypeId"], out documentTypeId);
            }
        

            if(documentTypeId == 0)
            {if (documentId != null)
            {
                var datacontextModel = new SkladDataContext();
                documentTypeId = datacontextModel.Documents.Where(x => x.DocumentId == (int)documentId)
                    .FirstOrDefault().DocumentTypeId;
            }}

            if (documentTypeId > 0)
            {
                if (documentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders
                    || documentTypeId == (int)EntityEnum.DocumentTypeEnum.Shipment
                    || documentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds)
                {
                    // SalePrice
                    JQGridColumn salePriceColumn = itemGrid.Columns.Find(c => c.DataField == "SalePrice");
                    salePriceColumn.Visible = true;
                    salePriceColumn.Editable = false;

                    JQGridColumn saleSumColumn = itemGrid.Columns.Find(c => c.DataField == "SaleSum");
                    saleSumColumn.Visible = true;

                    JQGridColumn marginabsColumn = itemGrid.Columns.Find(c => c.DataField == "MarginAbs");
                    marginabsColumn.Visible = true;
                    marginabsColumn.Editable = true;

                    /*
                        JQGrid1.Columns.FromDataField("Freight").FooterValue = freightTotal.ToString();
                        JQGrid1.Columns.FromDataField("CustomerID").FooterValue = "Total:";
                     */
                    //if(documentId != null)
                    //{
                    //    decimal saleSum = datacontextModel.ProductLines.Where(x => x.DocumentId == documentId).Sum(x => x.SaleSum);
                    //    //JQGridColumn saleSumColumn = itemGrid.Columns.Find(c => c.DataField == "SaleSum");
                    //    saleSumColumn.FooterValue = "Итого: " + saleSum.ToString();
                    //}
                }

                if (documentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders)
                {
                    JQGridColumn saleSumColumn = itemGrid.Columns.Find(c => c.DataField == "Shipped");
                    saleSumColumn.Visible = true;
                }
            }
        }


        public ActionResult DocumentEditRows(ProductLine editedItem)
        {
            // Get the grid and database models
            var gridModel = new SkladJqGridModel();
            var datacontextModel = new SkladDataContext();

            int documentId = -1;
            // If we are in "Edit" mode

            if (gridModel.ProductLineGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                // Get the data from and find the item corresponding to the edited row
                ProductLine item = (from x in datacontextModel.ProductLines
                                    where x.ProductLineId == editedItem.ProductLineId
                                    select x).First<ProductLine>();

                // update the Order information
                UpdateProductLine(item, editedItem);

                // save comment to iriginal product item description
                var product = datacontextModel.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if (product != null)
                {
                    if (String.IsNullOrEmpty(product.Description))
                    { product.Description = editedItem.Comment; }
                    else if (product.Description.IndexOf(editedItem.Comment) == -1)
                    {
                        product.Description += ". " + editedItem.Comment;
                    }
                    else
                        product.Description = editedItem.Comment;
                }

                documentId = item.DocumentId;
                datacontextModel.SaveChanges();
            }

            if (gridModel.ProductLineGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                ProductLine item = (from x in datacontextModel.ProductLines
                                    where x.ProductLineId == editedItem.ProductLineId
                                    select x)
                               .First<ProductLine>();
                documentId = item.DocumentId;
                // delete the record                
                datacontextModel.ProductLines.Remove(item);
                datacontextModel.SaveChanges();
            }

            var document = datacontextModel.Documents.Where(x => x.DocumentId == documentId).FirstOrDefault();

            if (document != null)
            {
                document.IsReportOutdated = true;
                datacontextModel.SaveChanges();
            }

            // Add
            //DocumentOperation operation = new DocumentOperation();
            //operation.UpdateDocument(datacontextModel, documentId);

            return RedirectToAction("Document", "Home", new { documentId = documentId });
        }

        private void UpdateProductLine(ProductLine item, ProductLine editedItem)
        {
            item.PurchasePrice = editedItem.PurchasePrice;
            item.SalePrice = editedItem.PurchasePrice + editedItem.MarginAbs;
            item.MarginAbs = editedItem.MarginAbs;
            item.Quantity = editedItem.Quantity;
            item.Discount = editedItem.Discount;
            item.Comment = (editedItem.Comment ?? String.Empty).Trim();
            item.Sum = editedItem.Quantity * editedItem.PurchasePrice;
            item.SaleSum = editedItem.Quantity * (editedItem.PurchasePrice + editedItem.MarginAbs);
        }

        public JsonResult GetContractorByCode(string term)
        {
            var datacontextModel = new SkladDataContext();

            var result = (from u in datacontextModel.Contractors
                          where u.Code.ToLower().Contains(term.ToLower()) 
                          select new { u.Code, u.ContractorId, u.Name, u.MarginAbs }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContractorByCodeAutoComplete(string term)
        {
            var datacontextModel = new SkladDataContext();

            var result = (from u in datacontextModel.Contractors
                          where u.Code.ToLower().Contains(term.ToLower())
                          select u.Code).ToList();
          
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = result };
        }

        private Product GetOrCreateProduct(SkladDataContext datacontextModel, int supplierId, decimal purchaseprice, decimal marginabs, string article, string comment)
        {
            Product product = datacontextModel.Products.Where(x => x.Article.ToLower() == article &&
                x.ContractorId == supplierId).FirstOrDefault();

            if (product == null)
            {
                // Add new product
                Product item = new Product
                {
                    Article = article,
                    ContractorId = supplierId,
                    PurchasePrice = purchaseprice,
                    SalePrice = purchaseprice + marginabs,
                    CreatedAt = DateTime.Now,
                };

                datacontextModel.Products.Add(item);
                datacontextModel.SaveChanges();

                product = datacontextModel.Products.Where(x => x.Article.ToLower() == article &&
                x.ContractorId == supplierId).FirstOrDefault();
            }
            
            if (String.IsNullOrEmpty(product.Description))
                product.Description = comment;
            else if (product.Description.IndexOf(comment) == -1)
                product.Description += ". " + comment;
            else
                product.Description = comment;

            datacontextModel.SaveChanges();

            return product;
        }

        /*
              "documentId": $("#DocId").val(),
                          "docNumber": $("#DocumentItem_Number").val(),
                          "docCreatedOf": $("#createdOfDatepicker").datepicker("getDate").toJSONLocal(),
                          "docComment": $("#DocumentItem_Comment").val(),
                          "docContractorId": $("#ContractorDocId").val(),
                          "docEmployeeId": $("#DocumentItem_Employee_UserId").val(),
                          "docPlanDate": !$("#planDateDatepicker").val() ? null : $("#planDateDatepicker").datepicker("getDate").toJSONLocal(),
         
         */

        public string GetReportFileLink(int documentId, string docNumber,
           DateTime docCreatedOf, string docComment,
           int? docContractorId, int? docEmployeeId,
           DateTime? docPlanDate
         )
        {
            if (documentId <= 0)
                return Constants.ErrorUrl;

            var datacontextModel = new SkladDataContext();

            var document = datacontextModel.Documents.Include("Products")
                .Include("Contractor").Where(x => x.DocumentId == documentId)
                .FirstOrDefault();

            if (document == null)
                return Constants.ErrorUrl;

            if (document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Posting
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Cancellation)
            {
                docContractorId = null;
                docEmployeeId = null;
                docPlanDate = null;
            }

            if (document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.CustomerOrders
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Refunds
                || document.DocumentTypeId == (int)EntityEnum.DocumentTypeEnum.Shipment)
            {
                if (docContractorId == null || docEmployeeId == null)
                    return Constants.ErrorUrl;
            }

            if (!String.IsNullOrEmpty(docNumber))
                document.Number = docNumber;

            document.CreatedOf = docCreatedOf;
            document.Comment = docComment;
            document.ContractorId = docContractorId;
            document.EmployeeId = docEmployeeId;
            document.PlannedDate = docPlanDate;

            // calculate sum
            document.Sum = datacontextModel.ProductLines
                .Where(x => x.DocumentId == document.DocumentId).Sum(x => x.Sum);

            document.SaleSum = datacontextModel.ProductLines
                .Where(x => x.DocumentId == document.DocumentId).Sum(x => x.SaleSum);

            return GetReportFile(datacontextModel, document, EntityEnum.ReportType.SaleReport);
        }

        // TODO: create ajax method which creates datacontextModel

        private string GetReportFile(SkladDataContext datacontextModel, 
            Document document, 
            EntityEnum.ReportType type)
        {
            if (document == null)
            {
                logger.ErrorFormat("GetReportFile. Document is null");
                throw new ArgumentException("GetReportFile. Document is null");
            }

            bool saveChanges = false;
            // check for folder names
            if (String.IsNullOrEmpty(document.SecureFolderName))
            {
                document.SecureFolderName = GenerateFolderName(document.CommonFolderName);
                saveChanges = true;
            }

            if (String.IsNullOrEmpty(document.CommonFolderName))
            {
                document.CommonFolderName = GenerateFolderName(document.SecureFolderName);
                saveChanges = true;
            }

            if (saveChanges)
                datacontextModel.SaveChanges();

            string relativePath = "../" + Constants.DocumentReportPath;
            string rootPath = Server.MapPath(relativePath);
            string reportFile = String.Empty;

            if (type == EntityEnum.ReportType.SaleReport)
            { 
                // Check if file already exists. 
                // Than check Modified date of the file in file name
                string docPath = Path.Combine(rootPath, document.DocumentId.ToString(), document.SecureFolderName);

                if(!Directory.Exists(docPath))
                    Directory.CreateDirectory(docPath);

                string dateSeparator = "-";
                string extension = ".xls";
                string mask = String.Format("{0}*{1}", Constants.DocumentReportPrefix, extension);

                var directory = new DirectoryInfo(docPath);
                // get last created file in directory
                var existingFile = directory.GetFiles(mask).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

                if (existingFile != null && !document.IsReportOutdated)
                {
                    // check if file is actual upon document modified date
                    reportFile = String.Format("{0}/{1}/{2}/{3}", relativePath,
                        document.DocumentId.ToString(), document.SecureFolderName, existingFile.Name);
                }
                else
                {
                    string fileName = String.Format("{0}{1}{4}{2}{4}{3}{5}", Constants.DocumentReportPrefix,
                        DateTimeOffset.Now.Year, DateTimeOffset.Now.ToString("MM"), DateTimeOffset.Now.ToString("dd"),
                        dateSeparator, extension);

                    // create report
                    ExcelReportInfo reportInfo = new ExcelReportInfo
                    {
                        CreatedOf = document.CreatedOf,
                        FileName = fileName,
                        FilePath = docPath,
                        DocumentSubject = "Отчёт №" + document.Number + " от " + document.CreatedOf,
                        SheetName = "Report-" + document.CreatedOf.ToString("dd.MM.yyyy"),
                        TitleLeft = (document.Contractor != null) ? document.Contractor.Code : String.Empty,
                        TitleCenter = document.CreatedOf.ToString("dd.MM.yyyy"),
                        TitleRight = "Док. №" + document.Number + " от " + document.CreatedOf.ToString("dd.MM.yyyy")
                    };

                    ReportHelper.GenerateProductLinesReport(document.Products, reportInfo);

                    document.IsReportOutdated = false;
                    datacontextModel.SaveChanges();
                    reportFile = String.Format("{0}/{1}/{2}/{3}", relativePath,
                        document.DocumentId.ToString(), document.SecureFolderName, fileName);
                }
            }

            return reportFile;
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

            grid.ToolBarSettings.ShowEditButton = true;
            grid.ToolBarSettings.ShowAddButton = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.AddDialogSettings.CloseAfterAdding = true;
        }
    }
}
