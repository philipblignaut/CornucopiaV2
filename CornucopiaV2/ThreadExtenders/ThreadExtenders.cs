using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CornucopiaV2
{
	public static class ThreadExtenders
	{
		public static IEnumerable<Thread> ExecuteInThreads
		   (this IEnumerable<Action> predicateCollection
		   )
		{
			List<Thread> threads = new List<Thread>();
			predicateCollection
			   .Each
			   (predicate =>
			   {
				   Thread worker = new Thread(() => predicate.Invoke());
				   worker.Start();
				   threads.Add(worker);
			   }
			   )
			   ;
			return threads;
		}
        public static IEnumerable<Thread> ExecuteInThreads<T>
            (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
            , int maxThreads
            , Action<int, int> progressPredicate
            , int progressReportInterval
            , int threadStartWaitTimeMilliSeconds
            )
        {
            object lockObject = new object();
            int completionCount = 0;
            List<TypedActionArgumentPair<T>> predicates = predicateCollection.ToList();
            int predicateCount = predicateCollection.Count();
            if (progressPredicate != null)
            {
                progressPredicate
                   .Invoke
                   (completionCount
                   , predicateCount
                   )
                   ;
            }
            List<Thread> threads = new List<Thread>();
            for (int threadIndex = 0; threadIndex < maxThreads; threadIndex++)
            {
                Thread worker =
                   new Thread
                   (() =>
                   {
                       while (true)
                       {
                           TypedActionArgumentPair<T> actionArgument = null;
                           lock (lockObject)
                           {
                               if (predicates.Count > 0)
                               {
                                   actionArgument =
                                      predicates[0]
                                      ;
                                   predicates.RemoveAt(0);
                               }
                           }
                           if (actionArgument != null)
                           {
                               actionArgument
                                  .Action
                                  .Invoke
                                  (actionArgument.ActionArgument
                                  )
                                  ;
                               lock (lockObject)
                               {
                                   completionCount++;
                                   if (completionCount % progressReportInterval == 0)
                                   {
                                       if (progressPredicate != null)
                                       {
                                           progressPredicate
                                              .Invoke
                                              (completionCount
                                              , predicateCount
                                              )
                                              ;
                                       }
                                   }
                               }
                               if (threadStartWaitTimeMilliSeconds > 0)
                               {
                                   Thread.Sleep(threadStartWaitTimeMilliSeconds);
                               }
                           }
                           else
                           {
                               break;
                           }
                       }
                   }
                   )
                   ;
                worker.Start();
                threads.Add(worker);
            }
            return threads;
        }
        public static IEnumerable<Thread> ExecuteInThreads<T>
            (this IEnumerable<TypedFuncArgumentPair<T>> predicateCollection
            , int maxThreads
            , Action<int, int> progressPredicate
            , int progressReportInterval
            , int threadStartWaitTimeMilliSeconds
            )
        {
            object lockObject = new object();
            int completionCount = 0;
            List<TypedFuncArgumentPair<T>> predicates = predicateCollection.ToList();
            int predicateCount = predicateCollection.Count();
            if (progressPredicate != null)
            {
                progressPredicate
                   .Invoke
                   (completionCount
                   , predicateCount
                   )
                   ;
            }
            List<Thread> threads = new List<Thread>();
            for (int threadIndex = 0; threadIndex < maxThreads; threadIndex++)
            {
                Thread worker =
                   new Thread
                   (() =>
                   {
                       while (true)
                       {
                           TypedFuncArgumentPair<T> actionArgument = null;
                           lock (lockObject)
                           {
                               if (predicates.Count > 0)
                               {
                                   actionArgument =
                                      predicates[0]
                                      ;
                                   predicates.RemoveAt(0);
                               }
                           }
                           if (actionArgument != null)
                           {
                               actionArgument
                                  .Func
                                  .Invoke
                                  (actionArgument.FuncArgument
                                  )
                                  ;
                               lock (lockObject)
                               {
                                   completionCount++;
                                   if (completionCount % progressReportInterval == 0)
                                   {
                                       if (progressPredicate != null)
                                       {
                                           progressPredicate
                                              .Invoke
                                              (completionCount
                                              , predicateCount
                                              )
                                              ;
                                       }
                                   }
                               }
                               if (threadStartWaitTimeMilliSeconds > 0)
                               {
                                   Thread.Sleep(threadStartWaitTimeMilliSeconds);
                               }
                           }
                           else
                           {
                               break;
                           }
                       }
                   }
                   )
                   ;
                worker.Start();
                threads.Add(worker);
            }
            return threads;
        }
        public static IEnumerable<Thread> ExecuteInThreads<T>
            (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
            , int maxThreads
            , Action<int, int> progressPredicate
            , int progressReportInterval
           )
        {
            return
               predicateCollection
               .ExecuteInThreads
               (maxThreads
               , progressPredicate
               , progressReportInterval
               , 0
               )
               ;
        }
        public static IEnumerable<Thread> ExecuteInThreads<T>
            (this IEnumerable<TypedFuncArgumentPair<T>> predicateCollection
            , int maxThreads
            , Action<int, int> progressPredicate
            , int progressReportInterval
           )
        {
            return
               predicateCollection
               .ExecuteInThreads
               (maxThreads
               , progressPredicate
               , progressReportInterval
               , 0
               )
               ;
        }
        public static IEnumerable<Thread> ExecuteInThreads<T>
		   (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
		   , int maxThreads
		   )
		{
			return
			   predicateCollection
			   .ExecuteInThreads
			   (maxThreads
			   , null
			   , 1000000
			   )
			   ;
		}
		public static IEnumerable<Thread> ExecuteInThreads<T>
		   (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
		   , Action<int, int> progressPredicate
			, int progressReportInterval
		   )
		{
			return
			   predicateCollection
			   .ExecuteInThreads
			   (predicateCollection.Count()
			   , progressPredicate
			   , progressReportInterval
			   )
			   ;
		}
		public static IEnumerable<Thread> ExecuteInThreads<T>
		   (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
		   )
		{
			return
			   predicateCollection
			   .ExecuteInThreads
			   (predicateCollection.Count()
			   , null
			   , 1000000
			   )
			   ;
		}
		public static void WaitForThreads
		   (this IEnumerable<Thread> threads
		   )
		{
			threads.Each(worker => worker.Join());
		}
	}
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CornucopiaV2
{
    public static class ThreadExtenders
    {
        public static IEnumerable<Thread> ExecuteInThreads
           (this IEnumerable<Action> predicateCollection
           )
        {
            List<Thread> threads = new List<Thread>();
            predicateCollection
               .Each
               (predicate =>
               {
                   Thread worker = new Thread(() => predicate.Invoke());
                   worker.Start();
                   threads.Add(worker);
               }
               )
               ;
            return threads;
        }

        public static IEnumerable<Thread> ExecuteInThreads<T>
          (this IEnumerable<TypedActionArgumentPair<T>> predicateCollection
          )
        {
            int processorCount = Environment.ProcessorCount;
            int threadsActive = 0;
            List<Thread> threads = new List<Thread>();
            predicateCollection
               .Each
               (predicateArgumentPair =>
               {
                   Thread worker =
                      new Thread
                         (() =>
                            predicateArgumentPair
                               .Action
                               .Invoke
                               (predicateArgumentPair.ActionArgument
                               )
                         )
                         ;
                   worker.Start();
                   threadsActive++;
                   threads.Add(worker);
               }
               )
               ;
            return threads;
        }
        public static void WaitForThreads
           (this IEnumerable<Thread> threads
           )
        {
            threads.Each(worker => worker.Join());
        }
    }
}
*/