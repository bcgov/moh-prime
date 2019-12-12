using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.Models;

namespace Prime.Services
{
    public class DefaultPharmanetApiService : BaseService, IPharmanetApiService
    {
        private static HttpClient Client = InitHttpClient();

        public DefaultPharmanetApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
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

        public async Task<PharmanetCollegeRecord> GetCollegeRecord(Certification certification)
        {
            if (string.IsNullOrWhiteSpace(certification.LicenseNumber))
            {
                return null;
            }

            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                // TODO handle local dev
                return null;
            }

            //Certification college = await _context.Certifications.SingleOrDefaultAsync(c => c.CollegeCode == certification.CollegeCode);
            return await CallPharmanetCollegeLicenceService(certification.LicenseNumber.Remove(4) + "P", "91");
        }

        private async Task<PharmanetCollegeRecord> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            var requestParams = new CollegeRecordRequestParams(licenceNumber, collegeReferenceId);
            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsJsonAsync(PrimeConstants.PHARMANET_API_URL, requestParams);

                if (!response.IsSuccessStatusCode)
                {
                    // TODO log error? Client error probably means bad licence number or college ref.
                    return null;
                }

                var content = await response.Content.ReadAsAsync<List<PharmanetCollegeRecord>>();
                var practicionerRecord = content.SingleOrDefault();
                if (practicionerRecord != null && practicionerRecord.applicationUUID != requestParams.applicationUUID)
                {
                    throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was\"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
                }

                return practicionerRecord;
            }
            catch (Exception ex)
            {
                throw new PharmanetCollegeApiException($"Pharmanet API returned an error. transactionid:[{requestParams.applicationUUID}]", ex);
            }
        }

        public class PharmanetCollegeApiException : Exception
        {
            public PharmanetCollegeApiException(string message)
                : base(message)
            {
            }

            public PharmanetCollegeApiException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        private class CollegeRecordRequestParams
        {
            public string applicationUUID { get; set; }
            public string programArea { get; set; }
            public string licenceNumber { get; set; }
            public string collegeReferenceId { get; set; }

            public CollegeRecordRequestParams(string licenceNumber, string collegeReferenceId)
            {
                applicationUUID = Guid.NewGuid().ToString();
                programArea = "PRIME";
                this.licenceNumber = licenceNumber;
                this.collegeReferenceId = collegeReferenceId;
            }
        }
    }
}
