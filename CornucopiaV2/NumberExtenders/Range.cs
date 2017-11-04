using System;

namespace CornucopiaV2
{
	public class Range
	{
		public double Min { get; private set; }
		public double Max { get; private set; }
		public Range
		   (double min
		   , double max
		   )
		{
			Min = min;
			Max = max;
		}
		public double Distance
		{
			get
			{
				return Math.Abs(Max - Min);
			}
		}

		public Range Zoom
		   (double ratio
		   )
		{
			double middle = Min + (Max - Min) / 2.0;
			double distance = Distance;
			Min = middle - distance / 2.0 * ratio;
			Max = middle + distance / 2.0 * ratio;
			return new Range(Min, Max);
		}
		public Range Move
		   (double distance
		   )
		{
			Min += distance;
			Max += distance;
			return new Range(Min, Max);
		}
		public override string ToString()
		{
			return Min.ToString() + " " + Max.ToString() + " " + Distance.ToString();
		}
	}
}