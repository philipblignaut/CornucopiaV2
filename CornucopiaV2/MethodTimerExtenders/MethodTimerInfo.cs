using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CornucopiaV2
{
	public class MethodTimerInfo
	{
		public string Caller { get; protected internal set; }
		public string PrettyName { get; private set; }
		public Stopwatch Stopwatch { get; protected internal set; }
		protected internal MethodTimerInfo
			(string prettyName
			)
		{
			Stopwatch = new Stopwatch();
			PrettyName = prettyName;
		}

		public override string ToString()
		{
			return "caller \"{0}\" stopwatch {1}"
				.FormatWith
				(PrettyName + " " + Caller
				, (!Stopwatch.IsRunning ? "Stopped " + Stopwatch.Elapsed.ToString() : "Running")
				)
				;
		}

	}
}
