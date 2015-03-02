namespace SidBy.Sklad.Web
{
    using System;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            HttpRouteCollectionExtensions.MapHttpRoute(config.get_Routes(), "DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}

