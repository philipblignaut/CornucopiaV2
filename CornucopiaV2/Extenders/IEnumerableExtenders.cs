using System;
using System.Collections.Generic;
using System.Linq;

namespace CornucopiaV2
{
	public static class Extender
	{

		/// <summary>
		/// Invokes predicate for each element in the collection<br />
		/// This 
		/// </summary>
		/// <typeparam name="T">Type of element in collection</typeparam>
		/// <param name="collection"></param>
		/// <param name="method"></param>
		public static void Each<T>
		  (this IEnumerable<T> collection
		  , Action<T> predicate
		  )
		{
			foreach (T element in collection)
			{
				predicate.Invoke(element);
			}
		}

		public static void EachIndexed<T>
		  (this IEnumerable<T> collection
		  , Action<T, int> method
		  )
		{
			int index = 0;
			foreach (T element in collection)
			{
				method.Invoke(element, index++);
			}
		}
		public static int FirstIndexOfWhere<T>
		   (this IEnumerable<T> collection
		   , Func<T, bool> function
		   )
		{
			int index = -1;
			collection
			   .EachIndexed
			   ((entry, entryIndex) =>
			   {
				   if (index == -1)
				   {
					   if (function(entry))
					   {
						   index = entryIndex;
					   }
				   }
			   }
			   )
			   ;
			return index;
		}
		public static IEnumerable<TResult> Convert<T, TResult>
		   (this IEnumerable<T> collection
		   , Func<T, TResult> func
		   )
		{
			foreach (T element in collection)
			{
				yield return func.Invoke(element);
			}
		}
		public static IEnumerable<TResult> ConvertIndexed<T, TResult>
		   (this IEnumerable<T> collection
		   , Func<T, int, TResult> func
		   )
		{
			int index = 0;
			foreach (T element in collection)
			{
				yield return func.Invoke(element, index++);
			}
		}
		public static IEnumerable<T> WhereIndexed<T>
		   (this IEnumerable<T> collection
		   , Func<T, int, bool> func
		   )
		{
			int index = 0;
			foreach (T element in collection)
			{
				if (func.Invoke(element, index++))
				{
					yield return element;
				}
			}
		}
		public static IEnumerable<T> Flatten<T>
		   (this IEnumerable<IEnumerable<T>> collections
		   )
		{
			foreach (IEnumerable<T> coll in collections)
			{
				foreach (T item in coll)
				{
					yield return item;
				}
			}
		}
		//public static void With<T>
		//  (this T element
		//  , Action<T> method
		//  )
		//{
		//	method.Invoke(element);
		//}
		//public static TResult WithFirstElementIfExists<T, TResult>
		//  (this IEnumerable<T> collection
		//  , Func<T, TResult> method
		//  , TResult defaultResult
		//  )
		//{
		//	if (collection.Count() > 0)
		//	{
		//		return method.Invoke(collection.First());
		//	}
		//	return defaultResult;
		//}
		//public static TResult WithFirstElementIfExists<T, TResult>
		//   (this IEnumerable<T> collection
		//   , Func<T, TResult> method
		//   , Func<TResult> methodIfNoElements
		//   )
		//{
		//	if (collection.Count() > 0)
		//	{
		//		return method.Invoke(collection.First());
		//	}
		//	return methodIfNoElements.Invoke();
		//}
		//public static void WithFirstElementIfExists<T>
		//   (this IEnumerable<T> collection
		//   , Action<T> method
		//   , Action methodIfNoElements
		//   )
		//{
		//	if (collection.Count() > 0)
		//	{
		//		method.Invoke(collection.First());
		//	}
		//	else
		//	{
		//		methodIfNoElements.Invoke();
		//	}
		//}
		//public static TResult WithLastElementIfExists<T, TResult>
		//  (this IEnumerable<T> collection
		//  , Func<T, TResult> method
		//  , TResult defaultResult
		//  )
		//{
		//	if (collection.Count() > 0)
		//	{
		//		return method.Invoke(collection.Last());
		//	}
		//	return defaultResult;
		//}

		/// <summary>
		/// Creates a character separated string by calling
		/// ToString() on each element in the collection
		/// and then using string.Join to concatenate
		/// the strings using the separator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="separator"></param>
		/// <returns></returns>
		public static string JoinToCharacterSeparatedValues<T>
		   (this IEnumerable<T> collection
		   , string separator
		   )
		{
			return
			   string.Join
				 (separator
				 , collection
					.Convert(entry => entry.ToString())
					.ToArray()
				 )
				 ;
		}

		public static string JoinToCharacterSeparatedValues<T>
		   (this List<string> collection
		   , string separator
		   )
		{
			return
			   string.Join
				 (separator
				 , collection
					.Convert(entry => entry.ToString())
					.ToArray()
				 )
				 ;
		}
		public static string JoinToCharacterSeparatedValues<T>
		   (this IEnumerable<T> collection
		   , string separator
		   , string prefix
		   , string suffix
		   )
		{
			return
			   prefix
			   + collection.JoinToCharacterSeparatedValues(separator)
			   + suffix
			   ;
		}
		//public static T Max<T>
		//      (this IEnumerable<T> collection
		//    , Func< T,T, T >compare
		//    )
		//  {
		//      T max = default(T);
		//      collection
		//          .Each
		//          (elm =>
		//              max = compare(max, elm)
		//          )
		//          ;
		//      return max;
		//  }

		public static List<T> AddRangeAndReturn<T>
		   (this List<T> list
		   , IEnumerable<T> collection
		   )
		{
			list.AddRange(collection);
			return list;
		}
		public static List<T> AddAndReturn<T>
		   (this List<T> list
		   , T entry
		   )
		{
			list.Add(entry);
			return list;
		}
	}
}