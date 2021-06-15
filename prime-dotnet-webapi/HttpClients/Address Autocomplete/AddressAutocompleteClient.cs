using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Flurl;

namespace Prime.HttpClients
{
    public class AddressAutocompleteClient : IAddressAutocompleteClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        private readonly AddressAutocompleteClientCredentials _credentials;

        public AddressAutocompleteClient(
            HttpClient client,
            ILogger<AddressAutocompleteClient> logger,
            AddressAutocompleteClientCredentials credentials)
        {
            _client = client;
            _logger = logger;
            _credentials = credentials;
        }

        public async Task<IEnumerable<AddressAutocompleteFindResponse>> Find(string searchTerm, string lastId)
        {
            var url = "Find/v2.10/json3ex.ws".SetQueryParams(new
            {
                Key = _credentials.ApiKey,
                SearchTerm = searchTerm,
                LastId = lastId,
            });

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(url);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new AddressAutocompleteApiException("Error occurred attempting to get the autocomplete find response: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new AddressAutocompleteApiException($"Error code {response.StatusCode} was provided when calling AddressAutocompleteClient::Find");
            }

            var body = await response.Content.ReadAsAsync<ApiResponse<AddressAutocompleteFindResponse>>();
            return body?.Items;
        }

        public async Task<IEnumerable<AddressAutocompleteRetrieveResponse>> Retrieve(string id)
        {
            var url = "Retrieve/v2.11/json3ex.ws".SetQueryParams(new
            {
                Key = _credentials.ApiKey,
                Id = id,
            });

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(url);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new AddressAutocompleteApiException("Error occurred attempting to get the autocomplete retrieve response: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new AddressAutocompleteApiException($"Error code {response.StatusCode} was provided when calling AddressAutocompleteClient::Retrieve");
            }

            var body = await response.Content.ReadAsAsync<ApiResponse<AddressAutocompleteRetrieveResponse>>();
            return body?.Items;
        }

        private class ApiResponse<T>
        {
            public IEnumerable<T> Items { get; set; }
        }

        public class AddressAutocompleteFindResponse
        {
            public string Id { get; set; } // The Id to use as the SearchTerm with the Find method.
            public string Text { get; set; } // The found item.
            public string Highlight { get; set; } // A series of number pairs that indicates which characters in the Text property have matched the SearchTerm.
            public string Cursor { get; set; } // A zero-based position in the Text response indicating the suggested position of the cursor if this item is selected. A -1 response indicates no suggestion is available.
            public string Description { get; set; } // Descriptive information about the found item, typically if it's a container.
            public string Next { get; set; } // The next step of the search process. (Find, Retrieve)
        }

        public class AddressAutocompleteRetrieveResponse
        {
            public string Id { get; set; }
            public string DomesticId { get; set; }
            public string Language { get; set; }
            public string LanguageAlternatives { get; set; }
            public string Department { get; set; }
            public string Company { get; set; }
            public string SubBuilding { get; set; }
            public string BuildingNumber { get; set; }
            public string BuildingName { get; set; }
            public string SecondaryStreet { get; set; }
            public string Street { get; set; }
            public string Block { get; set; }
            public string Neighbourhood { get; set; }
            public string District { get; set; }
            public string City { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            public string Line4 { get; set; }
            public string Line5 { get; set; }
            public string AdminAreaName { get; set; }
            public string AdminAreaCode { get; set; }
            public string Province { get; set; }
            public string ProvinceName { get; set; }
            public string ProvinceCode { get; set; }
            public string PostalCode { get; set; }
            public string CountryName { get; set; }
            public string CountryIso2 { get; set; }
            public string CountryIso3 { get; set; }
            public int CountryIsoNumber { get; set; }
            public string SortingNumber1 { get; set; }
            public string SortingNumber2 { get; set; }
            public string Barcode { get; set; }
            public string PoBoxNumber { get; set; }
            public string Label { get; set; }
            public string DataLevel { get; set; }
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

    public class AddressAutocompleteApiException : Exception
    {
        public AddressAutocompleteApiException() : base() { }
        public AddressAutocompleteApiException(string message) : base(message) { }
        public AddressAutocompleteApiException(string message, Exception inner) : base(message, inner) { }
    }
}
