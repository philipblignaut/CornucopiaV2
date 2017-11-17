using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace CornucopiaV2
{
	public static class NavExt
	{

		public static void FitGraph
			(this IEnumerable<NavUnit> navUnitList
			, int imageWidth
			, int imageHeight
			, float borderPixels
			)
		{
			float minX = navUnitList.MinX();
			float maxX = navUnitList.MaxX();
			float minY = navUnitList.MinY();
			float maxY = navUnitList.MaxY();
			float graphWidth = maxX - minX;
			float graphHeight = maxY - minY;
			float ratioX = graphWidth / (imageWidth - borderPixels * 2);
			float ratioY = graphHeight / (imageHeight - borderPixels * 2);
			float ratio = ratioX > ratioY ? ratioX : ratioY;
			foreach (NavUnit navUnit in navUnitList)
			{
				navUnit.AdjustXY(0 - minX, 0 - minY);
				navUnit.ScaleXY(1 / ratio, 1 / ratio);
			}
			navUnitList.CentreGraph(imageWidth, imageHeight, borderPixels);
		}

		public static void CentreGraph
			(this IEnumerable<NavUnit> navUnitList
			, int imageWidth
			, int imageHeight
			, float borderPixels
			)
		{
			float minX = navUnitList.MinX();
			float minY = navUnitList.MinY();
			float maxX = navUnitList.MaxX();
			float maxY = navUnitList.MaxY();
			float graphWidth = maxX - minX;
			float graphHeight = maxY - minY;
			float offsetX = (imageWidth - graphWidth) / 2F - minX;
			float offsetY = (imageHeight - graphHeight) / 2F - minY;
			foreach (NavUnit navUnit in navUnitList)
			{
				navUnit.AdjustXY(offsetX, offsetY);
			}
		}

		public static void Drive
			(this List<NavUnit> navUnitList
			, string map
			, Direction direction
			, float x
			, float y
			, float unitWidth
			, float unitHeight
			)
		{
			foreach (char mapChar in map)
			{
				NavUnit navUnit =
					new NavUnit
					(direction
					, x
					, y
					, unitWidth
					, unitHeight
					)
					;
				navUnit
						.Drive
						(mapChar.ToString()
						)
						;
				navUnitList.Add(navUnit);
				x = navUnit.XTo;
				y = navUnit.YTo;
				direction = navUnit.EndDirection;
			}
		}

		private static int row = 30;
		private static CorImage cImage = null;

		private static void CatchLine(string line)
		{
			cImage.DrawString(line, "Consolas", 7, Color.Black, 150, row, new PointF(-cImage.Width / 2, -cImage.Height / 2), new PointF(cImage.Width / 2, cImage.Height / 2));
			row += 10;
		}

		public static void Draw
			(this List<NavUnit> navUnitList
			, CorImage image
			, float lineWidth
			, CurveType curveType
			, ColorGradientFactory cgf
			)
		{
			cImage = image;
			for (int i = 0; i < navUnitList.Count; i++)
			{
				Color color = cgf.ColorAtPercent((0F + i) / (navUnitList.Count - 1F) * 100F);
				NavUnit navUnit = navUnitList[i];
				switch (navUnit.Quadrant)
				{
					case Quadrant.EastToEast:
					case Quadrant.SouthToSouth:
					case Quadrant.WestToWest:
					case Quadrant.NorthToNorth:
						image
							.DrawLine
							(color
							, lineWidth
							, navUnit.XFrom
							, navUnit.YFrom
							, navUnit.XTo
							, navUnit.YTo
							)
							;
						break;
					case Quadrant.EastToSouth:
					case Quadrant.EastToNorth:
					case Quadrant.SouthToWest: // ok
					case Quadrant.SouthToEast:
					case Quadrant.WestToNorth: // pk
					case Quadrant.WestToSouth:
					case Quadrant.NorthToEast: // ok
					case Quadrant.NorthToWest:
						switch (curveType)
						{
							case CurveType.Curve:
								image
									.DrawArc
									(color
									, lineWidth
									, navUnit.XFrom + navUnit.XLength * navUnit.XOffset
									, navUnit.YFrom + navUnit.YLength * navUnit.YOffset
									, navUnit.XLength * 2
									, navUnit.YLength * 2
									, navUnit.StartAngle
									, navUnit.SweepAngle
									)
									;
								break;
							case CurveType.Line:
								image
									.DrawLine
									(color
									, lineWidth
									, navUnit.XFrom
									, navUnit.YFrom
									, navUnit.XTo
									, navUnit.YTo
									)
									;
								break;
							case CurveType.Square:
								switch (navUnit.StartDirection)
								{
									case Direction.East:
									case Direction.West:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YFrom
											, navUnit.XTo
											, navUnit.YFrom
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XTo
											, navUnit.YFrom
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										break;
									case Direction.South:
									case Direction.North:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YFrom
											, navUnit.XFrom
											, navUnit.YTo
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YTo
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										break;
									default:
										break;
								}
								break;
							case CurveType.Rose:
								switch (navUnit.StartDirection)
								{
									case Direction.South:
									case Direction.North:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YTo
											, navUnit.XFrom
											, navUnit.YTo
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XTo
											, navUnit.YFrom
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										break;
									case Direction.East:
									case Direction.West:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YFrom
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom
											, navUnit.YTo
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										break;
									default:
										break;
								}
								break;
							case CurveType.ZigZag:
								//ConDeb
								//	.Print
								//	("{0,5} ".FormatWith(navUnit.StartDirection)
								//	+ navUnit.Quadrant
								//	)
								//	;
								switch (navUnit.StartDirection)
								{
									case Direction.South:
									case Direction.North:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom // zz
											, navUnit.YTo
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom  // zz working
											, navUnit.YFrom
											, navUnit.XFrom
											, navUnit.YTo
											)
											;
										break;
									case Direction.East:
									case Direction.West:
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom  // zz
											, navUnit.YFrom
											, navUnit.XFrom
											, navUnit.YTo
											)
											;
										image
											.DrawLine
											(color
											, lineWidth
											, navUnit.XFrom  // zz
											, navUnit.YTo
											, navUnit.XTo
											, navUnit.YTo
											)
											;
										break;
									default:
										break;
								}
								//image
								//	.Save
								//	(@"d:\numbers\philip\newphilip"
								//	+ curveType.ToString()
								//	+ "4"
								//	+ "11111111.gif"
								//	, ImageFormat.Gif
								//	)
								//	;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
		}

		private static void CircleStart
			(CorImage image
			, NavUnit navUnit
			, Color color
			)
		{
			image
				.DrawArc
				(color
				, 1
				, navUnit.XFrom - 3
				, navUnit.YFrom - 3
				, 6
				, 6
				, 0
				, 360
				)
				;
		}

		public static float MinX
			(this IEnumerable<NavUnit> navUnitList
			)
		{
			return Math
				.Min
					(navUnitList.Convert(unit => unit.XFrom).Min()
					, navUnitList.Convert(unit => unit.XTo).Min()
					)
					;
		}

		public static float MinY
			(this IEnumerable<NavUnit> navUnitList
			)
		{
			return Math
				.Min
					(navUnitList.Convert(unit => unit.YFrom).Min()
					, navUnitList.Convert(unit => unit.YTo).Min()
					)
					;
		}


		public static float MaxX
			(this IEnumerable<NavUnit> navUnitList
			)
		{
			return Math
				.Max
					(navUnitList.Convert(unit => unit.XFrom).Max()
					, navUnitList.Convert(unit => unit.XTo).Max()
					)
					;
		}

		public static float MaxY
			(this IEnumerable<NavUnit> navUnitList
			)
		{
			return Math
				.Max
					(navUnitList.Convert(unit => unit.YFrom).Max()
					, navUnitList.Convert(unit => unit.YTo).Max()
					)
					;
		}

	}
}