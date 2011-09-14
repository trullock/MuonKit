using System;
using MuonLab.Testing;

namespace MuonLab.Commons.Tests.ApplicationTime
{
	public class Today : Specification
	{
		private DateTime today;

		protected override void Given()
		{
			Commons.ApplicationTime.TodayFunc = () => new DateTime(2001, 1, 1, 1, 1, 1);
		}

		protected override void When()
		{
			this.today = Commons.ApplicationTime.Today;
		}

		[Then]
		public void should_exec_the_function()
		{
			this.today.ShouldEqual(new DateTime(2001, 1, 1, 0, 0, 0));
		}
	}
}