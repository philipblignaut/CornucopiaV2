using System.Collections.Generic;

namespace CornucopiaV2
{
	public static class Converter
	{
		public static object[] ToArray
			(params object[] args
			)
		{
			return args;
		}
		public static IEnumerable<object> ToIEnumerable
			(params object[] args
			)
		{
			foreach (object arg in args)
			{
				yield return arg;
			}
		}
	}
}