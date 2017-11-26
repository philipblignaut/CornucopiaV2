using System;
using System.Collections.Generic;
using System.Reflection;

namespace CornucopiaV2
{
	public static class ObjectPrinter
   {
      public static string PrintObject
        (this object theObject
        )
      {
         return PrintObject(theObject, 0, null);
      }
      private static string PrintObject
         (object theObject
         , int level
         , int? index
         )
      {
         string data = "";
         string output = "";
         if (theObject != null)
         {
            Type type = theObject.GetType();
            output =
               PrintObjectPrefix(level)
               + "Dumping object of type "
               + type.Name
               +
                  (index == null
                  ? string.Empty
                  : "[" + index.ToString() + "]"
                  )
               ;
            data += output + Environment.NewLine;
            if (type.IsPrimitive || type.Name == typeof(string).Name)
            {
               data += PrintObjectPrefix(level + 1) + theObject.ToString() + Environment.NewLine;
            }
            else
            {
               PropertyInfo[] infoA = type.GetProperties();
               foreach (PropertyInfo info in infoA)
               {
                  if (info.CanRead)
                  {
                     object value = info.GetValue(theObject, null);
                     output = PrintObjectPrefix(level + 1) + info.Name + " = " + (value == null ? "null" : value.ToString());
                     data += output + Environment.NewLine;
                  }
               }
               if (type.ToString().EndsWith("[]")) // an array
               {
                  int ix = 0;
                  foreach (object childObject in (IEnumerable<object>)theObject)
                  {
                     data += PrintObject(childObject, level + 1, ix);
                     ix++;
                  }
               }
            }
         }
         else
         {
            output = PrintObjectPrefix(level) + "Null Object *****";
            data += output + Environment.NewLine;
         }
         return data;
      }
      private static string PrintObjectPrefix(int level)
      {
         string prefix = "";
         for (int i = 0; i < level * 2; i++)
         {
            prefix += " ";
         }
         return prefix;
      }

   }
}
