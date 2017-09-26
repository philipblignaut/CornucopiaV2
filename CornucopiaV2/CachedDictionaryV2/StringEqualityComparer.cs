using System.Collections.Generic;

namespace CornucopiaV2
{
   public class StringEqualityComparer
    : IEqualityComparer<string>
   {
      public bool Equals(string x, string y)
      {
         return x == y;
      }
      public int GetHashCode(string obj)
      {
         return obj.GetHashCode();
      }
   }
}
