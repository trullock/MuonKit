using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using MuonLab.Commons.DI;
using MuonLab.Commons.English;
using MuonLab.Commons.Reflection;
using MuonLab.NHibernate;
using MuonLab.Validation;
using MuonLab.Web.Mvc.ModelBinding.Interception;

namespace MuonLab.Web.Mvc.ModelBinding
{
	public static class Binder
	{
		public const char NameDelimeter = ':';

		public static ValidationReport Bind(object entity, IEnumerable<NameObjectCollectionBase> collections)
		{
			IList<IViolation> violations = new List<IViolation>();

			foreach (var collection in collections)
			{
				if (collection == null)
					throw new NullReferenceException("NameObjectCollectionBase is null. Did you pass in a null FormCollection?");

				BindCollection(collection, entity, violations);
			}

			return new ValidationReport(violations);
		}

		private static void BindCollection(NameObjectCollectionBase collection, object entity, ICollection<IViolation> violations)
		{
			foreach (string key in collection)
			{
				if (key == null)
					continue;

				var violation = BindKey(key, collection, entity);

				if (violation != null)
					violations.Add(violation);
			}
		}

		private static IViolation BindKey(string key, NameObjectCollectionBase collection, object entity)
		{
			if (!key.StartsWith(NameDelimeter.ToString()))
				return null;

			var propertyChain = key.TrimStart(NameDelimeter);

			if (typeof (HttpFileCollectionBase).IsInstanceOfType(collection))
				return BindFile(key, collection, entity, propertyChain);

			if (typeof (NameValueCollection).IsInstanceOfType(collection))
				return startPropertySettingProcess(entity, propertyChain, (collection as NameValueCollection)[key]);
			
			throw new NotSupportedException();
		}

		private static IViolation BindFile(string key, NameObjectCollectionBase collection, object entity, string propertyChain)
		{
			var file = (collection as HttpFileCollectionBase)[key];

			if (!(file.ContentLength == 0 && string.IsNullOrEmpty(file.FileName)))
				return startPropertySettingProcess(entity, propertyChain, file);
			
			return startPropertySettingProcess(entity, propertyChain, null);
		}

		private static Violation startPropertySettingProcess(object entity, string propertyName, object propertyValue)
		{
			var parameterExpression = Expression.Parameter(entity.GetType(), "x");
			return SetPropertyValue(entity, parameterExpression, propertyName, propertyValue);
		}

		private static Violation SetPropertyValue(object subject, Expression parameter, string propertyName, object propertyValue)
		{
			var subjectType = subject.GetType();

			if (!propertyName.Contains(NameDelimeter))
			{
				var property = subjectType.GetProperty(propertyName);
				// do nothing, it probably wasnt a bindable property
				if (property == null)
					return null;

				if (property.GetSetMethod(false) == null)
					throw new ArgumentException("'" + propertyName + "' has no public setter");

				try
				{
					var value = ConvertValue(property.PropertyType, propertyValue);

					// Intercept
					value = ApplyInterceptors(subjectType, property, value);

					property.SetValue(subject, value, null);
				}
				catch (BinderException)
				{
					throw;
				}
				catch
				{
					return CreateBindingViolation(property, propertyValue, parameter);
				}
			}
			else
			{
				var p = propertyName.Split(new[] { NameDelimeter }, 2);
				var currentPropertyName = p[0];
				var restOfPropertyChain = p[1];

				var property = subjectType.GetProperty(currentPropertyName);

				var memberExpression = Expression.MakeMemberAccess(parameter, property);

				return SetPropertyValue(ReflectionHelper.GetPropertyValue(subject, currentPropertyName), memberExpression, restOfPropertyChain, propertyValue);
			}

			return null;
		}

		private static object ApplyInterceptors(Type subjectType, PropertyInfo property, object value)
		{
			var interceptorType = typeof (IInterceptor<>).MakeGenericType(subjectType);
			var interceptor = DependencyResolver.Current.TryGetInstance(interceptorType);
			if (interceptor != null)
				value = InterceptWithService(interceptor, property, value);
			return value;
		}

		private static Violation CreateBindingViolation(PropertyInfo property, object propertyValue, Expression parameter)
		{
			string message;

			if (property.PropertyType.IsEnum)
				message = "Please choose \"" + property.GetEnglishName() + "\"";
			else
				message = '"' + property.GetEnglishName() + "\" is not a valid " + property.PropertyType.GetEnglishName();

			var memberExpression = Expression.MakeMemberAccess(parameter, property);
			var rootParam = findRootParameter(parameter);
			var lambdaExpression = Expression.Lambda(memberExpression, rootParam);

			return new Violation(message, lambdaExpression, propertyValue);
		}

		public static object ConvertValue(Type propertyType, object propertyValue)
		{
		    if (propertyType == typeof (bool))
		        // Hack to make checkboxes work
		        return propertyValue.ToString().ToUpper().Contains("TRUE");

			if (propertyType.IsEnum)
				return convertEnum(propertyType, propertyValue);

            if (propertyType == typeof (HttpPostedFileBase))
		    {
		        if (!typeof (HttpPostedFileBase).IsInstanceOfType(propertyValue))
		            return null;
		        
                return propertyValue;
		    }

		    if (typeof (IEnumerable).IsAssignableFrom(propertyType) && propertyType != typeof (string))
		    {
		        if (propertyType.IsGenericType)
		        {
		            var genericTypeDefinition = propertyType.GetGenericTypeDefinition();

		            if (typeof (IEnumerable<>) == genericTypeDefinition)
		            {
		                var genericArgument = propertyType.GetGenericArguments()[0];
		                var list = new ArrayList();

		                var stringValue = propertyValue.ToString();
		                if (!string.IsNullOrEmpty(stringValue))
		                {
		                    var strings = stringValue.Split(genericArgument == typeof (string) ? '\n' : ',');
		                    foreach (var str in strings)
		                    {
		                        var value = ConvertValue(genericArgument, str);
		                        if (value == null)
		                            throw new ApplicationException("meh!");

		                        list.Add(value);
		                    }
		                }
		                return Activator.CreateInstance(typeof (GenericEnumerable<>).MakeGenericType(genericArgument), list);
		            }
		        }

		        throw new NotSupportedException("Binding for property type `" + propertyType + "` is not currenly supported. :(");
		    }

			if (propertyType == typeof(Guid) && string.IsNullOrEmpty(propertyValue.ToString()))
				return Guid.Empty;

            // all other types
		    var conv = TypeDescriptor.GetConverter(propertyType);
		    return conv.ConvertFrom(propertyValue);
		}

		private static object convertEnum(Type propertyType, object propertyValue)
		{
			var conv = TypeDescriptor.GetConverter(propertyType);

			if (string.IsNullOrEmpty((string) propertyValue) || propertyType.GetCustomAttributes(typeof (FlagsAttribute), true).Length == 0)
				return conv.ConvertFrom(propertyValue);

			var values = propertyValue.ToString();
			var strings = values.Split(',');

			int value = 0;

			foreach (var val in strings)
				value |= (int) conv.ConvertFrom(val);

			return value;
		}

		private static object InterceptWithService(object interceptor, PropertyInfo property, object value)
		{
			// If only c#3 had covariance/contravariance this wouldnt be necessary!

			// this gets the name of the getter method (so its strongly typed)
			Expression<Action<IInterceptor<IEntity>>> expression = x => x.InterceptProperty(null, null);
			var methodName = (expression.Body as MethodCallExpression).Method.Name;

			// get the type of the query service
			var interceptorType = interceptor.GetType();

			// get the getter method
			var interceptPropertyMethodInfo = interceptorType.GetMethod(methodName);

			// invoke the method with the entity and property

			if (interceptPropertyMethodInfo != null)
				return interceptPropertyMethodInfo.Invoke(interceptor, new object[] { property, value });
		    
            throw new NotSupportedException();
		}

		private static ParameterExpression findRootParameter(Expression expression)
		{
			if (expression is ParameterExpression)
				return expression as ParameterExpression;

			if (expression is MemberExpression)
			{
				var expr = expression as MemberExpression;
				while (expr.Expression is MemberExpression)
					expr = expr.Expression as MemberExpression;

				if (expr.Expression is ParameterExpression)
					return expr.Expression as ParameterExpression;
			}

			return null;
		}
	}
}