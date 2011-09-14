
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class WithValueAttributeSpecification<TComponent, TProperty> : ComponentTSpecification<TComponent, TProperty> where TComponent : IComponent<TProperty>, new()
	{
		protected abstract TProperty value { get; }

		protected override void When()
		{
			component = component.WithValue(value);
		}

		[Then]
		public void the_value_should_be_set_correctly()
		{
			component.ToString().ShouldEqual(expectedRendering);
		}
	}
}