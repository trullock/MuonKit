namespace MuonLab.Web.Xhtml.Components
{
    public interface IComponent : IRawHtml
    {
        string Name { get; }

        /// <summary>
        /// Sets the Id attribute of the component
        /// </summary>
        /// <param name="id">The id to set</param>
        /// <returns></returns>
        IComponent WithId(string id);

        /// <summary>
        /// Sets the Name attribute of the compontent
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns></returns>
        IComponent WithName(string name);

        /// <summary>
        /// Sets an attribute-value on the component
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IComponent WithAttr(string name, object value);

        /// <summary>
        /// Sets an attribute-value on the component if a condition is met
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IComponent WithAttrIf(bool condition, string name, object value);

        /// <summary>
        /// Removes an attribute if set
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IComponent WithoutAttr(string name);

        /// <summary>
        /// Fluent CssClass setter
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        IComponent AddClass(string className);

        /// <summary>
        /// Fluent CssClass setter
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        IComponent WithClass(string className);

        /// <summary>
        /// Set the field as disabled
        /// </summary>
        /// <returns></returns>
        IComponent Disabled();

        /// <summary>
        /// Sets the value for the component. Although this is weakly typed, you should set it with the correct type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IComponent WithValue(object value);

        /// <summary>
        /// Sets the raw string attempted value. This should not be used outside of the ComponentFactory
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IComponent WithAttemptedValue(string value);
    }

    public interface IComponent<TProperty> : IComponent
    {
        /// <summary>
        /// Sets the value for this component
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IComponent<TProperty> WithValue(TProperty value);
    }
}