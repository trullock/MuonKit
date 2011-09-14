using System.Collections.Generic;
using System.Linq;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml.Configuration
{
	public class DefaultFormConfiguration : FormConfiguration
	{
		public DefaultFormConfiguration()
		{
			Configure<IVisibleComponent>(c =>
				{
					c.WithRenderingOrder(ComponentPart.Label, ComponentPart.WrapperStartTag, ComponentPart.Component, ComponentPart.ValidationMarker, ComponentPart.HelpText, ComponentPart.WrapperEndTag);
					c.WithLabel();
					c.WithValidationMarker(ValidationMarkerMode.ShowOnError);
				});

			Configure<IPasswordBoxComponent>(c => c.AddClass("textBox"));
			Configure<ITextBoxComponent>(c => c.AddClass("textBox"));
			
			Configure<ITextAreaComponent>(c => c.AddClass("textArea"));
			Configure<ITextAreaComponent<IEnumerable<string>>>(c => c.FormatWith(x => string.Join("\n", x.ToArray())));

			Configure<ICheckBoxComponent>(c => c.AddClass("checkBox"));
			Configure<IDropDownComponent>(c => c.AddClass("dropDown"));
			Configure<IFileUploadComponent>(c => c.AddClass("fileUpload"));

			Configure<IRadioButtonsForBoolComponent>(c => c.AddClass("radioButtons"));
		}
	}
}