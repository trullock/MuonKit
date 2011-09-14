using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using MuonLab.Commons.Extensions;

namespace MuonLab.Web.Mvc.Xhtml
{
	public static class HtmlHelperExtensions
	{
		public static MvcForm AntiForgeryForm(this HtmlHelper html)
		{
			return html.AntiForgeryForm(null);
		}

		public static MvcForm AntiForgeryForm(this HtmlHelper html, object htmlAttributes)
		{
			var urlHelper = new UrlHelper(html.ViewContext.RequestContext, RouteTable.Routes);
			var routeData = urlHelper.RouteCollection.GetRouteData(html.ViewContext.HttpContext);

			var action = (string)routeData.Values["action"];
			var controller = (string)routeData.Values["controller"];

			var form = html.BeginForm(action, controller, routeData.Values, FormMethod.Post, htmlAttributes.ToDictionary());

			html.ViewContext.HttpContext.Response.Write(html.AntiForgeryToken(action));

			return form;
		}

		public static MvcForm AntiForgeryForm<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> controllerAction) where TController : IController
		{
			return html.AntiForgeryForm(controllerAction, FormMethod.Post, null);
		}

		public static MvcForm AntiForgeryForm<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> controllerAction, FormMethod method, object htmlAttributes) where TController : IController
		{
			var routeValues = UrlHelperExtensions.ExtractRouteValues(controllerAction);

			var action = (string)routeValues["action"];
			var controller = (string)routeValues["controller"];

			var form = html.BeginForm(action, controller, routeValues, method, htmlAttributes.ToDictionary());

			html.ViewContext.HttpContext.Response.Write(html.AntiForgeryToken(action));

			return form;
		}

		public static MvcForm AntiForgeryForm(this HtmlHelper html, string action, string controller, object routeValues, FormMethod method, object htmlAttributes)
		{
			var form = html.BeginForm(action, controller, routeValues, method, htmlAttributes.ToDictionary());

			html.ViewContext.HttpContext.Response.Write(html.AntiForgeryToken(action));

			return form;
		}




		public static MvcForm BeginForm<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> controllerAction) where TController : IController
		{
			return html.BeginForm(controllerAction, FormMethod.Post, null);
		}

		public static MvcForm BeginForm<TController>(this HtmlHelper html, Expression<Func<TController, ActionResult>> controllerAction, FormMethod method, object htmlAttributes) where TController : IController
		{
			var routeValues = UrlHelperExtensions.ExtractRouteValues(controllerAction);

			var action = (string)routeValues["action"];
			var controller = (string)routeValues["controller"];

			var form = html.BeginForm(action, controller, routeValues, method, htmlAttributes.ToDictionary());

			return form;
		}

	}
}