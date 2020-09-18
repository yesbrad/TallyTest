using System;
using CodeTestTallyIT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestTallyIT.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CarparkRateController : Controller
	{
		public const int maxHourlyHours = 3;
		public const float hourlyRate = 5;
		public const float flatDailyRate = 20;
		public const float earlyRate = 13;
		public const float nightRate = 6.5f;
		public const float weekendRate = 10;

		[HttpPost]
		public ActionResult<CarparkRate> PostRate([FromBody]CarparkDuriation duriation)
		{
			return GetRate(duriation);
		}

		private CarparkRate GetRate(CarparkDuriation duriation)
		{
			if (IsTimeBetween(duriation.EntryTime, 6, 9) && IsTimeBetween(duriation.ExitTime, 15, 23, 30, 30))
			{
				return new CarparkRate("Early Bird", earlyRate, CarparkRate.RateType.FlatRate);
			}

			if (IsTimeBetween(duriation.EntryTime, 18, 24) && IsTimeBetween(duriation.ExitTime, 15, 23, 0, 30) && IsWeekDay(duriation.EntryTime))
			{
				return new CarparkRate("Night Rate", nightRate, CarparkRate.RateType.FlatRate);
			}

			if (!IsWeekDay(duriation.EntryTime) && !IsWeekDay(duriation.ExitTime))
			{
				return new CarparkRate("Weekend Rate", weekendRate, CarparkRate.RateType.FlatRate);
			}

			return GetStandardRate(duriation);
		}

		private CarparkRate GetStandardRate(CarparkDuriation duriation)
		{			
			TimeSpan timeSpent = duriation.ExitTime - duriation.EntryTime;

			if (GetHours(timeSpent) <= maxHourlyHours && GetDays(timeSpent) < 1)
			{
				return new CarparkRate("Standard Ratte", GetHours(timeSpent) * hourlyRate, CarparkRate.RateType.Hourly);
			}

			return new CarparkRate("Standard Rate", GetDays(timeSpent) * flatDailyRate, CarparkRate.RateType.FlatRate);
		}

		private bool IsTimeBetween(DateTime time, int minTime, int maxTime, int minTimeMinutes = 0, int maxTimeMinutes = 0)
		{
			TimeSpan start = new TimeSpan(minTime, minTimeMinutes, 0);
			TimeSpan end = new TimeSpan(maxTime, maxTimeMinutes, 0);
			TimeSpan now = time.TimeOfDay;
			return ((now > start) && (now < end));
		}

		private int GetHours (TimeSpan span) => Math.Abs(span.Hours) == 0 ? 1 : Math.Abs(span.Hours);
		private int GetDays(TimeSpan span) => Math.Abs(span.Days) == 0 ? 1 : Math.Abs(span.Days);

		private bool IsWeekDay(DateTime time) => time.DayOfWeek != DayOfWeek.Saturday && time.DayOfWeek != DayOfWeek.Sunday;
	}
}

