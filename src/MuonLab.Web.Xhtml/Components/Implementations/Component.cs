using System;
using System.Collections.Generic;

namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public abstract class Component<TEntity, TProperty> : IComponent<TProperty> where TEntity : class
    {
        protected readonly IDictionary<string, object> htmlAttributes;
        public abstract string ControlPrefix { get; }
        protected TProperty value;
        protected string attemptedValue;

        public string Name { get { return this.getAttr("name"); } }

        protected Component()
        {
            this.htmlAttributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Sets the Id attribute of the component
        /// </summary>
        /// <param name="id">The id to set</param>
        /// <returns></returns>
        public IComponent WithId(string id)
        {
            return WithAttr("id", id);
        }

        /// <summary>
        /// Sets the Name attribute of the compontent
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <returns></returns>
        public IComponent WithName(string name)
        {
            this.WithAttr("name", name);
            return this;
        }

        /// <summary>
        /// Sets an attribute-value on the component
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IComponent WithAttr(string name, object value)
        {
            this.htmlAttributes[name] = value;
            return this;
        }

        /// <summary>
        /// Sets ann attribute-value on the component of a condition is true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IComponent WithAttrIf(bool condition, string name, object value)
        {
            if (condition)
                WithAttr(name, value);

            return this;
        }

        /// <summary>
        /// Removes an attribute if it exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IComponent WithoutAttr(string name)
        {
            if (this.htmlAttributes.ContainsKey(name))
                this.htmlAttributes.Remove(name);
            return this;
        }

        /// <summary>
        /// Sets the value for this component
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IComponent<TProperty> WithValue(TProperty value)
        {
            this.value = value;
            return this;
        }

        IComponent IComponent.WithValue(object value)
        {
            // TODO allow inheritance here
            if (value.GetType() != typeof(TProperty))
                throw new ArgumentException("value is not of type `" + typeof (TProperty) + "`");

            this.WithValue((TProperty) value);
            return this;
        }

        public IComponent WithAttemptedValue(string value)
        {
            this.attemptedValue = value;
            return this;
        }

        /// <summary>
        /// Fluent CssClass setter
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public virtual IComponent AddClass(string className)
        {
            if (!string.IsNullOrEmpty(getAttr("class")))
                return WithAttr("class", getAttr("class") + ' ' + className);
            else
                return WithAttr("class", className);
        }

        /// <summary>
        /// Set the field as disabled
        /// </summary>
        /// <returns></returns>
        public virtual IComponent Disabled()
        {
            this.WithAttr("disabled", "disabled");
            return this;
        }

        /// <summary>
        /// Fluent CssClass setter
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public virtual IComponent WithClass(string className)
        {
            return WithAttr("class", className);
        }

        protected string getAttr(string name)
        {
            if (this.htmlAttributes.ContainsKey(name))
                return this.htmlAttributes[name].ToString();
            else
                return null;
        }

        protected abstract string RenderComponent();

        public override string ToString()
        {
            return RenderComponent();
        }
    }
}