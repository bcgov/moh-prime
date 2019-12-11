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

        public void Send(string from, string to, string subject, string body)
        {
            throw new System.NotImplementedException();
        }

        public void SendReminderEmail(Enrollee enrollee)
        {
            throw new System.NotImplementedException();
        }
    }
}
