using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Prime.HttpClients.Mail.ChesApiDefinitions;

namespace Prime.HttpClients.Mail
{
    public class ChesClient : BaseClient, IChesClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public ChesClient(
            HttpClient httpClient,
            ILogger<ChesClient> logger)
            : base(PropertySerialization.CamelCase)
        {
            // Credentials and Base Url are set in Startup.cs
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }

        public async Task<Guid?> SendAsync(Email email)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync("email", CreateStringContent(new ChesEmailRequestParams(email)));
                var responseJsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var successResponse = JsonConvert.DeserializeObject<EmailSuccessResponse>(responseJsonString);
                    return successResponse.Messages.Single().MsgId;
                }
                else
                {
                    _logger.LogError($"CHES Response code: {(int)response.StatusCode}, response body:{responseJsonString}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CHES Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetStatusAsync(Guid msgId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"status?msgId={msgId}");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var statusResponse = JsonConvert.DeserializeObject<IEnumerable<StatusResponse>>(responseString);
                    return statusResponse.Single().Status;
                }
                else
                {
                    _logger.LogError($"CHES Response code: {(int)response.StatusCode}, response body:{responseString}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CHES Exception: {ex.Message}");
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }

        }

        public async Task<bool> HealthCheckAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("health");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred when calling CHES Healthcheck: {ex.Message}");
                return false;
            }
        }
    }
}
