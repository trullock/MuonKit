using System;
using System.Net;
using System.Web;

namespace MuonLab.Web.ExceptionHandling
{
	public class ExceptionHandlingContext : ExceptionLoggingContext
	{
		private readonly Action<ExceptionHandlingContext> logDelegate;

		public ExceptionHandlingContext(HttpContext httpContext, HttpStatusCode statusCode, Exception exception, Action<ExceptionHandlingContext> logDelegate) :
			base(httpContext, statusCode, exception)
		{
			this.logDelegate = logDelegate;
		}

		/// <summary>
		/// Causes the exception to be logged
		/// </summary>
		public void Log()
		{
			logDelegate(this);
		}
	}
}