using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class Maths
	{

		public static double Sqrt
			(this double d
			)
		{
			return Math.Sqrt(d);
		}

		public static double Abs
			(this double d
			)
		{
			return Math.Abs(d);
		}

		public static double Asin
			(this double angleRadians
			)
		{
			return Math.Asin(angleRadians);
		}

		public static double Acos
			(this double angleRadians
			)
		{
			return Math.Acos(angleRadians);
		}

		public static double Atan
			(this double angleRadians
			)
		{
			return Math.Atan(angleRadians);
		}

		public static float Sin
			(this float angleRadians
			)
		{
			return (float)Math.Sin(angleRadians);
		}

		public static float Cos
			(this float angleRadians
			)
		{
			return (float)Math.Cos(angleRadians);
		}

		public static float Tan
			(this float angleRadians
			)
		{
			return (float)Math.Tan(angleRadians);
		}

		public static double Sin
			(this double angleRadians
			)
		{
			return Math.Sin(angleRadians);
		}

		public static double Cos
			(this double angleRadians
			)
		{
			return Math.Cos(angleRadians);
		}

		public static double Tan
			(this double angleRadians
			)
		{
			return Math.Tan(angleRadians);
		}

		public static float Range
			(params float[] args
			)
		{
			float result = -1F;
			if (args.Length >= 2)
			{
				result = args.Max() - args.Min();
			}
			return result;
		}

	}
}