using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prime.Models.Api;

namespace Prime.HttpClients
{
    public class LdapClient : BaseClient, ILdapClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public LdapClient(
            HttpClient client,
            ILogger<LdapClient> logger)
            : base(PropertySerialization.CamelCase)
        {
            // Base Url is set in Startup.cs
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
        }

        public async Task<LdapResponseKeys> GetUserAsync(string username, string password)
        {
            var messageObject = new
            {
                userName = username,
                password
            };

            var httpContent = CreateStringContent(messageObject);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("users", httpContent);

                var responseJsonString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("CONTENT RESPONSE: {body}", responseJsonString);

                if (response.IsSuccessStatusCode)
                {
                    var successResponse = JsonConvert.DeserializeObject<GisUser>(responseJsonString);
                    // when LDAP API is live uncomment and replace the object below
                    // var ldapResponseKeys = new LdapResponseKeys
                    // {
                    //     RemainingAttempts = successResponse.RemainingAttempts,
                    //     LockoutTimeInHours = successResponse.LockoutTimeInHours,
                    //     GisUserRole = successResponse.Gisuserrole
                    // };
                    var ldapResponseKeys = new LdapResponseKeys
                    {
                        RemainingAttempts = successResponse?.RemainingAttempts,
                        LockoutTimeInHours = successResponse?.LockoutTimeInHours,
                        GisUserRole = successResponse?.Gisuserrole
                    };
                    return ldapResponseKeys;
                }
                else
                {
                    await LogError(response);
                    return null;
                }
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                return null;
            }
        }

        public class GisUser
        {
            public string Gisuserrole { get; set; }
            public string Authenticated { get; set; }
            public string Unlocked { get; set; }
            public string Username { get; set; }
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
