using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Prime.HttpClients
{
    public enum PropertySerialization
    {
        CamelCase
    }

    public class BaseClient
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public BaseClient(PropertySerialization option)
        {
            _serializerSettings = option switch
            {
                PropertySerialization.CamelCase => new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() },
                _ => throw new NotImplementedException($"{option}")
            };
        }

        /// <summary>
        /// Creates JSON StringContent based on the serialization settings set in the constructor
        /// </summary>
        /// <param name="data"></param>
        public StringContent CreateStringContent(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data, _serializerSettings), Encoding.UTF8, "application/json");
        }
    }
}
