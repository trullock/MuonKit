using System.Collections.Generic;

namespace MuonLab.Web.Xhtml
{
	public interface IErrorProvider
	{
		IEnumerable<string> GetErrorsFor(string componentName);
		bool HasErrors(string componentName);
		string GetAttemptedValue(string componentName);
	}
}