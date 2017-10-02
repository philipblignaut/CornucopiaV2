using System;
using System.Collections.Generic;
using System.Linq;

namespace CornucopiaV2
{
	public class Range
	{
		public double Min { get; private set; }
		public double Max { get; private set; }
		public Range
		   (double min
		   , double max
		   )
		{
			Min = min;
			Max = max;
		}
		public double Distance
		{
			get
			{
				return Math.Abs(Max - Min);
			}
		}
		public Range Zoom
		   (double ratio
		   )
		{
			double middle = Min + (Max - Min) / 2.0;
			double distance = Distance;
			Min = middle - distance / 2.0 * ratio;
			Max = middle + distance / 2.0 * ratio;
			return new Range(Min, Max);
		}
		public Range Move
		   (double distance
		   )
		{
			Min += distance;
			Max += distance;
			return new Range(Min, Max);
		}
		public override string ToString()
		{
			return Min.ToString() + " " + Max.ToString() + " " + Distance.ToString();
		}
	}
	public static class NumberExtenders
	{

		//public static bool IsPrime
		//	(this int number
		//	)
		//{
		//	return
		//		Enumerable
		//		.Range(1, number)
		//		.Where(x => number % x == 0)
		//		.SequenceEqual(new[] { 1, number })
		//		;
		//}

		public static bool IsPrime
			(this int number
			)
		{
			return
				Enumerable
					.Range
						(1
						, number
						)
					.Count(x => number % x == 0) == 2
			;
		}

		public static bool IsDouble
		   (this string text
		   )
		{
			return double.TryParse(text, out double result);
		}

		public static int ClosestTo
		   (this IEnumerable<int> range
		   , float value
		   )
		{
			return
			   range
			   .Convert
			   (elm =>
				  new
				  {
					  Element = elm,
					  Difference = Math.Abs(value - (elm + 0F)),
				  }
			   )
			   .OrderBy(seq => seq.Difference)
			   .First()
			   .Element
			   ;
		}

		public static int ClosestTo
		   (this IEnumerable<int> range
		   , double value
		   )
		{
			return
			   range
			   .Convert
			   (elm =>
				  new
				  {
					  Element = elm,
					  Difference = Math.Abs(value - (elm + 0D)),
				  }
			   )
			   .OrderBy(seq => seq.Difference)
			   .First()
			   .Element
			   ;
		}

		public static double RangeValue
		   (this double percent
		   , double valueAtZero
		   , double valueAtHundred
		   )
		{
			return
			   valueAtZero
			   + percent * (valueAtHundred - valueAtZero) / 100.0
			   ;
		}

		public static double RangeValue
		   (this double percent
		   , Range range
		   )
		{
			return
			   range.Min
			   + percent * (range.Max - range.Min) / 100.0
			   ;
		}

		public static double RangeValue
		   (this Range range
		   , double percent
		   )
		{
			return
			   range.Min
			   + percent * (range.Max - range.Min) / 100.0
			   ;
		}

		public
			static double ToRadians
		   (this double degrees
		   )
		{
			return Math.PI * degrees / 180.0;
		}

		public static double ToDegrees
		   (this double radians
		   )
		{
			return radians * (180.0 / Math.PI);
		}

		public static string ToBinary
			(this int value
			, int digits
			)
		{
			string result = C.es;
			while (value > 0)
			{
				result = (value % 2 == 0 ? "0" : "1") + result;
				value /= 2;
			}
			return result.PadLeft(digits,'0');
		}

	}
}
