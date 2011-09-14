using MuonLab.Commons.DI;

namespace MuonLab.Web
{
	public static class HtmlEncodingExtensions
	{
		public static string HtmlEncode(this object self)
		{
			var encoder = (DependencyResolver.Current != null ? DependencyResolver.Current.TryGetInstance<IHtmlEncoder>() : null) ?? new HttpUtilityHtmlEncoder();
			return encoder.HtmlEncode(self.ToString());
		}

		public static string HtmlAttributeEncode(this object self)
		{
			var encoder = (DependencyResolver.Current != null ? DependencyResolver.Current.TryGetInstance<IHtmlEncoder>() : null) ?? new HttpUtilityHtmlEncoder();
			return encoder.HtmlAttributeEncode(self.ToString());
		}
	}
}