using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace CornucopiaV2
{
   public static class ColorWheel
   {
      public static Color ColorAtAngleDepth
         (float angle
         , float depth
         )
      {
         int red = 0;
         int green = 0;
         int blue = 0;
         while (angle < 0F) angle += 360F;
         while (angle > 360F) angle -= 360F;
         if (depth > 255F) depth = 255F;
         if (depth < -255F) depth = -255F;
         if (angle <= 120F)
         {
            red = (int)(255F * (120F - angle) / 120F);
            green = (int)(255F * angle / 120F);
            blue = 0;
            SetColorDepth(ref red, ref green, ref blue, depth);
         }
         else if (angle <= 240F)
         {
            angle -= 120F;
            green = (int)(255F * (120F - angle) / 120F);
            blue = (int)(255F * angle / 120F);
            red = 0;
            SetColorDepth(ref green, ref blue, ref red, depth);
         }
         else
         {
            angle -= 240F;
            blue = (int)(255F * (120F - angle) / 120F);
            red = (int)(255F * angle / 120F);
            green = 0;
            SetColorDepth(ref blue, ref red, ref green, depth);
         }
         Debug.Print(red.ToString() + " " + green.ToString() + " " + blue.ToString());
         return Color.FromArgb(red, green, blue);
      }
      private static void SetColorDepth
         (ref int c1
         , ref int c2
         , ref int c0
         , float depth
         )
      {
         
         if (depth < 0F)
         {
            c1 = (int)(c1 * (depth + 255F) / 255F);
            c2 = (int)(c2 * (depth + 255F) / 255F);
         }
         else
         {
            c0 = (int)depth;
         }
      }
   }
}
