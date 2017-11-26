using System;
using System.IO;
using System.Drawing;

namespace CornucopiaV2
{
	public static class ImageResizer
   {
      /// <summary>
      /// Resizes Images to any other size keeping aspect ratio
      /// </summary>
      /// <param name="originalImageData">An array of bytes containing the binary content of the image (any format)</param>
      /// <param name="maxResizedImageWidth">The maximum width of the new Image</param>
      /// <param name="maxResizedImageHeight">The maximum height of the new Image</param>
      /// <returns>An array of bytes containing the new image in JPEG format</returns>
      public static byte[] ResizeImage
          (byte[] originalImageData
          , int maxResizedImageWidth
          , int maxResizedImageHeight
          )
      {
         // create an image object, using the filename we just retrieved
         MemoryStream originalImageStream = new MemoryStream();
         originalImageStream.Write(originalImageData, 0, originalImageData.Length);
         originalImageStream.Position = 0;
         Image originalImage = Image.FromStream(originalImageStream, true, true);
         int originalImageWidth = originalImage.Size.Width;
         int originalImageHeight = originalImage.Size.Height;
         float thumbnailWidth = maxResizedImageWidth;
         float thumbnailHeight = maxResizedImageHeight;
         float originalImageRatio = (float)originalImageWidth / (float)originalImageHeight;
         float thumbnailRatio = thumbnailWidth / thumbnailHeight;
         if (originalImageRatio > thumbnailRatio)
         {
            thumbnailHeight = thumbnailWidth / originalImageRatio;
         }
         else
         {
            thumbnailWidth = thumbnailHeight * originalImageRatio;
         }
         Bitmap newImageBitmap = new Bitmap((int)thumbnailWidth, (int)thumbnailHeight, originalImage.PixelFormat);
         Graphics newImageGraphics = Graphics.FromImage(newImageBitmap);
         newImageGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
         newImageGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
         newImageGraphics.DrawImage(originalImage, 0, 0, newImageBitmap.Width, newImageBitmap.Height);

         // make a memory stream to work with the image bytes
         MemoryStream newImageStream = new MemoryStream();

         // put the image into the memory stream
         //thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
         newImageBitmap.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

         // make byte array the same size as the image
         byte[] newImageContent = new Byte[newImageStream.Length];

         // rewind the memory stream
         newImageStream.Position = 0;

         // load the byte array with the image
         newImageStream.Read(newImageContent, 0, (int)newImageStream.Length);
         originalImage.Dispose();
         newImageBitmap.Dispose();
         newImageGraphics.Dispose();
         return newImageContent;
      }
   }
}
