using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CornucopiaV2
{
	public class SimpleImage
      : IDisposable
   {
      public int Width { get; private set; }
      public int Height { get; private set; }
      private Bitmap bitmap;
      private Graphics graphics;
      public SimpleImage
         (int width
         , int height
         , Color backgroundColor
         )
      {
         Width = width;
         Height = height;
         bitmap = new Bitmap(Width, Height);
         graphics = Graphics.FromImage(bitmap);
         //graphics.SmoothingMode = SmoothingMode.None;
         //graphics.InterpolationMode = InterpolationMode.High;
         graphics
            .FillRectangle
            (new SolidBrush(backgroundColor)
            , new Rectangle(0, 0, Width, Height)
            )
            ;
      }
      public SimpleImage
         (int width
         , int height
         )
         : this
         (width
         , height
         , Color.White
         )
      {
      }
      private object setPixelLock = new object();
      public void SetPixel
         (int x
         , int y
         , Color color
         )
      {
         lock (setPixelLock)
         {
            bitmap.SetPixel(x, y, color);
         }
      }
      public byte[] GetImageData
         (ImageFormat imageFormat
         )
      {
         using (MemoryStream stream = new MemoryStream())
         {
            ImageCodecInfo jpgEncoder = GetEncoder(imageFormat);
            Encoder encoder = Encoder.Quality;
            EncoderParameters eps = new EncoderParameters(1);
            eps.Param[0] = new EncoderParameter(encoder, 100L);
            bitmap.Save(stream, jpgEncoder, eps);
            stream.Flush();
            byte[] bytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
         }
      }
      private ImageCodecInfo GetEncoder(ImageFormat format)
      {
         ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
         foreach (ImageCodecInfo codec in codecs)
         {
            if (codec.FormatID == format.Guid)
            {
               return codec;
            }
         }
         return null;
      }
      public void Dispose()
      {
         graphics.Dispose();
         bitmap.Dispose();
      }
   }
}
