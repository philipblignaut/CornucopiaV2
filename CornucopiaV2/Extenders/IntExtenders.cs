using System;
using System.Collections.Generic;

namespace CornucopiaV2
{
	public struct FloatSegment
   {
      public float Start { get; private set; }
      public float End { get; private set; }
      public float Percent { get; private set; }
      public FloatSegment
         (float start
         , float end
         , float percent
         )
         : this()
      {
         Start = start;
         End = end;
         Percent = percent;
      }
      public override string ToString()
      {
         return
            Start.ToString()
            + " "
            + End.ToString()
            + " "
            + Percent.ToString()
            ;
      }
   }
   public static class IntExtenders
   {
      public static IEnumerable<T> ForLoop<T>
         (this int maxValue
         , Func<int, T> func
         , int startValue
         )
      {
         for (int index = startValue; index < maxValue; index++)
         {
            yield return func.Invoke(index);
         }
      }
      public static void ForLoop
         (this int maxValue
         , Action<int> action
         , int startValue
         )
      {
         for (int index = startValue; index < maxValue; index++)
         {
            action.Invoke(index);
         }
      }
      public static void ForLoop
         (this int maxValue
         , Action<int> action
         )
      {
         maxValue.ForLoop(action, 0);
      }
      public static IEnumerable<int> ForLoopArray
         (this int value
         )
      {
         for (int index = 0; index < value; index++)
         {
            yield return index;
         }
      }
      public static void ForLoop
         (this long maxValue
         , Action<long> action
         , long startValue
         )
      {
         for (long index = startValue; index < maxValue; index++)
         {
            action.Invoke(index);
         }
      }
      public static void ForLoop
         (this long maxValue
         , Action<long> action
         )
      {
         maxValue.ForLoop(action, 0);
      }
      public static IEnumerable<long> ForLoopArray
         (this long value
         )
      {
         for (long index = 0; index < value; index++)
         {
            yield return index;
         }
      }
      public static IEnumerable<FloatSegment> Range
         (this int segments
         , float start
         , float end
         )
      {
         float segmentSize = (end - start) / (segments + 0F);
         for (int segment = 0; segment < segments; segment++)
         {
            yield return
               new FloatSegment
               (start + segmentSize * segment
               , start + segmentSize * (segment + 1)
               , (segment + 0F) / (segments - 1F) * 100F
               )
               ;
         }
      }
   }
}