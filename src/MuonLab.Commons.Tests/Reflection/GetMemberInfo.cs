using MuonLab.Commons.Reflection;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.Reflection
{
	[TestFixture]
	public class GetMemberInfo
	{
		[Test]
		public void Should_get_the_correct_propertys_memberinfo()
		{
			Assert.AreEqual("System.String Name", ReflectionHelper.GetMemberInfo<TestClass, string>(t => t.Name).ToString());
		}

		[Test]
		public void Should_get_the_correct_deep_propertys_memberinfo()
		{
			Assert.AreEqual("Int32 Age", ReflectionHelper.GetMemberInfo<TestClass, int>(t => t.InnerClass.Age).ToString());
		}

		private class TestClass
		{
			public string Name { get; set; }
			public InnerClass InnerClass { get; set; }
		}
		private class InnerClass
		{
			public int Age { get; set; }
		}
	}
}