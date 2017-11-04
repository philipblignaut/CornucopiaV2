using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct PosVector
		: IEquatable<PosVector>
	{
		public PointF Start { get; private set; }
		public float AngleRadians { get; private set; }
		public float AngleDegrees { get => AngleRadians.ToDegrees(); private set { } }
		public float Length { get; private set; }
		public PointF End { get => CalculateEnd(); private set { } }

		public PosVector
			(PointF start
			, float length
			, float angleRadians
			)
		{
			Start = start;
			Length = length;
			AngleRadians = angleRadians;
			End = new PointF();
		}
		public PosVector
			(float x
			,float y
			, float angleRadians
			, float length
			)
		{
			Start = new PointF(x, y);
			Length = length;
			AngleRadians = angleRadians;
			End = new PointF();
		}

		private PointF CalculateEnd()
		{
			return
				new PointF
				(Start.X + Length * AngleRadians.CosF()
				, Start.Y + Length *AngleRadians.SinF()
				)
				;
		}

		public void Rotate
			(float angleRadians
			)
		{
			AngleRadians += angleRadians;
		}

		public void StretchShrink
			(float factor
			)
		{
			Length *= factor;
		}

		public override string ToString()
		{
			return $"s {Start} l {Length} ad {AngleDegrees}";
		}

		public override int GetHashCode()
		{
			return
				Start.GetHashCode()
				& Length.GetHashCode()
				& AngleRadians.GetHashCode()
				;
		}

		public bool Equals(PosVector other)
		{
			return
				Start == other.Start
				&& Length == other.Length
				&& AngleRadians == other.AngleRadians
				;
		}

		public override bool Equals(object obj)
		{
			if (obj is PosVector)
			{
				return Equals((PosVector)obj);
			}
			return false;
		}

		public static bool operator ==
			(PosVector left
			, PosVector right
			) => left.Equals(right)
			;

		public static bool operator !=
			(PosVector left
			, PosVector right
			) => !left.Equals(right)
			;

	}
}
