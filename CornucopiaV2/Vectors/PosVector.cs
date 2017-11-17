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
		public PointF Start { get; internal set; }
		public float AngleRadians { get; private set; }
		public float AngleDegrees { get => AngleRadians.ToDegrees(); private set { } }
		public float Length { get; set; }
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
			, float y
			, float length
			, float angleRadians
			)
			: this(new PointF(x, y), length, angleRadians)
		{
		}

		private PointF CalculateEnd()
		{
			return
				new PointF
				(Start.X + Length * AngleRadians.CosF()
				, Start.Y + Length * AngleRadians.SinF()
				)
				;
		}

		public void Rotate
			(float angleRadians
			)
		{
			AngleRadians += angleRadians;
		}

		/// <summary>
		/// <para>Lenthen the length with factor > 1.</para>
		/// <para>Shorten the length with factor < 1.</para>
		/// </summary>
		/// <param name="factor"></param>
		public void StretchShrink
			(float factor
			)
		{
			Length *= factor;
		}

		public void LengthenShorten
			(float distance
			)
		{
			Length += distance;
		}

		public override string ToString()
		{
			return $"s {Start.ToFormatString()}"
				+ $@" len {Length,8:###.000}"
				+ $@" a {AngleDegrees,7:###.00}"
				+ $@" e {End.ToFormatString()}"
				;
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

		public PosVector Abs()
			=>
			new PosVector
				(new PointF
					(Start.X.Abs()
					, Start.Y.Abs()
					)
				, Length
				, AngleRadians
				)
			;

		//public static PosVector Add
		//	(PosVector left
		//	,PosVector right
		//	)
		//	=>
		//	new PosVector
		//		(new PointF
		//			(left.Start.X
		//			+right.Start.Y
		//			)
		//		, 
		//		)

	}

}