using System;
using System.Linq.Expressions;

namespace MuonLab.Web.Xhtml.Configuration
{
	public interface IComponentNameResolver
	{
		string ResolveName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property);
	}
}