using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace Prime.Services
{
    public class DefaultHibcApiService : BaseService, IHibcApiService
    {
        public DefaultHibcApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<string> ValidateCollegeLicense()
        {
            X509Certificate2 certificate = new X509Certificate2(@"/opt/app-root/etc/certs/t1primesvc.pfx", PrimeConstants.HIBC_SSL_CERT_PASSWORD);
            var httpClientHandler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ClientCertificates = { certificate }
            };
            using (var client = new HttpClient(httpClientHandler))
            {
                var values = new
                {
                    applicationUUID = "b7a2993a-e55a-4455-b8c2-fcd12e57ce61",
                    programArea = "PRIME",
                    licenceNumber = "2I8R1",
                    collegeReferenceId = "91"
                };
                var resp = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, values);
                string content = await resp.Content.ReadAsStringAsync();
                return $"status code:[{(int)resp.StatusCode}], headers:[{resp.Content.Headers}], content:[{content}]";
            };
        }
    }
}
