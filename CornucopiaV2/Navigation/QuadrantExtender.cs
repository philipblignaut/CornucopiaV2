namespace CornucopiaV2
{
	public static class QuadrantExtender
	{
		public static float StartAngle
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<StartAngleAttribute>().Angle;
		}

		public static float SweepAngle
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<SweepAngleAttribute>().Angle;
		}

		public static float XOffset
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<XOffsetAttribute>().Offset;
		}

		public static float YOffset
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<YOffsetAttribute>().Offset;
		}

		public static float XIncrement
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<XIncrementAttribute>().Unit;
		}

		public static float YIncrement
			(this Quadrant quadrant
			)
		{
			return quadrant.GetAttribute<YIncrementAttribute>().Unit;
		}

	}

}