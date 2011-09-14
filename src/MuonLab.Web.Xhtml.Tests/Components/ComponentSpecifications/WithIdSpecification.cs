
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class WithIdSpecification<TComponent> : ComponentSpecification<TComponent> where TComponent : IComponent, new()
	{
        protected override void When()
        {
            component.WithId("theid");
        }

        [Then]
        public void the_id_should_be_set_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}