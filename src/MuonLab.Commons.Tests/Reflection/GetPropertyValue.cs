using MuonLab.Commons.Reflection;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.Reflection
{
	[TestFixture]
	public class GetPropertyValue
	{
		[Test]
		public void Should_get_the_correct_properties_value()
		{
			var testClass = new TestClass();
			Assert.AreEqual("Andrew", ReflectionHelper.GetPropertyValue(testClass, t => t.Name));
		}

		[Test]
		public void Should_get_the_correct_deep_properties_value()
		{
			var testClass = new TestClass();
			Assert.AreEqual("Bullock", ReflectionHelper.GetPropertyValue(testClass, t => t.InnerClass.Name));
		}

		private class TestClass
		{
			public string Name { get; set; }
			public InnerClass InnerClass { get; set; }

			public TestClass()
			{
				this.Name = "Andrew";
				this.InnerClass = new InnerClass {Name = "Bullock"};
			}
		}
		private class InnerClass
		{
			public string Name { get; set; }
		}
	}
}