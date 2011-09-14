using MuonLab.Web.Xhtml.Components.Implementations;

namespace MuonLab.Web.Xhtml.Tests.Components.ComponentSpecifications
{
	public class TestVisibleComponent<TEntity, TProperty> : VisibleComponent<TEntity, TProperty> where TEntity : class
	{
		public override string ControlPrefix
		{
			get { return "ctrl"; }
		}

		protected override string RenderComponent()
		{
			this.htmlAttributes["name"] = this.Name;
			var builder = new TagBuilder("test", this.htmlAttributes);
			return builder.ToString();
		}
	}
}