using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CornucopiaV2
{
   public static class ObjectExtensions
   {
      /// <summary>
      /// Copies all the properties of the "from" object to this object if they exist.
      /// </summary>
      /// <param name="to">The object in which the properties are copied</param>
      /// <param name="from">The object which is used as a source</param>
      /// <param name="excludedProperties">Exclude these properties from the copy</param>
      public static void CopyPropertiesFrom
        (this object to
        , object from
        , string[] excludedProperties
        )
      {
         List<string> exclPropNames =
           excludedProperties == null
           ? new List<string>()
           : excludedProperties.ToList()
           ;
         Type toType = to.GetType();
         Type fromType = from.GetType();
         (
           from propDetail
           in
              (
                from propInfo
                in fromType.GetProperties()
                where
                  (!exclPropNames.Contains(propInfo.Name))
                  && propInfo.CanRead
                  && (toType.GetProperty(propInfo.Name) != null)
                select new
                {
                   fromPropInfo = propInfo
                ,
                   propName = propInfo.Name
                ,
                   toPropInfo = toType.GetProperty(propInfo.Name)
                }
              )
           where
             propDetail.fromPropInfo.PropertyType
               == propDetail.toPropInfo.PropertyType
             && propDetail.toPropInfo.CanWrite
           select propDetail
         )
           .Each
             (propDetail =>
             {
                object value = propDetail.fromPropInfo.GetValue(from, null);
                if
                  (propDetail.fromPropInfo.PropertyType.IsPrimitive
                  || propDetail.fromPropInfo.PropertyType.IsEnum
                  || propDetail.fromPropInfo.PropertyType == typeof(string)
                  )
                {
                   propDetail.toPropInfo
                     .SetValue
                       (to
                       , value
                       , null
                       )
                       ;
                }
                else
                {
                   if (propDetail.fromPropInfo.PropertyType.IsSerializable)
                   {
                      StringBuilder strBr = new StringBuilder();
                      StringWriter strWr = new StringWriter(strBr);
                      XmlSerializer xmlSer =
                        new XmlSerializer
                          (propDetail.fromPropInfo.PropertyType
                          )
                          ;
                      xmlSer.Serialize(strWr, value);
                      strWr.Flush();
                      // Debug.Print(strBr.ToString());
                      propDetail.toPropInfo
                        .SetValue
                          (to
                          , xmlSer.Deserialize(new StringReader(strBr.ToString()))
                          , null
                          )
                          ;
                   }
                   else
                   {
                      throw
                        new Exception
                          ("Cannot copy Property "
                          + "[" + propDetail.fromPropInfo.PropertyType + "] "
                          + propDetail.fromPropInfo.Name
                          + " from object of type " + fromType.ToString()
                          + " to object of type " + toType.ToString()
                          )
                        ;
                   }
                }
             }
             )
             ;
      }

      /// <summary>
      /// Copies all the properties of the "from" object to this object if they exist.
      /// </summary>
      /// <param name="to">The object in which the properties are copied</param>
      /// <param name="from">The object which is used as a source</param>
      public static void CopyPropertiesFrom
        (this object to
        , object from
        )
      {
         to.CopyPropertiesFrom
           (from
           , null
           )
           ;
      }

      /// <summary>
      /// Copies all the properties of this object to the "to" object
      /// </summary>
      /// <param name="to">The object in which the properties are copied</param>
      /// <param name="from">The object which is used as a source</param>
      public static void CopyPropertiesTo
        (this object from
        , object to
        )
      {
         to.CopyPropertiesFrom
           (from
           , null
           )
           ;
      }

      /// <summary>
      /// Copies all the properties of this object to the "to" object
      /// </summary>
      /// <param name="to">The object in which the properties are copied</param>
      /// <param name="from">The object which is used as a source</param>
      /// <param name="excludedProperties">Exclude these properties from the copy</param>
      public static void CopyPropertiesTo
        (this object from
        , object to
        , string[] excludedProperties
        )
      {
         to.CopyPropertiesFrom
           (from
           , excludedProperties
           )
           ;
      }
      public static T ClonePropertiesFrom<T>
        (this T to
        , T from
        )
      {
         to.CopyPropertiesFrom(from, null);
         return to;
      }
      public static T ClonePropertiesTo<T>
        (this T from
        , T to
        )
      {
         from.CopyPropertiesTo(to, null);
         return to;
      }
   }
}
