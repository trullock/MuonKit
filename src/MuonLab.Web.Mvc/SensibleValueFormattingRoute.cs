using System;
using System.Web.Routing;
using MuonLab.Commons;

namespace MuonLab.Web.Mvc
{
	public class SensibleValueFormattingRoute : Route
	{
		public SensibleValueFormattingRoute(string url, IRouteHandler routeHandler)
			: base(url, routeHandler)
		{
		}

		public SensibleValueFormattingRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: base(url, defaults, routeHandler)
		{
		}

		public SensibleValueFormattingRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, defaults, constraints, routeHandler)
		{
		}

		public SensibleValueFormattingRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: base(url, defaults, constraints, dataTokens, routeHandler)
		{
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			var dictionary = new RouteValueDictionary();

			foreach (var kvp in values)
			{
				if (kvp.Value.GetType() == typeof(Guid))
					dictionary.Add(kvp.Key, new ShortGuid((Guid)kvp.Value));
				else if (kvp.Value.GetType() == typeof(DateTime))
					dictionary.Add(kvp.Key, ((DateTime)kvp.Value).ToString("yyyy-MM-dd"));
				else
					dictionary.Add(kvp.Key, kvp.Value);
			}

			return base.GetVirtualPath(requestContext, dictionary);
		}
	}
}