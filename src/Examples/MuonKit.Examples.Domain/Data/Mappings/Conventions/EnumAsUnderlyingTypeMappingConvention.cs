using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace MuonKit.Examples.Domain.Data.Mappings.Conventions
{
	/// <summary>
	/// Causes NHibernate to store Enums as their underlying value (usually Int32) rather than as strigns
	/// </summary>
	public class EnumAsUnderlyingTypeMappingConvention : IUserTypeConvention
	{
		public void Accept(IAcceptanceCriteria<IPropertyInspector> acceptance)
		{
			acceptance.Expect(x => x.Property.PropertyType.IsEnum);
		}

		public void Apply(IPropertyInstance instance)
		{
			instance.CustomType(instance.Property.PropertyType);
		}
	}
}