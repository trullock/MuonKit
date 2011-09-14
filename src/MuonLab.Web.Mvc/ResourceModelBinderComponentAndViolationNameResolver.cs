using MuonLab.Commons.Reflection;
using MuonLab.Validation;
using MuonLab.Web.Mvc.ModelBinding;
using MuonLab.Web.Xhtml;

namespace MuonLab.Web.Mvc
{
	public class ResourceModelBinderComponentAndViolationNameResolver : DelimitedComponentNameResolver, IViolationPropertyNameResolver
	{
		public ResourceModelBinderComponentAndViolationNameResolver() : base(Binder.NameDelimeter)
		{
		}

		public string ResolvePropertyName(IViolation violation)
		{
			var propertyChain = ReflectionHelper.PropertyChainToString(violation.Property, Binder.NameDelimeter);

			if (string.IsNullOrEmpty(propertyChain))
				return null;

			return Binder.NameDelimeter + propertyChain;
		}
	}
}