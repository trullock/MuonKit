using System.Collections.Generic;

namespace MuonLab.Data
{
	public class Update
	{
		public string ResourceName { get; private set; }
		public int Version { get; private set; }
		public IEnumerable<string> Commands { get; private set; }

		public Update(string resourceName, int version, IEnumerable<string> commands)
		{
			this.ResourceName = resourceName;
			this.Version = version;
			this.Commands = commands;
		}
	}
}