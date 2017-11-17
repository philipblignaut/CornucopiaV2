using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace CornucopiaV2
{
	public class CorImage
		: IDisposable
	{
#pragma warning disable RCNoAssignment // No assignment to a get-only auto-property.
		public int Width { get => Bitmap.Width; }
		public int Height { get => Bitmap.Height; }
#pragma warning restore RCNoAssignment // No assignment to a get-only auto-property.
		public Bitmap Bitmap { get; private set; }
		public Graphics Graphics { get; private set; }
		public bool Disposed { get; private set; }
		// max w/h sqrt(2GB/4)
		public CorImage(int imageWidth, int imageHeight, Color initialColor)
		{
			Bitmap = new Bitmap(imageWidth, imageHeight);
			Graphics = Graphics.FromImage(Bitmap);
			Graphics.Clear(initialColor);
			Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Graphics.ResetTransform();
		}

		public CorImage
			(int imageWidth
			, int imageHeight
			)
			:this
				 (imageWidth
				 , imageHeight
				 , Color.White
				 )
		{
		}

		public void FillRectangleSolid
			(Color color
			, float x
			, float y
			, float width
			, float height
			)
		{
			Graphics.FillRectangle(new SolidBrush(color), x, y, width, height);
		}

		public void DrawLine
			(Color color
			, float linewidth
			, float xfrom
			, float yfrom
			, float xto
			, float yto
			)
		{
			DrawLine
				(color
				, linewidth
				, new PointF(xfrom, yfrom)
				, new PointF(xto, yto)
				)
				;
		}

		public void DrawLine
			(Color color
			, float linewidth
			, double xfrom
			, double yfrom
			, double xto
			, double yto
			)

		{
			DrawLine
				(color
				, linewidth
				, (float)xfrom
				, (float)yfrom
				, (float)xto
				, (float)yto
				)
				;
		}

		public void DrawLine
			(Color color
			, float linewidth
			, PointF from
			, PointF to
			)
		{
			Pen pen = new Pen(color, linewidth);
			Graphics.DrawLine(pen, from, to);
		}

		public void DrawLines
			(Color color
			, float linewidth
			, IEnumerable< PointF> points
			)
		{
			PointF[] pArray = points.ToArray();
			if (pArray.Length>1)
			{
				Pen pen = new Pen(color, linewidth);
				for (int i = 1; i < pArray.Length; i++)
				{
					Graphics.DrawLine(pen, pArray[i-1], pArray[i]);
				}
			}
		}

		public void DrawString
			(string text
			, string fontFamilyName
			, float fontSize
			, Color color
			, PointF point
			, PointF min
			, PointF max
			)
		{
			Font font = new Font(new FontFamily(fontFamilyName), fontSize);
			point.X = point.X < min.X ? min.X : point.X;
			point.Y = point.Y < min.Y ? min.Y : point.Y;
			SizeF size = Graphics.MeasureString(text, font);
			point.X = point.X > max.X - size.Width ? max.X - size.Width : point.X;
			point.Y = point.Y > max.Y - size.Height ? max.Y - size.Height : point.Y;
			Graphics
				.DrawString
				(text
				, font
				, new SolidBrush(color)
				, point
				)
			;
		}

		public void DrawString
			(string text
			, string fontFamilyName
			, float fontSize
			, Color color
			, float x
			, float y
			, PointF min
			, PointF max
			) =>
			DrawString
				(text
				, fontFamilyName
				, fontSize
				, color
				, new PointF(x, y)
				, min
				, max
				)
			;

		public void DrawStringInBoxCentreMiddle
            (string text
            , string fontFamily
            , float initailFontSize
            , Color color
            , float left
            , float top
            , float width
            , float height
            )
        {
            Font font = new Font(new FontFamily(fontFamily), initailFontSize);
            SizeF size = Graphics.MeasureString(text, font);
            while (size.Width > width || size.Height > height)
            {
                initailFontSize -= .1F;
                font = new Font(new FontFamily(fontFamily), initailFontSize);
                size = Graphics.MeasureString(text, font);
            }
            float twidth = size.Width;
            float theight = size.Height;
            Graphics
                .DrawString
                (text
                , font
                , new SolidBrush(color)
                , left + (width - twidth) / 2
                , top + (height - theight) / 2
                )
                ;
        }

        public SizeF MeasureString
            (string text
            , string fontFamily
			,float fontSize
            )
        {
			Font font = new Font
				(new FontFamily( fontFamily)
				, fontSize
				)
				;
            return
                Graphics
                .MeasureString
                (text
                , font
                )
                ;
        }

		public void DrawCircle
			(PointF centre
			, float radius
			, float lineWidth
			, Color color
			)
		{
			Graphics
				.DrawEllipse
				(new Pen(color, lineWidth)
				, new RectangleF
					(new PointF
						(centre.X - radius
						, centre.Y - radius
						)
					, new SizeF
						(radius * 2
						, radius * 2
						)
					)
				)
				;
		}

        public void DrawArc
            (Color color
            , float arclinewidth
            , float x
            , float y
            , float width
            , float height
            , float startangle
            , float sweepangle
            )
        {
			width = width < 1 ? 1 : width;
			height = height < 1 ? 1 : height;
			arclinewidth = arclinewidth < 1 ? 1 : arclinewidth;
            Graphics
                .DrawArc
                (new
                    Pen
                    (color
                    , arclinewidth
                    )
                , x
                , y
                , width
                , height
                , startangle
                , sweepangle
                )
                ;
        }

        public void FillEllipse
            (Color color
            , float x
            , float y
            , float width
            , float height
            )
        {
            Graphics
                .FillEllipse
                (new SolidBrush(color)
                , x
                , y
                , width
                , height
                )
                ;
        }
		public void SetPixel(int x, int y, Color color)
        {
            Bitmap.SetPixel(x, y, color);
        }

        public void Save(string path, ImageFormat imageFormat)
        {
            Bitmap.Save(path, imageFormat);
        }

		public void Dispose()
		{
			if (!Disposed)
			{
				Graphics.Dispose();
				Bitmap.Dispose();
				Disposed = true;
			}
		}

	}
}