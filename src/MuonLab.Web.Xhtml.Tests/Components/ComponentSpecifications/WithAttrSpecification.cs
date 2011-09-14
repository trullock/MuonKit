
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class WithAttrSpecification<TComponent> : ComponentSpecification<TComponent> where TComponent : IComponent, new()
    {
        protected override void When()
        {
            component.WithAttr("misc", "random");
        }

        [Then]
        public void the_misc_attr_should_be_set_correctly_and_the_name_shouldnt_be_overridden()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}