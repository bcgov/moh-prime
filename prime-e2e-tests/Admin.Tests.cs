using NUnit.Framework;

namespace TestPrimeE2E.Enrollment
{
    public class AdminTests : BaseTest
    {
        [Test]
        public void ApproveEnrollment()
        {
            _driver.Navigate().GoToUrl(TestParameters.AdminUrl);

            LoginWithIdirAccount();
        }
    }
}
