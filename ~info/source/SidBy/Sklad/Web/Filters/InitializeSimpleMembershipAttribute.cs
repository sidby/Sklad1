namespace SidBy.Sklad.Web.Filters
{
    using SidBy.Sklad.DataAccess;
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Web.Mvc;
    using WebMatrix.WebData;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized<SimpleMembershipInitializer>(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);
                try
                {
                    using (UsersContext context = new UsersContext())
                    {
                        if (!context.get_Database().Exists())
                        {
                            context.get_ObjectContext().CreateDatabase();
                        }
                    }
                    if (!WebSecurity.get_Initialized())
                    {
                        WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
                    }
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", exception);
                }
            }
        }
    }
}

