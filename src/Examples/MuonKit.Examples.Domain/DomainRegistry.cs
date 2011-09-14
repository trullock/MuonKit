using MuonKit.Examples.Domain.Services;
using MuonKit.Examples.Domain.Services.Implementations;
using MuonLab.Commons.StructureMap.Validation;
using MuonLab.Validation;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace MuonKit.Examples.Domain
{
	public class DomainRegistry : Registry
	{
		public DomainRegistry()
		{
			registerValidators();
			registerServices();
		}

		private void registerServices()
		{
			Scan(x =>
				{
					x.TheCallingAssembly();
					x.WithDefaultConventions();
					x.IncludeNamespaceContainingType<UserService>();
					x.IncludeNamespaceContainingType<IUserService>();
				});
		}

		private void registerValidators()
		{
			Scan(x =>
			{
				x.TheCallingAssembly();
				x.With(new ValidatorRegistrationConvention());
			});

			// Add the empty validator so that you don't have to manually create empty validators for entities
			// which have no rules
			For(typeof(IValidator<>))
				.LifecycleIs(InstanceScope.Singleton)
				.Use(typeof(EmptyValidator<>));
		}
	}
}