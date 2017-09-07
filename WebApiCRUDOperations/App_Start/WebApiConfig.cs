using System.Web.Http;
using WebApiCRUDOperations.Filters;

namespace WebApiCRUDOperations
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //To apply the filter to all Web API controllers, add it to GlobalConfiguration.Filters.
            config.Filters.Add(new LogAttribute());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
