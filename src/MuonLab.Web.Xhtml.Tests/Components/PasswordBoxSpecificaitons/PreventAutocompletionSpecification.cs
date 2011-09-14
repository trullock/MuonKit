using System;

using MuonLab.Testing;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;
using MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications;
using NUnit.Framework;

namespace MuonLab.Web.Xhtml.Tests.Components.PasswordBoxSpecificaitons
{
	public class PreventAutocompletionSpecification : ComponentSpecification<PasswordBoxComponent<TestEntity>>
	{
		protected override void When()
		{
			this.component.PreventAutoComplete().WithRenderingOrder(ComponentPart.Component);
		}

		protected override string expectedRendering
		{
			get { return "<input type=\"password\" autocomplete=\"off\" />"; }
		}

		[Then]
		public void the_prevent_auto_completion_atrribute_should_be_rendered()
		{
			this.component.ToString().ShouldEqual(this.expectedRendering);
		}
	}
}