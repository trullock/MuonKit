using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class ComponentSpecification<TComponent> : Specification where TComponent : IComponent, new()
	{
        protected TComponent component;

		protected override void Given()
		{
			this.component = new TComponent();
		}

		protected abstract string expectedRendering { get; }
	}
}