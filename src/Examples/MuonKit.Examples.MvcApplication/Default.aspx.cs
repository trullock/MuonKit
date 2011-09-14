using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MuonKit.Examples.MvcApplication
{
	public class Default : Page
	{
		public void Page_Load(object sender, System.EventArgs e)
		{
			HttpContext.Current.RewritePath(Request.ApplicationPath, false);
			IHttpHandler httpHandler = new MvcHttpHandler();
			httpHandler.ProcessRequest(HttpContext.Current);
		}
	}
}
