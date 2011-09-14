using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using MuonLab.Web.Xhtml.Components.Implementations;

namespace MuonLab.Web.Xhtml.Tests.Components.VisibleComponentSpecifications
{
    public class RenderingOrderSpecification : Specification
    {
        private IVisibleComponent component;

        protected override void Given()
        {
            component = new TestComponent<TestEntity, string>().WithRenderingOrder(ComponentPart.Label, ComponentPart.WrapperStartTag, ComponentPart.Component,ComponentPart.ValidationMarker, ComponentPart.ValidationMessage, ComponentPart.HelpText, ComponentPart.WrapperEndTag);
        }

        protected override void When()
        {
        	component.WithLabel().WithHelpText("helptext");
        }

        [Then]
        public void the_parts_should_be_rendered_in_the_right_order()
        {
			component.ToString().ShouldEqual("labelwrapperstarttagcomponentvalidationmarkervalidationmessagehelptextwrapperendtag");
        }

        private class TestComponent<TEntity, TProperty> : VisibleComponent<TEntity, TProperty> where TEntity : class
        {
            public override string ControlPrefix
            {
                get { return "ctrl"; }
            }

            protected override string RenderComponent()
            {
                return "component";
            }

            protected override string RenderLabel()
            {
                return "label";
            }

            protected override string RenderValidationMarker()
            {
                return "validationmarker";
            }

            protected override string RenderValidationMessage()
            {
                return "validationmessage";
            }

            protected override string RenderHelpText()
            {
                return "helptext";
            }

            protected override string RenderWrapperEndTag()
            {
                return "wrapperendtag";
            }

            protected override string RenderWrapperStartTag()
            {
                return "wrapperstarttag";
            }
        }

        private class TestEntity
        {
            public string Property { get; set; }
        }
    }
}