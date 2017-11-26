using System;
using System.Collections.Generic;
using System.Linq;

namespace CornucopiaV2
{
	static public class TimeSpanExt
	{

		static public TimeSpan Average
			(this IEnumerable<TimeSpan> timeSpanList
			)
		{
			DateTime timeElapsed = new DateTime(1980, 1, 1, 0, 0, 0, 0);
			DateTime present = new DateTime(1980, 1, 1, 0, 0, 0, 0);
			timeSpanList
				.Each
				(timeSpan =>
				{
					timeElapsed += timeSpan;
				}
				)
				;
			int count = timeSpanList.Count();
			return new TimeSpan((timeElapsed.Ticks - present.Ticks) / count);
		}
		static public TimeSpan Sum
			(this IEnumerable<TimeSpan> timeSpanList)
		{
			DateTime timeElapsed = new DateTime(1980, 1, 1, 0, 0, 0, 0);
			DateTime present = new DateTime(1980, 1, 1, 0, 0, 0, 0);
			timeSpanList
				.Each
				(timeSpan =>
				{
					timeElapsed += timeSpan;
				}
				)
				;
			return timeElapsed - present;
		}
	}
}
