using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Prime.Services.Clients
{
    public class AddressValidationClient : IAddressValidationClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public AddressValidationClient(
            HttpClient client,
            ILogger<AddressValidationClient> logger)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
            _logger = logger;
        }

        public async Task<JObject> Find(string searchTerm, string lastId)
        {
            //Build the url
            var url = "Find/v2.10/json3ex.ws?";
            url += "Key=" + System.Web.HttpUtility.UrlEncode(PrimeConstants.ADDRESS_VALIDATION_API_KEY);
            url += "&SearchTerm=" + System.Web.HttpUtility.UrlEncode(searchTerm);
            url += "&LastId=" + System.Web.HttpUtility.UrlEncode(lastId);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(url);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new AddressValidationApiException("Error occurred attempting to get the autocomplete find response: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new AddressValidationApiException($"Error code {response.StatusCode} was provided when calling AddressValidationClient::Find");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET autocomplete find {@JObject}", JsonConvert.SerializeObject(body));

            return body;
        }

        public async Task<JObject> Retrieve(string Id)
        {
            //Build the url
            var url = "Retrieve/v2.11/json3ex.ws?";
            url += "Key=" + System.Web.HttpUtility.UrlEncode(PrimeConstants.ADDRESS_VALIDATION_API_KEY);
            url += "&Id=" + System.Web.HttpUtility.UrlEncode(Id);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(url);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new AddressValidationApiException("Error occurred attempting to get the autocomplete retrieve response: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new AddressValidationApiException($"Error code {response.StatusCode} was provided when calling AddressValidationClient::Retrieve");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET autocomplete retrieve {@JObject}", JsonConvert.SerializeObject(body));

            return body;
        }

        public class AutoCompleteResponse
        {
            public string xmlns { get; set; }
            public string Id { get; set; } // The Id to use as the SearchTerm with the Find method if IsRetrievable is false. If IsRetrievable is true, use this Id with the RetrieveById method. If blank, provide a more detailed SearchTerm.
            public string Text { get; set; } // The found item.
            public string Highlight { get; set; } // A series of number pairs that indicates which characters in the Text property have matched the SearchTerm.
            public string Cursor { get; set; } // A zero-based position in the Text response indicating the suggested position of the cursor if this item is selected. A -1 response indicates no suggestion is available.
            public string Description { get; set; } // Additional information about this result.
            public string Next { get; set; } // The next step of the search process. (Find, Retrieve)

            public AutoCompleteResponse()
            {
                xmlns = "";
                Id = "";
                Text = "";
                Highlight = "";
                Cursor = "";
                Description = "";
                Next = "Find";
            }
            public AutoCompleteResponse(JObject response)
            {
                xmlns = "";
                Id = "";
                Text = "";
                Highlight = "";
                Cursor = "";
                Description = "";
                Next = "Find";
            }

        }

        private async Task LogError(HttpResponseMessage response, Exception exception = null)
        {
            await LogError(null, response, exception);
        }

        private async Task LogError(HttpContent content, HttpResponseMessage response, Exception exception = null)
        {
            string secondaryMessage;
            if (exception != null)
            {
                secondaryMessage = $"Exception: {exception.Message}";
            }
            else if (response != null)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                secondaryMessage = $"Response code: {(int)response.StatusCode}, response body:{responseMessage}";
            }
            else
            {
                secondaryMessage = "No additional message. Http response and exception were null.";
            }

            _logger.LogError(exception, secondaryMessage, new Object[] { content, response });
        }
    }

    public class AddressValidationApiException : Exception
    {
        public AddressValidationApiException() : base() { }
        public AddressValidationApiException(string message) : base(message) { }
        public AddressValidationApiException(string message, Exception inner) : base(message, inner) { }
    }
}
