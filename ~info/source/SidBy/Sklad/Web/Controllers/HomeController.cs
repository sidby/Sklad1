namespace SidBy.Sklad.Web.Controllers
{
    using log4net;
    using SidBy.Common.Crypt;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Domain.Enums;
    using SidBy.Sklad.Web.BL;
    using SidBy.Sklad.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Trirand.Web.Mvc;

    [Authorize(Roles="admin,employee")]
    public class HomeController : Controller
    {
        private readonly ILog logger;

        public HomeController()
        {
            this.logger = LogManager.GetLogger(base.GetType());
        }

        public int AddNewProductLine(int? documentId, int doctypeId, string docNumber, DateTime docCreatedOf, string docComment, int supplierId, string article, decimal purchaseprice, decimal marginabs, decimal discount, int quantity, string comment)
        {
            ParameterExpression expression;
            int? nullable;
            if ((((supplierId <= 0) || string.IsNullOrEmpty(article)) || (purchaseprice < 0M)) || (doctypeId <= 0))
            {
                return -1;
            }
            SkladDataContext datacontextModel = new SkladDataContext();
            Contractor contractor = datacontextModel.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Constant(supplierId)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(Contractor.get_ContractorTypeId)), Expression.Constant(2, typeof(int)))), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
            if (contractor == null)
            {
                return -1;
            }
            article = article.Trim().Replace(" ", "").ToLower();
            comment = comment.Trim();
            Product product = this.GetOrCreateProduct(datacontextModel, supplierId, purchaseprice, marginabs, article);
            SidBy.Sklad.Domain.Document document = null;
            if (!(documentId.HasValue && !(((nullable = documentId).GetValueOrDefault() <= 0) && nullable.HasValue)))
            {
                DocumentOperation operation = new DocumentOperation();
                docNumber = string.IsNullOrEmpty(docNumber) ? operation.GetDocumentNumber(datacontextModel, DateTime.Now.Year, doctypeId).ToString() : docNumber.Trim().Replace(" ", "");
                string dublicate = this.GenerateFolderName(string.Empty);
                string str2 = this.GenerateFolderName(dublicate);
                SidBy.Sklad.Domain.Document document = new SidBy.Sklad.Domain.Document();
                document.set_DocumentTypeId(doctypeId);
                document.set_CreatedOf(docCreatedOf);
                document.set_Comment(docComment);
                document.set_CreatedAt(DateTime.Now);
                document.set_Number(docNumber);
                document.set_IsCommitted(false);
                document.set_SecureFolderName(dublicate);
                document.set_CommonFolderName(str2);
                document.set_IsReportOutdated(true);
                document = document;
                datacontextModel.get_Documents().Add(document);
                datacontextModel.SaveChanges();
                document = datacontextModel.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_Number)), Expression.Constant(docNumber), false, (MethodInfo) methodof(string.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentTypeId)), Expression.Constant(doctypeId))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Hour)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Hour)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Minute)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Minute)))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            }
            else
            {
                document = datacontextModel.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), typeof(int?)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            }
            if (document == null)
            {
                this.logger.ErrorFormat("Ошибка создания документа № {0} от {1}", docNumber, docCreatedOf);
                return -1;
            }
            document.set_IsReportOutdated(true);
            ProductLine item = datacontextModel.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_ProductId)), Expression.Convert(Expression.Property(Expression.Constant(product), (MethodInfo) methodof(Product.get_ProductId)), typeof(int?))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)))), new ParameterExpression[] { expression })).FirstOrDefault<ProductLine>();
            if (item == null)
            {
                ProductLine line2 = new ProductLine();
                line2.set_ProductArticle(article);
                line2.set_SupplierCode(contractor.get_Code());
                line2.set_SupplierId(new int?(contractor.get_ContractorId()));
                line2.set_Quantity(quantity);
                line2.set_PurchasePrice(purchaseprice);
                line2.set_SalePrice(purchaseprice + marginabs);
                line2.set_MarginAbs(marginabs);
                line2.set_Discount(discount);
                line2.set_Comment(comment);
                line2.set_ProductId(new int?(product.get_ProductId()));
                line2.set_DocumentId(document.get_DocumentId());
                line2.set_Sum(quantity * purchaseprice);
                line2.set_SaleSum(quantity * (purchaseprice + marginabs));
                item = line2;
                if (document.get_Products() == null)
                {
                    document.set_Products(new List<ProductLine>());
                }
                document.get_Products().Add(item);
            }
            else
            {
                item.set_ProductArticle(article);
                item.set_SupplierCode(contractor.get_Code());
                item.set_SupplierId(new int?(contractor.get_ContractorId()));
                item.set_Quantity(quantity);
                item.set_PurchasePrice(purchaseprice);
                item.set_Discount(discount);
                item.set_SalePrice(purchaseprice + marginabs);
                item.set_Comment(comment);
                item.set_ProductId(new int?(product.get_ProductId()));
                item.set_Sum(quantity * purchaseprice);
                item.set_SaleSum(quantity * (purchaseprice + marginabs));
            }
            datacontextModel.SaveChanges();
            return document.get_DocumentId();
        }

        private string CreateRelatedDocument(SkladDataContext datacontextModel, int documentId)
        {
            ParameterExpression expression;
            SidBy.Sklad.Domain.Document parentDoc = datacontextModel.get_Documents().Include("Products").Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            if (parentDoc == null)
            {
                return "/Error";
            }
            int newDocumentTypeId = 0;
            if (parentDoc.get_DocumentTypeId() == 3)
            {
                newDocumentTypeId = 4;
            }
            if (parentDoc.get_DocumentTypeId() == 4)
            {
                newDocumentTypeId = 5;
            }
            if (newDocumentTypeId == 0)
            {
                return "/Error";
            }
            DocumentOperation operation = new DocumentOperation();
            string dublicate = this.GenerateFolderName(string.Empty);
            string str2 = this.GenerateFolderName(dublicate);
            SidBy.Sklad.Domain.Document document = new SidBy.Sklad.Domain.Document();
            document.set_Number(parentDoc.get_Number() + "-" + operation.GetDocumentNumber(datacontextModel, DateTime.Now.Year, newDocumentTypeId).ToString());
            document.set_CreatedOf(parentDoc.get_CreatedOf());
            document.set_ContractorId(parentDoc.get_ContractorId());
            document.set_EmployeeId(parentDoc.get_EmployeeId());
            document.set_CreatedAt(DateTime.Now);
            document.set_FromWarehouseId(parentDoc.get_FromWarehouseId());
            document.set_ToWarehouseId(parentDoc.get_ToWarehouseId());
            document.set_DocumentTypeId(newDocumentTypeId);
            document.set_ParentDocumentId(new int?(parentDoc.get_DocumentId()));
            document.set_SecureFolderName(dublicate);
            document.set_CommonFolderName(str2);
            document.set_IsReportOutdated(true);
            SidBy.Sklad.Domain.Document newDocument = document;
            datacontextModel.get_Documents().Add(newDocument);
            datacontextModel.SaveChanges();
            newDocument = datacontextModel.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_ParentDocumentId)), Expression.Convert(Expression.Property(Expression.Constant(parentDoc), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), typeof(int?))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentTypeId)), Expression.Constant(newDocumentTypeId))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_Number)), Expression.Property(Expression.Constant(newDocument), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_Number)), false, (MethodInfo) methodof(string.op_Equality))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)), Expression.Property(Expression.Property(Expression.Constant(newDocument), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)), Expression.Property(Expression.Property(Expression.Constant(newDocument), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)), Expression.Property(Expression.Property(Expression.Constant(newDocument), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            newDocument.set_Products(new List<ProductLine>());
            List<ProductLine> list = (from x in parentDoc.get_Products()
                where x.get_Shipped() < x.get_Quantity()
                select x).ToList<ProductLine>();
            foreach (ProductLine line in list)
            {
                ProductLine item = new ProductLine();
                item.set_DocumentId(newDocument.get_DocumentId());
                item.set_ProductId(line.get_ProductId());
                item.set_SupplierId(line.get_SupplierId());
                item.set_ProductArticle(line.get_ProductArticle());
                item.set_SupplierCode(line.get_SupplierCode());
                item.set_Quantity(line.get_Quantity());
                item.set_Discount(line.get_Discount());
                item.set_Reserve(line.get_Reserve());
                item.set_Shipped(line.get_Shipped());
                item.set_Available(line.get_Available());
                item.set_PurchasePrice(line.get_PurchasePrice());
                item.set_SalePrice(line.get_SalePrice());
                item.set_MarginAbs(line.get_MarginAbs());
                item.set_Sum(line.get_Sum());
                item.set_SaleSum(line.get_SaleSum());
                item.set_VAT(line.get_VAT());
                item.set_IsPriceIncludesVAT(line.get_IsPriceIncludesVAT());
                item.set_Comment(line.get_Comment());
                newDocument.get_Products().Add(item);
            }
            datacontextModel.SaveChanges();
            return ("/Home/Document?documentId=" + newDocument.get_DocumentId());
        }

        public ActionResult Document(int? documentTypeId, int? documentId)
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid productLineGrid = model.ProductLineGrid;
            SkladDataContext context = new SkladDataContext();
            DocumentProductLineModel model2 = new DocumentProductLineModel {
                WarehouseList = context.get_Warehouses().ToList<Warehouse>(),
                OurCompanyList = context.get_LegalEntities().ToList<LegalEntity>(),
                EmployeeList = context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "x"), (MethodInfo) methodof(UserProfile.get_ContactTypeId)), Expression.Convert(Expression.Constant(1, typeof(int)), typeof(int?))), new ParameterExpression[] { expression })).ToList<UserProfile>(),
                Products = productLineGrid
            };
            if (documentTypeId.HasValue)
            {
                SidBy.Sklad.Domain.Document document = new SidBy.Sklad.Domain.Document();
                document.set_DocumentTypeId(documentTypeId.Value);
                document.set_DocumentType(context.get_DocumentTypes().Where<DocumentType>(Expression.Lambda<Func<DocumentType, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(DocumentType), "x"), (MethodInfo) methodof(DocumentType.get_DocumentTypeId)), typeof(int?)), Expression.Constant(documentTypeId)), new ParameterExpression[] { expression })).First<DocumentType>());
                document.set_CreatedOf(DateTime.Now);
                document.set_IsCommitted(true);
                document.set_IsReportOutdated(true);
                model2.DocumentItem = document;
            }
            else if (documentId.HasValue)
            {
                model2.DocumentItem = context.get_Documents().Include("DocumentType").Include("FromWarehouse").Include("ToWarehouse").Include("Contract").Include("Employee").Include("Contractor").Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), typeof(int?)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Document>();
            }
            if (model2.DocumentItem != null)
            {
                ((dynamic) base.ViewBag).Title = model2.DocumentItem.get_DocumentType().get_Name();
            }
            model2.JGridName = "DocumentGrid1";
            this.DocumentSetupGrid(productLineGrid, documentId);
            return base.View(model2);
        }

        [HttpPost]
        public ActionResult Document(IEnumerable<HttpPostedFileBase> files, int? documentId, int doctypeId)
        {
            int? contractorId = null;
            string str = base.Request.Params["contractorId"].ToString();
            if (!string.IsNullOrEmpty(str))
            {
                int result = 0;
                int.TryParse(str, out result);
                if (result <= 0)
                {
                    return base.Json(new { message = "Сначала выберите контрагента!", documentId = -1 });
                }
                contractorId = new int?(result);
            }
            string docNumber = base.Request.Params["docNumber"];
            DateTime now = DateTime.Now;
            if (base.Request.Params["docCreatedOf"] != null)
            {
                now = Convert.ToDateTime(base.Request.Params["docCreatedOf"].ToString());
            }
            foreach (HttpPostedFileBase base2 in files)
            {
                if (base2.ContentLength > 0)
                {
                    Stream inputStream = base2.InputStream;
                    if (!base2.FileName.EndsWith("xlsx"))
                    {
                        return base.Json(new { message = "Вы можете загружать только файлы Excel 2007-2010!", documentId = -1 });
                    }
                    string fileExtension = ".xlsx";
                    string str3 = base.Server.MapPath("~/uploads");
                    string filename = Path.Combine(str3, base.Session.LCID.ToString() + fileExtension);
                    if (!Directory.Exists(str3))
                    {
                        Directory.CreateDirectory(str3);
                    }
                    base2.SaveAs(filename);
                    List<ProductLine> list = ExcelHelper.GetProductsData(filename, fileExtension, "ProductLine-Template");
                    if (list != null)
                    {
                        int? nullable;
                        ParameterExpression expression;
                        SkladDataContext context = new SkladDataContext();
                        SidBy.Sklad.Domain.Document document = null;
                        if (!(documentId.HasValue && !(((nullable = documentId).GetValueOrDefault() <= 0) && nullable.HasValue)))
                        {
                            DocumentOperation operation = new DocumentOperation();
                            docNumber = string.IsNullOrEmpty(docNumber) ? operation.GetDocumentNumber(context, DateTime.Now.Year, doctypeId).ToString() : docNumber.Trim().Replace(" ", "");
                            string dublicate = this.GenerateFolderName(string.Empty);
                            string str6 = this.GenerateFolderName(dublicate);
                            SidBy.Sklad.Domain.Document document = new SidBy.Sklad.Domain.Document();
                            document.set_DocumentTypeId(doctypeId);
                            document.set_CreatedOf(now);
                            document.set_Comment(string.Empty);
                            document.set_CreatedAt(DateTime.Now);
                            document.set_Number(docNumber);
                            document.set_IsCommitted(false);
                            document.set_SecureFolderName(dublicate);
                            document.set_CommonFolderName(str6);
                            document.set_IsReportOutdated(true);
                            document = document;
                            context.get_Documents().Add(document);
                            context.SaveChanges();
                            document = context.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_Number)), Expression.Constant(docNumber), false, (MethodInfo) methodof(string.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentTypeId)), Expression.Constant(doctypeId))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Year)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Month)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Day)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Hour)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Hour)))), Expression.Equal(Expression.Property(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Minute)), Expression.Property(Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_CreatedAt)), (MethodInfo) methodof(DateTime.get_Minute)))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
                        }
                        else
                        {
                            document = context.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), typeof(int?)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
                        }
                        if (document == null)
                        {
                            return base.Json(new { message = string.Format("Не найден документ {0}!", documentId), documentId = -1 });
                        }
                        Contractor contractor = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_ContractorId)), typeof(int?)), Expression.Constant(contractorId)), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
                        if (contractor == null)
                        {
                            return base.Json(new { message = string.Format("Не найден контрагент {0}!", contractorId), documentId = -1 });
                        }
                        using (List<ProductLine>.Enumerator enumerator2 = list.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                ProductLine productLine = enumerator2.Current;
                                string article = productLine.get_ProductArticle().Trim().ToLower().Replace(" ", "");
                                Contractor contractor2 = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "x"), (MethodInfo) methodof(Contractor.get_Code)), Expression.Call(Expression.Property(Expression.Constant(productLine), (MethodInfo) methodof(ProductLine.get_SupplierCode)), (MethodInfo) methodof(string.Trim), new Expression[0]), false, (MethodInfo) methodof(string.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(Contractor.get_ContractorTypeId)), Expression.Constant(2, typeof(int)))), new ParameterExpression[] { expression })).FirstOrDefault<Contractor>();
                                if (contractor2 == null)
                                {
                                    return base.Json(new { message = string.Format("Фабрика {0} не найдена!", productLine.get_SupplierCode().Trim()), documentId = -1 });
                                }
                                decimal marginabs = 0M;
                                if (productLine.get_MarginAbs() > 0M)
                                {
                                    marginabs = productLine.get_MarginAbs();
                                }
                                else if (productLine.get_SalePrice() > 0M)
                                {
                                    marginabs = Math.Abs((decimal) (productLine.get_SalePrice() - productLine.get_PurchasePrice()));
                                    if (marginabs == 0M)
                                    {
                                        marginabs = contractor.get_MarginAbs();
                                    }
                                }
                                else
                                {
                                    marginabs = contractor.get_MarginAbs();
                                }
                                Product product = this.GetOrCreateProduct(context, contractor2.get_ContractorId(), productLine.get_PurchasePrice(), marginabs, article);
                                ProductLine item = context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_ProductId)), Expression.Convert(Expression.Property(Expression.Constant(product), (MethodInfo) methodof(Product.get_ProductId)), typeof(int?))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)))), new ParameterExpression[] { expression })).FirstOrDefault<ProductLine>();
                                string str8 = productLine.get_Comment();
                                if (str8 != null)
                                {
                                    str8 = str8.Trim();
                                }
                                if (item == null)
                                {
                                    ProductLine line2 = new ProductLine();
                                    line2.set_ProductArticle(article);
                                    line2.set_SupplierCode(contractor2.get_Code());
                                    line2.set_SupplierId(new int?(contractor2.get_ContractorId()));
                                    line2.set_Quantity(productLine.get_Quantity());
                                    line2.set_PurchasePrice(productLine.get_PurchasePrice());
                                    line2.set_SalePrice(productLine.get_PurchasePrice() + marginabs);
                                    line2.set_MarginAbs(marginabs);
                                    line2.set_Discount(productLine.get_Discount());
                                    line2.set_Comment(str8);
                                    line2.set_ProductId(new int?(product.get_ProductId()));
                                    line2.set_DocumentId(document.get_DocumentId());
                                    line2.set_Sum(productLine.get_Quantity() * productLine.get_PurchasePrice());
                                    line2.set_SaleSum(productLine.get_Quantity() * (productLine.get_PurchasePrice() + marginabs));
                                    item = line2;
                                    if (document.get_Products() == null)
                                    {
                                        document.set_Products(new List<ProductLine>());
                                    }
                                    document.get_Products().Add(item);
                                }
                                else
                                {
                                    item.set_ProductArticle(article);
                                    item.set_SupplierCode(contractor2.get_Code());
                                    item.set_SupplierId(new int?(contractor2.get_ContractorId()));
                                    item.set_Quantity(productLine.get_Quantity());
                                    item.set_PurchasePrice(productLine.get_PurchasePrice());
                                    item.set_SalePrice(productLine.get_PurchasePrice() + marginabs);
                                    item.set_Comment(str8);
                                    item.set_Discount(productLine.get_Discount());
                                    item.set_ProductId(new int?(product.get_ProductId()));
                                    item.set_Sum(productLine.get_Quantity() * productLine.get_PurchasePrice());
                                    item.set_SaleSum(productLine.get_Quantity() * (productLine.get_PurchasePrice() + marginabs));
                                }
                                document.set_IsReportOutdated(true);
                                context.SaveChanges();
                            }
                        }
                        document.set_IsReportOutdated(true);
                        context.SaveChanges();
                        return base.Json(new { message = "Документ успешно обновлен/добавлен", documentId = document.get_DocumentId() });
                    }
                }
            }
            return base.Json(new { message = "Возникла критическая ошибка!", documentId = -1 });
        }

        public ActionResult DocumentEditRows(ProductLine editedItem)
        {
            ProductLine line;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            int documentId = -1;
            if (model.ProductLineGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                line = context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_ProductLineId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(ProductLine.get_ProductLineId))), new ParameterExpression[] { expression })).First<ProductLine>();
                this.UpdateProductLine(line, editedItem);
                documentId = line.get_DocumentId();
                context.SaveChanges();
            }
            if (model.ProductLineGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                line = context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_ProductLineId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(ProductLine.get_ProductLineId))), new ParameterExpression[] { expression })).First<ProductLine>();
                documentId = line.get_DocumentId();
                context.get_ProductLines().Remove(line);
                context.SaveChanges();
            }
            SidBy.Sklad.Domain.Document document = context.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            if (document != null)
            {
                document.set_IsReportOutdated(true);
                context.SaveChanges();
            }
            return base.RedirectToAction("Document", "Home", new { documentId = documentId });
        }

        public ActionResult DocumentItemEditRows(SidBy.Sklad.Domain.Document editedItem)
        {
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.DocumentGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                return base.RedirectToAction("Document", new { documentId = editedItem.get_DocumentId() });
            }
            if (model.DocumentGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                ParameterExpression expression;
                SidBy.Sklad.Domain.Document document = context.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Document>();
                new DocumentOperation().UpdateDocument(context, document.get_DocumentId(), true);
                context.get_Documents().Remove(document);
                context.SaveChanges();
                this.logger.InfoFormat("удален документ {0} от {1}", editedItem.get_Number(), editedItem.get_CreatedOf());
            }
            return this.RedirectToDocumentList(editedItem.get_DocumentTypeId());
        }

        public JsonResult DocumentSearchGridDataRequested(int? documentId)
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.DocumentSetupGrid(model.ProductLineGrid, documentId);
            if (!documentId.HasValue)
            {
                documentId = 0;
            }
            return model.ProductLineGrid.DataBind(context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Convert(Expression.Constant(documentId), typeof(int))), new ParameterExpression[] { expression })));
        }

        private void DocumentSetupGrid(JQGrid grid, int? documentId)
        {
            this.SetUpGrid(grid, "DocumentGrid1", base.Url.Action("DocumentSearchGridDataRequested", new { documentId = documentId }), base.Url.Action("DocumentEditRows"));
            grid.ToolBarSettings.ShowAddButton = false;
            this.SetUpProductLineColumns(grid, documentId);
        }

        private string GenerateFolderName(string dublicate)
        {
            int num = 5;
            if (dublicate == null)
            {
                dublicate = string.Empty;
            }
            string strA = RandomGenerator.GenerateRandomText(num);
            if (string.Compare(strA, dublicate, true) == 0)
            {
                strA = RandomGenerator.GenerateRandomText(num + 1);
            }
            return strA;
        }

        public JsonResult GetContractorByCode(string term)
        {
            ParameterExpression expression;
            SkladDataContext context = new SkladDataContext();
            var data = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "u"), (MethodInfo) methodof(Contractor.get_Code)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), new ParameterExpression[] { expression })).Select(Expression.Lambda(Expression.New((ConstructorInfo) methodof(<>f__AnonymousType8<string, int, string, decimal>..ctor, <>f__AnonymousType8<string, int, string, decimal>), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(Contractor), "u"), (MethodInfo) methodof(Contractor.get_Code)), Expression.Property(expression, (MethodInfo) methodof(Contractor.get_ContractorId)), Expression.Property(expression, (MethodInfo) methodof(Contractor.get_Name)), Expression.Property(expression, (MethodInfo) methodof(Contractor.get_MarginAbs)) }, new MethodInfo[] { (MethodInfo) methodof(<>f__AnonymousType8<string, int, string, decimal>.get_Code, <>f__AnonymousType8<string, int, string, decimal>), (MethodInfo) methodof(<>f__AnonymousType8<string, int, string, decimal>.get_ContractorId, <>f__AnonymousType8<string, int, string, decimal>), (MethodInfo) methodof(<>f__AnonymousType8<string, int, string, decimal>.get_Name, <>f__AnonymousType8<string, int, string, decimal>), (MethodInfo) methodof(<>f__AnonymousType8<string, int, string, decimal>.get_MarginAbs, <>f__AnonymousType8<string, int, string, decimal>) }), new ParameterExpression[] { expression })).ToList();
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContractorByCodeAutoComplete(string term)
        {
            ParameterExpression expression;
            SkladDataContext context = new SkladDataContext();
            List<string> list = context.get_Contractors().Where<Contractor>(Expression.Lambda<Func<Contractor, bool>>(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "u"), (MethodInfo) methodof(Contractor.get_Code)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), new ParameterExpression[] { expression })).Select<Contractor, string>(Expression.Lambda<Func<Contractor, string>>(Expression.Property(expression = Expression.Parameter(typeof(Contractor), "u"), (MethodInfo) methodof(Contractor.get_Code)), new ParameterExpression[] { expression })).ToList<string>();
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = list };
        }

        private Product GetOrCreateProduct(SkladDataContext datacontextModel, int supplierId, decimal purchaseprice, decimal marginabs, string article)
        {
            ParameterExpression expression;
            Product product = datacontextModel.get_Products().Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.AndAlso(Expression.Equal(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(Product), "x"), (MethodInfo) methodof(Product.get_Article)), (MethodInfo) methodof(string.ToLower), new Expression[0]), Expression.Constant(article), false, (MethodInfo) methodof(string.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(Product.get_ContractorId)), Expression.Constant(supplierId))), new ParameterExpression[] { expression })).FirstOrDefault<Product>();
            if (product == null)
            {
                Product product3 = new Product();
                product3.set_Article(article);
                product3.set_ContractorId(supplierId);
                product3.set_PurchasePrice(purchaseprice);
                product3.set_SalePrice(purchaseprice + marginabs);
                product3.set_CreatedAt(DateTime.Now);
                Product product2 = product3;
                datacontextModel.get_Products().Add(product2);
                datacontextModel.SaveChanges();
                product = datacontextModel.get_Products().Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.AndAlso(Expression.Equal(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(Product), "x"), (MethodInfo) methodof(Product.get_Article)), (MethodInfo) methodof(string.ToLower), new Expression[0]), Expression.Constant(article), false, (MethodInfo) methodof(string.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(Product.get_ContractorId)), Expression.Constant(supplierId))), new ParameterExpression[] { expression })).FirstOrDefault<Product>();
            }
            return product;
        }

        public JsonResult GetProductByArticle(string term, int supplierId)
        {
            ParameterExpression expression;
            if (supplierId <= 0)
            {
                return null;
            }
            SkladDataContext context = new SkladDataContext();
            var list = context.get_Products().Where<Product>(Expression.Lambda<Func<Product, bool>>(Expression.AndAlso(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(Product), "u"), (MethodInfo) methodof(Product.get_Article)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(Product.get_ContractorId)), Expression.Constant(supplierId))), new ParameterExpression[] { expression })).Select(Expression.Lambda(Expression.New((ConstructorInfo) methodof(<>f__AnonymousType7<string, decimal>..ctor, <>f__AnonymousType7<string, decimal>), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(Product), "u"), (MethodInfo) methodof(Product.get_Article)), Expression.Property(expression, (MethodInfo) methodof(Product.get_PurchasePrice)) }, new MethodInfo[] { (MethodInfo) methodof(<>f__AnonymousType7<string, decimal>.get_Article, <>f__AnonymousType7<string, decimal>), (MethodInfo) methodof(<>f__AnonymousType7<string, decimal>.get_PurchasePrice, <>f__AnonymousType7<string, decimal>) }), new ParameterExpression[] { expression })).ToList();
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = list };
        }

        private string GetReportFile(SkladDataContext datacontextModel, SidBy.Sklad.Domain.Document document, EntityEnum.ReportType type)
        {
            if (document == null)
            {
                this.logger.ErrorFormat("GetReportFile. Document is null", new object[0]);
                throw new ArgumentException("GetReportFile. Document is null");
            }
            bool flag = false;
            if (string.IsNullOrEmpty(document.get_SecureFolderName()))
            {
                document.set_SecureFolderName(this.GenerateFolderName(document.get_CommonFolderName()));
                flag = true;
            }
            if (string.IsNullOrEmpty(document.get_CommonFolderName()))
            {
                document.set_CommonFolderName(this.GenerateFolderName(document.get_SecureFolderName()));
                flag = true;
            }
            if (flag)
            {
                datacontextModel.SaveChanges();
            }
            string path = "../Reports/Document";
            string str2 = base.Server.MapPath(path);
            string str3 = string.Empty;
            if (type != 1)
            {
                return str3;
            }
            string str4 = Path.Combine(str2, document.get_DocumentId().ToString(), document.get_SecureFolderName());
            if (!Directory.Exists(str4))
            {
                Directory.CreateDirectory(str4);
            }
            string str5 = "-";
            string str6 = ".xls";
            string searchPattern = string.Format("{0}*{1}", "Report-", str6);
            DirectoryInfo info = new DirectoryInfo(str4);
            FileInfo info2 = (from f in info.GetFiles(searchPattern)
                orderby f.LastWriteTime descending
                select f).FirstOrDefault<FileInfo>();
            if (!((info2 == null) || document.get_IsReportOutdated()))
            {
                return string.Format("{0}/{1}/{2}/{3}", new object[] { path, document.get_DocumentId().ToString(), document.get_SecureFolderName(), info2.Name });
            }
            string fileName = string.Format("{0}{1}{4}{2}{4}{3}{5}", new object[] { "Report-", DateTimeOffset.Now.Year, DateTimeOffset.Now.ToString("MM"), DateTimeOffset.Now.ToString("dd"), str5, str6 });
            ReportHelper.GenerateSalesReport(document, str4, fileName);
            document.set_IsReportOutdated(false);
            datacontextModel.SaveChanges();
            return string.Format("{0}/{1}/{2}/{3}", new object[] { path, document.get_DocumentId().ToString(), document.get_SecureFolderName(), fileName });
        }

        public string GetReportFileLink(int documentId, string docNumber, DateTime docCreatedOf, string docComment, int? docContractorId, int? docEmployeeId, DateTime? docPlanDate)
        {
            ParameterExpression expression;
            if (documentId <= 0)
            {
                return "/Error";
            }
            SkladDataContext datacontextModel = new SkladDataContext();
            SidBy.Sklad.Domain.Document document = datacontextModel.get_Documents().Include("Products").Include("Contractor").Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            if (document == null)
            {
                return "/Error";
            }
            if ((document.get_DocumentTypeId() == 1) || (document.get_DocumentTypeId() == 2))
            {
                docContractorId = null;
                docEmployeeId = null;
                docPlanDate = null;
            }
            if ((((document.get_DocumentTypeId() == 3) || (document.get_DocumentTypeId() == 5)) || (document.get_DocumentTypeId() == 4)) && !(docContractorId.HasValue && docEmployeeId.HasValue))
            {
                return "/Error";
            }
            if (!string.IsNullOrEmpty(docNumber))
            {
                document.set_Number(docNumber);
            }
            document.set_CreatedOf(docCreatedOf);
            document.set_Comment(docComment);
            document.set_ContractorId(docContractorId);
            document.set_EmployeeId(docEmployeeId);
            document.set_PlannedDate(docPlanDate);
            document.set_Sum(datacontextModel.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId))), new ParameterExpression[] { expression })).Sum<ProductLine>(Expression.Lambda<Func<ProductLine, decimal>>(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_Sum)), new ParameterExpression[] { expression })));
            document.set_SaleSum(datacontextModel.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId))), new ParameterExpression[] { expression })).Sum<ProductLine>(Expression.Lambda<Func<ProductLine, decimal>>(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_SaleSum)), new ParameterExpression[] { expression })));
            return this.GetReportFile(datacontextModel, document, 1);
        }

        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return base.View();
        }

        public ActionResult RedirectToDocumentList(int documentId)
        {
            switch (documentId)
            {
                case 1:
                    return base.RedirectToAction("Posting", "Warehouse");

                case 2:
                    return base.RedirectToAction("Cancellation", "Warehouse");

                case 3:
                    return base.RedirectToAction("CustomerOrders", "Sale");

                case 4:
                    return base.RedirectToAction("Shipment", "Sale");

                case 5:
                    return base.RedirectToAction("Refunds", "Sale");
            }
            return base.RedirectToAction("Index", "Indicators");
        }

        public string SaveDocument(int documentId, string docNumber, DateTime docCreatedOf, string docComment, bool docIsComitted, int? docContractorId, int? docEmployeeId, DateTime? docPlanDate, bool createRelatedDoc)
        {
            ParameterExpression expression;
            if (documentId <= 0)
            {
                return "/Error";
            }
            SkladDataContext context = new SkladDataContext();
            SidBy.Sklad.Domain.Document document = context.get_Documents().Include("Products").Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Constant(documentId)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
            if (document == null)
            {
                return "/Error";
            }
            if ((document.get_DocumentTypeId() == 1) || (document.get_DocumentTypeId() == 2))
            {
                docContractorId = null;
                docEmployeeId = null;
                docPlanDate = null;
            }
            if ((((document.get_DocumentTypeId() == 3) || (document.get_DocumentTypeId() == 5)) || (document.get_DocumentTypeId() == 4)) && !(docContractorId.HasValue && docEmployeeId.HasValue))
            {
                return "/Error";
            }
            if (!string.IsNullOrEmpty(docNumber))
            {
                document.set_Number(docNumber);
            }
            document.set_CreatedOf(docCreatedOf);
            document.set_Comment(docComment);
            document.set_ContractorId(docContractorId);
            document.set_EmployeeId(docEmployeeId);
            document.set_PlannedDate(docPlanDate);
            document.set_IsReportOutdated(true);
            document.set_Sum(context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId))), new ParameterExpression[] { expression })).Sum<ProductLine>(Expression.Lambda<Func<ProductLine, decimal>>(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_Sum)), new ParameterExpression[] { expression })));
            document.set_SaleSum(context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId))), new ParameterExpression[] { expression })).Sum<ProductLine>(Expression.Lambda<Func<ProductLine, decimal>>(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_SaleSum)), new ParameterExpression[] { expression })));
            DocumentOperation operation = new DocumentOperation();
            operation.UpdateDocument(context, document.get_DocumentId(), false);
            if (createRelatedDoc)
            {
                docIsComitted = true;
            }
            document.set_IsCommitted(docIsComitted);
            context.SaveChanges();
            if (document.get_ParentDocumentId().HasValue)
            {
                SidBy.Sklad.Domain.Document document = context.get_Documents().Include("Products").Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), typeof(int?)), Expression.Property(Expression.Constant(document), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_ParentDocumentId))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>();
                if ((document != null) && (document.get_Products() != null))
                {
                    using (IEnumerator<ProductLine> enumerator = document.get_Products().GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ProductLine product = enumerator.Current;
                            int? nullable = 0;
                            nullable = new int?(context.get_ProductLines().Where<ProductLine>(Expression.Lambda<Func<ProductLine, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_ProductId)), Expression.Property(Expression.Constant(product), (MethodInfo) methodof(ProductLine.get_ProductId))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(ProductLine.get_DocumentId)), Expression.Constant(documentId))), new ParameterExpression[] { expression })).Select<ProductLine, int>(Expression.Lambda<Func<ProductLine, int>>(Expression.Property(expression = Expression.Parameter(typeof(ProductLine), "x"), (MethodInfo) methodof(ProductLine.get_Quantity)), new ParameterExpression[] { expression })).FirstOrDefault<int>());
                            if (nullable.HasValue)
                            {
                                product.set_Shipped(nullable.Value);
                            }
                        }
                    }
                    context.SaveChanges();
                }
            }
            if (document.get_Products() != null)
            {
                operation.UpdateDocumentProducts(document.get_Products().ToList<ProductLine>(), false);
            }
            if (createRelatedDoc)
            {
                return this.CreateRelatedDocument(context, documentId);
            }
            return DocumentOperation.UrlToDocumentList(document.get_DocumentTypeId());
        }

        private void SetUpGrid(JQGrid grid, string gridID, string dataUrl, string editUrl)
        {
            grid.ID = gridID;
            grid.DataUrl = dataUrl;
            grid.EditUrl = editUrl;
            grid.ToolBarSettings.ShowSearchToolBar = true;
            grid.ToolBarSettings.ShowEditButton = true;
            grid.ToolBarSettings.ShowAddButton = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.AddDialogSettings.CloseAfterAdding = true;
        }

        private void SetUpProductLineColumns(JQGrid itemGrid, int? documentId)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(base.Request.QueryString["documentTypeId"]))
            {
                int.TryParse(base.Request.QueryString["documentTypeId"], out result);
            }
            if ((result == 0) && documentId.HasValue)
            {
                ParameterExpression expression;
                SkladDataContext context = new SkladDataContext();
                result = context.get_Documents().Where<SidBy.Sklad.Domain.Document>(Expression.Lambda<Func<SidBy.Sklad.Domain.Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Document), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Document.get_DocumentId)), Expression.Convert(Expression.Constant(documentId), typeof(int))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Document>().get_DocumentTypeId();
            }
            if (result > 0)
            {
                if (((result == 3) || (result == 4)) || (result == 5))
                {
                    JQGridColumn column = itemGrid.Columns.Find(c => c.DataField == "SalePrice");
                    column.Visible = true;
                    column.Editable = false;
                    itemGrid.Columns.Find(c => c.DataField == "SaleSum").Visible = true;
                    JQGridColumn column3 = itemGrid.Columns.Find(c => c.DataField == "MarginAbs");
                    column3.Visible = true;
                    column3.Editable = true;
                }
                if (result == 3)
                {
                    itemGrid.Columns.Find(c => c.DataField == "Shipped").Visible = true;
                }
            }
        }

        private void UpdateProductLine(ProductLine item, ProductLine editedItem)
        {
            item.set_PurchasePrice(editedItem.get_PurchasePrice());
            item.set_SalePrice(editedItem.get_PurchasePrice() + editedItem.get_MarginAbs());
            item.set_MarginAbs(editedItem.get_MarginAbs());
            item.set_Quantity(editedItem.get_Quantity());
            item.set_Discount(editedItem.get_Discount());
            item.set_Comment((editedItem.get_Comment() ?? string.Empty).Trim());
            item.set_Sum(editedItem.get_Quantity() * editedItem.get_PurchasePrice());
            item.set_SaleSum(editedItem.get_Quantity() * (editedItem.get_PurchasePrice() + editedItem.get_MarginAbs()));
        }
    }
}

