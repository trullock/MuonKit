using System;
using System.Linq.Expressions;
using MuonLab.Commons.Reflection;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml
{
	public class DelimitedComponentNameResolver : IComponentNameResolver
	{
		private readonly char delimeter;

		public DelimitedComponentNameResolver(char delimeter)
		{
			this.delimeter = delimeter;
		}

		public string ResolveName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
		{
			return delimeter + ReflectionHelper.PropertyChainToString(property, delimeter);
		}

	}
}