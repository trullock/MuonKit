using System;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.HiddenFieldSpecifications
{
	public class WithValueSpecification : WithValueAttributeSpecification<HiddenFieldComponent<TestEntity, string>, string>
	{
		public override string expectedRendering
		{
			get { return "<input type=\"hidden\" value=\"thevalue\" />"; }
		}

		protected override string value
		{
			get { return "thevalue"; }
		}
	}
}