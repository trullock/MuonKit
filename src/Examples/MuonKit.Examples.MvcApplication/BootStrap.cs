using System.Web.Mvc;
using System.Web.Routing;
using MuonKit.Examples.Domain;
using MuonKit.Examples.Domain.Data;
using MuonLab.Commons.DI;
using MuonLab.Commons.StructureMap;
using MuonLab.Commons.StructureMap.NHibernate;
using MuonLab.Web.Mvc;
using NHibernate.Cfg;
using StructureMap;

namespace MuonKit.Examples.MvcApplication
{
	public static class BootStrap
	{
		public static void Everything()
		{
			var config = NHibernate();
			ServiceLocation(config);
			Mvc();
		}

		public static void ServiceLocation(Configuration config)
		{
			ObjectFactory.Initialize(i =>
				{
					i.AddRegistry<DomainRegistry>();
					i.AddRegistry<MvcRegistry>();
					i.AddRegistry(new NHibernateRegistry(config));
				});

			DependencyResolver.SetCurrentResolver(new StructureMapAdapter());
		}

		public static void Mvc()
		{
			RouteRegistration.Register(RouteTable.Routes);

			ControllerBuilder.Current.SetControllerFactory(new NHibernateControllerFactory());
		}

		public static Configuration NHibernate()
		{
			return NHibernateConfiguration.Configure();
		}
	}
}