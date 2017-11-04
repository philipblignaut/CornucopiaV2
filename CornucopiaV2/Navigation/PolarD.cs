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
			, double radius
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

		public void Resize
			(double ratio
			)
			=> Radius *= ratio;

	}
}