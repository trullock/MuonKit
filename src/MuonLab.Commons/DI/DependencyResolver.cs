using System;

namespace MuonLab.Commons.DI
{
    public static class DependencyResolver
    {
        /// <summary>
        /// Return the current IDependencyResolverAdapter.
        /// </summary>
        public static IDependencyResolverAdapter Current { get; private set;}

        /// <summary>
        /// Sets the current IDependencyResolverAdapter
        /// </summary>
        /// <param name="resolverAdapter">Must not be null</param>
        public static void SetCurrentResolver(IDependencyResolverAdapter resolverAdapter)
        {
            if(resolverAdapter == null)
                throw new ArgumentNullException("resolverAdapter");

            Current = resolverAdapter;
        }
    }
}