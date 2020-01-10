using System.Web.Mvc;
using System.Web.Routing;
using CsStat.Domain;

namespace CsStat.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute
            (
                name: "getrepository",
                url: Settings.PlayersDataApiPath,
                defaults: new { controller = "Home", action = "GetRepository", id = UrlParameter.Optional }
            );

            //don't remove: custom admin
            //routes.MapRoute
            //(
            //    name: "Admin",
            //    url: "Admin",
            //    defaults: new { controller = "SignIn", action = "SignIn", id = UrlParameter.Optional }
            //);

            routes.MapRoute
            (
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "SignIn", action = "StrapiAdmin", id = UrlParameter.Optional }
            );

            routes.MapRoute
            (
                name: "getinfo",
                url: Settings.WikiDataApiPath,
                defaults: new { controller = "Wiki", action = "GetAllArticlesFromCms", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "wiki",
                url: Settings.WikiPagePath,
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
