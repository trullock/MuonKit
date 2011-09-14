namespace MuonLab.Web
{
	public interface IHtmlEncoder
	{
		string HtmlEncode(string input);
		string HtmlAttributeEncode(string input);
	}
}