using MuonKit.Examples.Domain;
using MuonKit.Examples.Domain.Data;
using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Commons.StructureMap.NHibernate;
using NHibernate.Cfg;
using StructureMap;

namespace MuonKit.Examples.WebForms
{
	public static class BootStrap
	{
		public static void Everything()
		{
			var config = NHibernate();
			ServiceLocation(config);
		}

		public static void ServiceLocation(Configuration config)
		{
			ObjectFactory.Initialize(i =>
			{
				i.AddRegistry<DomainRegistry>();
				i.AddRegistry(new NHibernateRegistry(config));
			});

			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());
		}


		public static Configuration NHibernate()
		{
			return NHibernateConfiguration.Configure();
		}
	}
}