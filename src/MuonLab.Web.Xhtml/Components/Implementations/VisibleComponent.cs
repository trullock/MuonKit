using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuonLab.Commons.Extensions;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public abstract class VisibleComponent<TEntity, TProperty> : Component<TEntity, TProperty>, IVisibleComponent<TProperty>
        where TEntity : class
    {
        protected IEnumerable<string> validationErrors;
        protected bool showLabel;

        protected bool showValidationMarker;
        protected bool showValidationMarkerForRequiredField;

        protected bool showValidationMessage;

        protected IDictionary<string, object> wrapperHtmlAttributes;
        protected string wrapperTagName;
		
        protected string helpText;

        protected IEnumerable<ComponentPart> renderingOrder;

        public string Label { get; protected set; }

        protected VisibleComponent()
        {
            this.validationErrors = new string[0];
            this.renderingOrder = new ComponentPart[0];
        }

        public IVisibleComponent WithErrors(IEnumerable<string> validationErrors)
        {
            this.validationErrors = validationErrors;
            return this;
        }

        /// <summary>
        /// Adds an HTML Label tag to the markup with text automatically determined from the property represented by the component
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithLabel()
        {
            return WithLabel(this.Label);
        }

        /// <summary>
        /// Adds an HTML Label tag to the markup with the given text.
        /// </summary>
        /// <param name="label">The label text</param>
        /// <returns></returns>
        public IVisibleComponent WithLabel(string label)
        {
            this.showLabel = true;
            this.Label = label;
            return this;
        }

        /// <summary>
        /// Prevents an HTML label from being rendered
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithoutLabel()
        {
            this.showLabel = false;
            return this;
        }

        /// <summary>
        /// Adds a validation marker to the markup
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IVisibleComponent WithValidationMarker(ValidationMarkerMode mode)
        {
            this.showValidationMarker = true;
            this.showValidationMarkerForRequiredField = mode == ValidationMarkerMode.ShowWhenRequiredAndOnError;
            return this;
        }

        /// <summary>
        /// Prevents a validation marker from being displayed
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithoutValidationMarker()
        {
            this.showValidationMarker = false;
            this.showValidationMarkerForRequiredField = false;
            return this;
        }

        /// <summary>
        /// Adds a validation message to the markup when the field is invalid
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithValidationMessage()
        {
            this.showValidationMessage = true;
            return this;
        }

        /// <summary>
        /// Prevents a validation message from being displayed
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithoutValidationMessage()
        {
            this.showValidationMarker = false;
            this.showValidationMarkerForRequiredField = false;
            return this;
        }

        /// <summary>
        /// Sets the help text for the component
        /// </summary>
        /// <returns></returns>
        public IVisibleComponent WithHelpText(string helpText)
        {
            this.helpText = helpText;
            return this;
        }

        /// <summary>
        /// set teh field as readonly
        /// </summary>
        /// <returns></returns>
        public virtual IVisibleComponent ReadOnly()
        {
            this.WithAttr("readonly", "readonly");
            return this;
        }

        /// <summary>
        /// Wraps all rendered tags in an outer tag with the given name
        /// </summary>
        /// <param name="tagName">the tag name, e.g. "div". Pass null for no wrapper</param>
        /// <returns></returns>
        public IVisibleComponent WithWrapper(string tagName)
        {
            this.wrapperTagName = tagName;
            this.wrapperHtmlAttributes = null;
            return this;
        }

        /// <summary>
        /// Wraps all rendered tags in an outer tag with the given name
        /// </summary>
        /// <param name="tagName">the tag name, e.g. "div". Pass null for no wrapper</param>
        /// <param name="htmlAttributes">The html attributes to apply to the wrapper</param>
        /// <returns></returns>
        public IVisibleComponent WithWrapper(string tagName, object htmlAttributes)
        {
            this.wrapperTagName = tagName;
            this.wrapperHtmlAttributes = htmlAttributes.ToDictionary();
            return this;
        }

        /// <summary>
        /// Sets the rendering order for this parts of the component
        /// </summary>
        /// <param name="renderingOrder"></param>
        /// <returns></returns>
        public IVisibleComponent WithRenderingOrder(params ComponentPart[] renderingOrder)
        {
            this.renderingOrder = renderingOrder;
            return this;
        }

        protected virtual string RenderLabel()
        {
            var htmlAttribs = new Dictionary<string, object>();
            htmlAttribs.Add("for", getAttr("id"));
			
            var labelBuilder = new TagBuilder("label", htmlAttribs);
            labelBuilder.SetInnerText(this.Label);
			
            return labelBuilder.ToString();
        }

        protected virtual string RenderValidationMarker()
        {
            var firstError = this.validationErrors.FirstOrDefault();

            if (firstError == null)
            {
                var attribs = new Dictionary<string, object>{ {"class", "field-validation-marker"}};
                var builder = new TagBuilder("span", attribs);
                builder.SetInnerText("*");
                return builder.ToString();
            }
            else
            {
                var attribs = new Dictionary<string, object>
                                  {
                                      { "class", "field-validation-marker field-validation-error" }, 
                                      { "title", firstError }
                                  };
                var builder = new TagBuilder("span", attribs);
                builder.SetInnerText("*");
                return builder.ToString();
            }
        }

        protected bool fieldIsValid()
        {
            return this.validationErrors.Count() == 0;
        }

        protected virtual string RenderValidationMessage()
        {
			if (!this.validationErrors.Any())
				return null;

            var builder = new StringBuilder("<ul class=\"field-validation-messages\">");

            foreach (var error in this.validationErrors)
            {
                builder.Append("<li><span>");
                // TODO: html encode this
                builder.Append(error);
                builder.Append("</span></li>");
            }

            builder.Append("</ul>");

            return builder.ToString();
        }

        protected virtual string RenderHelpText()
        {
            return "<span class=\"field-help-text\">" + this.helpText + "</span>";
        }

        protected virtual string RenderWrapperEndTag()
        {
            if (!string.IsNullOrEmpty(this.wrapperTagName))
                return "</" + this.wrapperTagName + ">";

            return null;
        }

        protected virtual string RenderWrapperStartTag()
        {
            if (!string.IsNullOrEmpty(this.wrapperTagName))
            {
                var builder = new TagBuilder(this.wrapperTagName, this.wrapperHtmlAttributes);
                return builder.ToString(TagRenderMode.StartTag);
            }

            return null;
        }

        protected virtual void prepareForRender()
        {
            if (!fieldIsValid())
                this.AddClass("input-validation-error");
        }

        public override string ToString()
        {
            prepareForRender();
			
            var builder = new StringBuilder();

            foreach (var part in renderingOrder)
            {
                switch (part)
                {
                    case ComponentPart.Label:
                        if(showLabel)
                            builder.Append(RenderLabel());
                        break;
                    case ComponentPart.Component:
                        builder.Append(RenderComponent());
                        break;
                    case ComponentPart.HelpText:
                        if(!string.IsNullOrEmpty(this.helpText))
                            builder.Append(RenderHelpText());
                        break;
                    case ComponentPart.ValidationMarker:
                        builder.Append(RenderValidationMarker());
                        break;
                    case ComponentPart.ValidationMessage:
                        builder.Append(RenderValidationMessage());
                        break;
                    case ComponentPart.WrapperStartTag:
                        builder.Append(RenderWrapperStartTag());
                        break;
                    case ComponentPart.WrapperEndTag:
                        builder.Append(RenderWrapperEndTag());
                        break;
                }
            }
            return builder.ToString();
        }		
    }
}