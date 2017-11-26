using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CornucopiaV2
{
	public class CachedDictionary<TKey, TValue>
      : ACacheDictionaryV2
   {
      public delegate TValue NewValueFromKey
        (TKey key
        )
        ;
      public delegate void EntryChanged
        (TKey key
        , TValue value
        )
        ;
      public delegate void ForEachKey
        (TKey key
        )
        ;
      public delegate void ForEachKeyValuePair
        (TKey key
        , TValue value
        )
        ;
      public delegate TKey CloneKeyMethod
        (TKey value
        )
        ;
      public delegate TValue CloneValueMethod
        (TValue value
        )
        ;
      #region private stuff
      private class Entry
      {
         public DateTime Created { get; private set; }
         public TValue Value { get; private set; }
         public Entry
           (TValue value
           )
         {
            Created = DateTime.Now;
            Value = value;
         }
      }
      private Dictionary<TKey, Entry> dictionary = null;
      private object lockObject = new object();
      private long expiryThreadId = 0;
      private bool expiryThreadRunning = false;
      private Thread expiryThread = null;
      private NewValueFromKey newValueMethod = null;
      private CloneKeyMethod cloneKeyMethod = null;
      private CloneValueMethod cloneValueMethod = null;
      #endregion
      #region properties
      public override uint ExpirySeconds { get; set; }
      public uint MaxEntries { get; private set; }
      public long HitCount { get; private set; }
      public long MissCount { get; private set; }
      public long NewCount { get; private set; }
      public long RemovedCount { get; private set; }
      public long ExpiredCount { get; set; }
      public bool KeyIsCloneable { get; private set; }
      public bool KeyIsCloneableExternalMethod { get; private set; }
      public bool ValueIsCloneable { get; private set; }
      public bool ValueIsCloneableExternalMethod { get; private set; }
      public override int Count { get { return dictionary.Count; } }
      public event Action Cleared;
      public string Stats
      {
         get
         {
            return
              "CachedDictionary<"
              + typeof(TKey).ToString() + ","
              + typeof(TValue).ToString() + "> "
              + "S:" + ExpirySeconds.ToString() + " "
              + "X:" + MaxEntries.ToString() + " "
              + "C:" + Count.ToString() + " "
              + "H:" + HitCount.ToString() + " "
              + "M:" + MissCount.ToString() + " "
              + "N:" + NewCount.ToString() + " "
              + "R:" + RemovedCount.ToString() + " "
              + "E:" + ExpiredCount.ToString() + " "
              + "K:" + (KeyIsCloneable ? "Y" : "N") + " "
              + "V:" + (ValueIsCloneable ? "Y" : "N")
              ;
         }
      }
      #endregion
      #region events
      public event EntryChanged EntryCreated = null;
      public event EntryChanged EntryRemoved = null;
      public event EntryChanged EntryExpired = null;
      #endregion
      #region constructors
      public CachedDictionary
        (IEqualityComparer<TKey> equalityComparer
        , NewValueFromKey newValueMethod
        , CloneKeyMethod cloneKeyMethod
        , CloneValueMethod cloneValueMethod
        , uint expirySeconds
        , uint maxEntries
        )
      {
         dictionary =
           new Dictionary<TKey, CachedDictionary<TKey, TValue>.Entry>
           (equalityComparer
           )
           ;
         this.newValueMethod = newValueMethod;
         KeyIsCloneable = false;
         KeyIsCloneableExternalMethod = false;
         if (typeof(TKey).GetInterfaces().Contains(typeof(ICloneable<TKey>)))
         {
            KeyIsCloneable = true;
         }
         else
         {
            this.cloneKeyMethod = cloneKeyMethod;
         }
         ValueIsCloneable = false;
         ValueIsCloneableExternalMethod = false;
         if (typeof(TValue).GetInterfaces().Contains(typeof(ICloneable<TValue>)))
         {
            ValueIsCloneable = true;
         }
         else
         {
            this.cloneValueMethod = cloneValueMethod;
         }
         ExpirySeconds = expirySeconds;
         MaxEntries = maxEntries;
         HitCount = 0;
         MissCount = 0;
         NewCount = 0;
         RemovedCount = 0;
         ExpiredCount = 0;
         KeyIsCloneableExternalMethod = this.cloneKeyMethod != null;
         ValueIsCloneableExternalMethod = this.cloneValueMethod != null;
      }
      public CachedDictionary
        (IEqualityComparer<TKey> equalityComparer
        , NewValueFromKey newValueMethod
        , uint expirySeconds
        , uint maxEntries
        )
         : this
         (equalityComparer
         , newValueMethod
         , null
         , null
         , expirySeconds
         , maxEntries
         )
      {
      }
      /// <summary>
      /// Creates a new Thread safe instance of
      /// CachedDictionaryV2.CachedDictionary&lt;TKey, TValue&gt;.
      /// Both the TKey and TValue classes may Implement
      /// CachedDictionaryV2.ICloneable&lt;T&gt;.
      /// </summary>
      /// <param name="equalityComparer">Any object that implements
      /// IEqualityComparer&lt;TKey&gt;.</param>
      /// <param name="newValueMethod">Any method or lambda expression
      /// that conforms to
      /// NewValueFromKey&lt;TKey, TValue&gt;.</param>
      /// <param name="expirySeconds">Number of seconds before a
      /// newly created Dictionary
      /// entries expire.</param>
      public CachedDictionary
        (IEqualityComparer<TKey> equalityComparer
        , NewValueFromKey newValueMethod
        , uint expirySeconds
        )
         : this
         (equalityComparer
         , newValueMethod
         , expirySeconds
         , 0
         )
      {
      }
      /// <summary>
      /// Creates a new Thread safe instance of
      /// CachedDictionaryV2.CachedDictionary&lt;TKey, TValue&gt;.
      /// Both the TKey and TValue classes may Implement
      /// CachedDictionaryV2.ICloneable&lt;T&gt;.
      /// Newly created entries will expire after 60 seconds.
      /// </summary>
      /// <param name="equalityComparer">Any object that implements
      /// IEqualityComparer&lt;TKey&gt;.</param>
      /// <param name="newValueMethod">Any method or lambda expression
      /// that conforms to
      /// NewValueFromKey&lt;TKey, TValue&gt;.</param>
      public CachedDictionary
        (IEqualityComparer<TKey> equalityComparer
        , NewValueFromKey newValueMethod
        )
         : this
         (equalityComparer
         , newValueMethod
         , 60
         )
      {
      }
      #endregion
      /// <summary>
      /// Returns a cloned TValue value corresponding to TKey key.
      /// If the entry does not exist, the newValueMethod
      /// method will be called to supply a value.
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      public TValue this[TKey key]
      {
         get
         {
            bool entryCreated = false;
            TValue value;
            lock (lockObject)
            {
               if (dictionary.ContainsKey(key))
               {
                  HitCount++;
               }
               else
               {
                  MissCount++;
                  NewCount++;
                  value = newValueMethod.Invoke(key);
                  dictionary.Add
                    (CloneKey(key)
                    , new Entry
                      (CloneValue(value)
                      )
                    )
                    ;
                  entryCreated = true;
                  CheckThread();
               }
               key = CloneKey(key);
               value = dictionary[key].Value;
               value = CloneValue(value);
            }
            if (entryCreated)
            {
               if (EntryCreated != null)
               {
                  EntryCreated(key, value);
               }
            }
            return value;
         }
      }
      public bool Remove
        (TKey key
        )
      {
         bool removed = false;
         TValue value = default(TValue);
         lock (lockObject)
         {
            if (dictionary.ContainsKey(key))
            {
               key = CloneKey(key);
               value = dictionary[key].Value;
               value = CloneValue(value);
               removed = dictionary.Remove(key);
               RemovedCount++;
            }
         }
         if (removed)
         {
            if (EntryRemoved != null)
            {
               EntryRemoved(key, value);
            }
         }
         return removed;
      }
      public override int Clear()
      {
         List<KeyValuePair<TKey, TValue>> pairs =
           new List<KeyValuePair<TKey, TValue>>()
           ;
         lock (lockObject)
         {
            (from pair in dictionary
             orderby pair.Value.Created ascending
             select pair
            )
            .Each
              (pair =>
              {
                 TValue value = pair.Value.Value;
                 pairs.Add
                   (new KeyValuePair<TKey, TValue>
                     (CloneKey(pair.Key)
                     , CloneValue(value)
                     )
                   )
                   ;
              }
              )
              ;
            dictionary.Clear();
            RemovedCount += pairs.Count;
            CheckThread();
         }
         if (EntryRemoved != null)
         {
            pairs.Each
              (pair =>
              {
                 EntryRemoved(pair.Key, pair.Value);
              }
              )
              ;
         }
         if (Cleared != null)
         {
            Cleared.Invoke();
         }
         return 0;
      }
      #region each methods
      public void EachKey
        (ForEachKey method
        )
      {
         List<TKey> keys = GetClonedKeyList();
         keys.Each
           (key =>
           {
              method(key);
           }
           )
           ;
      }
      public List<TKey> Keys
      {
         get
         {
            return GetClonedKeyList();
         }
      }
      private List<TKey> GetClonedKeyList()
      {
         List<TKey> keys = new List<TKey>();
         lock (lockObject)
         {
            (from pair in dictionary
             orderby pair.Value.Created ascending
             select pair.Key
            )
            .Each
              (key =>
              {
                 keys.Add(CloneKey(key));
              }
              )
              ;
         }
         return keys;
      }
      public void EachKeyValuePair
        (ForEachKeyValuePair method
        )
      {
         List<KeyValuePair<TKey, TValue>> pairs =
           new List<KeyValuePair<TKey, TValue>>()
           ;
         lock (lockObject)
         {
            (from pair in dictionary
             orderby pair.Value.Created ascending
             select pair
            )
            .Each
              (pair =>
              {
                 TValue value = pair.Value.Value;
                 pairs.Add
                   (new KeyValuePair<TKey, TValue>
                     (CloneKey(pair.Key)
                     , CloneValue(value)
                     )
                   )
                   ;
              }
              )
              ;
         }
         pairs.Each
           (pair =>
           {
              method(pair.Key, pair.Value);
           }
           )
           ;
      }
      #endregion
      private TKey CloneKey
        (TKey key
        )
      {
         return
           KeyIsCloneableExternalMethod
           ? cloneKeyMethod(key)
           : KeyIsCloneable
             ? ((ICloneable<TKey>)key).Clone()
             : key
             ;

      }
      private TValue CloneValue
        (TValue value
        )
      {
         return
           value == null
           ? value
           : ValueIsCloneableExternalMethod
             ? cloneValueMethod(value)
             : ValueIsCloneable
               ? ((ICloneable<TValue>)value).Clone()
               : value
               ;

      }
      private void CheckThread()
      {
         if (expiryThreadRunning && dictionary.Count == 0)
         {
            if (expiryThread != null)
            {
               try
               {
                  if (expiryThread.IsAlive)
                  {
                     //Debug.Print("Killing " + expiryThread.Name);
                     expiryThread.Abort();
                     expiryThread.Join();
                  }
               }
               catch
               {
               }
               expiryThreadRunning = false;
            }
         }
         if ((!expiryThreadRunning) && dictionary.Count > 0)
         {
            expiryThreadRunning = true;
            expiryThread = new Thread(ExpiryThread);
            expiryThread.Name =
              "ExpiryThread "
              + (expiryThreadId++).ToString()
              + " "
              + typeof(TKey).ToString()
              + " "
              + typeof(TValue).ToString()
              ;
            // Debug.Print("Starting " + expiryThread.Name);
            string stackTrace = string.Empty;
            expiryThread.Start();
         }
      }
      #region expiry thread
      private void ExpiryThread()
      {
         int count = 0;
         lock (lockObject)
         {
            count = dictionary.Count;
         }
         while (count > 0)
         {
            try
            {
               List<KeyValuePair<TKey, TValue>> pairs =
                 new List<KeyValuePair<TKey, TValue>>()
                 ;
               lock (lockObject)
               {
                  (from pair in dictionary
                   orderby pair.Value.Created ascending
                   select pair
                  )
                  .Each
                  (pair =>
                  {
                     if
                     (DateTime.Now.Subtract(pair.Value.Created).TotalSeconds
                     >= ExpirySeconds
                     )
                     {
                        TValue value = pair.Value.Value;
                        pairs.Add
                        (new KeyValuePair<TKey, TValue>
                        (CloneKey(pair.Key)
                        , CloneValue(value)
                        )
                        )
                        ;
                     }
                  }
                  )
                  ;
                  ExpiredCount += pairs.Count;
                  ExpireEntryList(pairs);
                  if (MaxEntries > 0)
                  {
                     while (dictionary.Count > MaxEntries)
                     {
                        KeyValuePair<TKey, CachedDictionary<TKey, TValue>.Entry> pair =
                           dictionary
                              .OrderBy(entry => entry.Value.Created)
                              .First()
                              ;
                        TValue value = pair.Value.Value;
                        KeyValuePair<TKey, TValue> rPair =
                        new KeyValuePair<TKey, TValue>
                        (CloneKey(pair.Key)
                        , CloneValue(value)
                        )
                        ;
                        ExpiredCount++;
                        ExpireEntry(rPair);
                     }
                  }
                  count = dictionary.Count;
               }
            }
            catch
            {
            }
            if (count > 0)
            {
               Thread.Sleep(1000);
            }
         }
      }
      private void ExpireEntryList(List<KeyValuePair<TKey, TValue>> pairs)
      {
         pairs.Each
         (pair =>
         {
            ExpireEntry(pair);
         }
         )
         ;
      }
      private void ExpireEntry(KeyValuePair<TKey, TValue> pair)
      {
         dictionary.Remove(pair.Key);
         if (EntryExpired != null)
         {
            EntryExpired
            (pair.Key
            , pair.Value
            )
            ;
         }
      }
      #endregion
      public override long CacheHitCount
      {
         get { return HitCount; }
      }
      public override long CacheMissCount
      {
         get { return MissCount; }
      }
      public override long CacheNewEntryCount
      {
         get { return NewCount; }
      }
      public override long CacheRemovedEntryCount
      {
         get { return RemovedCount; }
      }
      public override long CacheExpiredEntryCount
      {
         get { return ExpiredCount; }
      }
      public override string DebugText
      {
         get { return string.Empty; }
      }
      public override string CollectionName
      {
         get;
         set;
      }
   }
}
