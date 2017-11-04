﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class ImageExt
	{/// <summary>
	 /// Sets the origin (0,0) at the centre of the image with both
	 /// positive x and positive y in the top right quadrant.
	 /// <para>Angles start on the x axis and rotate incrementally anti-clocwise.</para>
	 /// </summary>
	 /// <param name="image"></param>
		public static void NormalCartesian
			(this CorImage image
			)
		{
			image.Graphics.ScaleTransform(1.0F, -1.0F);
			image.Graphics.TranslateTransform(image.Width / 2, -image.Height / 2);
		}

		public static RectangleF CircleBounds
			(this PointF centre
			, float radius
			)
		{
			return 
				new RectangleF
					(new PointF
						(centre.X - radius / 2
						, centre.Y - radius / 2
						)
					, new SizeF(radius, radius)
					)
					;
		}
	}
}