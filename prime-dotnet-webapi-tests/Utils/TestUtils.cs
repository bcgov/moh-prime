using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Bogus;
using Bogus.Extensions;

using Prime;
using Prime.Models;
using Prime.Services;

using PrimeTests.Utils.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Internal;

namespace PrimeTests.Utils
{
    public class TestUtils
    {

        public static string[] countries = new[] { "CA" };

        public static string[] provinces = new[] { "AB", "BC", "MB", "NB", "NL", "NS", "ON", "PE", "QC", "SK", "NT", "NU", "YT" };

        public static Faker<PhysicalAddress> PhysicalAddressFaker = new Faker<PhysicalAddress>()
                                .RuleFor(a => a.CountryCode, f => f.PickRandom(countries))
                                .RuleFor(a => a.ProvinceCode, TestUtils.RandomProvince())
                                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                                .RuleFor(a => a.City, f => f.Address.City())
                                .RuleFor(a => a.Postal, f => f.Address.ZipCode("?#?#?#"))
                                ;

        public static Faker<MailingAddress> MailingAddressFaker = new Faker<MailingAddress>()
                                .RuleFor(a => a.CountryCode, f => f.PickRandom(countries))
                                .RuleFor(a => a.ProvinceCode, TestUtils.RandomProvince())
                                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                                .RuleFor(a => a.City, f => f.Address.City())
                                .RuleFor(a => a.Postal, f => f.Address.ZipCode("?#?#?#"))
                                ;

        public static Faker<Certification> CertificationFaker = new Faker<Certification>()
                                .RuleFor(c => c.CollegeCode, f => f.Random.Short(1, 5))
                                .RuleFor(c => c.LicenseNumber, f => f.Random.Int(100000, 999999).ToString().Substring(1))
                                .RuleFor(c => c.LicenseCode, f => f.Random.Short(1, 4))
                                .RuleFor(c => c.RenewalDate, f => f.Date.Future(1))
                                .RuleFor(c => c.PracticeCode, f => f.Random.Short(1, 4))
                                ;

        public static Faker<Job> JobFaker = new Faker<Job>()
                                .RuleFor(j => j.Title, f => f.Random.Word())
                                ;

        public static Faker<Organization> OrganizationFaker = new Faker<Organization>()
                                .RuleFor(o => o.OrganizationTypeCode, f => f.Random.Short(1, 2))
                                ;

        public static Faker<EnrolmentStatus> EnrolmentStatusFaker = new Faker<EnrolmentStatus>()
              .RuleFor(es => es.StatusCode, f => Status.IN_PROGRESS_CODE)
              .RuleFor(es => es.Status, f => new Status { Code = Status.IN_PROGRESS_CODE, Name = "In Progress" })
              .RuleFor(es => es.StatusDate, f => DateTime.Now)
              .RuleFor(es => es.PharmaNetStatus, f => false)
              ;

        public static Faker<Enrollee> EnrolleeFaker = new Faker<Enrollee>()
                                .RuleFor(e => e.UserId, f => Guid.NewGuid())
                                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                                .RuleFor(e => e.MiddleName, f => f.Name.FirstName())
                                .RuleFor(e => e.LastName, f => f.Name.LastName())
                                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                                .RuleFor(e => e.PhysicalAddress, f => PhysicalAddressFaker.Generate())
                                .RuleFor(e => e.MailingAddress, f => MailingAddressFaker.Generate())
                                .RuleFor(e => e.Certifications, f => CertificationFaker.Generate(2))
                                .RuleFor(e => e.DeviceProviderNumber, TestUtils.RandomDeviceProviderNumber())
                                .RuleFor(e => e.IsInsulinPumpProvider, f => f.Random.Bool())
                                .RuleFor(e => e.Jobs, f => JobFaker.Generate(2))
                                .RuleFor(e => e.HasConviction, f => f.Random.Bool())
                                .RuleFor(e => e.HasConvictionDetails, f => f.Lorem.Paragraphs(2))
                                .RuleFor(e => e.HasRegistrationSuspended, f => f.Random.Bool())
                                .RuleFor(e => e.HasRegistrationSuspendedDetails, f => f.Lorem.Paragraphs(2))
                                .RuleFor(e => e.HasDisciplinaryAction, f => f.Random.Bool())
                                .RuleFor(e => e.HasDisciplinaryActionDetails, f => f.Lorem.Paragraphs(2))
                                .RuleFor(e => e.HasPharmaNetSuspended, f => f.Random.Bool())
                                .RuleFor(e => e.HasPharmaNetSuspendedDetails, f => f.Lorem.Paragraphs(2))
                                .RuleFor(e => e.Organizations, f => OrganizationFaker.Generate(2))
                                .RuleFor(e => e.EnrolmentStatuses, f => EnrolmentStatusFaker.Generate(1))
                                ;

        public static string RandomProvince(string[] excluded = null)
        {
            if (excluded == null)
            {
                excluded = new string[0];
            }
            return new Faker().PickRandom(provinces.Except(excluded));
        }

        public static string RandomDeviceProviderNumber()
        {
            return new Faker().Random.Int(100000, 999999).ToString().Substring(1);
        }

        public static void AddAdminRoleToUser(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(ClaimTypes.Role, PrimeConstants.PRIME_ADMIN_ROLE));
        }

        public static void RemoveAdminRoleFromUser(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            var claim = (from c in user.Claims
                         where c.Value == PrimeConstants.PRIME_ADMIN_ROLE
                         select c).Single();
            identity.RemoveClaim(claim);
        }

        public static int? CreateEnrollee(ApiDbContext apiDbContext, HttpContextAccessor httpContext, IAutomaticAdjudicationService automaticAdjudicationService, IEmailService emailService)
        {
            return new DefaultEnrolleeService(apiDbContext, httpContext, automaticAdjudicationService, emailService).CreateEnrolleeAsync(TestUtils.EnrolleeFaker.Generate()).Result;
        }

        public static Enrollee GetEnrolleeById(ApiDbContext apiDbContext, HttpContextAccessor httpContext, IAutomaticAdjudicationService automaticAdjudicationService, int enrolmentId, IEmailService emailService)
        {
            return new DefaultEnrolleeService(apiDbContext, httpContext, automaticAdjudicationService, emailService).GetEnrolleeAsync(enrolmentId).Result;
        }

        public static void InitializeDbForTests(ApiDbContext db)
        {
            // db.Enrollees.AddRange(EnrolleeFaker.Generate(5));

            if (!db.Set(typeof(College)).Any())
            {
                db.AddRange(new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91" });
                db.AddRange(new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1" });
                db.AddRange(new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96" });
                db.AddRange(new College { Code = 4, Name = "None", Prefix = null });
            }

            if (!db.Set(typeof(License)).Any())
            {
                db.AddRange(new License { Code = 1, Name = "Full - General" });
                db.AddRange(new License { Code = 2, Name = "Full - Pharmacist" });
                db.AddRange(new License { Code = 3, Name = "Full - Specialty" });
                db.AddRange(new License { Code = 4, Name = "Registered Nurse" });
                db.AddRange(new License { Code = 5, Name = "Temporary Registered Nurse" });
            }

            if (!db.Set(typeof(CollegeLicense)).Any())
            {
                db.AddRange(new CollegeLicense { CollegeCode = 1, LicenseCode = 2 });
                db.AddRange(new CollegeLicense { CollegeCode = 1, LicenseCode = 3 });
                db.AddRange(new CollegeLicense { CollegeCode = 2, LicenseCode = 4 });
                db.AddRange(new CollegeLicense { CollegeCode = 2, LicenseCode = 5 });
                db.AddRange(new CollegeLicense { CollegeCode = 3, LicenseCode = 1 });
                db.AddRange(new CollegeLicense { CollegeCode = 3, LicenseCode = 5 });
            }

            if (!db.Set(typeof(Practice)).Any())
            {
                db.AddRange(new Practice { Code = 1, Name = "Remote Practice" });
                db.AddRange(new Practice { Code = 2, Name = "Reproductive Care" });
                db.AddRange(new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" });
                db.AddRange(new Practice { Code = 4, Name = "None" });
            }

            if (!db.Set(typeof(JobName)).Any())
            {
                db.AddRange(new JobName { Code = 1, Name = "Medical Office Assistant" });
                db.AddRange(new JobName { Code = 2, Name = "Pharmacy Assistant" });
                db.AddRange(new JobName { Code = 3, Name = "Registration Clerk" });
                db.AddRange(new JobName { Code = 4, Name = "Ward Clerk" });
            }

            if (!db.Set(typeof(OrganizationType)).Any())
            {
                db.AddRange(new OrganizationType { Code = 1, Name = "Health Authority" });
                db.AddRange(new OrganizationType { Code = 2, Name = "Pharmacy" });
            }

            if (!db.Set(typeof(Status)).Any())
            {
                db.AddRange(new Status { Code = Status.IN_PROGRESS_CODE, Name = "In Progress" });
                db.AddRange(new Status { Code = Status.SUBMITTED_CODE, Name = "Submitted" });
                db.AddRange(new Status { Code = Status.APPROVED_CODE, Name = "Adjudicated/Approved" });
                db.AddRange(new Status { Code = Status.DECLINED_CODE, Name = "Declined" });
                db.AddRange(new Status { Code = Status.ACCEPTED_TOS_CODE, Name = "Accepted TOS (Terms of Service)" });
                db.AddRange(new Status { Code = Status.DECLINED_TOS_CODE, Name = "Declined TOS (Terms of Service)" });
            }

            if (!db.Set(typeof(Country)).Any())
            {
                db.AddRange(new Country { Code = "CA", Name = "Canada" });
            }

            if (!db.Set(typeof(Province)).Any())
            {
                db.AddRange(new Province { Code = "AB", Name = "Alberta" });
                db.AddRange(new Province { Code = "BC", Name = "British Columbia" });
                db.AddRange(new Province { Code = "MB", Name = "Manitoba" });
                db.AddRange(new Province { Code = "NB", Name = "New Brunswick" });
                db.AddRange(new Province { Code = "NL", Name = "Newfoundland and Labrador" });
                db.AddRange(new Province { Code = "NS", Name = "Nova Scotia" });
                db.AddRange(new Province { Code = "ON", Name = "Ontario" });
                db.AddRange(new Province { Code = "PE", Name = "Prince Edward Island" });
                db.AddRange(new Province { Code = "QC", Name = "Quebec" });
                db.AddRange(new Province { Code = "SK", Name = "Saskatchewan" });
                db.AddRange(new Province { Code = "NT", Name = "Northwest Territories" });
                db.AddRange(new Province { Code = "NU", Name = "Nunavut" });
                db.AddRange(new Province { Code = "YT", Name = "Yukon" });
            }

            if (!db.Set(typeof(StatusReason)).Any())
            {
                db.AddRange(new StatusReason { Code = 1, Name = "Automatic" });
                db.AddRange(new StatusReason { Code = 2, Name = "Manual" });
                db.AddRange(new StatusReason { Code = 3, Name = "Name Discrepancy" });
                db.AddRange(new StatusReason { Code = 4, Name = "Not in PharmaNet" });
                db.AddRange(new StatusReason { Code = 5, Name = "Insulin Pump Provider" });
                db.AddRange(new StatusReason { Code = 6, Name = "Licence Class" });
                db.AddRange(new StatusReason { Code = 7, Name = "Self Declaration" });
                db.AddRange(new StatusReason { Code = 8, Name = "Contact address or Identity Address Out of British Columbia" });
            }

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApiDbContext db)
        {
            // db.Enrollees.RemoveRange(db.Enrollees);
            InitializeDbForTests(db);
        }


        public static StringContent GetStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        public static async Task<string> GetBodyFromResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public static T DeserializeBody<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return DeserializeBody<T>(await GetBodyFromResponse(response));
        }

        public static void DetachAllEntities(ApiDbContext _dbContext)
        {
            var changedEntriesCopy = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }

        public static BearerTokenBuilder TokenBuilder()
        {
            return new BearerTokenBuilder()
                        .ForAudience(TestAuthorizationConstants.Audience)
                        .IssuedBy(TestAuthorizationConstants.Issuer)
                        .WithSigningCertificate(EmbeddedResourceReader.GetCertificate(TestAuthorizationConstants.CertificatePassword));
        }

        public static HttpRequestMessage CreateRequest(
            HttpMethod method,
            string requestUri,
            Guid subject)
        {
            return CreateRequest<string>(method, requestUri, subject, null);
        }

        public static HttpRequestMessage CreateAdminRequest(
            HttpMethod method,
            string requestUri,
            Guid subject)
        {
            return CreateAdminRequest<string>(method, requestUri, subject, null);
        }

        public static HttpRequestMessage CreateRequest<T>(
            HttpMethod method,
            string requestUri,
            Guid subject,
            T payload)
        {
            // create a request with an AUTH token
            var request = new HttpRequestMessage(method, requestUri);
            var _token = TestUtils.TokenBuilder()
                .ForAudience(Startup.StaticConfig["Jwt:Audience"])
                .ForSubject(subject.ToString())
                .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ENROLLEE_ROLE)
                .WithClaim(PrimeConstants.ASSURANCE_LEVEL_CLAIM_TYPE, "3")
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            if (payload != null)
            {
                request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            }

            return request;
        }

        public static HttpRequestMessage CreateAdminRequest<T>(
            HttpMethod method,
            string requestUri,
            Guid subject,
            T payload)
        {
            var request = CreateRequest<T>(method, requestUri, subject, payload);

            var audience = System.Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            if (audience == null)
            {
                audience = Startup.StaticConfig["Jwt:Audience"];
            }

            // replace the token - with an admin version of the token
            var _token = TestUtils.TokenBuilder()
                 .ForAudience(audience)
                 .ForSubject(subject.ToString())
                 .WithClaim(ClaimTypes.Role, PrimeConstants.PRIME_ADMIN_ROLE)
                 .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            return request;
        }

    }
}
