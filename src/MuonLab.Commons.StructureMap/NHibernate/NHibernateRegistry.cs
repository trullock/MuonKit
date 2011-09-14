using MuonLab.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace MuonLab.Commons.StructureMap.NHibernate
{
	public class NHibernateRegistry : Registry
	{
		public NHibernateRegistry(Configuration configuration)
		{
			var sessionFactory = configuration.BuildSessionFactory();

			For<ISessionFactory>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use(sessionFactory);

			For<IUnitOfWork>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Hybrid))
				.Use<UnitOfWork>();
		}
	}
}