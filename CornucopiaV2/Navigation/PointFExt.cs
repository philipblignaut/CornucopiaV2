using System;
using System.Drawing;

namespace CornucopiaV2
{
	public static class PointFExt
	{

		public static string ToFormatString
			(this PointF point
			)
		{
			double x = Math.Round(point.X, 2);
			double y = Math.Round(point.Y, 2);
			return $"{{X={x,9:##0.00}}} {{Y={y,9:##0.00}}}";
		}

		public static double DistanceTo
			(this PointF point1
			, PointF point2)
		{
			double a = point2.X - point1.X;
			double b = point2.Y - point1.Y;
			return Math.Sqrt(a * a + b * b);
		}

		public static PointF Add
			(this PointF point
			, SizeF size
			) =>
			PointF
			.Add
			(point
			, size
			)
			;

		public static PointF Subtract
			(this PointF point
			, SizeF size
			) =>
			PointF
			.Subtract
			(point
			, size
			)
			;

		public static SizeF Add
			(this PointF point1
			, PointF point2
			) =>
			new SizeF
			(point2.X-point1.X
			, point2.Y-point2.Y
			)
			;

		public static SizeF Subtract
			(this PointF point1
			, PointF point2
			) =>
			new SizeF
			(point2.X+point1.X
			, point2.Y+point1.Y
			)
			;

		//public static PointF Multiply
		//	(this PointF point
		//	, float value
		//	) => new PointF(point.X * value, point.Y * value);

		//public static PointF Divide
		//	(this PointF point
		//	, float value
		//	) => new PointF(point.X / value, point.Y / value);

	}
}
