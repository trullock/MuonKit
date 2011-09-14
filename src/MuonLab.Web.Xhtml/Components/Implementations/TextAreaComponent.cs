namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public class TextAreaComponent<TEntity, TProperty> : FormattableComponent<TEntity, TProperty>, ITextAreaComponent<TProperty> where TEntity : class
    {
        public override string ControlPrefix
        {
            get { return "txt"; }
        }

        public ITextAreaComponent WithRows(int rows)
        {
            WithAttr("rows", rows);
            return this;
        }

        public ITextAreaComponent WithCols(int cols)
        {
            WithAttr("cols", cols);
            return this;
        }

        protected override string RenderComponent()
        {
            var builder = new TagBuilder("textarea", this.htmlAttributes);
            builder.InnerHtml = formatValue(this.value);
            return builder.ToString();
        }
    }
}