using System;

using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
    public class AddClassWithExistingClassSpecification : Specification
    {
		private IComponent component;

		protected override void Given()
		{
			this.component = new TestComponent<TestEntity, string>();
		}

    	protected override void When()
        {
            component = component.WithClass("firstclass").AddClass("additional class");
        }

        [Then]
        public void the_class_should_be_set_correctly()
        {
            component.ToString().ShouldEqual("<test class=\"firstclass additional class\" />");
        }
    }
}