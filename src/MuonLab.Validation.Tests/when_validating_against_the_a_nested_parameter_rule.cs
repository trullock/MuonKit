using System;

using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_a_nested_parameter_rule : Specification
	{
		private TestClassWrapperValidator validator;
		private ValidationReport report;

		protected override void Given()
		{
			this.validator = new TestClassWrapperValidator();
		}

		protected override void When()
		{
			this.report = this.validator.Validate(new TestClassWrapper());
		}

		[Test]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(p => false, "should fail"));
			}
		}

		public class TestClassWrapperValidator : Validator<TestClassWrapper>
		{
			private TestClassValidator classValidator;

			public TestClassWrapperValidator()
			{
				this.classValidator = new TestClassValidator();
			}

			protected override void Rules()
			{
				Ensure(x => x.TestClass.Satisfies(classValidator));
			}
		}
	}
}