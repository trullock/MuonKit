using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MuonLab.Web.Mvc.ShortGuids
{
	public static class ShortGuidRouteExtensions
	{
		public static Route MapSensiblyFormattedRoute(this RouteCollection routes, string name, string url)
		{
			return routes.MapSensiblyFormattedRoute(name, url, null);
		}

		public static Route MapSensiblyFormattedRoute(this RouteCollection routes, string name, string url, object defaults)
		{
			if (routes == null)
				throw new ArgumentNullException("routes");

			if (url == null)
				throw new ArgumentNullException("url");

			Route route = new SensibleValueFormattingRoute(url, new MvcRouteHandler());

			if (defaults != null)
				route.Defaults = new RouteValueDictionary(defaults);

			route.Constraints = new RouteValueDictionary();

			routes.Add(name, route);
			return route;
		}
	}
}