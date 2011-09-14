namespace MuonLab.Web.Xhtml.Components
{
    public interface IPasswordBoxComponent : IVisibleComponent<string>
    {
        IPasswordBoxComponent PreventAutoComplete();
        IPasswordBoxComponent AllowAutoComplete();
    }
}