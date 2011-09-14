using MuonLab.Commons.Cryptography;
using MuonLab.Web.Mvc;
using MuonLab.Web.Xhtml;
using MuonLab.Web.Xhtml.Configuration;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace MuonKit.Examples.MvcApplication
{
	public class MvcRegistry : Registry
	{
		public MvcRegistry()
		{
			ConfigureForms();
		}

		private void ConfigureForms()
		{
			// IComponentLabelResolver is responsible for determining the label text from the property
			// a form component represents
			For<IComponentLabelResolver>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use<EnglishComponentLabelResolver>();

			// IComponentIdResolver is responsible for determining the id attribute from the property
			// a form component represents
			For<IComponentIdResolver>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use<DefaultComponentIdResolver>();

			// IComponentNameResolver is responsible for determining the name attribute from the property
			// a form component represents
			For<IComponentNameResolver>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use<ResourceModelBinderComponentAndViolationNameResolver>();

			// IComponentFactory<> is the factory which produces form components for the views
			// and applies the default configurations
			For(typeof(IComponentFactory<>))
				.LifecycleIs(InstanceScope.Hybrid)
				.Use(typeof(ComponentFactory<>));

			// This is required by the SecureComponentFacotry<>
			For<ICryptoService>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use<CryptoService>();

			// IFormConfiguration is the configuration for form component rendering defaults
			For<IFormConfiguration>()
				.LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton))
				.Use<DefaultFormConfiguration>();
		}
	}
}