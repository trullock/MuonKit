
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.TextBoxSpecifications
{
    public class PreventAutocompletionSpecification : ComponentSpecification<TextBoxComponent<TestEntity, string>>
    {
        protected override void When()
        {
            component.PreventAutoComplete().WithRenderingOrder(ComponentPart.Component);
        }

        protected override string expectedRendering
        {
            get { return "<input type=\"text\" autocomplete=\"off\" />"; }
        }

        [Then]
        public void the_prevent_auto_completion_atrribute_should_be_rendered()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}