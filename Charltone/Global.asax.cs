using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Charltone.Controllers;
using Charltone.Plumbing;

namespace Charltone
{
	public class MvcApplication : HttpApplication
	{
		private static IWindsorContainer container;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
				);
		}

		protected void Application_End()
		{
#if DEBUG
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Stop();
#endif
			container.Dispose();
		}

		protected void Application_Start()
		{
#if DEBUG
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif
			AreaRegistration.RegisterAllAreas();
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			BootstrapContainer();
		}

		private static void BootstrapContainer()
		{
			container = new WindsorContainer().Install(FromAssembly.This());
        
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
			var ticket = TryDecryptCookie(cookie);

			if (ticket != null) //Already signed-in
			{
				//TODO: running in Cassini, you can not use custom IIdentity impelentations
				//      so you can directly use identity below if using IIS/IISExpress

				var identity = new AppIdentity(ticket.Name);
				var genericIdentity = new GenericIdentity(identity.Name);
				var principal = new GenericPrincipal(genericIdentity, new string[0]);

				Context.User = principal;
			}
		}

		private static FormsAuthenticationTicket TryDecryptCookie(HttpCookie cookie)
		{
			if (cookie == null || cookie.Value == null)
				return null;

			return FormsAuthentication.Decrypt(cookie.Value);
		}

        //protected void Application_Error()
        //{
        //    var exception = Server.GetLastError();
        //    var httpException = exception as HttpException;
        //    Response.Clear();
        //    Server.ClearError();
        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Errors";
        //    routeData.Values["action"] = "General";
        //    routeData.Values["exception"] = exception;
        //    Response.StatusCode = 500;
        //    if (httpException != null)
        //    {
        //        Response.StatusCode = httpException.GetHttpCode();
        //        switch (Response.StatusCode)
        //        {
        //            case 403:
        //                routeData.Values["action"] = "Http403";
        //                break;
        //            case 404:
        //                routeData.Values["action"] = "Http404";
        //                break;
        //        }
        //    }

        //    IController errorsController = new ErrorController();
        //    var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
        //    errorsController.Execute(rc);
        //}
	}
}