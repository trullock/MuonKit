using System;

namespace MuonLab.Commons
{
	public static class ApplicationTime
	{
		public static Func<DateTime> NowFunc = () => DateTime.Now;
		public static Func<DateTime> TodayFunc = () => DateTime.Today;

		public static DateTime Now
		{
			get { return NowFunc.Invoke(); }
		}

		public static DateTime Today
		{
			get { return TodayFunc.Invoke().Date; }
		}
	}
}