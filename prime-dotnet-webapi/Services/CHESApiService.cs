using System;
using System.Collections.Generic;
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
        private static HttpClient Client = InitHttpClient();

        public CHESApiService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        private static HttpClient InitHttpClient()
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                return null;
            }

            X509Certificate2 certificate = new X509Certificate2(PrimeConstants.PHARMANET_SSL_CERT_FILENAME, PrimeConstants.PHARMANET_SSL_CERT_PASSWORD);
            var client = new HttpClient(
                new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ClientCertificates = { certificate }
                }
            );

            var authBytes = ASCIIEncoding.ASCII.GetBytes($"{PrimeConstants.PHARMANET_API_USERNAME}:{PrimeConstants.PHARMANET_API_PASSWORD}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            return client;
        }

        public async Task Send(string from, IEnumerable<string> to, string subject, string body)
        {
            var requestParams = new CHESEmailRequestParams(from, to, subject, body);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams));

            HttpResponseMessage response = null;
            try
            {
                response = await Client.PostAsync(PrimeConstants.PHARMANET_API_URL, requestContent);
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
}
