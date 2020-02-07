using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prime.Models.Api
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ApiResponse(int statusCode) : this(statusCode, null)
        { }

        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    return "Resource not found";
                case StatusCodes.Status500InternalServerError:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}
