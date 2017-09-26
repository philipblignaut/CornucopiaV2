using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public delegate void PrintDelegate
		(string line
		)
		;

	public static class ConDeb
	{
		public static event PrintDelegate ConDebPrintDelegate;

		public static void Print
			(this string line
			)
		{
			Console.WriteLine(line);
			Debug.Print(line);
			if (ConDebPrintDelegate != null)
			{
				ConDebPrintDelegate.Invoke(line);
			}

		}
		public static void Print
			(string format
			, params object[] args
			)
		{
			Print(format.FormatWith(args));
		}
		public static void Print
			(params object[] args
			)
		{
			Print(args.JoinToCharacterSeparatedValues(C.sp));
		}
	}
}