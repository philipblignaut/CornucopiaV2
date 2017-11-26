using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CornucopiaV2
{
	public static class DrawingVectorExt
	{

		public static void MinMaxPoints
			(this IEnumerable<DrawingVector> vectorList
			, Action<PointF, PointF> reportBackMinAndMax
			)
		{
			List<float> minmaxX = new List<float>();
			List<float> minmaxY = new List<float>();
			"Loop vectorList MinMaxPoints".Print();
			foreach (DrawingVector drawingVector in vectorList)
			{
				minmaxX.Add(drawingVector.PosVector.Start.X);
				minmaxX.Add(drawingVector.PosVector.End.X);

				minmaxY.Add(drawingVector.PosVector.Start.Y);
				minmaxY.Add(drawingVector.PosVector.End.Y);
			}
			//ConDeb.Print($@"minmax count {minmaxX.Count()} vectorlist count {vectorList.Count()} ");
			reportBackMinAndMax
				.Invoke
				(new PointF
					(minmaxX.Min()
					, minmaxY.Min()
					)
				, new PointF
					(minmaxX.Max()
					, minmaxY.Max()
					)
				)
				;
		}

		public static List<DrawingVector> CentreDrawing
			(this IEnumerable<DrawingVector> drawingVectorList
			, PointF topLeft
			, PointF bottomRight
			)
		{
			PointF drawingCentre = 
				drawingVectorList
				.DrawingCentre
				((min, size)=>
				{
					$@"Min  {min.ToFormatString()}".Print();
					$@"Size {size.ToFormatString()}".Print();
				}
				)
				;
			float imageWidth = (topLeft.X - bottomRight.X);
			float imageHeight = (topLeft.Y - bottomRight.Y);
			PointF imageCentre =
				new PointF
				(bottomRight.X + imageWidth / 2
				, bottomRight.Y + imageHeight / 2
				)
				;
			float differenceX = imageCentre.X - drawingCentre.X;
			float differenceY = imageCentre.Y - drawingCentre.Y;
			List<DrawingVector> centredList = new List<DrawingVector>();
			"Loop drawingVectorList CentreDrawing".Print();
			foreach (DrawingVector drawingVector in drawingVectorList)
			{
				//$@"before {drawingVector}".Print();
				PointF start = drawingVector.PosVector.Start;
				start =
					new PointF
					(start.X + differenceX
					, start.Y + differenceY
					)
					;
				DrawingVector newVector =
					new DrawingVector
					(new PosVector
						(start
						, drawingVector.PosVector.Length
						, drawingVector.PosVector.AngleRadians
						)
					, drawingVector.LineWidth
					, drawingVector.Color
					)
					;
				centredList.Add(newVector);
				//$@"middle {newVector}".Print();
			}
			//centredList.Print();
			PointF drawingMin = new PointF();
			SizeF drawingSize = new SizeF();
			drawingCentre = 
				centredList
				.DrawingCentre
				((min, size)=>
				{
					$@"Min  {min.ToFormatString()}".Print();
					$@"Size {size.ToFormatString()}".Print();
					drawingMin = min;
					drawingSize = size;
				}
				)
				;
			float drawingWidthRatio = drawingSize.Width / imageWidth;
			float drawingHeightRatio = drawingSize.Height / imageHeight;
			float drawingRatio =
				drawingWidthRatio > drawingHeightRatio
				? drawingWidthRatio
				: drawingHeightRatio
				;
			$@"ratio w {drawingWidthRatio} h {drawingHeightRatio} r {drawingRatio}".Print();
			List<DrawingVector> resizedList = new List<DrawingVector>();
			"Loop centredList CentreDrawing".Print();
			foreach (DrawingVector drawingVector in centredList)
			{
				PosVector posVector = drawingVector.PosVector;
				DrawingVector resizedVector =
					new DrawingVector
					(new PosVector
						(posVector.Start
						, posVector.Length / drawingRatio
						, posVector.AngleRadians
						)
					, drawingVector.LineWidth
					, drawingVector.Color
					)
					;
				//$@"after  {resizedVector}".Print();
				resizedList.Add(resizedVector);
			}
			return resizedList.ToList();
		}

		private static PointF DrawingCentre
			(this IEnumerable<DrawingVector> drawingVectorList
			, Action<PointF, SizeF> reportBackDrawingMinAndSize
			)
		{
			PointF drawingMin = new PointF();
			PointF drawingMax = new PointF();
			drawingVectorList
				.MinMaxPoints
				((min, max) =>
				{
					drawingMin = min;
					drawingMax = max;
				}
				)
				;
			$@"min  {drawingMin.ToFormatString()}".Print();
			$@"max  {drawingMax.ToFormatString()}".Print();

			float drawingWidth = drawingMax.X - drawingMin.X;
			float drawingHeight = drawingMax.Y - drawingMin.Y;
			SizeF drawingSize =
				new SizeF
				(drawingWidth
				, drawingHeight
				)
				;
			reportBackDrawingMinAndSize
				.Invoke
				(drawingMin
				, drawingSize
				)
				;
			$@"dim w {drawingWidth:###0.00}".Print();
			$@"dim h {drawingHeight:###0.00}".Print();

			PointF drawingCentre =
				new PointF
				(drawingMin.X + drawingWidth / 2
				, drawingMin.Y + drawingHeight / 2
				)
				;
			$@"drawing centre {drawingCentre.ToFormatString()}".Print();
			//if (drawingCentre.DistanceTo(new PointF(0, 0)) < 1)
			//{
			//	throw new ExceptionOf<PointF>
			//		("After centring all DrawingVectors, the new calculated centre is "
			//		+ $@"{drawingCentre.ToString()}"
			//		)
			//		;
			//}
			return drawingCentre;
		}

		public static void Print
			(this IEnumerable<DrawingVector> imageVectorList
			)
		{
			int ivIndex = 0;
			foreach (DrawingVector imageVector in imageVectorList)
			{
				ConDeb.Print($@"{ivIndex,5:0} {imageVector}");
				ivIndex++;
			}
		}


	}
}