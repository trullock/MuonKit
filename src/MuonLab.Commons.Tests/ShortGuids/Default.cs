using System;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.ShortGuids
{
	public class Default : Specification
	{
		private Guid guid;
		private Guid returned;

		protected override void Given()
		{
			this.guid = Guid.NewGuid();
		}

		protected override void When()
		{
			var shortGuid = new ShortGuid(this.guid);

			ShortGuid outed;
			ShortGuid.TryParse(shortGuid.ToString(), out outed);

			this.returned = outed.ToGuid();
		}

		[Then]
		public void should_convert_back_and_forth()
		{
			returned.ShouldEqual(guid);
		}
	}
}