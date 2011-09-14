using System;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace MuonLab.Web.Mvc.Security
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidateRequestAttribute : FilterAttribute, IAuthorizationFilter
	{
		private readonly ValidateAntiForgeryTokenAttribute mvcAntiForgeryValidator;

		public ValidateRequestAttribute()
		{
			// This is what happens when Microsoft make classes sealed/internal
			this.mvcAntiForgeryValidator = new ValidateAntiForgeryTokenAttribute();
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			mvcAntiForgeryValidator.Salt = (string)filterContext.RouteData.Values["action"];
			mvcAntiForgeryValidator.OnAuthorization(filterContext);
		}
	}
}