using System;

using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.TextBoxSpecifications
{
    public class ShowDefaultAsEmptySpecification : ComponentSpecification<TextBoxComponent<TestEntity, DateTime>>
    {
        protected override void When()
        {
			component.ShowDefaultAsEmpty().WithRenderingOrder(ComponentPart.Component).WithValue(default(DateTime));
        }

        protected override string expectedRendering
        {
            get { return "<input type=\"text\" />"; }
        }

        [Then]
        public void the_value_should_be_set_to_the_default_value()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}