using System;
using System.Linq.Expressions;

namespace MuonLab.Web.Xhtml.Configuration
{
	public interface IComponentIdResolver
	{
		string ResolveId<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, string controlPrefix);
	}
}