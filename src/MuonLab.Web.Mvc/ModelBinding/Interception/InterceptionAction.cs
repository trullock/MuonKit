using System;

namespace MuonLab.Web.Mvc.ModelBinding.Interception
{
	internal class InterceptionAction<TProperty> : IInterceptionAction<TProperty>
	{
		private readonly Action<Func<object, object>> action;

		public InterceptionAction(Action<Func<object, object>> action)
		{
			this.action = action;
		}

		public void With(Func<TProperty, TProperty> func)
		{
			this.action(o => func((TProperty)o));
		}
	}
}