using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.HiddenFieldSpecifications
{
	public class WithNameSpecification : WithNameSpecification<HiddenFieldComponent<TestEntity, string>>
	{
		protected override string expectedRendering
		{
			get { return "<input type=\"hidden\" name=\"thename\" />"; }
		}
	}
}