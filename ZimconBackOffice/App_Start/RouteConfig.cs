using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZimconBackOffice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Maps",
                url: "replay/{id}",
                defaults: new { controller = "MapView", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Device",
                url: "device/{id}",
                defaults: new { controller = "Device", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Consignee",
               url: "consignee/{id}",
               defaults: new { controller = "Consignee", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
