using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuonLab.Commons.DI;

namespace MuonLab.Web.Xhtml
{
    public class TagBuilder
    {
    	private readonly IHtmlEncoder encoder;
    	public string TagName { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }
        public string InnerHtml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagname">The tag name</param>
        /// <param name="htmlAttributes">A dictionary of html attributes. Values get HtmlEncoded.</param>
        public TagBuilder(string tagname, IDictionary<string, object> htmlAttributes)
        {
            this.TagName = tagname;
            this.HtmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

        	this.encoder = (DependencyResolver.Current != null ? DependencyResolver.Current.TryGetInstance<IHtmlEncoder>() : null) ?? new HttpUtilityHtmlEncoder();
        }

		public void SetInnerText(string text)
		{
			this.InnerHtml = this.encoder.HtmlEncode(text);
		}

        public override string ToString()
        {
            var builder = new StringBuilder();

            RenderStart(builder);

        	if (!string.IsNullOrEmpty(this.InnerHtml) || 
				TagName.ToUpper() == "SELECT" ||
				TagName.ToUpper() == "TEXTAREA" ||
				TagName.ToUpper() == "UL" ||
				TagName.ToUpper() == "OL")
            {
				builder
                    .Append('>')
                    .Append(InnerHtml)
                    .Append("</")
                    .Append(TagName)
                    .Append('>');
            }
            else
                builder.Append(" />");

            return builder.ToString();
        }

		private void RenderStart(StringBuilder builder)
		{
			builder.Append('<').Append(TagName);
			foreach (var key in OrderedHtmlAttributeKeys)
			{
				var encoded = HtmlEncode(this.HtmlAttributes[key]);
				if(encoded != null)
					builder.AppendFormat(" {0}=\"{1}\"", key, encoded);
			}
		}

		private string HtmlEncode(object value)
		{
			if(value == null)
				return null;

			var stringValue = value.ToString();

			return stringValue == null ? null : this.encoder.HtmlAttributeEncode(stringValue);
		}

		// TODO: Improve this!
		protected IEnumerable<string> OrderedHtmlAttributeKeys
		{
			get
			{
				return this.HtmlAttributes.Keys.OrderBy(k =>
				{
					switch (k.ToUpper())
					{
						case "TYPE":
							return ".";
						case "ID":
							return "..";
						case "NAME":
							return "...";
						default:
							return k;
					}
				});
			}
		}

    	public string ToString(TagRenderMode mode)
    	{
    	    if(mode == TagRenderMode.StartTag)
    		{
				var builder = new StringBuilder();
				RenderStart(builder);
    			builder.Append('>');
    			return builder.ToString();
    		}
    	
            return "</" + this.TagName + ">";
    	}
    }
}