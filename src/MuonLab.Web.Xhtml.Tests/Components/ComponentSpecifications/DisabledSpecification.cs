
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class DisabledSpecification<TComponent> : ComponentSpecification<TComponent> where TComponent : IComponent, new()
	{
        protected override void When()
        {
            component.Disabled();
        }

        [Then]
        public void the_disabled_attr_should_be_set()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
	}
}