using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace MuonLab.Web.Mvc
{
	public static class UrlHelperExtensions
	{
		public static string For<TController>(this UrlHelper self, Expression<Func<TController, ActionResult>> action) where TController : IController
		{
			return self.Action(action);
		}

		public static string Action<TController>(this UrlHelper self, Expression<Func<TController, ActionResult>> action) where TController : IController
		{
			var routeValues = ExtractRouteValues(action);
			return self.RouteUrl(routeValues);
		}

		public static string Action<TController>(this UrlHelper self, Expression<Func<TController, ActionResult>> action, object extraRouteValues) where TController : IController
		{
			var routeValues = ExtractRouteValues(action);

			if (extraRouteValues != null)
			{
				var extras = new RouteValueDictionary(extraRouteValues);
				foreach (var entry in extras)
					routeValues.Add(entry.Key, entry.Value);
			}

			return self.RouteUrl(routeValues);
		}

		public static RouteValueDictionary ExtractRouteValues<TController>(Expression<Func<TController, ActionResult>> action) where TController : IController
		{
			var methodExpression = action.Body as MethodCallExpression;
			var actionName = methodExpression.Method.Name;
			var controllerName = methodExpression.Object.Type.Name.Replace("Controller", string.Empty);

			var routeValues = new RouteValueDictionary
			                  	{
			                  		{"controller", controllerName},
			                  		{"action", actionName}
			                  	};

            var parameters = methodExpression.Method.GetParameters();

			// is there a parameter on the action method being called?
		    if (parameters.Any())
			{
				// yes - for each argument to the action method
				for (var i = 0; i < parameters.Length; i++)
				{
					// get the value of the argument
					var value = Expression.Lambda(methodExpression.Arguments[i]).Compile().DynamicInvoke();

					if (value != null)
					{
						// if we retrieved a value add it to the route value dictionary with its name
						routeValues.Add(parameters[i].Name, value);
					}
				}
			}

			return routeValues;
		}
	}
}