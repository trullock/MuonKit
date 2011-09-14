namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public class PasswordBoxComponent<TEntity> : VisibleComponent<TEntity, string>, IPasswordBoxComponent 
        where TEntity : class
    {
        public override string ControlPrefix
        {
            get { return "txt"; }
        }

        public IPasswordBoxComponent PreventAutoComplete()
        {
            WithAttr("autocomplete", "off");
            return this;
        }

        public IPasswordBoxComponent AllowAutoComplete()
        {
            WithoutAttr("autocomplete");
            return this;
        }

        protected override string RenderComponent()
        {
            this.htmlAttributes.Add("value", this.value);
            this.htmlAttributes.Add("type", "password");
			
            var builder = new TagBuilder("input", this.htmlAttributes);
            return builder.ToString();
        }
    }
}