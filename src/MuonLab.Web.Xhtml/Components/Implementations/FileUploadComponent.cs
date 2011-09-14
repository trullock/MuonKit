namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public class FileUploadComponent<TEntity, TProperty> : VisibleComponent<TEntity, TProperty>, IFileUploadComponent where TEntity : class
    {
        public override string ControlPrefix
        {
            get { return "fup"; }
        }

        protected override string RenderComponent()
        {
            this.htmlAttributes.Add("type", "file");

            var builder = new TagBuilder("input", this.htmlAttributes);
            return builder.ToString();
        }
    }
}