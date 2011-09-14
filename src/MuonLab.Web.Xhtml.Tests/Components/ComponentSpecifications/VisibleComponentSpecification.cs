using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public abstract class VisibleComponentSpecification<TComponent> : Specification where TComponent : IVisibleComponent, new()
	{
        protected TComponent component;

		protected override void Given()
		{
			this.component = new TComponent();
		}

		protected abstract string expectedRendering { get; }
	}
}