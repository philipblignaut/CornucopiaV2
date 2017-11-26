using System.Linq;

namespace CornucopiaV2
{
	public static class Converters
   {
      public static T[] Convert<T>
         (params T[] args
         )
      {
         return args;
      }
      public static T[] Add<T>
         (this T[] array
         , T element
         )
      {
         return array.ToList().AddAndReturn(element).ToArray();
      }
   }
}
