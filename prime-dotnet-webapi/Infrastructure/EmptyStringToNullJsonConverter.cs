using System;
using Newtonsoft.Json;

namespace Prime.Infrastructure
{
    public class EmptyStringToNullJsonConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader), "The passed in JsonReader cannot be null.");
            }

            string value = (string)reader.Value;
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
