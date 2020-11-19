using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Prime.HttpClients
{
    public class ChesClient : IChesClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public ChesClient(
            HttpClient httpClient,
            ILogger<ChesClient> logger)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }

        public async Task<Guid?> SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            var chesAttachments = new List<ChesAttachment>();
            foreach (var (Filename, Content) in attachments)
            {
                var chesAttachment = new ChesAttachment()
                {
                    Content = Convert.ToBase64String(Content),
                    ContentType = "application/pdf",
                    Encoding = "base64",
                    Filename = Filename
                };

                chesAttachments.Add(chesAttachment);
            }

            var requestParams = new ChesEmailRequestParams(from, to, subject, body, chesAttachments);

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(
                    requestParams,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                ),
                Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync("email", requestContent);
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
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
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
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }

        }
    }

    public class ChesEmailRequestParams
    {
        public IEnumerable<ChesAttachment> Attachments { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public string BodyType { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public int? DelayTS { get; set; }
        public string Encoding { get; set; }
        public string From { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public string Tag { get; set; }
        public IEnumerable<string> To { get; set; }

        public ChesEmailRequestParams(string from, IEnumerable<string> to, string subject, string body, IEnumerable<ChesAttachment> attachments)
        {
            this.Attachments = attachments;
            Bcc = new List<string>();
            BodyType = "html";
            this.Body = body;
            Cc = new List<string>();
            DelayTS = 1570000000;
            Encoding = "utf-8";
            this.From = from;
            Priority = "normal";
            this.Subject = subject;
            Tag = "tag";
            this.To = to;
        }
    }

    public class ChesAttachment
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string Encoding { get; set; }
        public string Filename { get; set; }
    }

    public class EmailSuccessResponse
    {
        public IList<Message> Messages { get; set; }
        public Guid TxId { get; set; }
    }

    public class Message
    {
        public Guid MsgId { get; set; }
        public string Tag { get; set; }
        public IEnumerable<string> To { get; set; }
    }

    public class StatusResponse
    {
        public long CreatedTS { get; set; }
        public long DelayTS { get; set; }
        public Guid MsgId { get; set; }
        public string Status { get; set; }
        public ICollection<StatusHistoryObject> StatusHistory { get; set; }
        public string Tag { get; set; }
        public Guid TxId { get; set; }
        public long UpdatedTS { get; set; }
    }

    public class StatusHistoryObject
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public int Timestamp { get; set; }
    }

    public sealed class ChesStatus
    {
        public static ChesStatus Accepted = new ChesStatus("accepted");
        public static ChesStatus Cancelled = new ChesStatus("cancelled");
        public static ChesStatus Completed = new ChesStatus("completed");
        public static ChesStatus Failed = new ChesStatus("failed");
        public static ChesStatus Pending = new ChesStatus("pending");
        public string Value { get; private set; }

        private ChesStatus(string value)
        {
            Value = value;
        }
    }
}
