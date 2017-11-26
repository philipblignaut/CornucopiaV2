using System.Drawing;

namespace CornucopiaV2
{
	public struct PointD
	{
		public double X { get; private set; }
		public double Y { get; private set; }
		public PointD
			(double x
			, double y
			)
		{
			X = x;
			Y = y;
		}
		public static implicit operator PointF(PointD point)
		{
			return new PointF((float)point.X, (float)point.Y);
		}
		public static implicit operator PointD(PointF point)
		{
			return new PointF(point.X, point.Y);
		}
	}
}
