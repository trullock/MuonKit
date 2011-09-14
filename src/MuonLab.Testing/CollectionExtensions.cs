using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Testing
{
	public static class CollectionExtensions
	{
		private static ArrayList ToArrayList(this IEnumerable enumerable)
		{
			var arrayList = new ArrayList();

			foreach (var obj in enumerable)
				arrayList.Add(obj);

			return arrayList;
		}

		public static void ShouldBeEmpty(this IEnumerable collection)
		{
			Assert.IsEmpty(collection.ToArrayList());
		}

		public static bool ContainsAny<T>(this IEnumerable<T> collection, IEnumerable<T> values)
		{
			foreach (T item in values)
			{
				if (collection.Contains<T>(item))
				{
					return true;
				}
			}
			return false;
		}

		public static void ShouldContain<T>(this IEnumerable<T> actual, params T[] expected)
		{
			foreach (T item in expected)
				Assert.Contains(item, actual.ToArrayList());
		}

		public static void ShouldContain(this IEnumerable actual, params object[] expected)
		{
			foreach (object item in expected)
				Assert.Contains(item, actual.ToArrayList());
		}

		public static void ShouldContainOnly<T>(this IEnumerable<T> actual, params T[] expected)
		{
			actual.ShouldContainOnly(new List<T>(expected));
		}

		public static void ShouldContainOnly<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
		{
			var actualList = new List<T>(actual);
			var remainingList = new List<T>(actualList);
			foreach (T item in expected)
			{
				Assert.Contains(item, actualList);
				remainingList.Remove(item);
			}
			Assert.IsEmpty(remainingList);
		}

		public static void ShouldNotBeEmpty(this IEnumerable collection)
		{
			Assert.IsNotEmpty(collection.ToArrayList());
		}

		public static void ShouldNotContain(this IEnumerable collection, object expected)
		{
			int i = 0;
			foreach (var item in collection)
			{
				if (item.Equals(expected))
					Assert.Fail("Collection DOES contain item at position " + i);
				i++;
			}
		}
	}
}