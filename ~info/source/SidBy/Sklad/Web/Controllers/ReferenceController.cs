namespace SidBy.Sklad.Web.Controllers
{
    using log4net;
    using SidBy.Common.Helpers;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using Trirand.Web.Mvc;

    [Authorize(Roles="admin,employee")]
    public class ReferenceController : Controller
    {
        private readonly ILog logger;

        public ReferenceController()
        {
            this.logger = LogManager.GetLogger(base.GetType());
        }

        public void AddNewProduct(int contractorId, string article, decimal purchaseprice, decimal saleprice, decimal vat)
        {
            SidBy.Sklad.Domain.Product item;
            if ((contractorId > 0) && !string.IsNullOrEmpty(article))
            {
                ParameterExpression expression;
                SidBy.Sklad.Domain.Product product = new SidBy.Sklad.Domain.Product();
                product.set_Article(article);
                product.set_PurchasePrice(purchaseprice);
                product.set_SalePrice(saleprice);
                product.set_VAT(vat);
                product.set_ContractorId(contractorId);
                item = product;
                SkladDataContext context = new SkladDataContext();
                this.PrepareProductData(item);
                item.set_Supplier(context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)), Expression.Property(Expression.Constant(item), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ContractorId))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorTypeId)), Expression.Constant(2, typeof(int)))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Contractor>());
                if (item.get_Supplier() == null)
                {
                    this.logger.ErrorFormat("для товара {0} не найден поставщик с id={1}", item.get_Article(), item.get_ContractorId());
                }
                else
                {
                    string str = this.ValidateProductData(context.get_Products(), item, true);
                    if (!string.IsNullOrEmpty(str))
                    {
                        this.logger.Error(str);
                    }
                    else
                    {
                        context.get_Products().Add(item);
                        context.SaveChanges();
                        this.logger.InfoFormat("Создан товар {0} от поставщика {1}", item.get_Article(), item.get_Supplier().get_Code());
                    }
                }
            }
        }

        public ActionResult Contractor()
        {
            ((dynamic) base.ViewBag).Title = "Контрагент";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid contractorGrid = model.ContractorGrid;
            this.ContractorSetupGrid(contractorGrid);
            return base.View(model);
        }

        public ActionResult ContractorEditRows(SidBy.Sklad.Domain.Contractor editedItem)
        {
            string str;
            SidBy.Sklad.Domain.Contractor contractor;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.ContractorGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                this.PrepareContractorData(editedItem);
                str = this.ValidateContractorData(context.get_Contractors(), editedItem, false);
                if (!string.IsNullOrEmpty(str))
                {
                    return model.ContractorGrid.ShowEditValidationMessage(str);
                }
                contractor = context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Contractor>();
                this.UpdateContractor(contractor, editedItem);
                context.SaveChanges();
                this.logger.InfoFormat("контрагент изменён {0}", contractor.get_Code());
            }
            if (model.ContractorGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                this.PrepareContractorData(editedItem);
                str = this.ValidateContractorData(context.get_Contractors(), editedItem, true);
                if (!string.IsNullOrEmpty(str))
                {
                    return model.ContractorGrid.ShowEditValidationMessage(str);
                }
                SidBy.Sklad.Domain.Contractor contractor2 = new SidBy.Sklad.Domain.Contractor();
                contractor2.set_IsArchived(false);
                contractor2.set_CreatedAt(DateTime.Now);
                contractor = contractor2;
                this.UpdateContractor(contractor, editedItem);
                context.get_Contractors().Add(contractor);
                context.SaveChanges();
                this.logger.InfoFormat("контрагент добавлен {0}", contractor.get_Code());
            }
            if (model.ContractorGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                contractor = context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Contractor>();
                context.get_Contractors().Remove(contractor);
                context.SaveChanges();
                this.logger.InfoFormat("контрагент удалён {0}", contractor.get_Code());
            }
            return base.RedirectToAction("Contractor", "Reference");
        }

        public JsonResult ContractorSearchGridDataRequested()
        {
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.ContractorSetupGrid(model.ContractorGrid);
            return model.ContractorGrid.DataBind(context.get_Contractors());
        }

        private void ContractorSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "ContractorGrid1", base.Url.Action("ContractorSearchGridDataRequested"), base.Url.Action("ContractorEditRows"));
            this.SetUpContractorEditDropDown(grid);
        }

        public JsonResult GetSupplierByCode(string term)
        {
            ParameterExpression expression;
            SkladDataContext context = new SkladDataContext();
            var data = context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.AndAlso(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "u"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorTypeId)), Expression.Constant(2, typeof(int)))), new ParameterExpression[] { expression })).Select(Expression.Lambda(Expression.New((ConstructorInfo) methodof(<>f__AnonymousTypeb<string, int, string>..ctor, <>f__AnonymousTypeb<string, int, string>), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "u"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)), Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Name)) }, new MethodInfo[] { (MethodInfo) methodof(<>f__AnonymousTypeb<string, int, string>.get_Code, <>f__AnonymousTypeb<string, int, string>), (MethodInfo) methodof(<>f__AnonymousTypeb<string, int, string>.get_ContractorId, <>f__AnonymousTypeb<string, int, string>), (MethodInfo) methodof(<>f__AnonymousTypeb<string, int, string>.get_Name, <>f__AnonymousTypeb<string, int, string>) }), new ParameterExpression[] { expression })).ToList();
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupplierByCodeAutoComplete(string term)
        {
            ParameterExpression expression;
            SkladDataContext context = new SkladDataContext();
            List<string> list = context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.AndAlso(Expression.Call(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "u"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), (MethodInfo) methodof(string.ToLower), new Expression[0]), (MethodInfo) methodof(string.Contains), new Expression[] { Expression.Call(Expression.Constant(term), (MethodInfo) methodof(string.ToLower), new Expression[0]) }), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorTypeId)), Expression.Constant(2, typeof(int)))), new ParameterExpression[] { expression })).Select<SidBy.Sklad.Domain.Contractor, string>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, string>>(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "u"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), new ParameterExpression[] { expression })).ToList<string>();
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = list };
        }

        private void PrepareContractorData(SidBy.Sklad.Domain.Contractor contractor)
        {
            contractor.set_Name((contractor.get_Name() ?? string.Empty).Trim());
            contractor.set_Code((contractor.get_Code() ?? string.Empty).Trim());
            contractor.set_Phone((contractor.get_Phone() ?? string.Empty).Trim());
            contractor.set_Fax((contractor.get_Fax() ?? string.Empty).Trim());
            contractor.set_Email((contractor.get_Email() ?? string.Empty).Trim());
            contractor.set_ActualAddress((contractor.get_ActualAddress() ?? string.Empty).Trim());
            contractor.set_Comment((contractor.get_Comment() ?? string.Empty).Trim());
            contractor.set_ContactPersonName((contractor.get_ContactPersonName() ?? string.Empty).Trim());
            if (string.IsNullOrEmpty(contractor.get_Name()))
            {
                contractor.set_Name(contractor.get_Code());
            }
            if (string.IsNullOrEmpty(contractor.get_Code()))
            {
                contractor.set_Code(contractor.get_Name());
            }
        }

        private void PrepareProductData(SidBy.Sklad.Domain.Product product)
        {
            product.set_Article((product.get_Article() ?? string.Empty).Trim().Replace(" ", ""));
            product.set_Description((product.get_Description() ?? string.Empty).Trim());
        }

        public ActionResult Product()
        {
            ((dynamic) base.ViewBag).Title = "Товар";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid productGrid = model.ProductGrid;
            this.ProductSetupGrid(productGrid);
            return base.View(model);
        }

        public ActionResult ProductEditRows(SidBy.Sklad.Domain.Product editedItem)
        {
            SidBy.Sklad.Domain.Product product;
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            if (model.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                product = context.get_Products().Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Product>();
                this.UpdateProduct(product, editedItem);
                context.SaveChanges();
            }
            if (model.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                product = context.get_Products().Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId)), Expression.Property(Expression.Constant(editedItem), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Product>();
                context.get_Products().Remove(product);
                context.SaveChanges();
            }
            return base.RedirectToAction("Product", "Reference");
        }

        public JsonResult ProductSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.ProductSetupGrid(model.ProductGrid);
            string contractorIdStr = base.Request.QueryString["ContractorId"];
            if (!string.IsNullOrEmpty(contractorIdStr))
            {
                SidBy.Sklad.Domain.Contractor contractor = context.get_Contractors().Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Call(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), (MethodInfo) methodof(string.ToLower), new Expression[0]), Expression.Call(Expression.Constant(contractorIdStr), (MethodInfo) methodof(string.ToLower), new Expression[0]), false, (MethodInfo) methodof(string.op_Equality)), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Contractor>();
                if (contractor != null)
                {
                    return model.ProductGrid.DataBind(QueryableExtensions.Include<SidBy.Sklad.Domain.Product, SidBy.Sklad.Domain.Contractor>(context.get_Products().Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ContractorId)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId))), new ParameterExpression[] { expression })), Expression.Lambda<Func<SidBy.Sklad.Domain.Product, SidBy.Sklad.Domain.Contractor>>(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Supplier)), new ParameterExpression[] { expression })));
                }
            }
            return model.ProductGrid.DataBind(QueryableExtensions.Include<SidBy.Sklad.Domain.Product, SidBy.Sklad.Domain.Contractor>(context.get_Products(), Expression.Lambda<Func<SidBy.Sklad.Domain.Product, SidBy.Sklad.Domain.Contractor>>(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Supplier)), new ParameterExpression[] { expression })));
        }

        private void ProductSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "ProductGrid1", base.Url.Action("ProductSearchGridDataRequested"), base.Url.Action("ProductEditRows"));
            grid.ToolBarSettings.ShowAddButton = false;
            this.SetUpProductSearchData(grid);
        }

        private void SetUpContractorEditDropDown(JQGrid itemGrid)
        {
            JQGridColumn column = itemGrid.Columns.Find(c => c.DataField == "ResponsibleId");
            JQGridColumn column2 = itemGrid.Columns.Find(c => c.DataField == "ContractorTypeId");
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                ParameterExpression expression;
                SkladDataContext context = new SkladDataContext();
                IEnumerable<SelectListItem> source = from x in context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.LessThan(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "m"), (MethodInfo) methodof(UserProfile.get_ContactTypeId)), Expression.Convert(Expression.Constant(3, typeof(int)), typeof(int?))), new ParameterExpression[] { expression })).AsEnumerable<UserProfile>() select new SelectListItem { Text = x.get_DisplayName(), Value = x.get_UserId().ToString() };
                IEnumerable<SelectListItem> enumerable2 = from x in (from m in context.get_ContractorTypes() select m).AsEnumerable<ContractorType>() select new SelectListItem { Text = x.get_Name(), Value = x.get_ContractorTypeId().ToString() };
                column.EditList.AddRange(source.ToList<SelectListItem>());
                column.SearchList = source.ToList<SelectListItem>();
                SelectListItem item = new SelectListItem {
                    Text = "Все",
                    Value = ""
                };
                column.SearchList.Insert(0, item);
                column2.SearchList = enumerable2.ToList<SelectListItem>();
                SelectListItem item2 = new SelectListItem {
                    Text = "Все",
                    Value = ""
                };
                column2.SearchList.Insert(0, item2);
                column2.EditList.AddRange(enumerable2.ToList<SelectListItem>());
            }
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

        private void SetUpProductSearchData(JQGrid itemGrid)
        {
            JQGridColumn column = itemGrid.Columns.Find(c => c.DataField == "ContractorId");
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
            }
        }

        private void UpdateContractor(SidBy.Sklad.Domain.Contractor item, SidBy.Sklad.Domain.Contractor editedItem)
        {
            item.set_Name((editedItem.get_Name() ?? string.Empty).Trim());
            item.set_Code((editedItem.get_Code() ?? string.Empty).Trim());
            item.set_Phone((editedItem.get_Phone() ?? string.Empty).Trim());
            item.set_Fax((editedItem.get_Fax() ?? string.Empty).Trim());
            item.set_Email((editedItem.get_Email() ?? string.Empty).Trim());
            item.set_Region((editedItem.get_Region() ?? string.Empty).Trim());
            item.set_ActualAddress((editedItem.get_ActualAddress() ?? string.Empty).Trim());
            item.set_Comment((editedItem.get_Comment() ?? string.Empty).Trim());
            item.set_ResponsibleId(editedItem.get_ResponsibleId());
            item.set_ContractorTypeId(editedItem.get_ContractorTypeId());
            item.set_ContactPersonName(editedItem.get_ContactPersonName());
            item.set_MarginAbs(editedItem.get_MarginAbs());
        }

        private void UpdateProduct(SidBy.Sklad.Domain.Product item, SidBy.Sklad.Domain.Product editedItem)
        {
            item.set_Article((editedItem.get_Article() ?? string.Empty).Trim());
            item.set_PurchasePrice(editedItem.get_PurchasePrice());
            item.set_SalePrice(editedItem.get_SalePrice());
            item.set_VAT(editedItem.get_VAT());
            item.set_ContractorId(editedItem.get_ContractorId());
            item.set_Description((editedItem.get_Description() ?? string.Empty).Trim());
        }

        private string ValidateContractorData(DbSet<SidBy.Sklad.Domain.Contractor> dbSet, SidBy.Sklad.Domain.Contractor contractor, bool isNew)
        {
            ParameterExpression expression;
            if (string.IsNullOrEmpty(contractor.get_Name()))
            {
                return "Имя пользователя обязательно";
            }
            if (string.IsNullOrEmpty(contractor.get_Code()))
            {
                return "Отображаемое имя обязательно";
            }
            if (isNew)
            {
                if (dbSet.Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.OrElse(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Name)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Name)), false, (MethodInfo) methodof(string.op_Equality))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Contractor>() != null)
                {
                    return "Контрагент с таким именем существует";
                }
            }
            else
            {
                SidBy.Sklad.Domain.Contractor contractor2 = dbSet.Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Contractor>();
                if ((contractor2.get_Name().ToLower() != contractor.get_Name().ToLower()) && (dbSet.Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Name)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Name)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Contractor>() != null))
                {
                    return "Контрагент с таким именем существует";
                }
                if ((contractor2.get_Code().ToLower() != contractor.get_Code().ToLower()) && (dbSet.Where<SidBy.Sklad.Domain.Contractor>(Expression.Lambda<Func<SidBy.Sklad.Domain.Contractor, bool>>(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Contractor), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), Expression.Property(Expression.Constant(contractor), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_Code)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Contractor>() != null))
                {
                    return "Контрагент с таким именем существует";
                }
            }
            if (!string.IsNullOrEmpty(contractor.get_Email()) && !ValidationHelper.IsValidEmail(contractor.get_Email()))
            {
                return "Неверный формат email";
            }
            return string.Empty;
        }

        private string ValidateProductData(DbSet<SidBy.Sklad.Domain.Product> dbSet, SidBy.Sklad.Domain.Product product, bool isNew)
        {
            ParameterExpression expression;
            if (string.IsNullOrEmpty(product.get_Article()))
            {
                return "Артикул товара обязателен";
            }
            if (product.get_Supplier() == null)
            {
                return "Не указан поставщик товара";
            }
            if (isNew)
            {
                if (dbSet.Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.AndAlso(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Article)), Expression.Property(Expression.Constant(product), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Article)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), Expression.Equal(Expression.Property(expression, (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ContractorId)), Expression.Property(Expression.Property(Expression.Constant(product), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Supplier)), (MethodInfo) methodof(SidBy.Sklad.Domain.Contractor.get_ContractorId)))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Product>() != null)
                {
                    return "Товар с таким артикулом существует";
                }
                product.set_CreatedAt(DateTime.Now);
            }
            else
            {
                if ((dbSet.Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId)), Expression.Property(Expression.Constant(product), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_ProductId))), new ParameterExpression[] { expression })).First<SidBy.Sklad.Domain.Product>().get_Article().ToLower() != product.get_Article().ToLower()) && (dbSet.Where<SidBy.Sklad.Domain.Product>(Expression.Lambda<Func<SidBy.Sklad.Domain.Product, bool>>(Expression.Equal(Expression.Call(null, (MethodInfo) methodof(string.Compare), new Expression[] { Expression.Property(expression = Expression.Parameter(typeof(SidBy.Sklad.Domain.Product), "x"), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Article)), Expression.Property(Expression.Constant(product), (MethodInfo) methodof(SidBy.Sklad.Domain.Product.get_Article)), Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison)) }), Expression.Constant(0, typeof(int))), new ParameterExpression[] { expression })).FirstOrDefault<SidBy.Sklad.Domain.Product>() != null))
                {
                    return "Товар с таким артикулом существует";
                }
                product.set_ModifiedAt(new DateTime?(DateTime.Now));
            }
            return string.Empty;
        }
    }
}

