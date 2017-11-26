using System;

namespace CornucopiaV2
{
	public class KVPAction
   {
      public string Key { get; private set; }
      public Action<string> Action { get; private set; }
      public KVPAction
         (string key
         , Action<string> action
         )
      {
         Key = key;
         Action = action;
      }
   }
}
