using System.Web.Mvc;
using MuonLab.Commons.DI;
using MuonLab.NHibernate;

namespace MuonLab.Web.Mvc
{
	public class NHibernateControllerFactory : DependencyResolvingControllerFactory
	{
		public NHibernateControllerFactory(IActionInvoker actionInvoker) : base(actionInvoker)
		{
		}

		public NHibernateControllerFactory()
		{
		}

		protected override IController GetControllerInstance(System.Type controllerType)
		{
			IController controllerInstance = null;
			var unitOfWork = DependencyResolver.Current.GetInstance<IUnitOfWork>();

			try
			{
				controllerInstance = base.GetControllerInstance(controllerType);
			}
			finally
			{
				if (controllerInstance == null)
				{
					try
					{
						unitOfWork.Rollback();
					}
					finally
					{
						unitOfWork.Dispose();
					}
				}
			}

			return controllerInstance;
		}

		public override void ReleaseController(IController controller)
		{
			base.ReleaseController(controller);

			var unitOfWork = DependencyResolver.Current.GetInstance<IUnitOfWork>();
			try
			{
				unitOfWork.Commit();
			}
			catch
			{
				unitOfWork.Rollback();
				throw;
			}
			finally
			{
				unitOfWork.Dispose();
			}
		}
	}
}