using System;
using System.Linq.Expressions;
using MuonLab.Commons.Reflection;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml
{
	public class DefaultComponentIdResolver : IComponentIdResolver
	{
		public string ResolveId<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, string controlPrefix)
		{
			// TODO: De-meh this
			return controlPrefix + ReflectionHelper.PropertyChainToString(property, '!').Replace("!", string.Empty);
		}
	}
}