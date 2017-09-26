using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct PointD
	{
		public double OriginX { get; private set; }
		public double OriginY { get; private set; }
		public double X { get; private set; }
		public double Y { get; private set; }
		public PointD
			(double originX
			, double originY
			, double x
			, double y
			)
		{
			OriginX = originX;
			OriginY = originY;
			X = x;
			Y = y;
		}
	}
}
