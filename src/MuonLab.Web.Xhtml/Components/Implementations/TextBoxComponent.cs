namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public class TextBoxComponent<TEntity, TProperty> : FormattableComponent<TEntity, TProperty>, ITextBoxComponent<TProperty> where TEntity : class
    {
        public override string ControlPrefix
        {
            get { return "txt"; }
        }

        protected bool asDefaultEmpty;

        public virtual ITextBoxComponent ShowDefaultAsEmpty()
        {
            this.asDefaultEmpty = true;
            return this;
        }

        public virtual ITextBoxComponent PreventAutoComplete()
        {
            WithAttr("autocomplete", "off");
            return this;
        }

        public ITextBoxComponent AllowAutoComplete()
        {
            WithoutAttr("autocomplete");
            return this;
        }

        protected override string RenderComponent()
        {
            string fieldValue;

            if (this.asDefaultEmpty && Equals(this.value, default(TProperty)))
                fieldValue = null;
            else
                fieldValue = formatValue(this.value);

            if (this.attemptedValue != null)
                fieldValue = this.attemptedValue;

            this.htmlAttributes.Add("type", "text");
            this.htmlAttributes.Add("value", fieldValue);
            var builder = new TagBuilder("input", this.htmlAttributes);
            return builder.ToString();
        }

    }
}