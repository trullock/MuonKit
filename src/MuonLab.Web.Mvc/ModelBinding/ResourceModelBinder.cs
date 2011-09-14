using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using MuonLab.Commons.DI;
using MuonLab.Validation;
using MuonLab.Web.Mvc.Validation;

namespace MuonLab.Web.Mvc.ModelBinding
{
	public class ResourceModelBinder : DefaultModelBinder
	{
		private readonly Func<Type, bool> argumentEvaluator;

		public ResourceModelBinder()
			: this(t => t.Name.EndsWith("Resource"))
		{
		}

		public ResourceModelBinder(Func<Type, bool> argumentEvaluator)
		{
			this.argumentEvaluator = argumentEvaluator;
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (this.argumentEvaluator(bindingContext.ModelType))
			{
				var resource = CreateResource(controllerContext, bindingContext);

				var report = Binder.Bind(resource, new[] { controllerContext.RequestContext.HttpContext.Request.Form });

				if (report.IsValid)
				{
					var validatorType = typeof(IValidator<>).MakeGenericType(bindingContext.ModelType);
					var validator = DependencyResolver.Current.GetInstance(validatorType) as IValidator;
					report = validator.Validate(resource);
				}

				if (!report.IsValid)
					bindingContext.ModelState.AddViolations(report.Violations);

				return resource;
			}

			return base.BindModel(controllerContext, bindingContext);
		}

		private static object CreateResource(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var ctors = bindingContext.ModelType
				.GetConstructors()
				.Select(c => new { Ctor = c, Params = c.GetParameters() })
				.OrderByDescending(c => c.Params.Length);

			if(!ctors.Any())
				throw new BinderException("Resource of type `" + bindingContext.ModelType + "` has no public constructor.");

			var ctor = ctors.First();

			var parameters = ctor.Params.Select(p => TryBindRouteValue(p, controllerContext, bindingContext.ValueProvider)).ToArray();

			var resource = Activator.CreateInstance(bindingContext.ModelType, parameters);

			return resource;
		}

		private static object TryBindRouteValue(ParameterInfo param, ControllerContext controllerContext, IDictionary<string, ValueProviderResult> valueProvider)
		{
			var propertyBinder = ModelBinders.Binders.GetBinder(param.ParameterType);

			var propertyModelBindingContext = new ModelBindingContext
			{
				ModelName = param.Name,
				ModelType = param.ParameterType,
				ValueProvider = valueProvider
			};

			var paramValue = propertyBinder.BindModel(controllerContext, propertyModelBindingContext);

			return paramValue;
		}
	}
}