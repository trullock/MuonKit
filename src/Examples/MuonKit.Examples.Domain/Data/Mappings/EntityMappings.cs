using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using MuonKit.Examples.Domain.Entities;
using MuonLab.NHibernate;

namespace MuonKit.Examples.Domain.Data.Mappings
{
	public class EntityMappings
	{
		public static void Map(MappingConfiguration mappingConfiguration)
		{
			mappingConfiguration.AutoMappings.Add(AutoMap
													.AssemblyOf<User>()
													.UseOverridesFromAssemblyOf<User>()
													.Conventions.AddFromAssemblyOf<User>()
													.Where(t => typeof(IEntity).IsAssignableFrom(t))
				);
		}
	}
}