using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_Hutech
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Thongtin",
                url: "thong-tin",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Events", action = "Login", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
              name: "Login",
              url: "{controller}/{action}",
              defaults: new { controller = "Events", action = "Login", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Detail",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Events", action = "Detail", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "GetUsers",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Events", action = "GetUsers", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Create_BL",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Events", action = "Create_BL", id = UrlParameter.Optional }
        );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Events", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
