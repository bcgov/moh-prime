using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Prime.Helpers
{
    public static class XmlSerializerHelper
    {
        public static T Deserialize<T>(this T value, string xml)
        {
            var model = default(T);
            if (string.IsNullOrEmpty(xml)) return model;

            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream(Encoding.Unicode.GetBytes(xml))
            {
                Position = 0
            };
            return (T) serializer.Deserialize(ms);
        }
    }
}
