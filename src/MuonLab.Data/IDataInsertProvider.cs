using System.Collections.Generic;

namespace MuonLab.Data
{
	public interface IDataInsertProvider
	{
		IEnumerable<IDataInsert> All();
	}
}