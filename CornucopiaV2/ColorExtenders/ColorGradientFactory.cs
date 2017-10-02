using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace CornucopiaV2
{
    public static class ColorExtenders
    {
        public static string ToHTML
           (this Color color
           )
        {
            return ColorTranslator.ToHtml(color);
        }
        public static Color ColorFromHTML
           (this string htmlColor
           )
        {
            return ColorTranslator.FromHtml(htmlColor);
        }
		public static int ToWin32
		   (this Color color
		   )
		{
			return ColorTranslator.ToWin32(color);
		}
	}
	public class ColorGradientFactory
    {
        private Dictionary<float, Color>
           colorAtPercent =
           new Dictionary<float, Color>()
           ;
        public ColorGradientFactory
           (params Color[] colorRange
           )
        {
            colorRange
               .EachIndexed
               ((color, colorIndex) =>
                  colorAtPercent
                  .Add
                  ((colorIndex + 0F) / (colorRange.Length - 1F)
                  , color
                  )
               )
               ;
        }
        public ColorGradientFactory
           (Color colorStart
           , Color colorEnd
           )
            : this
            (new Color[]
			{
			colorStart,
			colorEnd
			}
            )
        {
        }
        public ColorGradientFactory
           (Color colorStart
           , Color colorAtFiftyPercent
           , Color colorEnd
           )
            : this
            (new Color[]
			{
			colorStart,
			colorAtFiftyPercent,
			colorEnd
			}
            )
        {
        }
		public ColorGradientFactory
		   (Color colorStart
		   , Color colorAtThirtyThreePercent
		   , Color colorAtSixySevenPercent
		   , Color colorEnd
		   )
			: this
			(new Color[]
			{
			colorStart,
			colorAtThirtyThreePercent,
			colorAtSixySevenPercent,
			colorEnd
			}
			)
		{
		}
		public ColorGradientFactory
		   (Color colorStart
		   , Color colorAtTwentyFivePercent
		   , Color colorAtFiftyPercent
		   , Color colorAtSeventyFivePercent
		   , Color colorEnd
		   )
			: this
			(new Color[]
			{
			colorStart,
			colorAtTwentyFivePercent,
			colorAtFiftyPercent,
			colorAtSeventyFivePercent,
			colorEnd
			}
			)
		{
		}

        public ColorGradientFactory
           (Color colorStart
           , Color colorAtTwentyPercent
           , Color colorAtFourtyPercent
           , Color colorAtSixtyPercent
           , Color colorAtEightyPercent
           , Color colorEnd
           )
            : this
            (new Color[]
			{
			colorStart,
			colorAtTwentyPercent,
			colorAtFourtyPercent,
			colorAtSixtyPercent,
			colorAtEightyPercent,
			colorEnd
			}
            )
        {
        }

        public void InsertChangeColorAtPercent
           (Color color
           , float percent
           )
        {
            percent = CheckAndTransformPercent(percent);
            if (colorAtPercent.ContainsKey(percent))
            {
                colorAtPercent[percent] = color;
            }
            else
            {
                colorAtPercent.Add(percent, color);
            }
        }

		public float CalculatePercent
			(double lowerValueIncluded
			, double upperValueExcluded
			)
		{
			return (float)(lowerValueIncluded / (upperValueExcluded) * 100F);
		}

        private static float CheckAndTransformPercent(float percent)
        {
            if (percent > 100F)
            {
                percent = 100F;
            }
            if (percent < 0F)
            {
                percent = 0F;
            }
            percent /= 100F;
            return percent;
        }

		public Color ColorAtPercent
			(float lowerValueIncluded
			, float upperValueExcluded
			) =>
			ColorAtPercent
				(CalculatePercent
					(lowerValueIncluded
					, upperValueExcluded
					)
				)
		;

		public Color ColorAtPercent
		   (float percent
		   )
		{
			Color color = colorAtPercent[0F];
            percent = CheckAndTransformPercent(percent);
            if (colorAtPercent.ContainsKey(percent))
            {
                color = colorAtPercent[percent];
            }
            else
            {
                float percStart = 0;
                float percEnd = 0;
                Color colorStart = colorAtPercent[0F];
                Color colorEnd = colorAtPercent[0F];
                colorAtPercent
                   .Where(cap => cap.Key <= percent)
                   .Last()
                   .With
                   (pair =>
                   {
                       percStart = pair.Key;
                       colorStart = pair.Value;
                   }
                   )
                   ;
                colorAtPercent
                   .Where(cap => cap.Key >= percent)
                   .First()
                   .With
                   (pair =>
                   {
                       percEnd = pair.Key;
                       colorEnd = pair.Value;
                   }
                   )
                   ;
                float percDiff = (percent - percStart) / (percEnd - percStart);
                int a = (int)(colorStart.A + percDiff * (colorEnd.A - colorStart.A));
                int r = (int)(colorStart.R + percDiff * (colorEnd.R - colorStart.R));
                int g = (int)(colorStart.G + percDiff * (colorEnd.G - colorStart.G));
                int b = (int)(colorStart.B + percDiff * (colorEnd.B - colorStart.B));

				color = Color.FromArgb(a, r, g, b);
            }
            return color;
        }

    }
}