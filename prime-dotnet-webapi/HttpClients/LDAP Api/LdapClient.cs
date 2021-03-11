using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_client.BaseAddress}users/{userId}"),
                    Content = new StringContent(password, Encoding.UTF8, "application/json"),
                };
                response = await _client.SendAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GIS_USER_ROLE: {gisuserrole}", (string)body.SelectToken("gisuserrole"));
            _logger.LogInformation("CONTENT RESPONSE: {body}", await response.Content.ReadAsStringAsync());

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

}
