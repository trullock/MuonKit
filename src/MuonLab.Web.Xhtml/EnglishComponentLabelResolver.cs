using System;
using System.Linq.Expressions;
using MuonLab.Commons.English;
using MuonLab.Commons.Reflection;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml
{
	public class EnglishComponentLabelResolver : IComponentLabelResolver
	{
		public string ResolveLabel<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
		{
			return ReflectionHelper.GetMemberInfo(property).GetEnglishName() + ":";
		}

		public string ResolveBool(bool value)
		{
			return value ? "Yes" : "No";
		}
	}
}