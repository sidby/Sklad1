namespace SidBy.Sklad.Web.Controllers
{
    using log4net;
    using SidBy.Sklad.DataAccess;
    using SidBy.Sklad.Domain;
    using SidBy.Sklad.Web.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using Trirand.Web.Mvc;

    [Authorize(Roles="admin,employee")]
    public class WarehouseController : Controller
    {
        private readonly ILog logger;

        public WarehouseController()
        {
            this.logger = LogManager.GetLogger(base.GetType());
        }

        public ActionResult Cancellation()
        {
            ((dynamic) base.ViewBag).Title = "Списания";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid documentGrid = model.DocumentGrid;
            this.CancellationSetupGrid(documentGrid);
            return base.View(model);
        }

        public JsonResult CancellationSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.CancellationSetupGrid(model.DocumentGrid);
            return model.DocumentGrid.DataBind(context.get_Documents().Where<Document>(Expression.Lambda<Func<Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Document), "x"), (MethodInfo) methodof(Document.get_DocumentTypeId)), Expression.Constant(2, typeof(int))), new ParameterExpression[] { expression })));
        }

        private void CancellationSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "CancellationGrid1", base.Url.Action("CancellationSearchGridDataRequested"), base.Url.Action("DocumentItemEditRows", "Home"));
        }

        public ActionResult Posting()
        {
            ((dynamic) base.ViewBag).Title = "Оприходования";
            SkladJqGridModel model = new SkladJqGridModel();
            JQGrid documentGrid = model.DocumentGrid;
            this.PostingSetupGrid(documentGrid);
            return base.View(model);
        }

        public JsonResult PostingSearchGridDataRequested()
        {
            ParameterExpression expression;
            SkladJqGridModel model = new SkladJqGridModel();
            SkladDataContext context = new SkladDataContext();
            this.PostingSetupGrid(model.DocumentGrid);
            return model.DocumentGrid.DataBind(context.get_Documents().Where<Document>(Expression.Lambda<Func<Document, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(Document), "x"), (MethodInfo) methodof(Document.get_DocumentTypeId)), Expression.Constant(1, typeof(int))), new ParameterExpression[] { expression })));
        }

        private void PostingSetupGrid(JQGrid grid)
        {
            this.SetUpGrid(grid, "PostingGrid1", base.Url.Action("PostingSearchGridDataRequested"), base.Url.Action("DocumentItemEditRows", "Home"));
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
    }
}

