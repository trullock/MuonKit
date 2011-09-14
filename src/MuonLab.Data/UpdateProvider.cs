using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MuonLab.Data
{
	public class UpdateProvider : IUpdateProvider
	{
		private const string resourceNamePattern = @"(\d+)_(.*?).sql";
		private readonly Assembly scriptAssembly;

		public UpdateProvider(Assembly scriptAssembly)
		{
			this.scriptAssembly = scriptAssembly;
		}

		public IEnumerable<Update> AllUpdates()
		{
			return getUpdates();
		}

		private IEnumerable<Update> getUpdates()
		{
			return this.scriptAssembly.GetManifestResourceNames()
				.Where(r => Regex.IsMatch(r, resourceNamePattern, RegexOptions.IgnoreCase))
				.Select(r => new Update(r, getVersionFromResourceName(r), getCommandsFromResource(r, this.scriptAssembly)))
				.OrderBy(u => u.Version);
		}

		private static int getVersionFromResourceName(string name)
		{
			return int.Parse(Regex.Match(name, resourceNamePattern).Groups[1].Value);
		}

		private static IEnumerable<string> getCommandsFromResource(string resourceName, Assembly assembly)
		{
			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var streamReader = new StreamReader(stream))
			{
				var stringBuilder = new StringBuilder();

				while (streamReader.Peek() > -1)
				{
					var line = streamReader.ReadLine();
                    
					if (line.Trim().ToUpper() == "GO")
					{
						yield return stringBuilder.ToString();
						stringBuilder = new StringBuilder();
					}
					else
					{
						stringBuilder.AppendLine(line);
					}
				}

				if (stringBuilder.Length > 0)
					yield return stringBuilder.ToString();
			}
		}
	}
}