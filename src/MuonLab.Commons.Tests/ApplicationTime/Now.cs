using System;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.ApplicationTime
{
	public class Now : Specification
	{
		private DateTime now;

		protected override void Given()
		{
			Commons.ApplicationTime.NowFunc = () => new DateTime(2001, 1, 1, 1, 1, 1);
		}

		protected override void When()
		{
			this.now = Commons.ApplicationTime.Now;
		}

		[Then]
		public void should_exec_the_function()
		{
			now.ShouldEqual(new DateTime(2001, 1, 1, 1, 1, 1));
		}
	}
}