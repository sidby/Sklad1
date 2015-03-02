namespace SidBy.Sklad.Web.Controllers
{
    using log4net;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using Trirand.Web.Mvc;

    [Authorize(Roles="admin,employee")]
    public class SaleController : Controller
    {
        private readonly ILog logger;

        public SaleController()
        {
            this.logger = LogManager.GetLogger(base.GetType());
        }

        public ActionResult CustomerOrders()
        {
            ((dynamic) base.ViewBag).Title = "Заказы покупателя";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid documentGrid = model.DocumentGrid;
            this.CustomerOrdersSetupGrid(documentGrid);
            return base.View(model);
        }

        public JsonResult CustomerOrdersSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.CustomerOrdersSetupGrid(model.DocumentGrid);
            return model.DocumentGrid.DataBind(context.get_Documents().Where<Document>(Expression.Lambda<Func<Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Document), "x"), (MethodInfo) methodof(Document.get_DocumentTypeId)), Expression.Constant(3, typeof(int))), new ParameterExpression[] { expression })));
        }

        private void CustomerOrdersSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "CustomerOrdersGrid1", base.Url.Action("CustomerOrdersSearchGridDataRequested"), base.Url.Action("DocumentItemEditRows", "Home"));
            this.SetUpContractorEmployeeEditDropDown(grid);
        }

        public ActionResult Refunds()
        {
            ((dynamic) base.ViewBag).Title = "Возвраты покупателя";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid documentGrid = model.DocumentGrid;
            this.RefundsSetupGrid(documentGrid);
            return base.View(model);
        }

        public JsonResult RefundsSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.RefundsSetupGrid(model.DocumentGrid);
            return model.DocumentGrid.DataBind(context.get_Documents().Where<Document>(Expression.Lambda<Func<Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Document), "x"), (MethodInfo) methodof(Document.get_DocumentTypeId)), Expression.Constant(5, typeof(int))), new ParameterExpression[] { expression })));
        }

        private void RefundsSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "RefundsGrid1", base.Url.Action("RefundsSearchGridDataRequested"), base.Url.Action("DocumentItemEditRows", "Home"));
            this.SetUpContractorEmployeeEditDropDown(grid);
        }

        private void SetUpContractorEmployeeEditDropDown(JQGrid itemGrid)
        {
            itemGrid.Columns.Find(c => c.DataField == "EmployeeName").Visible = true;
            itemGrid.Columns.Find(c => c.DataField == "ContractorName").Visible = true;
            itemGrid.Columns.Find(c => c.DataField == "ContractorId").Visible = true;
            itemGrid.Columns.Find(c => c.DataField == "SaleSum").Visible = true;
            JQGridColumn column5 = itemGrid.Columns.Find(c => c.DataField == "EmployeeId");
            column5.Visible = true;
            if (itemGrid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                ParameterExpression expression;
                SkladDataContext context = new SkladDataContext();
                IEnumerable<SelectListItem> source = from x in context.get_UserProfiles().Where<UserProfile>(Expression.Lambda<Func<UserProfile, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(UserProfile), "m"), (MethodInfo) methodof(UserProfile.get_ContactTypeId)), Expression.Convert(Expression.Constant(1, typeof(int)), typeof(int?))), new ParameterExpression[] { expression })).AsEnumerable<UserProfile>() select new SelectListItem { Text = x.get_DisplayName(), Value = x.get_UserId().ToString() };
                column5.EditList.AddRange(source.ToList<SelectListItem>());
                column5.SearchList = source.ToList<SelectListItem>();
                SelectListItem item = new SelectListItem {
                    Text = "Все",
                    Value = ""
                };
                column5.SearchList.Insert(0, item);
            }
        }

        private void SetUpGrid(JQGrid grid, string gridID, string dataUrl, string editUrl)
        {
            grid.ID = gridID;
            grid.DataUrl = dataUrl;
            grid.EditUrl = editUrl;
            grid.ToolBarSettings.ShowSearchToolBar = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.ToolBarSettings.ShowAddButton = false;
            grid.AddDialogSettings.CloseAfterAdding = true;
        }

        public ActionResult Shipment()
        {
            ((dynamic) base.ViewBag).Title = "Отгрузки";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid documentGrid = model.DocumentGrid;
            this.ShipmentSetupGrid(documentGrid);
            return base.View(model);
        }

        public JsonResult ShipmentSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.ShipmentSetupGrid(model.DocumentGrid);
            return model.DocumentGrid.DataBind(context.get_Documents().Where<Document>(Expression.Lambda<Func<Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Document), "x"), (MethodInfo) methodof(Document.get_DocumentTypeId)), Expression.Constant(4, typeof(int))), new ParameterExpression[] { expression })));
        }

        private void ShipmentSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "ShipmentGrid1", base.Url.Action("ShipmentSearchGridDataRequested"), base.Url.Action("DocumentItemEditRows", "Home"));
            this.SetUpContractorEmployeeEditDropDown(grid);
        }
    }
}

