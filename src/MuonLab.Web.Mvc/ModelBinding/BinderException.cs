using System;
using System.Runtime.Serialization;

namespace MuonLab.Web.Mvc.ModelBinding
{
	public class BinderException : Exception
	{
		public BinderException(string message) : base(message)
		{
		}

		public BinderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BinderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}