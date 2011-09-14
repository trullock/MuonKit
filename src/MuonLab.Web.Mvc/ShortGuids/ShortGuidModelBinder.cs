using System;
using System.Web.Mvc;
using MuonLab.Commons;

namespace MuonLab.Web.Mvc.ShortGuids
{
	public class ShortGuidModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ValueProvider.ContainsKey(bindingContext.ModelName))
			{
				ShortGuid shortGuid;
				var valid = ShortGuid.TryParse(bindingContext.ValueProvider[bindingContext.ModelName].AttemptedValue, out shortGuid);

				if (valid)
				{
					if (bindingContext.ModelType == typeof(Guid))
						return shortGuid.ToGuid();

					if (bindingContext.ModelType == typeof(ShortGuid))
						return shortGuid;
				}
			}

			return Guid.Empty;
		}
	}
}