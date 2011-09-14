using System;
using MuonLab.Testing;

namespace MuonLab.Validation.Tests
{
	public abstract class given_a_test_class_with_data : Specification
	{
		private TestClass testClass;

		protected override void Given()
		{
			this.testClass = new TestClass();
			this.testClass.Age = 18;
			this.testClass.DateOfBirth = new DateTime(2008, 1, 1);
			this.testClass.Name = "Andrew";
		}
	}
}