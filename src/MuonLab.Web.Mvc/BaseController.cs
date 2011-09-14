using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MuonLab.Web.Mvc
{
	public abstract class BaseController<TController> : BaseController where TController : IController
	{
		protected ActionResult RedirectToAction(Expression<Func<TController, ActionResult>> action)
		{
			return RedirectToAction<TController>(action);
		}
	}

	[ValidateInput(false)]
	public abstract class BaseController : Controller
	{
		protected ActionResult RedirectToAction<TController>(Expression<Func<TController, ActionResult>> action) where TController: IController
		{
			return RedirectToRoute(UrlHelperExtensions.ExtractRouteValues(action));
		}

		protected void AddResultMessage(string message)
		{
			this.TempData["ResultMessage"] = message;
		}
	}
}