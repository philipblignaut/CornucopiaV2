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
		[XIncrement(-10)]
		[YIncrement(-10)]
		NorthToWest = 128,

		[XIncrement(10)]
		[YIncrement(0)]
		EastToEast = 256,
		[XIncrement(0)]
		[YIncrement(10)]
		SouthToSouth = 512,
		[XIncrement(-10)]
		[YIncrement(0)]
		WestToWest = 1024,
		[XIncrement(0)]
		[YIncrement(-10)]
		NorthToNorth = 2048,

		EastToWest = 4096,
		SouthToNorth = 8192,
		WestToEast = 16384,
		NorthToSouth = 32768,
	}

}