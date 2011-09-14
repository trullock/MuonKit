using NHibernate.ByteCode.Castle;

namespace MuonLab.Data
{
	/// <summary>
	/// This is a hax to make the bytecode dll get deployed to assemblies which reference this assembly.
	/// </summary>
	internal class DllHax
	{
		private DllHax()
		{
			new ProxyFactory();
		}
	}
}