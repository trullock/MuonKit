using System;
using MuonLab.Validation;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace MuonLab.Commons.StructureMap.Validation
{
	public class ValidatorRegistrationConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			var validatorTypes = type.FindInterfacesThatClose(typeof(IValidator<>));

			foreach(var validatorType in validatorTypes)
				registry.AddType(validatorType, type);
		}
	}
}