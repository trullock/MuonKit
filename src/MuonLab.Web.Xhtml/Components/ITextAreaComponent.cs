namespace MuonLab.Web.Xhtml.Components
{
    public interface ITextAreaComponent : IFormattableComponent
    {
        ITextAreaComponent WithRows(int rows);
        ITextAreaComponent WithCols(int cols);
    }

    public interface ITextAreaComponent<TProperty> : IFormattableComponent<TProperty>, ITextAreaComponent
    {

    }
}