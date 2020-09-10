using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class EmailServiceMock : BaseMockService, IEmailService
    {
        public EmailServiceMock() : base()
        { }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class
        }

        public Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            throw new System.NotImplementedException();
        }

        public Task SendReminderEmailAsync(Enrollee enrollee)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetPharmaNetProvisionerEmailAsync(string pharmaNetVendor)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task SendSiteRegistrationAsync(Site site)
        {
            throw new System.NotImplementedException();
        }

        public Task SendRemoteUsersUpdatedAsync(Site site)
        {
            throw new System.NotImplementedException();
        }

        public Task SendRemoteUsersNotificationAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            throw new System.NotImplementedException();
        }
    }
}
