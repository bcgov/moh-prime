using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using Prime.Models;

namespace Prime.Services
{
    public class PharmanetApiService : BaseService, IPharmanetApiService
    {
        private static HttpClient Client = InitHttpClient();

        public PharmanetApiService(
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

        public async Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification)
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                return LocalDevApiMock(certification.LicenseNumber);
            }

            if (certification.College == null)
            {
                // The College is not loaded by default in the base enrollee query.
                // We expect this to be populated when called from CreateEnrolmentStatus but this could be called from elsewhere in the app.
                await _context.Entry(certification).Reference(c => c.College).LoadAsync();
            }

            return await CallPharmanetCollegeLicenceService(certification.LicenseNumber, certification.College.Prefix);
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
                throw new PharmanetCollegeApiException("Error occurred when calling Pharmanet API. Try again later.", ex);
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
                throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was \"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
            }

            return practicionerRecord;
        }

        private PharmanetCollegeRecord LocalDevApiMock(string licenceNumber)
        {
            if (licenceNumber == "error")
            {
                throw new PharmanetCollegeApiException();
            }

            int parsed;
            if (!Int32.TryParse(licenceNumber, out parsed) ||  parsed < 1 || parsed > 11)
            {
                return null;
            }

            var lookup = new[]
            {
                null,
                new {Date = "2000-05-17", Name = "ONE"},
                new {Date = "1998-08-07", Name = "TWO"},
                new {Date = "1998-08-08", Name = "THREE"},
                new {Date = "1999-10-01", Name = "FOUR"},
                new {Date = "1999-01-31", Name = "FIVE"},
                new {Date = "2000-02-25", Name = "SIX"},
                new {Date = "1999-03-14", Name = "SEVEN"},
                new {Date = "1999-01-04", Name = "EIGHT"},
                new {Date = "1997-10-12", Name = "NINE"},
                new {Date = "2000-05-30", Name = "TEN"},
                new {Date = "2000-06-07", Name = "ELEVEN"}
            };
            var info = lookup[parsed];

            return new PharmanetCollegeRecord
            {
                applicationUUID = new Guid().ToString(),
                firstName = "PRIMET",
                lastName = info.Name,
                dateofBirth = DateTime.Parse(info.Date),
                status = "P",
                effectiveDate = DateTime.Today
            };
        }

        public class PharmanetCollegeApiException : Exception
        {
            public PharmanetCollegeApiException() : base() { }
            public PharmanetCollegeApiException(string message) : base(message) { }
            public PharmanetCollegeApiException(string message, Exception inner) : base(message, inner) { }
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
