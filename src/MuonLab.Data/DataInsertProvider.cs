using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MuonLab.Commons.DI;

namespace MuonLab.Data
{
	public class DataInsertProvider : IDataInsertProvider
	{
		private readonly IEnumerable<IDataInsert> inserts;

		public DataInsertProvider(Assembly assembly)
		{
			this.inserts = assembly.GetTypes()
				.Where(t => typeof (IDataInsert).IsAssignableFrom(t))
				.Select(t => DependencyResolver.Current.GetInstance(t) as IDataInsert);
		}

		public IEnumerable<IDataInsert> All()
		{
			return this.inserts;
		}
	}
}