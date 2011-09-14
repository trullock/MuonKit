using System;

namespace MuonLab.Commons.Formatting
{
	public static class ObjectExtensions
	{
		public static string Format<T>(this T self)
		{
			return Formatter.Format(self);
		}

		public static string Format<T>(this T self, Func<T, string> formatFunction)
		{
			return formatFunction(self);
		}
	}
}