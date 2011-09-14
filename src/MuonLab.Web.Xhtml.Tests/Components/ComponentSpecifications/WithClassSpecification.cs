using System;

using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class WithClassSpecification<TComponent> : ComponentSpecification<TComponent> where TComponent : IComponent, new()
    {
        protected override void When()
        {
            component.WithClass("firstclass").AddClass("additional class").WithClass("theclass");
        }

        [Then]
        public void the_class_should_be_set_correctly()
        {
            component.ToString().ShouldEqual(expectedRendering);
        }
    }
}