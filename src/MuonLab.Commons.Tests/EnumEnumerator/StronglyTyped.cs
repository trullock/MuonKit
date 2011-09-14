using System.Linq;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.EnumEnumerator
{
	public class StronglyTyped : Specification
	{
		private TestEnum[] enums;

		protected override void When()
		{
			this.enums = Enumerator<TestEnum>.GetAll().ToArray();
		}

		[Then]
		public void all_values_should_be_returned()
		{
			enums[0].ShouldEqual(TestEnum.alpha);
			enums[1].ShouldEqual(TestEnum.beta);
			enums[2].ShouldEqual(TestEnum.delta);
			enums[3].ShouldEqual(TestEnum.gamma);
		}

		public enum TestEnum
		{
			alpha,
			beta,
			delta,
			gamma
		}
	}
}