using System;
using System.Linq;

namespace CornucopiaV2
{
	public static class Maths
	{

		public static double Sign
			(this double d
			) => d == 0 ? 0 : d > 0 ? 1 : -1;

		public static double Sqrt
			(this double d
			) => Math.Sqrt(d);

		public static double Abs
			(this double d
			) => Math.Abs(d);

		public static double Asin
			(this double angleRadians
			) => Math.Asin(angleRadians);

		public static double Acos
			(this double angleRadians
			) => Math.Acos(angleRadians);

		public static double Atan
			(this double angleRadians
			) => Math.Atan(angleRadians);

		public static float Abs
			(this float value
			) => Math.Abs(value);

		public static float SqrtF
			(this float value
			) => (float)Math.Sqrt(value);

		public static float SinF
			(this float angleRadians
			) => (float)Math.Sin(angleRadians);

		public static float CosF
			(this float angleRadians
			) => (float)Math.Cos(angleRadians);

		public static float TanF
			(this float angleRadians
			) => (float)Math.Tan(angleRadians);

		public static double Sin
			(this double angleRadians
			) => Math.Sin(angleRadians);

		public static double Cos
			(this double angleRadians
			) => Math.Cos(angleRadians);

		public static double Tan
			(this double angleRadians
			) => Math.Tan(angleRadians);

		public static double SinD
			(this double angleDegrees
			) => Math.Sin(angleDegrees.ToRadians());

		public static double CosD
			(this double angleDegrees
			) => Math.Cos(angleDegrees.ToRadians());

		public static double TanD
			(this double angleDegrees
			) => Math.Tan(angleDegrees.ToRadians());

		public static double Round
			(this double value
			, int digits
			) => Math.Round(value, digits);

		public static float Range
			(params float[] args
			) => (args.Length >= 2)
			? args.Max() - args.Min()
			: -1F
			;

	}
}