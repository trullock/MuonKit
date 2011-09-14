using System.Collections.Generic;

using MuonLab.Commons.Extensions;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.Extensions
{
	public class ObjectToDictionaryExtensionFixture : Specification
	{
		private TestClass testClass;
		private IDictionary<string, object> testClassDict;
		private IDictionary<string, object> anonClassDict;


		protected override void Given()
		{
			this.testClass = new TestClass { AProperty = "foo" };
		}

		protected override void When()
		{
			this.testClassDict = this.testClass.ToDictionary();
			this.anonClassDict = new { Prop1 = "hello", Prop2 = "goodbye" }.ToDictionary();
		}


		[Then]
		public void the_anon_class_should_be_serialised_properly()
		{
			this.anonClassDict.Keys.Count.ShouldEqual(2);
			this.anonClassDict["Prop1"].ShouldEqual("hello");
			this.anonClassDict["Prop2"].ShouldEqual("goodbye");
		}

		[Then]
		public void the_static_typed_class_should_be_serialsed_properly()
		{
			this.testClassDict.Keys.Count.ShouldEqual(1);
			this.testClassDict["AProperty"].ShouldEqual("foo");
		}

		public class TestClass
		{
			public string AProperty { get; set; }
			public string AField;

			public void AMethod()
			{
			}
		}
	}
}