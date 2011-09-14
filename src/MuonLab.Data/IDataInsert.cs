namespace MuonLab.Data
{
	public interface IDataInsert
	{
		void Run();
		string Name { get; }
	}
}