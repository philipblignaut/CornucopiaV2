using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornucopiaV2
{
   public class PropertyAttribute
      : Attribute 
   {
      public string SerializationId { get; private set; }
      public PropertyAttribute()
      {
         SerializationId = string.Empty;
      }
      public PropertyAttribute
         (string serializationId
         )
      {
         SerializationId = serializationId;
      }
   }
}
