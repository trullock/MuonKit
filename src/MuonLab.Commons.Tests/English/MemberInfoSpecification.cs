using System.Reflection;

using MuonLab.Commons.English;
using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.English
{
	public class MemberInfoSpecification : Specification
	{
		private PropertyInfo property;
		private PropertyInfo englishAttrProperty;
		private string propertyEnglishName;
		private string englishAttrPropertyPropertyName;
		private PropertyInfo acronymProperty;
		private string acronymPropertyEnglishName;

		protected override void Given()
		{
			this.property = typeof(TestClass).GetProperty("CamelCaseName");
			this.acronymProperty = typeof(TestClass).GetProperty("ADBCamelCase");
			this.englishAttrProperty = typeof(TestClass).GetProperty("ABCDE");
		}

		protected override void When()
		{
			this.propertyEnglishName = this.property.GetEnglishName();
			this.acronymPropertyEnglishName = this.acronymProperty.GetEnglishName();
			this.englishAttrPropertyPropertyName = this.englishAttrProperty.GetEnglishName();
		}

		[Then]
		public void the_attributeless_propertys_name_should_be_correctly_returned()
		{
			this.propertyEnglishName.ShouldEqual("Camel case name");
		}

		[Then]
		public void the_acronym_propertys_name_should_be_correctly_returned()
		{
			this.acronymPropertyEnglishName.ShouldEqual("ADB camel case");
		}

		[Then]
		public void the_name_attributed_propertys_name_should_be_correctly_returned()
		{
			this.englishAttrPropertyPropertyName.ShouldEqual("English name");
		}


		private class TestClass
		{
			public string CamelCaseName { get; set; }
			public string ADBCamelCase { get; set; }

			[EnglishName(Name = "English name")]
			public string ABCDE { get; set; }
		}
	}
}