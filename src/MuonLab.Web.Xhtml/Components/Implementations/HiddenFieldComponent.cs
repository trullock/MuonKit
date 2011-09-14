using System;

namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public class HiddenFieldComponent<TEntity, TProperty> : Component<TEntity, TProperty>, IHiddenFieldComponent<TProperty> where TEntity : class
    {
        private readonly Func<TProperty, string> toStringFunc;

        public override string ControlPrefix
        {
            get { return "hid"; }
        }

        public HiddenFieldComponent() : this(x => x.ToString())
        {
        }

        public HiddenFieldComponent(Func<TProperty, string> toStringFunc)
        {
            this.toStringFunc = toStringFunc;
        }

        protected override string RenderComponent()
        {
            var builder = new TagBuilder("input", this.htmlAttributes);
            builder.HtmlAttributes.Add("type", "hidden");
            if(!ReferenceEquals(this.value, null))
                builder.HtmlAttributes.Add("value", toStringFunc(this.value));
            return builder.ToString();
        }
    }
}