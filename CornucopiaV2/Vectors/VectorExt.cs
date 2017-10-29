using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class VectorExt
	{
		public static Vector2 ToVector
			(this PointF point
			)
		{
			return new Vector2(point.X, point.Y);
		}

		public static PointF ToPoint
			(this Vector2 vector
			)
		{
			vector.Length();
			return new PointF(vector.X, vector.Y);
		}

		public static float Dot
			(this Vector2 value1
			, Vector2 value2
			)
		{
			return Vector2.Dot(value1, value2);
		}

		public static double HalfAngleDegrees
			(this double angleToDegrees
			, double angleFromDegrees
			)
		{
			angleFromDegrees = angleFromDegrees.FixDegrees();
			angleToDegrees = angleToDegrees.FixDegrees();
			double angle = ((angleToDegrees - angleFromDegrees) / 2).FixDegrees();
			double hangle = angleFromDegrees + angle;
			hangle = hangle.FixDegrees();
			ConDeb.Print
				("***************"
				+ " f " + angleFromDegrees
				+ $" t " + angleToDegrees
				+ " da " + (angleToDegrees - angleFromDegrees).ToString()
				+ " a " + angle.ToString()
				+ " h " + hangle.ToString()
				)
				;
			return hangle;
		}

		//public static double ZeroTo360Degrees
		//	(this double angleDegrees
		//	)
		//{
		//	while (angleDegrees < 0)
		//	{
		//		angleDegrees += 360;
		//	}
		//	while (angleDegrees > 360)
		//	{
		//		angleDegrees -= 360;
		//	}
		//	return angleDegrees;
		//}

		public static double FixDegrees
			(this double angleDegrees
			)
		{
			while (angleDegrees > 180)
			{
				angleDegrees -= 360;
			}
			while (angleDegrees < -180)
			{
				angleDegrees += 360;
			}
			return angleDegrees;
		}

		public static double FixDegrees0T360
			(this double angleDegrees
			)
		{
			while (angleDegrees > 360)
			{
				angleDegrees -= 360;
			}
			while (angleDegrees < 0)
			{
				angleDegrees += 360;
			}
			return angleDegrees;
		}

		public static Vector2 Transformx
			(this Vector2 position
			, Matrix3x2 matrix
			)
		{
			return Vector2.Transform(position, matrix);
		}

		public static Vector2 AlterDirectionDegrees
			(this Vector2 vector
			, double angleDegrees
			)
		{
			Vector2 vector2 = new Vector2((int)vector.X, (int)vector.Y);
			double angleRadians = angleDegrees.ToRadians();
			double angle =
				Math
				.Atan2
				(angleRadians.Cos()
				, angleRadians.Sin()
				)
				;
			return new Vector2((float)angle.Cos(), (float)angle.Sin());
		}
	}
}