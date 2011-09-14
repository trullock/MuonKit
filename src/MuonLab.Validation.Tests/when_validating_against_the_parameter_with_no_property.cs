using System;

using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_parameter_with_no_property : Specification
	{
		private TestValidator validator;
		private ValidationReport report;

		protected override void Given()
		{
			this.validator = new TestValidator();
		}

		protected override void When()
		{
			this.report = this.validator.Validate(new TestClass());
		}

		[Test]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(p => false, "should fail"));
			}
		}
	}
}