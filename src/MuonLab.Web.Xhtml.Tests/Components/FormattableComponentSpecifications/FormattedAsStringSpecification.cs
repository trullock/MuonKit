using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.FormattableComponentSpecifications
{
    public abstract class FormattedAsStringSpecification<TComponent, TProperty> : FormattableComponentSpecification<TComponent> where TComponent : IFormattableComponent, new()
    {
        protected override void When()
        {
            component = component.FormattedAs(formatString).WithValue(value) as IFormattableComponent;
        }

        protected abstract TProperty value { get; }
        protected abstract string formatString { get; }

        [Then]
        public void the_value_should_be_formatted_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}