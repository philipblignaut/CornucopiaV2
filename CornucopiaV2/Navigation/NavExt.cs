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
			cImage.DrawString(line, "Consolas", 7, Color.Black, 150, row);
			row += 10;
		}

		public static void Draw
			(this List<NavUnit> navUnitList
			, CorImage image
			, float lineWidth
			)
		{
			//ConDeb.ConDebPrintDelegate += CatchLine;
			cImage = image;
			List<Color> colors = new List<Color>();
			colors.Add(Color.Red);
			colors.Add(Color.Green);
			colors.Add(Color.Green);
			colors.Add(Color.Blue);
			colors.Add(Color.Blue);
			colors.Add(Color.Magenta);
			colors.Add(Color.Magenta);
			colors.Add(Color.Red);
			colors.Add(Color.Red);
			colors.Add(Color.Green);
			colors.Add(Color.Green);
			colors.Add(Color.Blue);
			colors.Add(Color.Blue);
			colors.Add(Color.Magenta);
			colors.Add(Color.Magenta);
			colors.Add(Color.Red);
			colors.Add(Color.Red);
			colors.Add(Color.Green);
			colors.Add(Color.Green);
			colors.Add(Color.Blue);
			colors.Add(Color.Blue);
			colors.Add(Color.Magenta);
			colors.Add(Color.Magenta);
			colors.Add(Color.Red);
			colors.Add(Color.Red);
			colors.Add(Color.Green);
			colors.Add(Color.Green);
			colors.Add(Color.Blue);
			colors.Add(Color.Blue);
			colors.Add(Color.Magenta);
			colors.Add(Color.Magenta);
			colors.Add(Color.Red);
			ColorGradientFactory cgf =
				new ColorGradientFactory
				(colors.ToArray()
				)
				;
			for (int i = 0; i < navUnitList.Count; i++)
			{
				Color color = cgf.ColorAtPercent((0F + i) / (navUnitList.Count - 1F) * 100F);
				NavUnit navUnit = navUnitList[i];
				//ConDeb.Print(navUnit.Quadrant.ToString());
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
						ConDeb
							.Print
							("C " + navUnit.ToString()
							+ C.sp + color.ToHTML()
							)
							;
						if (i % 2 == 0)
						{
							//CircleStart(image, navUnit, color);
						}
						//image.Save(@"d:\numbers\fractaline.png", ImageFormat.Png);
						break;
					case Quadrant.EastToSouth:
					case Quadrant.EastToNorth:
					case Quadrant.SouthToWest:
					case Quadrant.SouthToEast:
					case Quadrant.WestToNorth:
					case Quadrant.WestToSouth:
					case Quadrant.NorthToEast:
					case Quadrant.NorthToWest:
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
						//image
						//	.DrawLine
						//	(color
						//	, lineWidth
						//	, navUnit.XFrom
						//	, navUnit.YFrom
						//	, navUnit.XTo
						//	, navUnit.YFrom
						//	)
						//	;
						//image
						//	.DrawLine
						//	(color
						//	, lineWidth
						//	, navUnit.XTo
						//	, navUnit.YFrom
						//	, navUnit.XTo
						//	, navUnit.YTo
						//	)
						//	;
						if (i % 2 == 0)
						{
							//CircleStart(image, navUnit, color);
						}
						//ConDeb
						//	.Print
						//	("C " + navUnit.ToString()
						//	+ C.sp + color.ToHTML()
						//	)
						//	;
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