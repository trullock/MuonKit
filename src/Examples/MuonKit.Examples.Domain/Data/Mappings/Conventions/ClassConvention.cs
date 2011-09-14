using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace MuonKit.Examples.Domain.Data.Mappings.Conventions
{
	public class ClassConvention : IClassConvention
	{
		public void Apply(IClassInstance instance)
		{
			instance.Not.LazyLoad();
		}
	}
}