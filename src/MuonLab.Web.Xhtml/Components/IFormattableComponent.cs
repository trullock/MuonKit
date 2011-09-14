using System;

namespace MuonLab.Web.Xhtml.Components
{
    public interface IFormattableComponent : IVisibleComponent
    {
        /// <summary>
        /// Sets the format string for the value attribute.
        /// </summary>
        /// <param name="formatString">The format string, do not include {0:}. To format as a date, set as "dd/MM/yyyy" - without quotes, idiot.</param>
        /// <returns></returns>
        IFormattableComponent FormattedAs(string formatString);
    }

    public interface IFormattableComponent<TProperty> : IVisibleComponent<TProperty>, IFormattableComponent
    {
        IFormattableComponent<TProperty> FormatWith(Func<TProperty, string> formatFunc);
    }
}