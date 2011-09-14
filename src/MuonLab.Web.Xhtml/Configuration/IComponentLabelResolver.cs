using System;
using System.Linq.Expressions;

namespace MuonLab.Web.Xhtml.Configuration
{
	public interface IComponentLabelResolver
	{
		string ResolveLabel<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property);
		string ResolveBool(bool value);
	}
}