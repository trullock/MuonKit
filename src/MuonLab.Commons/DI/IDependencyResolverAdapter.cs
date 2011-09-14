using System;

namespace MuonLab.Commons.DI
{
    public interface IDependencyResolverAdapter
    {
        object GetInstance(Type serviceType);
        object TryGetInstance(Type serviceType);
        TService GetInstance<TService>();
        TService TryGetInstance<TService>();
    }
}