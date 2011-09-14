using System.Collections;
using System.Collections.Generic;

namespace MuonLab.Web.Mvc.ModelBinding
{
	internal class GenericEnumerable<T> : IEnumerable<T>
	{
		private readonly IEnumerable enumerable;

		public GenericEnumerable(IEnumerable enumerable)
		{
			this.enumerable = enumerable;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator<T>(enumerable.GetEnumerator());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	    private class Enumerator<T2> : IEnumerator<T2>
		{
			private IEnumerator enumerator;

			public Enumerator(IEnumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			public void Dispose()
			{
				enumerator = null;
			}

			public bool MoveNext()
			{
				return enumerator.MoveNext();
			}

			public void Reset()
			{
				enumerator.Reset();
			}

			public T2 Current
			{
				get { return (T2) enumerator.Current; }
			}

			object IEnumerator.Current
			{
				get { return enumerator.Current; }
			}
		}
	}
}