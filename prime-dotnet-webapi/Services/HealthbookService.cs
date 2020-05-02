using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.Services
{
    public class HealthbookService : BaseService, IHealthbookService
    {
        private static HttpClient Client = new HttpClient()
        {
            // TODO: URL
            BaseAddress = new Uri("")
        };

        public HealthbookService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task PushBcscInfoAsync(Enrollee enrollee)
        {
            var parameters = new
            {
                userId = enrollee.UserId.ToString(),
                firstName = enrollee.FirstName,
                lastName = enrollee.LastName,
                hpdid = enrollee.HPDID
            };

            // TODO: URL
            await SendInfoToHealthbook("/", parameters);
        }

        public async Task PushGpidAsync(Enrollee enrollee)
        {
            var parameters = new
            {
                userId = enrollee.UserId.ToString(),
                gpid = enrollee.GPID
            };

            // TODO: URL
            await SendInfoToHealthbook("/", parameters);
        }

        public class HealthbookApiException : Exception
        {
            public HealthbookApiException() : base() { }
            public HealthbookApiException(string message) : base(message) { }
            public HealthbookApiException(string message, Exception inner) : base(message, inner) { }
        }

        private async Task SendInfoToHealthbook(string endpointUrl, object parameters)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parameters));

            HttpResponseMessage response = null;
            try
            {
                response = await Client.PostAsync(endpointUrl, content);
            }
            catch (Exception ex)
            {
                throw new HealthbookApiException("Error occurred when calling Healthbook API.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HealthbookApiException($"Error code {response.StatusCode} was returned when calling Healthbook API.");
            }
        }
    }
}
