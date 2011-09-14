using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Web.Mvc;
using MuonLab.Web.Mvc;
using MuonLab.Web.Mvc.Security;
using NUnit.Framework;

namespace MuonLab.Testing.Mvc
{
	public static class Extensions
	{
		public static bool ActionIsProtectedAgainstForgery<TController>(this TController controller, Expression<Func<TController, ActionResult>> actionMethod) where TController : IController
		{
			var methodCallExpression = actionMethod.Body as MethodCallExpression;
			MethodInfo method = methodCallExpression.Method;
			var attributes = method.GetCustomAttributes(false);

			return attributes.Any(a => a.GetType() == typeof (ValidateRequestAttribute));
		}

		/// <summary>
		/// Ensures a RedirectToRouteResult redirects to the correct action with the correct route values
		/// </summary>
		/// <typeparam name="TController"></typeparam>
		/// <param name="redirect"></param>
		/// <param name="action"></param>
		public static void ShouldRedirectTo<TController>(this RedirectToRouteResult redirect, Expression<Func<TController, ActionResult>> action) where TController : IController
		{
			Assert.IsNotNull(redirect);

			var values = UrlHelperExtensions.ExtractRouteValues(action);

			foreach (var key in values.Keys)
				Assert.AreEqual(values[key], redirect.RouteValues[key], "Route values `" + key + "` do not match");
		}

		public static void ShouldHaveModel<T>(this ViewResult result, T model)
		{
			result.ViewData.Model.ShouldEqual(model);
		}
	}
}