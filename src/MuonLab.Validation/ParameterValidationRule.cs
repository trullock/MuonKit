using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MuonLab.Commons.English;
using MuonLab.Commons.Formatting;

namespace MuonLab.Validation
{
	public class ParameterValidationRule<T> : BaseValidationRule<T, T>
	{
		public ParameterValidationRule(Expression<Func<T, ICondition<T>>> validationExpression)
			: base(validationExpression)
		{
			var param = this.Condition.Arguments[0] as ParameterExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, T>>(param, param);
		}

		public override IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var condition = this.validationExpression.Compile().Invoke(entity) as PropertyCondition<T>;

			var valid = condition.Condition.Invoke(entity);

			if (!valid)
			{
				if (prefix != null)
					return new[] {createViolation(condition, entity, prefix)};
				else
					return new[] {createViolation(condition, entity, this.PropertyExpression)};
			}
			else
				return new IViolation[0];
		}

		protected IViolation createViolation(PropertyCondition<T> condition, T entity, Expression property)
		{
			var errorMessage = condition.ErrorMessage;

			errorMessage = errorMessage.Replace("{val}", typeof (T).GetEnglishName());

			for (int i = 1; i < this.Condition.Arguments.Count; i++)
				errorMessage = errorMessage.Replace("{arg" + i + "}", evaluateExpression(this.Condition.Arguments[i], entity));

			return new Violation(errorMessage, property, entity);
		}

		protected string getMemberName(MemberExpression member)
		{
			if (this.property.Member.DeclaringType.IsGenericType && this.property.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return (member.Expression as MemberExpression).Member.GetEnglishName();
			else
				return member.Member.GetEnglishName();
		}

		protected string evaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return getMemberName(expression as MemberExpression);
			else
			{
				var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);

				var value = lambda.Compile().DynamicInvoke(entity);

				return value.Format();
			}
		}
	}
}