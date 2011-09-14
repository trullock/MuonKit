
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public class WithAttrForNameSpecification : Specification
	{
		private IComponent component;

		protected override void Given()
		{
			this.component = new TestComponent<TestEntity, string>();
		}

		protected override void When()
        {
			component = component
				.WithName("name")
				.WithAttr("name", "other")
				.WithAttr("misc", "random");
        }

		[Then]
		public void the_misc_attr_should_be_set_correctly_and_the_name_should_be_overridden()
		{
			component.ToString().ShouldEqual("<test name=\"other\" misc=\"random\" />");
		}
	}
}