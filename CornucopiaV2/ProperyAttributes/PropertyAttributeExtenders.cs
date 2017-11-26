using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CornucopiaV2
{
	public static class AttributeExtenders
   {
      public static List<TAttributeType>
         ClassAttributeList<T, TAttributeType>()
         where T : class
      {
         return
            typeof(T)
            .GetCustomAttributes(true)
            .Where(attr => attr is TAttributeType)
            .Cast<TAttributeType>()
            .ToList()
            ;
      }
      public static List<KeyValuePair<PropertyInfo, TAttributeType>>
         ClassPropertyAttributeList<T, TAttributeType>()
         where T : class
      {
         return
            typeof(T)
            .GetProperties()
            .Where
            (prop =>
               prop.CanRead
               && prop
                  .GetCustomAttributes(true)
                  .Where(attr => attr is TAttributeType)
                  .Count()
                  > 0
            )
            .Convert
            (prop =>
               new KeyValuePair<PropertyInfo, TAttributeType>
               (prop
               , prop
                  .GetCustomAttributes(true)
                  .Where(attr => attr is TAttributeType)
                  .Cast<TAttributeType>()
                  .First()
               )
            )
            .ToList()
            ;
      }
   }
}
