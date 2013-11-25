﻿using SidBy.Sklad.Web.Migration;
using SidBy.Sklad.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SidBy.Sklad.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // Set the database init strategy
            //UsersContext.UsersContext.Database.SetInitializer(new MigrateDatabaseToLatestVersion<UsersContext, UsersContextMigrationConfiguration>());
            // trigger the database init strategy with a read
            //new UsersContext().UserProfiles.Find(1);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("ru");

            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name); 
        }
    }
}