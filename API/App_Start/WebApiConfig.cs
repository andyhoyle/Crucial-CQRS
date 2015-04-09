using Crucial.Framework.IoC.StructureMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "ControllerWithAction",
            //    routeTemplate: "api/{controller}/{id}/{action}/{actionId}",
            //    defaults: new { id = RouteParameter.Optional, actionId = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
