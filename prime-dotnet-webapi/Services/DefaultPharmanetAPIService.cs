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
using Newtonsoft.Json;

using Prime.Models;

namespace Prime.Services
{
    public class DefaultPharmanetApiService : BaseService, IPharmanetApiService
    {
        private static HttpClient Client = InitHttpClient();

        private ILookupService _lookupService;

        public DefaultPharmanetApiService(
            ApiDbContext context, IHttpContextAccessor httpContext, ILookupService lookupService)
            : base(context, httpContext)
        {
            _lookupService = lookupService;
        }

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
            if (string.IsNullOrWhiteSpace(certification.LicenseNumber) || certification.CollegeCode == 0)
            {
                return null;
            }

            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                // TODO handle local dev
                return null;
            }

            College college;
            if (certification.College != null)
            {
                college = certification.College;
            }
            else
            {
                var colleges = await _lookupService.GetLookupsAsync<short, College>();
                college = colleges.Single(c => c.Code == certification.CollegeCode);
            }

            return await CallPharmanetCollegeLicenceService(certification.LicenseNumber, college.Prefix);
        }

        private async Task<PharmanetCollegeRecord> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            var requestParams = new CollegeRecordRequestParams(licenceNumber, collegeReferenceId);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams));

            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync(PrimeConstants.PHARMANET_API_URL, requestContent);
            }
            catch (Exception ex)
            {
                // TODO HTTP error. Log error? Retry?
                throw new PharmanetCollegeApiException($"Calling Pharmanet API caused an error. Try again.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                // TODO log error? Client error probably means bad licence number or college ref.
                return null;
            }

            var content = await response.Content.ReadAsAsync<List<PharmanetCollegeRecord>>();
            var practicionerRecord = content.SingleOrDefault();

            // If we get a record back, it should have the same transaction UUID as our request.
            if (practicionerRecord != null && practicionerRecord.applicationUUID != requestParams.applicationUUID)
            {
                throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was\"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
            }

            return practicionerRecord;
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
