using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WeatherServiceHW04
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

/*            config.Routes.MapHttpRoute(
                name: "TempByAgg",
                routeTemplate: "api/{controller}/{type}/{period}/{aggregate}",
                defaults:
                    new
                    {
                        controller = "temperature",
                        type = RouteParameter.Optional,
                        period = RouteParameter.Optional,
                        aggregate = RouteParameter.Optional
                    }
                );*/

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
