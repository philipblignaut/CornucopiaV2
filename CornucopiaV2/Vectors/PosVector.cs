using System;
using System.Drawing;

namespace CornucopiaV2
{
	public struct PosVector
		: IEquatable<PosVector>
	{

		internal event ReportRotated Rotated;

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
			Rotated = null;
			End = new PointF();
		}

		public PosVector
			(float x
			, float y
			, float length
			, float angleRadians
			)
			: this
			(new PointF(x, y)
			, length
			, angleRadians
			)
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
			if (Rotated!=null)
			{
				Rotated.Invoke(angleRadians);
			}
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

		public bool Equals(PosVector other)
		{
			return
				other!=null
				&&Start == other.Start
				&& Length == other.Length
				&& AngleRadians == other.AngleRadians
				;
		}

		public override bool Equals(object obj)
			=> obj != null
			&& obj is PosVector other
			&& Equals(other)
			;

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

		public override int GetHashCode()
			=>
			Start.GetHashCode()
			& Length.GetHashCode()
			& AngleRadians.GetHashCode()
			;

		public override string ToString()
			=>
			$"s {Start.ToFormatString()}"
			+ $" len {Length,8:###.000}"
			+ $" a {AngleDegrees,7:###.00}"
			+ $" e {End.ToFormatString()}"
			;

		public PosVector Clone()
			=>
			new PosVector
				(new PointF
					(Start.X
					, Start.Y
					)
				, Length
				, AngleRadians
				)
			;

	}

}