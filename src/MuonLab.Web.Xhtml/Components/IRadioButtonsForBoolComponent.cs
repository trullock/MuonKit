namespace MuonLab.Web.Xhtml.Components
{
    public interface IRadioButtonsForBoolComponent : IVisibleComponent
    {
    	IRadioButtonsForBoolComponent WithTrueLabel(string label);
    	IRadioButtonsForBoolComponent WithFalseLabel(string label);
    }
}