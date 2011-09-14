using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml.Components
{
    public interface IVisibleComponent<TProperty> : IComponent<TProperty>, IVisibleComponent
    {
		
    }

    public interface IVisibleComponent : IComponent
    {
        IVisibleComponent WithRenderingOrder(params ComponentPart[] renderingOrder);


        string Label { get; }

        /// <summary>
        /// Adds an HTML Label tag to the markup with text automatically determined from the property represented by the component
        /// </summary>
        /// <returns></returns>
        IVisibleComponent WithLabel();

        /// <summary>
        /// Adds an HTML Label tag to the markup with the given text.
        /// </summary>
        /// <param name="label">The label text</param>
        /// <returns></returns>
        IVisibleComponent WithLabel(string label);


        /// <summary>
        /// Prevents an HTML label from being rendered
        /// </summary>
        /// <returns></returns>
        IVisibleComponent WithoutLabel();

        /// <summary>
        /// Adds a validation marker to the markup
        /// </summary>
        /// <param name="side">The position of the marker relative to the component</param>
        /// <param name="mode">The display mode of the validation marker</param>
        /// <returns></returns>
        IVisibleComponent WithValidationMarker(ValidationMarkerMode mode);

        /// <summary>
        /// Prevents a validation marker from being displayed
        /// </summary>
        /// <returns></returns>
        IVisibleComponent WithoutValidationMarker();

        /// <summary>
        /// Adds a validation message to the markup when the field is invalid
        /// </summary>
        /// <param name="side">The position of the marker relative to the component</param>
        /// <returns></returns>
        IVisibleComponent WithValidationMessage();

        /// <summary>
        /// Prevents a validation message from being displayed
        /// </summary>
        /// <returns></returns>
        IVisibleComponent WithoutValidationMessage();

        /// <summary>
        /// Sets the help text for the component
        /// </summary>
        /// <returns></returns>
        IVisibleComponent WithHelpText(string helpText);

        /// <summary>
        /// set teh field as readonly
        /// </summary>
        /// <returns></returns>
        IVisibleComponent ReadOnly();

        /// <summary>
        /// Wraps all rendered tags in an outer tag with the given name
        /// </summary>
        /// <param name="tagName">the tag name, e.g. "div". Pass null for no wrapper</param>
        /// <returns></returns>
        IVisibleComponent WithWrapper(string tagName);

        /// <summary>
        /// Wraps all rendered tags in an outer tag with the given name
        /// </summary>
        /// <param name="tagName">the tag name, e.g. "div". Pass null for no wrapper</param>
        /// <param name="htmlAttributes">The html attributes to apply to the wrapper</param>
        /// <returns></returns>
        IVisibleComponent WithWrapper(string tagName, object htmlAttributes);
    }
}