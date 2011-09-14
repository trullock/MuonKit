using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using MuonLab.Commons.DI;
using MuonLab.Commons.Formatting;
using MuonLab.Validation;

namespace MuonLab.Web.Mvc.Validation
{
	public static class ValidationExtensions
	{
		public static void AddViolations(this ModelStateDictionary modelState, IEnumerable<IViolation> violations)
		{
			// todo: MEH, REFACTOR THIS
			var propertyNameResolver = DependencyResolver.Current.GetInstance<IViolationPropertyNameResolver>();

			foreach (var violation in violations)
			{
				var propertyName = propertyNameResolver.ResolvePropertyName(violation) ?? string.Empty;

				modelState.AddModelError(propertyName, violation.ErrorMessage);
				modelState.SetModelValue(propertyName, new ValueProviderResult(violation.AttemptedValue, violation.AttemptedValue == null ? string.Empty : violation.AttemptedValue.Format(), Thread.CurrentThread.CurrentCulture));
			}
		}

		public static void AddViolationFor<TResource>(this ModelStateDictionary modelState, Expression<Func<TResource, object>> property, string errorMessage)
		{
			// todo: MEH, REFACTOR THIS
			var propertyNameResolver = DependencyResolver.Current.GetInstance<IViolationPropertyNameResolver>();

			var violation = new Violation(errorMessage, property, null);

			var propertyName = propertyNameResolver.ResolvePropertyName(violation) ?? string.Empty;

			modelState.AddModelError(propertyName, violation.ErrorMessage);
			modelState.SetModelValue(propertyName, new ValueProviderResult(violation.AttemptedValue, violation.AttemptedValue == null ? string.Empty : violation.AttemptedValue.Format(), Thread.CurrentThread.CurrentCulture));
		}
	}
}