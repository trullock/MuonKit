namespace MuonLab.Web.Xhtml.Components
{
    public interface IHiddenFieldComponent : IComponent
    {
		
    }

    public interface IHiddenFieldComponent<TProperty> : IComponent<TProperty>, IHiddenFieldComponent
    {

    }
}