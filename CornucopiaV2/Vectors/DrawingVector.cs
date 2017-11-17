using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct DrawingVector
		: IEquatable<DrawingVector>
	{
		public PosVector PosVector { get;  set; } // internal
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
		}

		public void Draw
			(CorImage image
			,bool drawStartEndCircles = false
			)
		{
			float lineWidth = LineWidth;
			lineWidth = lineWidth < 1 ? 1 : lineWidth;
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

		//internal void ChangeStart(PointF start)
		//{
		//	float lenght = PosVector.Length;
		//	float angleRadians = PosVector.AngleRadians;
		//	PosVector =
		//		new PosVector
		//		(start
		//		, lenght
		//		, angleRadians
		//		)
		//		;
		//}

		public bool Equals(DrawingVector other)
		{
			return
				PosVector == other.PosVector
				&& LineWidth == other.LineWidth
				&& Color == other.Color
				;
		}

		public override string ToString()
		{
			return
				$@"{PosVector} {LineWidth:##.0} {Color}"
				;
		}
	}
}
