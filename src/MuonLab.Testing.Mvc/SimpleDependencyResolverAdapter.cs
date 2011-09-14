using System;
using System.Collections.Generic;
using MuonLab.Commons.DI;

namespace MuonLab.Testing.Mvc
{
	public class SimpleDependencyResolverAdapter : IDependencyResolverAdapter
	{
		private readonly Dictionary<Type, object> hash;

		public SimpleDependencyResolverAdapter()
		{
			this.hash = new Dictionary<Type, object>();
		}

		public void RegisterType<T>(T instance)
		{
			if (this.hash.ContainsKey(typeof (T)))
				throw new ArgumentException("Type: " + typeof (T).Name + " already registered with Simlpe Service Locator");

			this.hash.Add(typeof (T), instance);
		}

		public object GetInstance(Type serviceType)
		{
		    if (this.hash.ContainsKey(serviceType))
				return this.hash[serviceType];
		    
            throw new DependencyResolverException("Type: `" + serviceType + "` is not registered with the Simple Service Locator");
		}

	    public object TryGetInstance(Type serviceType)
		{
			try
			{
				return this.GetInstance(serviceType);
			}
			catch
			{
				return null;
			}
		}

		public TService GetInstance<TService>()
		{
			return (TService)this.GetInstance(typeof (TService));
		}

		public TService TryGetInstance<TService>()
		{
			try
			{
				return this.GetInstance<TService>();
			}
			catch
			{
				return default(TService);
			}
		}
	}
}