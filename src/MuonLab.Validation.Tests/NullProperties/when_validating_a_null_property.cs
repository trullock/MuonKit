
using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.NullProperties
{
	public class when_validating_a_null_property : Specification
	{
		private TestClassValidator validator;
		private ValidationReport report;

		protected override void Given()
		{
			this.validator = new TestClassValidator();
		}

		protected override void When()
		{
			this.report = this.validator.Validate(new TestClass());
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.IsValid.ShouldBeTrue();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.Satisfies(p => true, "should work!"));
			}
		}
	}
}