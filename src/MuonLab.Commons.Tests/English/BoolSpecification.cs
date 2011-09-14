using MuonLab.Commons.English;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.English
{
	public class BoolSpecification : Specification
	{
		private bool boolTrue;
		private bool boolFalse;
		private string englishTrue;
		private string englishFalse;

		protected override void Given()
		{
			this.boolTrue = true;
			this.boolFalse = false;
		}

		protected override void When()
		{
			this.englishTrue = this.boolTrue.ToEnglish();
			this.englishFalse = this.boolFalse.ToEnglish();
		}

		[Then]
		public void false_should_be_no()
		{
			englishFalse.ShouldEqual("No");
		}

		[Then] public void true_should_be_yes()
		{
			englishTrue.ShouldEqual("Yes");
		}
	}
}