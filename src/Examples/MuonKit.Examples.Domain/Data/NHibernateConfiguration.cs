using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MuonKit.Examples.Domain.Data.Mappings;
using MuonLab.Commons.Extensions;
using Configuration=NHibernate.Cfg.Configuration;

namespace MuonKit.Examples.Domain.Data
{
	public class NHibernateConfiguration
	{
		public static Configuration Configure()
		{
			var configuration = Fluently.Configure().Database(
				MsSqlConfiguration.MsSql2005.ConnectionString(
				ConfigurationManager.ConnectionStrings["NHibernate.Connection"].ConnectionString));

			configuration.ExposeConfiguration(c =>
			{
				if (bool.Parse(ConfigurationManager.AppSettings["NHibernate.Generate-Statistics"]))
					c.SetProperty("generate_statistics", "true");
			});

			configuration.Mappings(m =>
			{
				EntityMappings.Map(m);

				if (bool.Parse(ConfigurationManager.AppSettings["NHibernate.Export-Mappings"]))
				{
					m.AutoMappings.ExportTo(ConfigurationManager.AppSettings["NHibernate.Export-Mappings-Path"]);
					m.FluentMappings.ExportTo(ConfigurationManager.AppSettings["NHibernate.Export-Mappings-Path"]);
				}
			});

			return configuration.BuildConfiguration();
		}
	}
}