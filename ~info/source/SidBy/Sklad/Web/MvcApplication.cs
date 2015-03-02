namespace SidBy.Sklad.Web
{
    using log4net.Config;
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using WebMatrix.WebData;

    public class MvcApplication : HttpApplication
    {
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("ru");
            Thread.CurrentThread.CurrentUICulture = info;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(info.Name);
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.get_Configuration());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.get_Bundles());
            AuthConfig.RegisterAuth();
            Database.SetInitializer<SkladDataContext>(new MigrateDatabaseToLatestVersion<SkladDataContext, SkladDataContextMigrationConfiguration>());
            if (!WebSecurity.get_Initialized())
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            }
        }
    }
}

