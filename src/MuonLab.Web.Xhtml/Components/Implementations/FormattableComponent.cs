using System;

namespace MuonLab.Web.Xhtml.Components.Implementations
{
    public abstract class FormattableComponent<TEntity, TProperty> : VisibleComponent<TEntity, TProperty>, IFormattableComponent<TProperty> where TEntity : class
    {
        protected enum FormatMode
        {
            String,
            Func
        }

        protected string format;
        protected FormatMode formatMode;
        protected Func<TProperty, string> formatFunction;

        protected FormattableComponent()
        {
            this.format = "{0}";
            this.formatMode = FormatMode.String;
        }

        public virtual IFormattableComponent FormattedAs(string formatString)
        {
            this.formatMode = FormatMode.String;
            this.format = string.Concat("{0:", formatString, "}");
            return this;
        }

        public virtual IFormattableComponent<TProperty> FormatWith(Func<TProperty, string> formatFunc)
        {
            this.formatMode = FormatMode.Func;
            this.formatFunction = formatFunc;
            return this;
        }

        protected string formatValue(TProperty value)
        {
            if (this.formatMode == FormatMode.Func)
                return this.formatFunction.Invoke(this.value);
		    
            if(ReferenceEquals(this.value, null))
                return null;

            return string.Format(this.format, this.value);
        }
    }
}