using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.HttpClients
{
    public class ChesClient : IChesClient
    {
        private static HttpClient _client;

        public ChesClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Guid> SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            var chesAttachments = new List<ChesAttachment>();
            foreach (var attachment in attachments)
            {
                var chesAttachment = new ChesAttachment()
                {
                    Content = Convert.ToBase64String(attachment.Content),
                    ContentType = "application/pdf",
                    Encoding = "base64",
                    Filename = attachment.Filename
                };

                chesAttachments.Add(chesAttachment);
            }

            var requestParams = new ChesEmailRequestParams(from, to, subject, body, chesAttachments);

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await _client.PostAsync("email", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    var successResponse = JsonConvert.DeserializeObject<EmailSuccessResponse>(responseJsonString);
                    return successResponse.Messages[0].MsgId;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }
        }

        public async Task<string> GetStatusAsync(Guid msgId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"status?msgId={msgId}");
                var statusResponse = JsonConvert.DeserializeObject<IList<StatusResponse>>(await response.Content.ReadAsStringAsync());
                return statusResponse[0].Status;
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
