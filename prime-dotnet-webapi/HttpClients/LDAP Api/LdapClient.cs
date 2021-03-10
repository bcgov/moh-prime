using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.HttpClients
{
    public class LdapClient : ILdapClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public LdapClient(
            HttpClient client,
            ILogger<LdapClient> logger)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
            _logger = logger;
        }

        public async Task<JObject> GetUserAsync(string userId, string password)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"users/{userId}?userPassword={password}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new LdapApiException("Error occurred attempting to get the schema id by issuer did: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new LdapApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetSchema");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GIS_USER_ROLE: {gisuserrole}", (string)body.SelectToken("gisuserrole"));

            return body;
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

    public class LdapApiException : Exception
    {
        public LdapApiException() : base() { }
        public LdapApiException(string message) : base(message) { }
        public LdapApiException(string message, Exception inner) : base(message, inner) { }
    }
}
