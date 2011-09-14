using System.Web;

namespace MuonLab.Web
{
	public class HttpUtilityHtmlEncoder : IHtmlEncoder
	{
		public string HtmlEncode(string input)
		{
			return HttpUtility.HtmlEncode(input);
		}

		public string HtmlAttributeEncode(string input)
		{
			return HttpUtility.HtmlAttributeEncode(input);
		}
	}
}