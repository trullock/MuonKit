
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using MuonLab.Web.Xhtml.Configuration;
using Rhino.Mocks;

namespace MuonLab.Web.Xhtml.Tests.ComponentFactorySpecifications
{
	public abstract class ComponentFactorySpecification : Specification
	{
		protected TestEntity entity;
		protected IComponent component;
		protected ComponentFactory<TestEntity> factory;
		protected IFormConfiguration configuration;

		protected override void Given()
		{
			Dependency<IComponentNameResolver>()
				.Stub(s => s.ResolveName<TestEntity, string>(null))
				.IgnoreArguments()
				.Return("thename");

			Dependency<IComponentIdResolver>()
				.Stub(s => s.ResolveId<TestEntity, string>(null, null))
				.IgnoreArguments()
				.Return("theid");

			Dependency<IComponentLabelResolver>()
				.Stub(s => s.ResolveLabel<TestEntity, string>(null))
				.IgnoreArguments()
				.Return("thelabel");

			this.configuration = Dependency<IFormConfiguration>();

			this.entity = new TestEntity();

			factory = Subject<ComponentFactory<TestEntity>>();
		}

		protected abstract string expectedRendering { get; }

		[Then]
		public void the_component_should_have_the_configuration_applied_to_it()
		{
			configuration.AssertWasCalled(c => c.Initialize(component));
		}

		[Then]
		public void the_component_should_be_rendered_correctly()
		{
			component.ToString().ShouldEqual(expectedRendering);
		}
	}
}