using System;
using System.Drawing;

namespace CornucopiaV2
{
	public struct DrawingVector
		: IEquatable<DrawingVector>
	{
		public event ReportRotated Rotated;
		public PosVector PosVector { get; set; } // internal
		public float LineWidth { get; private set; }
		public Color Color { get; private set; }
		/// <summary>Record Constructor</summary>
		/// <param name="posVector"><see cref="PosVector"/></param>
		/// <param name="lineWidth"><see cref="LineWidth"/></param>
		/// <param name="color"><see cref="Color"/></param>
		public DrawingVector
			(PosVector posVector
			, float lineWidth
			, Color color
			)
		{
			PosVector = posVector;
			LineWidth = lineWidth;
			Color = color;
			Rotated = null;
			posVector.Rotated += PosVectorRotated;
		}

		private void PosVectorRotated(float angleRadians)
		{
			if (Rotated != null)
			{
				Rotated.Invoke(angleRadians);
			}
		}

		public void Draw
			(CorImage image
			, bool drawStartEndCircles = false
			)
		{
			float lineWidth = LineWidth < 1 ? 1 : LineWidth;
			image
				.DrawLine
				(Color
				, lineWidth
				, PosVector.Start
				, PosVector.End
				)
				;
			if (drawStartEndCircles)
			{
				image
					.DrawCircle
					(PosVector.Start
					, 100
					, lineWidth
					, Color
					)
					;
				image
					.DrawCircle
					(PosVector.End
					, 100
					, lineWidth
					, Color
					)
					;
			}
		}

		public bool Equals(DrawingVector other)
			=> PosVector == other.PosVector
				&& LineWidth == other.LineWidth
				&& Color == other.Color
				;

		public override bool Equals(object obj)
			=> (obj is DrawingVector other)
			? Equals(other)
			: false
			;

		public static bool operator ==
			(DrawingVector left
			, DrawingVector right
			) => left.Equals(right)
			;

		public static bool operator !=
			(DrawingVector left
			, DrawingVector right
			) => !left.Equals(right)
			;

		public override int GetHashCode()
			=>
				PosVector.GetHashCode()
				& LineWidth.GetHashCode()
				& Color.GetHashCode()
				;

		public override string ToString()
			=>
				$"{PosVector} {LineWidth.Round(2):##.0} {Color}"
				;

	}
}