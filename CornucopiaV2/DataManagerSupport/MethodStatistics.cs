
namespace CornucopiaV2
{
   public class MethodStatistics
   {
      public string MethodName { get; private set; }
      public int CallCount { get; private set; }
      public double MinMilliSeconds { get; private set; }
      public double AvgMilliSeconds { get; private set; }
      public double MaxMilliSeconds { get; private set; }
      public MethodStatistics
         (string methodName
         )
      {
         MethodName = methodName;
         CallCount = 0;
         MinMilliSeconds = 0.0;
         AvgMilliSeconds = 0.0;
         MaxMilliSeconds = 0.0;
      }
      public void AddCallStatistics
         (double milliSeconds
         )
      {
         if (CallCount == 0)
         {
            MinMilliSeconds = milliSeconds;
            AvgMilliSeconds = milliSeconds;
            MaxMilliSeconds = milliSeconds;
         }
         else
         {
            if (MinMilliSeconds > milliSeconds)
            {
               MinMilliSeconds = milliSeconds;
            }
            AvgMilliSeconds =
               ((AvgMilliSeconds * CallCount) + milliSeconds)
               / (CallCount + 1)
               ;
            if (MaxMilliSeconds < milliSeconds)
            {
               MaxMilliSeconds = milliSeconds;
            }
         }
         CallCount++;
      }
      public override string ToString()
      {
         return
            string
            .Format
            ("{0,30} {1,8} {2,10:0.00} {3,10:0.00} {4,10:0.00}"
            , MethodName
            , CallCount
            , MinMilliSeconds
            , AvgMilliSeconds
            , MaxMilliSeconds
            )
            ;

      }
   }
}
