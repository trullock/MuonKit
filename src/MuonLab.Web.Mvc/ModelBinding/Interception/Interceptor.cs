using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MuonLab.Web.Mvc.ModelBinding.Interception
{
	public abstract class Interceptor<T> : IInterceptor<T>
	{
		private readonly IDictionary<Type, IList<Func<object, object>>> typeInterceptions;
		private readonly IDictionary<string, IList<Func<object, object>>> propertyInterceptions;

		protected Interceptor()
		{
			this.typeInterceptions = new Dictionary<Type, IList<Func<object, object>>>();
			this.propertyInterceptions = new Dictionary<string, IList<Func<object, object>>>();
			Configure();
		}

		protected abstract void Configure();

		protected IInterceptionAction<TProperty> InterceptAll<TProperty>()
		{
			if(!this.typeInterceptions.ContainsKey(typeof(TProperty)))
				this.typeInterceptions.Add(typeof(TProperty), new List<Func<object, object>>());

			return new InterceptionAction<TProperty>(x => this.typeInterceptions[typeof(TProperty)].Add(x));
		}

		protected IInterceptionAction<TProperty> Intercept<TProperty>(Expression<Func<T, TProperty>> property)
		{
			var expression = property.Body as MemberExpression;
			if(!this.propertyInterceptions.ContainsKey(expression.Member.Name))
				this.propertyInterceptions.Add(expression.Member.Name, new List<Func<object, object>>());

			return new InterceptionAction<TProperty>(x => this.propertyInterceptions[expression.Member.Name].Add(x));
		}

		protected void Ignore<TProperty>(Expression<Func<T, TProperty>> property)
		{
			Intercept(property).With(c => c);
		}

		public object InterceptProperty(PropertyInfo property, object value)
		{
			if (this.propertyInterceptions.ContainsKey(property.Name))
				value = ApplyInterception(this.propertyInterceptions[property.Name], value);
			else if (this.typeInterceptions.ContainsKey(property.PropertyType))
				value = ApplyInterception(this.typeInterceptions[property.PropertyType], value);

			return value;
		}

		private static object ApplyInterception(IEnumerable<Func<object, object>> interceptions, object value)
		{
			foreach (var interception in interceptions)
				value = interception(value);

			return value;
		}
	}
}