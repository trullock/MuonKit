namespace MuonKit.Examples.MvcApplication
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			BootStrap.Everything();
		}

		protected void Application_BeginRequest()
		{
			// hack to make routes work in iis6 and 7 at the same time
			if (this.Request.AppRelativeCurrentExecutionFilePath.Contains(".mvc"))
				this.Context.RewritePath(this.Request.Url.PathAndQuery.Replace(".mvc", string.Empty));
		}
	}
}