using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Prime.HttpClients
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

            var requestParams = new ChesEmailRequestParams(from, to, cc, subject, body, chesAttachments);

            try
            {
                HttpResponseMessage response = await _client.PostAsync("email", CreateStringContent(requestParams));
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

        public ChesEmailRequestParams(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<ChesAttachment> attachments)
        {
            Attachments = attachments;
            Bcc = new List<string>();
            BodyType = "html";
            Body = body;
            Cc = cc;
            DelayTS = 1570000000;
            Encoding = "utf-8";
            From = from;
            Priority = "normal";
            Subject = subject;
            Tag = "tag";
            To = to;
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
