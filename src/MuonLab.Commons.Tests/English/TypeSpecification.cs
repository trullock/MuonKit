
using MuonLab.Commons.English;
using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.English
{
	public class TypeSpecification : Specification
	{
		private string name;

		protected override void Given()
		{
			
		}

		protected override void When()
		{
			this.name = typeof(SillyName).GetEnglishName();
		}

		[Then]
		public void the_name_should_be_correctly_returned()
		{
			this.name.ShouldEqual("sillyname");
		}
	}

	public class SillyName
	{
		
	}
}