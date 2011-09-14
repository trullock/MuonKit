
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Configuration;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public class WithLabelSpecification : VisibleComponentSpecification<TestVisibleComponent<TestEntity, string>>
    {
		protected override void Given()
		{
			base.Given();

			component.WithRenderingOrder(ComponentPart.Label);
		}

		protected override string expectedRendering
		{
			get { return "<label for=\"theid\">thelabel</label>"; }
		}

		protected override void When()
        {
			component.WithLabel("thelabel").WithId("theid");
        }

        [Then]
        public void the_label_should_be_rendered_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}