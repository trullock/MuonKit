using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	/// <summary>
	/// An empty validator with no rules.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public class EmptyValidator<TEntity> : IValidator<TEntity>
	{
		public ValidationReport Validate(TEntity entity)
		{
			return Validate<object>(entity, null);
		}

		public ValidationReport Validate<TOuter>(TEntity entity, Expression<Func<TOuter, TEntity>> prefix)
		{
			return new ValidationReport(new List<IViolation>());
		}

		ValidationReport IValidator.Validate(object entity)
		{
			return Validate((TEntity)entity);
		}

		public IEnumerable<IValidationRule<TEntity>> GetRulesFor<TProperty>(Expression<Func<TEntity, TProperty>> property)
		{
			return new IValidationRule<TEntity>[0];
		}

		public IEnumerable<IValidationRule<TEntity>> ValidationRules
		{
			get { return new IValidationRule<TEntity>[0]; }
		}
	}
}