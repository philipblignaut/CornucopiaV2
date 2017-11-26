using System;

namespace CornucopiaV2
{
	public class TypedActionArgumentPair<T>
   {
      public Action<T> Action { get; private set; }
      public T ActionArgument { get; private set; }
      public TypedActionArgumentPair
         (Action<T> predicate
         , T actionArgument
         )
      {
         Action = predicate;
         ActionArgument = actionArgument;
      }

	  public TypedActionArgumentPair()
	  {
		  // TODO: Complete member initialization
	  }
   }
}
