using System;
using System.Collections.Generic;
using System.Linq;

namespace MuonLab.Commons.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Flattens an enumerable of T to a single string, separated by the provided string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="toStringFunc">The function to convert the item from the enumerable to a string</param>
		/// <param name="separator">the list separator</param>
		/// <returns></returns>
		public static string Join<T>(this IEnumerable<T> self, Func<T, string> toStringFunc, string separator)
		{
			return String.Join(separator, self.Select(x => toStringFunc.Invoke(x)).ToArray());
		}

		public static string Join<T>(this IEnumerable<T> self, string separator)
		{
			return String.Join(separator, self.Select(x => x.ToString()).ToArray());
		}

	}
}