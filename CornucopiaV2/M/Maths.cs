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

		public static double Acos
			(this double angleRadians
			)
		{
			return Math.Acos(angleRadians);
		}

		public static double Asin
			(this double angleRadians
			)
		{
			return Math.Asin(angleRadians);
		}

		public static double Atan
			(this double angleRadians
			)
		{
			return Math.Atan(angleRadians);
		}

		public static double Cos
			(this double angleRadians
			)
		{
			return Math.Cos(angleRadians);
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

		public static double Sin
			(this double angleRadians
			)
		{
			return Math.Sin(angleRadians);
		}

		public static double Tan
			(this double angleRadians
			)
		{
			return Math.Tan(angleRadians);
		}
	}
}