using Newtonsoft.Json;


namespace Pip.Infrastructure
{
    public class EmptyStringToNullJsonConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader.ThrowIfNull(nameof(reader));

            string? value = reader.Value as string;
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
