using MuonLab.Commons.Reflection;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.Reflection
{
	[TestFixture]
	public class PropertyChainToString
	{
		private char delimeter;

		[Test]
		public void Should_work_for_very_deep_properties()
		{
			this.delimeter = ':';
			var name = ReflectionHelper.PropertyChainToString<Outer, string>(x => x.Inner.Centre.CentreProperty, this.delimeter);
			Assert.AreEqual("Inner" + this.delimeter + "Centre" + this.delimeter + "CentreProperty", name);
		}

		[Test]
		public void Should_work_for_very_deep_nullable_properties()
		{
			var name = ReflectionHelper.PropertyChainToString<Outer, bool?>(x => x.Inner.Centre.CentreNullableProperty, this.delimeter);
			Assert.AreEqual("Inner" + this.delimeter + "Centre" + this.delimeter + "CentreNullableProperty", name);
		}

		[Test]
		public void Should_work_for_deep_properties()
		{
			var name = ReflectionHelper.PropertyChainToString<Outer, string>(x => x.Inner.InnerProperty, this.delimeter);
			Assert.AreEqual("Inner" + this.delimeter + "InnerProperty", name);
		}

		[Test]
		public void Should_work_for_deep_nullable_properties()
		{
			var name = ReflectionHelper.PropertyChainToString<Outer, bool?>(x => x.Inner.InnerNullableProperty, this.delimeter);
			Assert.AreEqual("Inner" + this.delimeter + "InnerNullableProperty", name);
		}

		[Test]
		public void Should_work_for_properties()
		{
			var name = ReflectionHelper.PropertyChainToString<Outer, string>(x => x.OuterProperty, this.delimeter);
			Assert.AreEqual("OuterProperty", name);
		}

		[Test]
		public void Should_work_for_enum_properties()
		{
			var name = ReflectionHelper.PropertyChainToString<Outer, SomeEnum>(x => x.SomeEnum, this.delimeter);
			Assert.AreEqual("SomeEnum", name);
		}


		private class Outer
		{
			public Inner Inner { get; set; }
			public string OuterProperty { get; set; }
			public SomeEnum SomeEnum { get; set; }
		}
		private class Inner
		{
			public Centre Centre { get; set; }
			public string InnerProperty { get; set; }
			public bool? InnerNullableProperty { get; set; }
		}
		private class Centre
		{
			public string CentreProperty { get; set; }
			public bool? CentreNullableProperty { get; set; }
		}

		private enum SomeEnum
		{
			one = 0,
			two = 1,
			three = 2
		}
	}
}