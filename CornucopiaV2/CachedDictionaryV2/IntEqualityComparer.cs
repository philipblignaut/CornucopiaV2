using System.Collections.Generic;

namespace CornucopiaV2
{
   public class IntEqualityComparer
    : IEqualityComparer<int>
   {
      public bool Equals(int x, int y)
      {
         return x.Equals(y);
      }
      public int GetHashCode(int obj)
      {
         return obj.GetHashCode();
      }
   }
   public class LongEqualityComparer
     : IEqualityComparer<long>
   {
      public bool Equals(long x, long y)
      {
         return x.Equals(y);
      }
      public int GetHashCode(long obj)
      {
         return obj.GetHashCode();
      }
   }
   public class UIntEqualityComparer
     : IEqualityComparer<uint>
   {
      public bool Equals(uint x, uint y)
      {
         return x.Equals(y);
      }
      public int GetHashCode(uint obj)
      {
         return obj.GetHashCode();
      }
   }
}
