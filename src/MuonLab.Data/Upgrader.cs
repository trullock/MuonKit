using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using MuonLab.Commons.DI;
using MuonLab.NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace MuonLab.Data
{
	public class Upgrader
	{
		private readonly IConnectionFactory connectionFactory;
		private readonly IUpdateProvider updateProvider;
		private readonly IDataInsertProvider insertProvider;
		private int version;

		private Upgrader(IConnectionFactory connectionFactory, IUpdateProvider updateProvider, IDataInsertProvider insertProvider)
		{
			this.connectionFactory = connectionFactory;
			this.updateProvider = updateProvider;
			this.insertProvider = insertProvider;
		}

		private int? GetCurrentVersion()
		{
			int? version;

			using (var scope = new TransactionScope())
			using (var connection = this.connectionFactory.NewConnection())
			using (var command = connection.CreateCommand())
			{
				ensureVersionTableExists(command);
				version = getCurrentVersion(command);
				scope.Complete();
			}

			return version;
		}

		private static void ensureVersionTableExists(IDbCommand command)
		{
			var exists = command.ExecuteScalar("SELECT name FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Version]')");

			if (exists == null)
			{
				command.ExecuteNonQuery("CREATE TABLE dbo.Version (Version int)");
				command.ExecuteNonQuery("INSERT INTO dbo.Version (Version) VALUES (NULL)");
			}
		}

		private static int? getCurrentVersion(IDbCommand command)
		{
			var scalar = command.ExecuteScalar("SELECT TOP 1 Version FROM dbo.Version");

			if (scalar == null || scalar == DBNull.Value)
				return null;
			else
				return Convert.ToInt32(scalar);
		}

		private int RunUpdates(int currentVersion)
		{
			var updates = this.updateProvider
				.AllUpdates()
				.Where(u => u.Version > currentVersion);

			using (var scope = new TransactionScope())
			using (var connection = this.connectionFactory.NewConnection())
			using (var command = connection.CreateCommand())
			{
				applyUpdates(command, updates);

				scope.Complete();
			}

			return updates.Count() > 0 ? updates.Max(u => u.Version) : currentVersion;
		}

		private void WipeDatabase()
		{
			Console.WriteLine("Wiping Database");

			var updates = new UpdateProvider(this.GetType().Assembly).AllUpdates();

			using (var scope = new TransactionScope())
			using (var connection = this.connectionFactory.NewConnection())
			using (var command = connection.CreateCommand())
			{
				applyUpdates(command, updates);
				scope.Complete();
			}
		}

		/// <summary>
		/// Runs the database upgrader
		/// </summary>
		/// <param name="scriptAssembly">The assembly containing the upgrade scripts</param>
		/// <param name="nhConfiguration">The NHibernate Configuration. The connection string must use a dbo user</param>
		public static int EnsureCurrentVersion(Assembly scriptAssembly, Configuration nhConfiguration)
		{
			var connectionString = nhConfiguration.GetProperty("connection.connection_string");

			var upgrader = new Upgrader(new ConnectionFactory(connectionString), new UpdateProvider(scriptAssembly), new DataInsertProvider(scriptAssembly));

			var oldVersion = upgrader.GetCurrentVersion();

			if (!oldVersion.HasValue)
			{
				upgrader.WipeDatabase();
				upgrader.CreateSchema(nhConfiguration);
				upgrader.RunInserts();
				return upgrader.SetLatestVersion();
			}
			else
			{
				return upgrader.RunUpdates(oldVersion.Value);
			}
		}

		private void CreateSchema(Configuration configuration)
		{
			Console.WriteLine("Creating Schema");

			new SchemaExport(configuration).Create(true, true);

			using (var sessionFactory = configuration.BuildSessionFactory())
			using (var unitOfWork = new UnitOfWork(sessionFactory))
				unitOfWork.Commit();
		}

		private int getLatestUpdateVersion()
		{
			if (this.updateProvider.AllUpdates().Any())
			{
				return this.updateProvider
					.AllUpdates()
					.Max(u => u.Version);
			}
			else
				return 0;
		}

		private int SetLatestVersion()
		{
			this.version = getLatestUpdateVersion();
			using (var scope = new TransactionScope())
			using (var connection = this.connectionFactory.NewConnection())
			using (var command = connection.CreateCommand())
			{
				setVersion(command, this.version);
				scope.Complete();
			}

			Console.WriteLine("Setting version to: " + this.version);

			return this.version;
		}

		private void RunInserts()
		{
			var inserts = this.insertProvider.All();

			var unitOfWork = DependencyResolver.Current.GetInstance<IUnitOfWork>();

			foreach (var insert in inserts)
			{
				Console.WriteLine("Running insert script: " + insert.Name);
				insert.Run();
			}
			
			unitOfWork.Commit();
		}

		private static void applyUpdates(IDbCommand command, IEnumerable<Update> updates)
		{
			foreach (var update in updates)
			{
				foreach (var sqlCommand in update.Commands)
				{
					try
					{
						Console.WriteLine("Running upgrade script: " + update.ResourceName);
						command.ExecuteNonQuery(sqlCommand);
					}
					catch (Exception exception)
					{
						throw new Exception("Applying " + update.ResourceName + " failed", exception);
					}
				}
				setVersion(command, update.Version);
			}
		}

		private static void setVersion(IDbCommand command, int id)
		{
			command.ExecuteNonQuery("UPDATE dbo.Version SET Version = " + id);
		}
	}
}