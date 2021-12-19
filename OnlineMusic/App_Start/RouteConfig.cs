using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineMusic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Blog Detail",
                url: "blog/{metatitle}-{id}",
                defaults: new { controller = "Blog1", action = "Blog_Detail", id = UrlParameter.Optional }
            );*/
            routes.MapRoute(
                name: "Product Detail",
                url: "chitiet/{metatitle}-{id}",
                defaults: new { controller = "Product1", action = "Product_Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Add To Cart",
                url: "them-vao-gio",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
