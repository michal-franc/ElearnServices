using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using elearn.ActionFilters;
using NLog;
using Ninject;

namespace elearn
{
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LayoutChangerAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Paged", // Route name
                "{controller}/{action}/{pageNumber}", // URL with parameters
                new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional } // Parameter defaults
             );  
        }

        protected void Application_Start()
        {
            //Dependancy Injection
            var kernel = new StandardKernel(new ProfileModule());
            var resolver = new NinjectDependencyResolver(kernel);
            DependencyResolver.SetResolver(resolver);

            AreaRegistration.RegisterAllAreas();


            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Logger.Info("Application Started");
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
            Logger.Error("Application Error - {0}", lastException.Message);
        }
    }
}