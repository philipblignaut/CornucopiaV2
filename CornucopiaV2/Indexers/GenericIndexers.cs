using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornucopiaV2
{
   public class Indexer<TKey, TResult>
   {
      private Func<TKey, TResult> func = null;
      public Indexer
         (Func<TKey, TResult> func
         )
      {
         this.func = func;
      }
      public TResult this[TKey key]
      {
         get { return func.Invoke(key); } 
      }
   }
   public class Indexer<TKey1, TKey2, TResult>
   {
      private Func<TKey1, TKey2, TResult> func = null;
      public Indexer
         (Func<TKey1, TKey2, TResult> func
         )
      {
         this.func = func;
      }
      public TResult this[TKey1 key1, TKey2 key2]
      {
         get { return func.Invoke(key1, key2); }
      }
   }
}
