using NUnit.Framework;

namespace TestPrimeE2E.Admin
{
    public class AdminTests : BaseTest
    {
        [Test]
        public void ApproveEnrollment()
        {
            _driver.Navigate().GoToUrl(TestParameters.AdminUrl);

            LoginWithIdirAccount();

            string expectedTitle = "Enrollees";
            VerifyAdminPageTitle(expectedTitle);

            // TODO: Refactor into BaseTest function?
            var testEnrolleeNum = int.Parse(TestParameters.BcscId.Substring("PRIMET0".Length));
            var enrolleeName = "PRIMET " + NumberToWords(testEnrolleeNum).ToUpper();

            // Click on triple vertical dots for the row of target enrollee
            _driver.FindPatiently($"//tr[td[contains(text(), '{enrolleeName}')]]/td/button/span/mat-icon[contains(text(), 'more_vert')]").Click();
            CheckLogThenScreenshot(expectedTitle);
            // Go to Overview
            _driver.FindPatiently("//button[span[contains(text(), 'Overview')]]").Click();


            _driver.FindPatiently("//h2[contains(text(), 'Self-declaration')]");
            expectedTitle = "Overview";
            VerifyAdminPageTitle(expectedTitle);
            CheckLogThenScreenshot(expectedTitle);
        }
    }
}
