namespace MuonLab.Web.Xhtml.Components
{
    public interface ITextBoxComponent : IFormattableComponent
    {
        ITextBoxComponent ShowDefaultAsEmpty();
        ITextBoxComponent PreventAutoComplete();
        ITextBoxComponent AllowAutoComplete();
    }

    public interface ITextBoxComponent<TProperty> : IFormattableComponent<TProperty>, ITextBoxComponent
    {
		
    }
}