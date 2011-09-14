using System.Data;

namespace MuonLab.Data
{
	public interface IConnectionFactory
	{
		IDbConnection NewConnection();
	}
}