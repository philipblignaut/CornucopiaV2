using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public class Vector
	{
		public double Length { get; private set; } = 0;
		public double Angle { get; private set; } = 0;
		public Vector()
		{
		}
		public Vector
			(double lenght
			, double angle
			)
		{
			Length = lenght;
			Angle = angle;
		}
		public Vector
			(PointF point
			)
		{
			Length =
				(Math.Sqrt
					(Math.Pow(point.X, 2)
					+ Math.Pow(point.Y, 2)
					)
				)
				;
			Angle = Math.Atan(point.Y / point.X);
		}
		public void Extend(double multiplier)
		{
			Length *= multiplier;
		}
		public static implicit operator Vector
			(PointF point
			)
		{
			return
				new Vector
				(Math.Sqrt
					(Math.Pow(point.X, 2) 
					+ Math.Pow(point.Y, 2)
					)
				, Math.Tan(point.Y / point.X)
				)
				;
		}
		public static implicit operator PointF
			(Vector vector
			)
		{
			return 
				new PointF
				(
				);
		}
		public PointF ToPoint()
		{
			return
				new PointF
				((float)(Length * Math.Cos(Angle))
				, (float)(Length * Math.Sin(Angle))
				)
				;
		}
		public override string ToString()
		{
			return
				"Length:"
				+ Length.ToString("F3").TrimEnd(new[] { '0', '.' })
				+ C.sp
				+ "Angle(R:"
				+ Angle.ToString("F3").TrimEnd(new[] { '0', '.' })
				+ C.sp
				+ "D:"
				+ Angle.ToDegrees().ToString("F3").TrimEnd(new[] { '0', '.' })
				+ ")"
				;
		}
	}
}