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
                var resp = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, new { });
                return await resp.Content.ReadAsStringAsync();
            };
        }
    }
}
