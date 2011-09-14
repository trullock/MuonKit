
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class WithNameSpecification<TComponent> : ComponentSpecification<TComponent> where TComponent : IComponent, new()
    {
        protected override void When()
        {
            component.WithName("thename");
        }

        [Then]
        public void the_name_should_be_set_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}