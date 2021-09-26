using Newtonsoft.Json;
using System;

namespace JYORMApi.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.ReadAsString());
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            // writer.WriteValue(value.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"));
            writer.WriteValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}