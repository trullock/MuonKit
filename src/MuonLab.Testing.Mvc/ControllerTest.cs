using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using MuonLab.Commons.DI;
using MuonLab.Validation;
using NUnit.Framework;
using Rhino.Mocks;

namespace MuonLab.Testing.Mvc
{
	public abstract class ControllerTest<TController> : Specification where TController : ControllerBase
	{
		protected readonly SimpleDependencyResolverAdapter dependencyResolverAdapter;

		protected ControllerTest()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new TestingViewEngine());

			var viewDataDictionary = new ViewDataDictionary();
			Inject(viewDataDictionary);
			Inject(viewDataDictionary.ModelState);

			var violationPropertyNameResolver = Stub<IViolationPropertyNameResolver>();
			violationPropertyNameResolver
				.Stub(r => r.ResolvePropertyName(null))
				.IgnoreArguments()
				.Return(string.Empty);

			this.dependencyResolverAdapter = new SimpleDependencyResolverAdapter();
			dependencyResolverAdapter.RegisterType(violationPropertyNameResolver);
			DependencyResolver.SetCurrentResolver(this.dependencyResolverAdapter);
		}

		protected TController Subject()
		{
			return this.Subject<TController>();
		}

		protected void AssertViewIsCorrectAndExists(ViewResult result, string viewName)
		{
			Assert.AreEqual(viewName, result.ViewName, "View names do not match");

			var controllerContext = Stub<ControllerContext>();
			controllerContext.RouteData = new RouteData();
			controllerContext.Controller = this.Subject<TController>();

			
			controllerContext.RouteData.Values.Add("controller", getControllerName(typeof(TController)));

			var viewEngineResult = result.ViewEngineCollection.FindView(controllerContext, result.ViewName, result.MasterName);
			Assert.IsNotNull(viewEngineResult.View, "View '" + result.ViewName + "' could not be found.");
		}

		protected virtual string getControllerName(Type controllerType)
		{
			var controllerName = controllerType.Name;
			var i = controllerName.IndexOf("Controller");
			return controllerName.Substring(0, i);
		}

		protected void AssertFileIsCorrectAndExists(FileResult result, string filename)
		{
			// hack of death!!!!
			var webProjectPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, Path.Combine("../../../", typeof(TController).Assembly.GetName().Name)));

			// crudely trim leading ~/
			if (filename.StartsWith("~/"))
				filename = filename.Substring(2);

			var fullPath = Path.Combine(webProjectPath, filename);

			Assert.IsTrue(File.Exists(fullPath), "File `" + fullPath + "` does not exist");
		}

		protected override T CreateSubjectUnderTest<T>()
		{
			var controller = base.CreateSubjectUnderTest<T>() as ControllerBase;
			controller.ViewData = Dependency<ViewDataDictionary>();

			return controller as T;
		}
	}
}