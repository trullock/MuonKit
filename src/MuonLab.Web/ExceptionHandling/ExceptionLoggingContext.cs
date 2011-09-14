using System;
using System.Net;
using System.Web;

namespace MuonLab.Web.ExceptionHandling
{
	public class ExceptionLoggingContext
	{
		public readonly HttpContext HttpContext;
		public readonly HttpStatusCode StatusCode;
		public readonly Exception Exception;

		public ExceptionLoggingContext(HttpContext httpContext, HttpStatusCode statusCode, Exception exception)
		{
			this.HttpContext = httpContext;
			this.StatusCode = statusCode;
			this.Exception = exception;
		}
	}
}