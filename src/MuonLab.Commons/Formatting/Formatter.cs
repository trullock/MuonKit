using System;
using System.Collections.Generic;

namespace MuonLab.Commons.Formatting
{
	public static class Formatter
	{
		#region Constants 
		private const int SECOND = 1;
		private const int MINUTE = 60 * SECOND;
		private const int HOUR = 60 * MINUTE;
		private const int DAY = 24 * HOUR;
		private const int MONTH = 30 * DAY;
		#endregion

		private static readonly Dictionary<Type, Func<object, string>> lookup;

		public static Func<int, string> Integer { get; set; }
		public static Func<decimal, string> Decimal { get; set; }
		public static Func<DateTime, string> Date { get; set; }
		public static Func<DateTime?, string> NullableDate { get; set; }
		public static Func<DateTime, string> Time { get; set; }
		public static Func<DateTime, string> DateAndTime { get; set; }
		public static Func<DateTime?, string> NullableDateAndTime { get; set; }
		public static Func<TimeSpan, string> EnglishTimeSpan { get; set; }
		public static Func<DateTime, string> EnglishDateTime { get; set; }

		static Formatter()
		{
			Decimal = x => x.ToString("0.##");
			Integer = x => x.ToString();

			Date = x => x.ToString("dd/MM/yyyy");
			NullableDate = x => x.HasValue ? x.Value.ToString("dd/MM/yyyy") : string.Empty;

			Time = x => x.ToString("HH:mm");
			DateAndTime = x => x.ToString("dd/MM/yyyy HH:mm");
			NullableDateAndTime = x => x.HasValue ? x.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
			
			EnglishTimeSpan = timeSpan =>
				{
					if (timeSpan.TotalSeconds < 1 * MINUTE)
						return timeSpan.Seconds == 1 ? "one second" : timeSpan.Seconds + " seconds";
					if (timeSpan.TotalSeconds < 2 * MINUTE)
						return "a minute";
					if (timeSpan.TotalSeconds < 45 * MINUTE)
						return timeSpan.Minutes + " minutes";
					if (timeSpan.TotalSeconds < 90 * MINUTE)
						return "an hour";
					if (timeSpan.TotalSeconds < 24 * HOUR)
						return timeSpan.Hours + " hours";
					if (timeSpan.TotalSeconds < 7 * DAY)
						return timeSpan.Days <= 1 ? "1 day" : timeSpan.Days + " days";
					if (timeSpan.TotalSeconds < 1 * MONTH)
					{
						int weeks = Convert.ToInt32(Math.Floor((double)timeSpan.Days / 7));
						return weeks <= 1 ? "1 week" : weeks + " weeks";
					}
					if (timeSpan.TotalSeconds < 12 * MONTH)
					{
						int months = Convert.ToInt32(Math.Floor((double)timeSpan.Days / 30));
						return months <= 1 ? "1 month" : months + " months";
					}
					else
					{
						int years = Convert.ToInt32(Math.Floor((double)timeSpan.Days / 365));
						return years <= 1 ? "1 year" : years + " years";
					}
				};

			EnglishDateTime = x =>
				{
					var span = ApplicationTime.Now - x;
					if (span.TotalSeconds < 0)
						return "in " + EnglishTimeSpan(span.Negate());
					else
						return EnglishTimeSpan(span) + " ago";
				};

			lookup = new Dictionary<Type, Func<object, string>>();

			Format<DateTime>()
				.With(DateAndTime);
			Format<DateTime?>()
				.With(NullableDateAndTime);
			Format<int>()
				.With(Integer);
			Format<decimal>()
				.With(Decimal);
		}

		public static formatDefault<T> Format<T>()
		{
			return new formatDefault<T>();
		}
		public sealed class formatDefault<T>
		{
			public void With(Func<T, string> formatFunction)
			{
				if (lookup.ContainsKey(typeof(T)))
					lookup.Remove(typeof (T));

				lookup.Add(typeof(T), x => formatFunction((T)x));
			}
		}

		public static string Format<T>(T obj)
		{
			Type type;

			if (obj != null)
			{
				type = obj.GetType();

				if (lookup.ContainsKey(type))
					return lookup[type](obj);
			}

			type = typeof(T);
			if(lookup.ContainsKey(type))
				return lookup[type](obj);

			if(obj == null)
				return null;

			return obj.ToString();
		}
	}
}