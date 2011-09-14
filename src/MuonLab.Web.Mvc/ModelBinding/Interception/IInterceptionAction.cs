using System;

namespace MuonLab.Web.Mvc.ModelBinding.Interception
{
	public interface IInterceptionAction<TProperty>
	{
		void With(Func<TProperty, TProperty> func);
	}
}