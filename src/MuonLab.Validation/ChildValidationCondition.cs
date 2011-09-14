using System.Collections.Generic;

namespace MuonLab.Validation
{
	public class ChildValidationCondition<TValue> : ICondition<TValue>
	{
		public IValidator<TValue> Validator { get; protected set; }

		public ChildValidationCondition(IValidator<TValue> validator)
		{
			this.Validator = validator;
		}
	}

	public class ChildListValidationCondition<TValue> : ICondition<IList<TValue>>
	{
		public IValidator<TValue> Validator { get; protected set; }

		public ChildListValidationCondition(IValidator<TValue> validator)
		{
			this.Validator = validator;
		}
	}
}