namespace MuonLab.Web.Xhtml.Components
{
    public interface IDropDownComponent : IVisibleComponent
    {
        /// <summary>
        /// Adds a Null option with the default null option text.
        /// </summary>
        /// <returns></returns>
        IDropDownComponent WithNullOption();

        /// <summary>
        /// Adds a Null option with and sets the null option text.
        /// </summary>
        /// <param name="nullOptionText">The null option text.</param>
        /// <returns></returns>
        IDropDownComponent WithNullOption(string nullOptionText);

        /// <summary>
        /// Removes a previously set null option
        /// </summary>
        /// <returns></returns>
        IDropDownComponent WithoutNullOption();
    }

    public interface IDropDownComponent<TProperty> : IDropDownComponent
    {

    }
}