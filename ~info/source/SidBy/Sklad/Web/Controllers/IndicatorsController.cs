namespace SidBy.Sklad.Web.Controllers
{
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
    public class IndicatorsController : Controller
    {
        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Message = "Графики на Highcharts.";
            LogJqGridModel model = new LogJqGridModel();
            JQGrid logGrid = model.LogGrid;
            this.LogSetupGrid(logGrid);
            IndicatorsModel model2 = new IndicatorsModel {
                LogGrid = model
            };
            return base.View(model2);
        }

        public JsonResult LogSearchGridDataRequested()
        {
            ParameterExpression expression;
            LogJqGridModel model = new LogJqGridModel();
            LoggingDataContext context = new LoggingDataContext();
            this.LogSetupGrid(model.LogGrid);
            return model.LogGrid.DataBind(context.get_Logs().OrderByDescending<Log, DateTime>(Expression.Lambda<Func<Log, DateTime>>(Expression.Property(expression = Expression.Parameter(typeof(Log), "x"), (MethodInfo) methodof(Log.get_Date)), new ParameterExpression[] { expression })));
        }

        private void LogSetupGrid(JQGrid grid)
        {
            grid.ID = "Log1";
            grid.DataUrl = base.Url.Action("LogSearchGridDataRequested");
            grid.ToolBarSettings.ShowSearchToolBar = true;
        }
    }
}

