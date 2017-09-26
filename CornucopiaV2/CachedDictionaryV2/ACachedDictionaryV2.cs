using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornucopiaV2
{
   public abstract class ACacheDictionaryV2
   {
      public abstract long CacheHitCount { get; }
      public abstract long CacheMissCount { get; }
      public abstract long CacheNewEntryCount { get; }
      public abstract long CacheRemovedEntryCount { get; }
      public abstract long CacheExpiredEntryCount { get; }
      public abstract string DebugText { get; }
      public abstract int Count { get; }
      public abstract uint ExpirySeconds { get; set; }
      public abstract int Clear();
      public abstract string CollectionName { get; set; }
      public string ShortStats
      {
         get
         {
            return
               "C" + Count.ToString() + " "
               + "H" + CacheHitCount.ToString() + " "
               + "M" + CacheMissCount.ToString() + " "
               + "R" + CacheRemovedEntryCount.ToString() + " "
               + "X" + CacheExpiredEntryCount.ToString()
               ;

         }
      }
   }
}
