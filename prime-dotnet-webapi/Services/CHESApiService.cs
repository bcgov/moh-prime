using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prime.Services
{
    public class CHESApiService : BaseService, ICHESApiService
    {
        private static HttpClient Client;
        private String accessToken = "";

        public CHESApiService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        private static async Task<HttpClient> InitHttpClientAsync()
        {
            // if (PrimeConstants.ENVIRONMENT_NAME == "local")
            // {
            //     return null;
            // }

            HttpClient client = new HttpClient();

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            values.Add(new KeyValuePair<string, string>("client_id", "PRIME_SERVICE_CLIENT"));
            values.Add(new KeyValuePair<string, string>("client_secret", PrimeConstants.PRIME_SERVICE_CLIENT));
            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync("https://sso-dev.pathfinder.gov.bc.ca/auth/realms/jbd6rnxw/protocol/openid-connect/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    var successResponse = JsonConvert.DeserializeObject<SuccessResponse>(responseJsonString);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        successResponse.access_token
                    );
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }

            return client;
        }

        public async Task SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body)
        {
            Client = await InitHttpClientAsync();
            var requestParams = new CHESEmailRequestParams(from, to, subject, body);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams));

            HttpResponseMessage response = null;
            try
            {
                response = await Client.PostAsync(PrimeConstants.CHES_API_URL + "/email", requestContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when calling CHES Email API. Try again later.", ex);
            }
        }
    }

    public class CHESEmailRequestParams
    {
        public IEnumerable<Attachment> attachments { get; set; }
        public string bcc { get; set; }
        public string bodyType { get; set; }
        public string body { get; set; }
        public IEnumerable<string> cc { get; set; }
        public int delayTS { get; set; }
        public string encoding { get; set; }
        public string from { get; set; }
        public string priority { get; set; }
        public string subject { get; set; }
        public string tag { get; set; }
        public IEnumerable<string> to { get; set; }

        public CHESEmailRequestParams(string from, IEnumerable<string> to, string subject, string body)
        {
            this.from = from;
            this.to = to;
            this.subject = subject;
            this.body = body;
            bodyType = "html";
            delayTS = 1570000000;
            encoding = "utf-8";
            priority = "normal";
            tag = "tag";
        }
    }

    public class Attachment
    {
        public string content { get; set; }
        public string contentType { get; set; }
        public string encoding { get; set; }
        public string filename { get; set; }
    }

    public class SuccessResponse
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
}
