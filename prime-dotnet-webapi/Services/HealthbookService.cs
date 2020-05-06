using System;
using System.Linq;
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
            BaseAddress = new Uri("http://localhost:5000/")
        };

        public HealthbookService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task PushBcscInfoAsync(Enrollee enrollee)
        {
            await new BcscInfo().PushToHealthbookAsync(enrollee);
        }

        public async Task PushGpidInfoAsync(Enrollee enrollee)
        {
            await new GpidInfo().PushToHealthbookAsync(enrollee);
        }

        public async Task PushCpbcInfoAsync(Enrollee enrollee)
        {
            await new CpbcInfo().PushToHealthbookAsync(enrollee);
        }

        public class HealthbookApiException : Exception
        {
            public HealthbookApiException() : base() { }
            public HealthbookApiException(string message) : base(message) { }
            public HealthbookApiException(string message, Exception inner) : base(message, inner) { }
        }

        private abstract class HealthbookRequest
        {
            public abstract string Url { get; }
            public abstract string Schema { get; }
            public abstract string Version { get; }

            public abstract Task PushToHealthbookAsync(Enrollee enrollee);

            protected async Task CallHealthbookApiAsync(object attributes)
            {
                var parameters = new
                {
                    schema = Schema,
                    version = Version,
                    attributes = attributes
                };
                string serialized = JsonConvert.SerializeObject(new[] { parameters });
                System.Console.WriteLine($"Calling Healthbook API with these parmeters: {serialized}");
                var content = new StringContent(serialized);

                HttpResponseMessage response = null;
                try
                {
                    response = await Client.PostAsync(Url, content);
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

        private class BcscInfo : HealthbookRequest
        {
            public override string Url { get => "bcsc/issue-credential"; }
            public override string Schema { get => "BCSC Information"; }
            public override string Version { get => "1.0.7"; }

            public override async Task PushToHealthbookAsync(Enrollee enrollee)
            {
                var attributes = new
                {
                    user_id = enrollee.UserId.ToString(),
                    legal_name = $"{enrollee.FirstName} {enrollee.LastName}",
                    hpdid = enrollee.HPDID
                };

                await CallHealthbookApiAsync(attributes);
            }
        }

        private class GpidInfo : HealthbookRequest
        {
            public override string Url { get => "gpid/issue-credential"; }
            public override string Schema { get => "General Practitioner ID"; }
            public override string Version { get => "1.0.1"; }

            public override async Task PushToHealthbookAsync(Enrollee enrollee)
            {
                var attributes = new
                {
                    hpdid = enrollee.HPDID,
                    gpid = enrollee.GPID
                };

                await CallHealthbookApiAsync(attributes);
            }
        }

        private class CpbcInfo : HealthbookRequest
        {
            public override string Url { get => "cpbc/issue-credential"; }
            public override string Schema { get => "Registered Pharmacist"; }
            public override string Version { get => "1.0.1"; }

            public override async Task PushToHealthbookAsync(Enrollee enrollee)
            {
                var cert = enrollee.Certifications.First();
                var attributes = new
                {
                    hpid = enrollee.HPDID,
                    licence_num = cert.LicenseNumber,
                    licence_class = cert.License.Name
                };

                await CallHealthbookApiAsync(attributes);
            }
        }
    }
}
