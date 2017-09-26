using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace CornucopiaV2
{
	public class MethodTimer
	{
		static private object lockList = new object();
		static private Dictionary<int, MethodTimerInfo> list = new Dictionary<int, MethodTimerInfo>();
		public MethodTimerInfo MethodTimerInfo { get; private set; }
		public int ID { get; private set; }

		public MethodTimer()
			: this(string.Empty)
		{
		}
		
		public MethodTimer(string prettyName)
		{
			//Out.TextDC(Environment.StackTrace.SplitKeepEmptyEntries(Environment.NewLine).ConvertIndexed((text, ti) => ti.ToString() +" "+text).JoinToCharacterSeparatedValues(Environment.NewLine));
			string caller =
				Environment.StackTrace
				.SplitRemoveEmptyEntries(Environment.NewLine)
				[3]
				.ReplaceRepeat("  "," ")
				.Replace(" at ", string.Empty)
				;
			MethodTimerInfo= null;
			lock (lockList)
			{
				MethodTimerInfo = new MethodTimerInfo(prettyName); ;
				MethodTimerInfo.Caller = caller;
				ID = list.Count;
				list.Add(ID, MethodTimerInfo);
				MethodTimerInfo.Stopwatch.Start();
			}
		}

		public void Stop()
		{
			lock (lockList)
			{
				MethodTimerInfo.Stopwatch.Stop();
				//Out.TextDC("stop caller " + MethodTimerInfo.PrettyName);
			}
		}

		public TimeSpan Elapsed
		{
			get
			{
				return MethodTimerInfo.Stopwatch.Elapsed;
			}
		}

		public override string ToString()
		{
			string output = string.Empty;
			lock (lockList)
			{
				List<string> prettyList =
					list
					.Convert(kvp => kvp.Value.PrettyName)
					.Distinct()
					.ToList()
					;
				prettyList
					.Each
					(prettyName =>
						{
							List<MethodTimerInfo> prettyInfo =
								list
								.Where(kvp => kvp.Value.PrettyName == prettyName)
								.Convert(kvp => kvp.Value)
								.ToList()
								;
							TimeSpan minTimeSpan =
								prettyInfo
								.Convert(info => info.Stopwatch.Elapsed)
								.Min()
								;
							TimeSpan maxTimeSpan =
								prettyInfo
								.Convert(info => info.Stopwatch.Elapsed)
								.Max()
								;
							TimeSpan totTimeSpan =
								prettyInfo
								.Convert(info => info.Stopwatch.Elapsed)
								.Sum()
								;
							TimeSpan avgTimeSpan =
								prettyInfo
								.Convert(info => info.Stopwatch.Elapsed)
								.Average()
								;
							output += 
								prettyName 
								+ " Min {0} Max {1} Tot {2} Avg {3} Cnt {4}"
									.FormatWith
									(minTimeSpan
									, maxTimeSpan
									, totTimeSpan
									, avgTimeSpan
									, prettyInfo.Count
									)
									;
							output += Environment.NewLine;
						}
					)
					;
			}
			return output;
		}
	}
}
