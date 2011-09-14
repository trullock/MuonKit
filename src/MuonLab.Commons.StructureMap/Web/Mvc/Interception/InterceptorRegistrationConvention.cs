using System;
using MuonLab.Web.Mvc.ModelBinding.Interception;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace MuonLab.Commons.StructureMap.Web.Mvc.Interception
{
    public class InterceptorRegistrationConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			var interceptorTypes = type.FindInterfacesThatClose(typeof(IInterceptor<>));

			foreach(var interceptorType in interceptorTypes)
				registry.AddType(interceptorType, type);
		}
	}
}