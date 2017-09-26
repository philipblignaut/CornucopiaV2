using System;
using System.Collections.Generic;
using System.Linq;

namespace CornucopiaV2
{
   public class DMStatistics
   {
      private MethodStatistics[] methodStatisticsArray;
      public string ClassName { get; private set; }
      public List<MethodStatistics> Statistics
      {
         get
         {
            return
               methodStatisticsArray
               .ToList()
               ;
         }
      }
      public DMStatistics
         (string managerClassName
         , string[] methodNames
         )
      {
         ClassName = managerClassName;
         methodStatisticsArray =
            new MethodStatistics[methodNames.Length]
            ;
         methodNames
            .EachIndexed
            ((methodName, index) =>
               methodStatisticsArray[index] =
                  new MethodStatistics(methodName)
            )
            ;
      }
      public void AddCallStatistics
         (int methodIndex
         , DateTime startDateTime
         )
      {
         methodStatisticsArray[methodIndex]
            .AddCallStatistics
            (DateTime.Now.Subtract(startDateTime).TotalMilliseconds
            )
            ;
      }
   }
}
