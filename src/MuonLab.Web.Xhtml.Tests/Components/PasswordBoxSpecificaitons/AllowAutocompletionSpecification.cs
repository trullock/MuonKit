
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.PasswordBoxSpecificaitons
{
	public class AllowAutocompletionSpecification : ComponentSpecification<PasswordBoxComponent<TestEntity>>
	{
		protected override void When()
		{
			this.component.PreventAutoComplete().AllowAutoComplete().WithRenderingOrder(ComponentPart.Component);
		}

		protected override string expectedRendering
		{
			get { return "<input type=\"password\" />"; }
		}

		[Then]
		public void the_prevent_auto_completion_atrribute_should_not_be_rendered()
		{
			this.component.ToString().ShouldEqual(this.expectedRendering);
		}
	}
}