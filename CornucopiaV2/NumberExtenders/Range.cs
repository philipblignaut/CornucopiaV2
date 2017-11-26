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
			=> (Max - Min).Abs();

		public Range Zoom
		   (double ratio
		   )
		{
			double middle = Min + (Max - Min) / 2.0;
			double distance = Distance / 2.0 * ratio;
			return new Range(middle - distance, middle + distance);
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
			=> $"{Min} {Max} {Distance}";

	}
}