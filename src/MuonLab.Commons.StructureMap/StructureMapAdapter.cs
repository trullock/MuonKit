using System;
using MuonLab.Commons.DI;
using StructureMap;

namespace MuonLab.Commons.StructureMap
{
	public class StructureMapAdapter : IDependencyResolverAdapter
	{
		private readonly IContainer container;

		public StructureMapAdapter() :this(ObjectFactory.Container)
		{
		}

		public StructureMapAdapter(IContainer container)
		{
			this.container = container;
		}

		public object GetInstance(Type serviceType)
		{
			return container.GetInstance(serviceType);
		}

		public object TryGetInstance(Type serviceType)
		{
			return container.TryGetInstance(serviceType);
		}

		public TService GetInstance<TService>()
		{
			return container.GetInstance<TService>();
		}

		public TService TryGetInstance<TService>()
		{
			return container.TryGetInstance<TService>();
		}
	}
}