using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class M
	{

		public static double Sqrt(double d)
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

		public static float Max(params float[] args)
		{
			float result = 0F;
			if (args.Length > 0)
			{
				result = args[0];
				for (int index = 1; index < args.Length; index++)
				{
					result = Math.Max(result, args[index]);
				}
			}
			return result;
		}
		public static int Max
			(this IEnumerable<int> collection
			)
		{
			int result = 0;
			foreach (int item in collection)
			{
				if (item > result)
				{
					result = item;
				}
			}
			return result;
		}
		public static float Min(params float[] args)
		{
			float result = 0F;
			if (args.Length > 0)
			{
				result = args[0];
				for (int index = 1; index < args.Length; index++)
				{
					result = Math.Min(result, args[index]);
				}
			}
			return result;
		}

		public static float Range
			(params float[] args
			)
		{
			float result = -1F;
			if (args.Length >= 2)
			{
				result = Max(args) - Min(args);
			}
			return result;
		}
		public static float Similar
			(params float[] args
			)
		{
			float result = 0F;
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