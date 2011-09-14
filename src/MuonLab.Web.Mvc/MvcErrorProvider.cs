using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MuonLab.Web.Xhtml;

namespace MuonLab.Web.Mvc
{
	public class MvcErrorProvider : IErrorProvider
	{
		private readonly ModelStateDictionary modelState;

		public MvcErrorProvider(ModelStateDictionary modelState)
		{
			this.modelState = modelState;
		}

		public IEnumerable<string> GetErrorsFor(string componentName)
		{
			if (this.modelState.ContainsKey(componentName))
				return this.modelState[componentName].Errors.Select(e => e.ErrorMessage);

			return new string[0];
		}

		public bool HasErrors(string componentName)
		{
			if (this.modelState.ContainsKey(componentName))
				return this.modelState[componentName].Errors.Any();

			return false;
		}

		public string GetAttemptedValue(string componentName)
		{
			return this.modelState[componentName].Value.AttemptedValue;
		}
	}
}