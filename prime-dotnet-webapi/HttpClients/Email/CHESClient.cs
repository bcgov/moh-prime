using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Prime.HttpClients
{
    public class ChesClient : IChesClient
    {
        private static HttpClient _client;
        private readonly ILogger _logger;

        public ChesClient(
            HttpClient httpClient,
            ILogger<ChesClient> logger)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }

        public async Task<Guid> SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
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

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync("email", requestContent);
                var responseJsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var successResponse = JsonConvert.DeserializeObject<EmailSuccessResponse>(responseJsonString);
                    return successResponse.Messages[0].MsgId;
                }
                else
                {
                    _logger.LogError($"CHES Response code: {(int)response.StatusCode}, response body:{responseJsonString}");
                    return Guid.Empty;
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
                    var statusResponse = JsonConvert.DeserializeObject<IList<StatusResponse>>(responseString);
                    return statusResponse[0].Status;
                }
                else
                {
                    _logger.LogError($"CHES Response code: {(int)response.StatusCode}, response body:{responseString}");
                    return null;
                }

            }
            catch (Exception ex)
            {
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
        // must be lower case for CHES to accept params
        public IEnumerable<ChesAttachment> attachments { get; set; }
        public IEnumerable<string> bcc { get; set; }
        public string bodyType { get; set; }
        public string body { get; set; }
        public IEnumerable<string> cc { get; set; }
        public int? delayTS { get; set; }
        public string encoding { get; set; }
        public string from { get; set; }
        public string priority { get; set; }
        public string subject { get; set; }
        public string tag { get; set; }
        public IEnumerable<string> to { get; set; }

        public ChesEmailRequestParams(string from, IEnumerable<string> to, string subject, string body, IEnumerable<ChesAttachment> attachments)
        {
            this.attachments = attachments;
            bcc = new List<string>();
            bodyType = "html";
            this.body = body;
            cc = new List<string>();
            delayTS = 1570000000;
            encoding = "utf-8";
            this.from = from;
            priority = "normal";
            this.subject = subject;
            tag = "tag";
            this.to = to;
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
        public string Status { get; set; } // (StatusType)Enum: "accepted" "cancelled" "completed" "failed" "pending"
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
}
