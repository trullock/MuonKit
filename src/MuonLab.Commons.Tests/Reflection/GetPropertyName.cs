using MuonLab.Commons.Reflection;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.Reflection
{
	[TestFixture]
	public class GetPropertyName
	{
		[Test]
		public void Should_get_the_correct_propertys_name()
		{
			var testClass = new TestClass();
			Assert.AreEqual("Name", ReflectionHelper.GetPropertyName<TestClass, string>(t => t.Name));
		}

		[Test]
		public void Should_get_the_correct_deep_propertys_name()
		{
			var testClass = new TestClass();
			Assert.AreEqual("Age", ReflectionHelper.GetPropertyName<TestClass, int>(t => t.InnerClass.Age));
		}

		private class TestClass
		{
			public string Name { get; set; }
			public InnerClass InnerClass { get; set; }

			public TestClass()
			{
				this.Name = "Andrew";
				this.InnerClass = new InnerClass {Age = 23};
			}
		}
		private class InnerClass
		{
			public int Age { get; set; }
		}
	}
}