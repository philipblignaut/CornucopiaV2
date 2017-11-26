using System.Drawing;

namespace CornucopiaV2
{
	public class NavUnit
	{
		public Direction StartDirection { get; private set; }
		public Direction EndDirection { get; private set; }
		public Quadrant Quadrant { get;  set; }
#pragma warning disable RCNoAssignment // No assignment to a get-only auto-property.
		public float XOffset { get => Quadrant.XOffset(); }
		public float YOffset { get => Quadrant.YOffset(); }
		public float StartAngle { get => Quadrant.StartAngle(); }
		public float SweepAngle { get => Quadrant.SweepAngle(); }
		public float XIncrement { get => Quadrant.XIncrement(); }
		public float YIncrement { get => Quadrant.YIncrement(); }
#pragma warning restore RCNoAssignment // No assignment to a get-only auto-property.
		public float XFrom { get; private set; }
		public float YFrom { get; private set; }
		public float XTo { get; private set; }
		public float YTo { get; private set; }
		public float XLength { get; private set; }
		public float YLength { get; private set; }
#pragma warning disable RCNoAssignment // No assignment to a get-only auto-property.
		public PointF PointFFrom { get => new PointF(XFrom, YFrom); }
		public PointF PointFTo { get => new PointF(XTo, YTo); }
#pragma warning restore RCNoAssignment // No assignment to a get-only auto-property.

		public NavUnit
			(Direction direction
			, float x
			, float y
			, float xLength
			, float yLength

			)
		{
			StartDirection = direction;
			EndDirection = direction;
			XFrom = x;
			YFrom = y;
			XTo = x;
			YTo = y;
			XLength = xLength;
			YLength = yLength;
		}

		public void AdjustXY
			(float deltaX
			, float deltaY
			)
		{
			XFrom += deltaX;
			XTo += deltaX;
			YFrom += deltaY;
			YTo += deltaY;
		}

		public void ScaleXY
			(float scaleX
			, float scaleY
			)
		{
			XFrom *= scaleX;
			XTo *= scaleX;
			YFrom *= scaleY;
			YTo *= scaleY;
			XLength *= scaleX;
			YLength *= scaleY;
		}

		public void Drive
			(string direction
			)
		{
			switch (direction.ToUpper())
			{
				case "L":
					switch (StartDirection)
					{
						case Direction.East:
							{
								EndDirection = Direction.North;
								Quadrant = Quadrant.EastToNorth;
							}
							break;
						case Direction.South:
							{
								EndDirection = Direction.East;
								Quadrant = Quadrant.SouthToEast;
							}
							break;
						case Direction.West:
							{
								EndDirection = Direction.South;
								Quadrant = Quadrant.WestToSouth;
							}
							break;
						case Direction.North:
							{
								EndDirection = Direction.West;
								Quadrant = Quadrant.NorthToWest;
							}
							break;
					}
					break;
				case "R":
					switch (StartDirection)
					{
						case Direction.East:
							{
								EndDirection = Direction.South;
								Quadrant = Quadrant.EastToSouth;
							}
							break;
						case Direction.South:
							{
								EndDirection = Direction.West;
								Quadrant = Quadrant.SouthToWest;
							}
							break;
						case Direction.West:
							{
								EndDirection = Direction.North;
								Quadrant = Quadrant.WestToNorth;
							}
							break;
						case Direction.North:
							{
								EndDirection = Direction.East;
								Quadrant = Quadrant.NorthToEast;
							}
							break;
					}
					break;
				case "F":
				default:
					if (direction != "F")
					{
						ConDeb.Print(direction + " ???");
					}
					EndDirection = StartDirection;
					switch (EndDirection)
					{
						case Direction.East:
							Quadrant = Quadrant.EastToEast;
							break;
						case Direction.South:
							Quadrant = Quadrant.SouthToSouth;
							break;
						case Direction.West:
							Quadrant = Quadrant.WestToWest;
							break;
						case Direction.North:
							Quadrant = Quadrant.NorthToNorth;
							break;
					}
					break;
				//default:
					//throw
					//	new ArgumentException
					//	("Bad turn Character '" + direction + "'", "direction"
					//	)
					//	;
					//break;
			}
			XTo += XIncrement * XLength;
			YTo += YIncrement * YLength;
		}

		public override string ToString()
		{
			return
				Converter
				.ToIEnumerable
				("xf"
				, "{0,7:#.##}".FormatWith(XFrom)
				, "yf"
				, "{0,7:#.##}".FormatWith(YFrom)
				, "xt"
				, "{0,7:#.##}".FormatWith(XTo)
				, "yt"
				, "{0,7:#.##}".FormatWith(YTo)
				, "xi"
				, "{0,2}".FormatWith(XIncrement)
				, "yi"
				, "{0,2}".FormatWith(YIncrement)
				, "q"
				, "{0,12}".FormatWith(Quadrant)
				, (int)Quadrant > (int)Quadrant.NorthToWest
					? C.es
					: " xo "
					+"{0,2}".FormatWith(XOffset)
					+" yo "
					+"{0,2}".FormatWith(YOffset)
				)
				.JoinToCharacterSeparatedValues(C.sp)
				;
		}

	}
}
