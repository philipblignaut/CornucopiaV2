using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct PolarD
	{
		public PointD Origin { get; private set; }
		public double Radius { get; private set; }
		public double Angle { get; private set; }
		public PolarD
			(PointD origin
			,double radius
			, double angle
			)
		{
			Origin = origin;
			Radius = radius;
			Angle = angle;
		}

		public double Degrees
		{
			get => Angle.ToDegrees();
		}

		public void Resize(double ratio) => Radius *= ratio;

		public static implicit operator PointD(PolarD polarD)
		{
			double x = polarD.Radius * polarD.Angle.Cos();
			double y = polarD.Radius * polarD.Angle.Sin();
			return new PointD(polarD.Origin.X, polarD.Origin.Y, x, y);
		}

		public static implicit operator PolarD(PointD pointD)
		{
			double angle = (pointD.Y / pointD.X).Atan();
			double radius = Math.Sqrt(pointD.X * pointD.X + pointD.Y * pointD.Y);
			return new PolarD(new PointD(pointD.OriginX, pointD.OriginY, pointD.X, pointD.Y), radius, angle);
		}

	}
}
