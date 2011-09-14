using System.Web.Mvc;
using System.Web.Routing;

namespace MuonKit.Examples.MvcApplication
{
	public static class RouteRegistration
	{
		public static void Register(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);

		}
	}
}