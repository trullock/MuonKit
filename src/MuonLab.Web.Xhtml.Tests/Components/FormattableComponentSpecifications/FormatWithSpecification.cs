using System;

using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.FormattableComponentSpecifications
{
    public abstract class FormatWithSpecification<TComponent, TProperty> : FormattableComponentTSpecification<TComponent, TProperty> where TComponent : IFormattableComponent<TProperty>, new()
    {
        protected override void When()
        {
            component = component.FormatWith(formatFunc).WithValue(value) as IFormattableComponent<TProperty>;
        }

        protected abstract TProperty value { get; }
        protected abstract Func<TProperty, string> formatFunc { get; }

        [Then]
        public void the_value_should_be_formatted_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}