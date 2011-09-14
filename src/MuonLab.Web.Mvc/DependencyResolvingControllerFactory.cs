using System;
using System.Web.Mvc;
using MuonLab.Commons.DI;

namespace MuonLab.Web.Mvc
{
	public class DependencyResolvingControllerFactory : DefaultControllerFactory
	{
		private readonly IActionInvoker actionInvoker;

		public DependencyResolvingControllerFactory(IActionInvoker actionInvoker)
		{
			this.actionInvoker = actionInvoker;
		}

		public DependencyResolvingControllerFactory()
		{
			this.actionInvoker = new ControllerActionInvoker();
		}

		protected override IController GetControllerInstance(Type controllerType)
		{
			if(controllerType == null)
				return null;

			// use structuremap to create the controller, injecting any constructor dependencies
			var controller = DependencyResolver.Current.GetInstance(controllerType) as Controller;

			// set the action invoker to our custom one
			controller.ActionInvoker = this.actionInvoker;

			return controller;
		}
	}
}