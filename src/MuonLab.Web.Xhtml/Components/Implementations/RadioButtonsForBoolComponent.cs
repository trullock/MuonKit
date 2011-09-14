using System;
using System.Collections.Generic;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml.Components.Implementations
{
	public class RadioButtonsForBoolComponent<T> : VisibleComponent<T, bool?>, IRadioButtonsForBoolComponent where T : class
	{
		private string trueLabel;
		private string falseLabel;

		public RadioButtonsForBoolComponent(string trueLabel, string falseLabel)
		{
			this.trueLabel = trueLabel;
			this.falseLabel = falseLabel;
		}

		public override string ControlPrefix
		{
			get { return "rb"; }
		}

		protected override string RenderComponent()
		{
			var name = this.Name;
			this.htmlAttributes.Remove("name");

			var miniWrapper = new TagBuilder("span", this.htmlAttributes);

			var labelYes = new TagBuilder("label", new Dictionary<string, object> {{"for", this.getAttr("id") + "_True"}});
			labelYes.SetInnerText(trueLabel);

			var radioButtonYes = new TagBuilder("input", new Dictionary<string, object> {{"id", this.getAttr("id") + "_True"}});
			radioButtonYes.HtmlAttributes.Add("type", "radio");
			radioButtonYes.HtmlAttributes.Add("value", "TRUE");
			radioButtonYes.HtmlAttributes.Add("name", name);

			if (this.value.HasValue && this.value.Value)
				radioButtonYes.HtmlAttributes.Add("checked", "checked");

			var labelNo = new TagBuilder("label", new Dictionary<string, object> {{"for", this.getAttr("id") + "_False"}});
			labelNo.SetInnerText(falseLabel);

			var radioButtonNo = new TagBuilder("input", new Dictionary<string, object> {{"id", this.getAttr("id") + "_False"}});
			radioButtonNo.HtmlAttributes.Add("type", "radio");
			radioButtonNo.HtmlAttributes.Add("value", "FALSE");
			radioButtonNo.HtmlAttributes.Add("name", name);

			if (this.value.HasValue && !this.value.Value)
				radioButtonNo.HtmlAttributes.Add("checked", "checked");

			return miniWrapper.ToString(TagRenderMode.StartTag) + labelYes + radioButtonYes + labelNo + radioButtonNo + miniWrapper.ToString(TagRenderMode.EndTag);
		}

		public IRadioButtonsForBoolComponent WithTrueLabel(string label)
		{
			this.trueLabel = label;
			return this;
		}

		public IRadioButtonsForBoolComponent WithFalseLabel(string label)
		{
			this.falseLabel = label;
			return this;
		}
	}
}