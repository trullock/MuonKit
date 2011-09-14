using System.Data;
using System.Data.SqlClient;

namespace MuonLab.Data
{
	public class ConnectionFactory : IConnectionFactory
	{
		private readonly string connectionString;

		public ConnectionFactory(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IDbConnection NewConnection()
		{
			return new SqlConnection(this.connectionString);
		}
	}
}