using System.Collections.Generic;

namespace MuonLab.Data
{
	public interface IUpdateProvider
	{
		IEnumerable<Update> AllUpdates();
	}
}