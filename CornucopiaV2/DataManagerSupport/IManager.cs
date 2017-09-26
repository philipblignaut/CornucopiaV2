using System.Collections.Generic;

namespace CornucopiaV2
{
   public abstract class ADataManager
   {
      public abstract string ClassName { get; }
      public abstract List<MethodStatistics> Statistics { get; }
   }
}
