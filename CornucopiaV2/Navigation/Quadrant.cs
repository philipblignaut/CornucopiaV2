using System;
using System.Reflection;

namespace CornucopiaV2
{

	[FlagsAttribute]
	public enum Quadrant
	{
		[StartAngle(0)]
		[SweepAngle(90)]
		[XOffset(-1)]
		[YOffset(-2)]
		[XIncrement(1)]
		[YIncrement(-1)]
		EastToNorth = 1,

		[StartAngle(270)]
		[SweepAngle(90)]
		[XOffset(-1)]
		[YOffset(0)]
		[XIncrement(1)]
		[YIncrement(1)]
		EastToSouth = 2,

		[StartAngle(90)]
		[SweepAngle(90)]
		[XOffset(0)]
		[YOffset(-1)]
		[XIncrement(1)]
		[YIncrement(1)]
		SouthToEast = 4,

		[StartAngle(0)]
		[SweepAngle(90)]
		[XOffset(-2)]
		[YOffset(-1)]
		[XIncrement(-1)]
		[YIncrement(1)]
		SouthToWest = 8,

		[StartAngle(180)]
		[SweepAngle(90)]
		[XOffset(-1)]
		[YOffset(0)]
		[XIncrement(-1)]
		[YIncrement(1)]
		WestToSouth = 16,

		[StartAngle(90)]
		[SweepAngle(90)]
		[XOffset(-1)]
		[YOffset(-2)]
		[XIncrement(-1)]
		[YIncrement(-1)]
		WestToNorth = 32,

		[StartAngle(180)]
		[SweepAngle(90)]
		[XOffset(0)]
		[YOffset(-1)]
		[XIncrement(1)]
		[YIncrement(-1)]
		NorthToEast = 64,

		[StartAngle(270)]
		[SweepAngle(90)]
		[XOffset(-2)]
		[YOffset(-1)]
		[XIncrement(-1)]
		[YIncrement(-1)]
		NorthToWest = 128,

		[XIncrement(1)]
		[YIncrement(0)]
		EastToEast = 256,
		[XIncrement(0)]
		[YIncrement(1)]
		SouthToSouth = 512,
		[XIncrement(-1)]
		[YIncrement(0)]
		WestToWest = 1024,
		[XIncrement(0)]
		[YIncrement(-1)]
		NorthToNorth = 2048,

		EastToWest = 4096,
		SouthToNorth = 8192,
		WestToEast = 16384,
		NorthToSouth = 32768,
	}

	#region Angle

	public abstract class AngleAttribute : Attribute
	{
		public float Angle { get; private set; }
		public AngleAttribute(float angle) { Angle = angle; }
	}

	public class StartAngleAttribute : AngleAttribute
	{
		public StartAngleAttribute(float angle) : base(angle) { }
	}

	public class SweepAngleAttribute : AngleAttribute
	{
		public SweepAngleAttribute(float angle) : base(angle) { }
	}

	#endregion

	#region Offset

	public abstract class OffsetAttribute : Attribute
	{
		public float Offset { get; private set; }
		public OffsetAttribute(float offset) { Offset = offset; }
	}

	public class XOffsetAttribute : OffsetAttribute
	{
		public XOffsetAttribute(float offset) : base(offset) { }
	}

	public class YOffsetAttribute : OffsetAttribute
	{
		public YOffsetAttribute(float offset) : base(offset) { }
	}

	#endregion

	#region Increment

	public abstract class IncrementAttribute : Attribute
	{
		public int Unit { get; private set; }
		public IncrementAttribute(int unit) { Unit = unit; }
	}

	public class XIncrementAttribute : IncrementAttribute
	{
		public XIncrementAttribute(int unit) : base(unit) { }
	}

	public class YIncrementAttribute : IncrementAttribute
	{
		public YIncrementAttribute(int unit) : base(unit) { }
	}

	#endregion
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

	public enum Goeters
	{
		[Subject("aaaaaaaaaa")]
		A = 1,
	}

	public abstract class AStringAttribute : Attribute
	{
		public string Subject { get; private set; }
		public AStringAttribute(string subject)
		{
			Subject = subject;
		}
	}

	public class SubjectAttribute: AStringAttribute
	{
		public SubjectAttribute
			(string subject)
			
			: base(subject)
		{
		}
	}

	public static class SubjectExtender
	{
		public static string Subject
			(this Goeters ding
			)
		{
			string result = null;
			result = ding.GetAttribute<SubjectAttribute>().Subject;
			return result;
		}
	}
}