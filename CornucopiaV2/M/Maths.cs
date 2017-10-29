using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class Maths
	{

		public static double Sign
			(this double d
			)
		{
			return d == 0 ? 0 : d > 0 ? 1 : -1;
		}

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

		public static float SinF
			(this float angleRadians
			)
		{
			return (float)Math.Sin(angleRadians);
		}

		public static float CosF
			(this float angleRadians
			)
		{
			return (float)Math.Cos(angleRadians);
		}

		public static float TanF
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

		public static double SinD
			(this double angleDegrees
			)
		{
			return Math.Sin(angleDegrees.ToRadians());
		}

		public static double CosD
			(this double angleDegrees
			)
		{
			return Math.Cos(angleDegrees.ToRadians());
		}

		public static double TanD
			(this double angleDegrees
			)
		{
			return Math.Tan(angleDegrees.ToRadians());
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