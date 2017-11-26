using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CornucopiaV2
{
	public static class XmlSerializeDeserialize
   {
      public static string XmlSerialize<T>
         (this T obj
         ) where T : class
      {
         MemoryStream memStream = new MemoryStream();
         XmlSerializer serializer = new XmlSerializer(obj.GetType());
         serializer.Serialize(memStream, obj);
         memStream.Flush();
         memStream.Seek(0, 0);
         byte[] data = new byte[memStream.Length];
         memStream.Read(data, 0, data.Length);
         string xml = Encoding.UTF8.GetString(data);
         return xml;
      }
      public static T XmlDeserialize<T>
         (this string xml
         ) where T : class
      {
         MemoryStream memStream = new MemoryStream();
         byte[] data = Encoding.UTF8.GetBytes(xml);
         memStream.Write(data, 0, data.Length);
         memStream.Flush();
         memStream.Seek(0, 0);
         XmlSerializer serializer = new XmlSerializer(typeof(T));
         T obj = (T)serializer.Deserialize(memStream);
         return obj;
      }
   }
}