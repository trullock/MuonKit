using System;
using System.Linq;
using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public class ErrorMessagesShouldWork : Specification
	{
		private TestValidator validator;
		private ValidationReport report;

		protected override void Given()
		{
			this.validator = new TestValidator();
		}

		protected override void When()
		{
			this.report = this.validator.Validate(new TestClass { Age = 12});
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.Violations.First().ErrorMessage.ShouldEqual("Age 12 10");
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.ShouldProduceErrorMessage(10));
			}
		}
	}

	public static class TestExtensions
	{
		public static ICondition<int> ShouldProduceErrorMessage(this int self, int someArg)
		{
			return self.Satisfies(x => false, "{val} {arg0} {arg1}");
		}
	}
}