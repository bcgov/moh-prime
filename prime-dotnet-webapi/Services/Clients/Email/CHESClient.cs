using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Prime.Services
{
    public class ChesClient : IChesClient
    {
        private static HttpClient _client;

        public ChesClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            var chesAttachments = new List<ChesAttachment>();
            foreach (var attachment in attachments)
            {
                var chesAttachment = new ChesAttachment()
                {
                    content = Convert.ToBase64String(attachment.Content),
                    contentType = "application/pdf",
                    encoding = "base64",
                    filename = attachment.Filename
                };

                chesAttachments.Add(chesAttachment);
            }

            var requestParams = new CHESEmailRequestParams(from, to, subject, body, chesAttachments);

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("email", requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<EmailSuccessResponse>(responseJsonString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }
        }

        public async Task<bool> HealthCheckAsync()
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync("health");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }

        }
    }

    public class CHESEmailRequestParams
    {
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

        public CHESEmailRequestParams(string from, IEnumerable<string> to, string subject, string body, IEnumerable<ChesAttachment> attachments)
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
        public string content { get; set; }
        public string contentType { get; set; }
        public string encoding { get; set; }
        public string filename { get; set; }
    }

    public class OpenIdSuccessResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string not_before_policy { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
    }

    public class EmailSuccessResponse
    {
        public IEnumerable<Message> messages { get; set; }
        public Guid txId { get; set; }
    }

    public class Message
    {
        public Guid msgId { get; set; }
        public string tag { get; set; }
        public IEnumerable<string> to { get; set; }
    }
}
