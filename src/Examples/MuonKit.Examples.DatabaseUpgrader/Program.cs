using MuonKit.Examples.Domain.Data;
using MuonKit.Examples.MvcApplication;
using MuonLab.Data;

namespace MuonKit.Examples.DatabaseUpgrader
{
	class Program
	{
		static void Main(string[] args)
		{
			var configuration = BootStrap.NHibernate();
			BootStrap.ServiceLocation(configuration);

			var cfg = NHibernateConfiguration.Configure();
			Upgrader.EnsureCurrentVersion(typeof(NHibernateConfiguration).Assembly, cfg);
		}
	}
}
