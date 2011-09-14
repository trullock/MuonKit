
using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.FileUploadspecifications
{
	public class DefaultSpecification : ComponentSpecification<FileUploadComponent<TestEntity, string>>
	{
		protected override void When()
		{
			this.component.WithRenderingOrder(ComponentPart.Component);
		}

		protected override string expectedRendering
		{
			get { return "<input type=\"file\" />"; }
		}

		[Then]
		public void the_prevent_auto_completion_atrribute_should_be_rendered()
		{
			this.component.ToString().ShouldEqual(this.expectedRendering);
		}
	}
}