using System.Data;

namespace MuonLab.Data
{
	public static class IDbCommandHelpers
	{
		public static void ExecuteNonQuery(this IDbCommand command, string commandText)
		{
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
			}
			command.CommandText = commandText;
			command.ExecuteNonQuery();
		}

		public static object ExecuteScalar(this IDbCommand command, string commandText)
		{
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
			}
			command.CommandText = commandText;
			return command.ExecuteScalar();
		}
	}
}