﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CornucopiaV2
{
	public static class NumberExtenders2
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
		public const int BitsInLong = 64;
		public const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Converts the given decimal number to the numeral system with the
		/// specified radix (in the range [2, 36])
		/// </summary>
		/// <param name="value">The number to convert</param>
		/// <param name="radix">The radix of the destination numeral system (in the range [2, 36])</param>
		/// <returns></returns>
		public static string ToBase
			(this long value
			, int radix
			)
		{

			if (radix < 2 || radix > Digits.Length)
				throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

			if (value == 0)
				return "0";

			int index = BitsInLong - 1;
			long currentNumber = Math.Abs(value);
			char[] charArray = new char[BitsInLong];
			while (currentNumber != 0)
			{
				int remainder = (int)(currentNumber % radix);
				charArray[index--] = Digits[remainder];
				currentNumber = currentNumber / radix;
			}
			string result = new String(charArray, index + 1, BitsInLong - index - 1);
			if (value < 0)
			{
				result = "-" + result;
			}
			return result;
		}

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

		public static double ToRadians
		   (this double degrees
		   )
		{
			return Math.PI * degrees / 180.0;
		}

		public static float ToRadians
		   (this float degrees
		   )
		{
			return (float)(Math.PI * degrees / 180.0);
		}

		public static double ToDegrees
		   (this double radians
		   )
		{
			return radians * (180.0 / Math.PI);
		}

		public static float ToDegrees
			(this float radians
			) => (float)(radians * (180.0 / Math.PI))
			;

		public static float Round
			(this float value
			, int decimals
			) => (float)Math.Round(value, decimals)
			;

		public static double Translate
			(this double value
			, double fromMin
			, double fromMax
			, double toMin
			, double toMax
			)
		{
			double ratio = ((value - fromMin) / (fromMax - fromMin)).Abs();
			return toMin + ratio * (toMax - toMin);
		}

	}
}