
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public class AddClassSpecification : Specification
	{
		private IComponent component;

		protected override void Given()
		{
			this.component = new TestComponent<TestEntity, string>();
		}

		protected override void When()
        {
            component = component.AddClass("myclass");
        }

        [Then]
        public void the_class_should_be_set_correctly()
        {
			component.ToString().ShouldEqual("<test class=\"myclass\" />");
        }
	}
}