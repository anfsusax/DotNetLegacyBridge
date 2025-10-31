using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Custom routes without attribute routing (Web API 1 style)
            config.Routes.MapHttpRoute(
                name: "ClienteList",
                routeTemplate: "cliente-get",
                defaults: new { controller = "Cliente", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ClienteGetById",
                routeTemplate: "cliente-get{id}",
                defaults: new { controller = "Cliente" }
            );

            config.Routes.MapHttpRoute(
                name: "ClientePost",
                routeTemplate: "cliente-post",
                defaults: new { controller = "Cliente" }
            );

            config.Routes.MapHttpRoute(
                name: "ClientePut",
                routeTemplate: "cliente-Put{id}",
                defaults: new { controller = "Cliente" }
            );

            config.Routes.MapHttpRoute(
                name: "ClienteDelete",
                routeTemplate: "cliente-delete{id}",
                defaults: new { controller = "Cliente" }
            );
        }
    }
}
