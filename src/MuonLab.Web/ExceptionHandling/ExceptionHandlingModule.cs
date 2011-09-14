using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace MuonLab.Web.ExceptionHandling
{
	public abstract class ExceptionHandlingModule : IHttpModule
	{
		private readonly IDictionary<HttpStatusCode, Action<ExceptionHandlingContext>> handlers;

		protected ExceptionHandlingModule()
		{
			this.handlers = new Dictionary<HttpStatusCode, Action<ExceptionHandlingContext>>();
		}

		public void Init(HttpApplication context)
		{
			context.Error += ContextError;
		}

		void ContextError(object sender, EventArgs args)
		{
			var httpApplication = sender as HttpApplication;

			var httpContext = httpApplication.Context;

			httpContext.Response.Clear();

			var exception = httpContext.Server.GetLastError();

			var statusCode = GetStatusCode(exception);

			var errorLoggingContext = new ExceptionLoggingContext(httpContext, statusCode, exception);
			var exceptionHandlingContext = new ExceptionHandlingContext(httpContext, statusCode, exception, this.LogException);

			if (this.handlers.ContainsKey(statusCode))
			{
				try
				{
					httpContext.Server.ClearError();
					this.handlers[statusCode](exceptionHandlingContext);
					return;
				}
				catch (Exception e)
				{
					LogException(new ExceptionLoggingContext(httpContext, HttpStatusCode.InternalServerError, e));
				}
			}

			LogException(errorLoggingContext);

			try
			{
				DefaultAction(exceptionHandlingContext);
				httpContext.Server.ClearError();
			}
			catch (Exception e)
			{
				LogException(new ExceptionLoggingContext(httpContext, HttpStatusCode.InternalServerError, e));

				HttpContext.Current.Response.StatusCode = 500;
			}
		}

		public virtual void Dispose()
		{

		}

		protected abstract void Log(ExceptionLoggingContext context);
		protected abstract void DefaultAction(ExceptionHandlingContext context);

		/// <summary>
		/// Sets a handler for a specific HttpStatusCode. 
		/// </summary>
		/// <param name="code">The HttpStatusCode to handle</param>
		/// <param name="handler">Handler code should result in a redirect or throw an exception</param>
		protected void SetHandler(HttpStatusCode code, Action<ExceptionHandlingContext> handler)
		{
			this.handlers[code] = handler;
		}

		private void LogException(ExceptionLoggingContext context)
		{
			try
			{
				Log(context);
			}
			catch
			{
				// cant do anything, epic fail :(
			}
		}

		private static HttpStatusCode GetStatusCode(Exception exception)
		{
			if (exception is InvalidOperationException && (exception as InvalidOperationException).Message.Contains("did not return a controller for a controller named"))
				return HttpStatusCode.NotFound;

			if (exception is HttpException)
			{
				var httpException = exception as HttpException;
				return (HttpStatusCode)httpException.GetHttpCode();
			}

			return HttpStatusCode.InternalServerError;
		}
	}
}