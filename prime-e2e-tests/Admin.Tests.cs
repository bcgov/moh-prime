using Bogus;
using Bogus.DataSets;
using NUnit.Framework;
using OpenQA.Selenium;

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
            ClickHamburgerMenuInTable(enrolleeName);
            CheckLogThenScreenshot(expectedTitle);
            // Go to Overview
            _driver.FindPatiently("//button[span[contains(text(), 'Overview')]]").Click();


            _driver.FindPatiently("//h2[contains(text(), 'Self-declaration')]");
            expectedTitle = "Overview";
            VerifyAdminPageTitle(expectedTitle);
            CheckLogThenScreenshot(expectedTitle);

            // TODO: Needs more work ...
        }


        [Test]
        public void ApproveRejectUnrejectSiteRegistration()
        {
            _driver.Navigate().GoToUrl(TestParameters.AdminUrl);

            LoginWithIdirAccount();

            // TODO: Eliminate Sleep which sometimes seems needed before can move onto Site Registrations ... loading Enrollees?
            System.Threading.Thread.Sleep(2000);

            // Click on left menu item "Site Registrations"
            _driver.FindPatiently("(//mat-list-item/div)[2]").Click();

            // Click on tab "Community Pharmacies"
            _driver.FindPatiently("(//div[@role='tab'])[2]").Click();

            // TODO: Needs more work ...
        }


        [Test]
        public void EnterHealthAuthorityOrgInfo()
        {
            _driver.Navigate().GoToUrl(TestParameters.AdminUrl);

            LoginWithIdirAccount();

            // TODO: Eliminate Sleep which sometimes seems needed before can move onto Site Registrations ... loading Enrollees?
            System.Threading.Thread.Sleep(2000);

            // Click on left menu item "Site Registrations"
            _driver.FindPatiently("(//mat-list-item/div)[2]").Click();

            // Click on tab "Health Authorities"
            _driver.FindPatiently("(//div[@role='tab'])[3]").Click();

            // Click on triple vertical dots for the row of target Health Authority
            ClickHamburgerMenuInTable(TestParameters.HealthAuthority);
            ClickHamburgerMenuItem("Add Organization Information");

            ClickButton("Add Organization Information");

            string expectedTitle = "Health Authority Care Types";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
            TypeIntoField("Care Type", "Acute Care");
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            _driver.FindTextPatiently("Identify the Vendors");
            expectedTitle = "Vendors";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
            SelectDropdownItem("vendor", "BDM");
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            _driver.FindTextPatiently("for the health authority Privacy Office");
            Person privacyOfficer = new Person();
            // TODO: Is there something more appropriate than using a Person type?
            var privacyOfficeAsPerson = new Person();
            var privacyOfficeAddress = new Address();
            expectedTitle = "Privacy Office";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
            // Privacy Office fields
            FillFormField("email", privacyOfficeAsPerson.Email);
            FillFormField("phone", GetVancouverPhoneNum(privacyOfficeAsPerson));
            ClickButton("Add address manually");
            SelectDropdownItem("countryCode", "Canada");
            SelectDropdownItem("provinceCode", "British Columbia");
            FillFormField("street", privacyOfficeAddress.StreetAddress());
            FillFormField("city", privacyOfficeAddress.City());
            FillFormField("postal", GetCanadianPostalCode(privacyOfficeAddress));
            // Privacy Officer fields
            FillFormField("firstName", privacyOfficer.FirstName);
            FillFormField("lastName", privacyOfficer.LastName);
            // Privacy Officer Contact Information fields
            FillFormField("email", privacyOfficer.Email, "app-contact-profile-form");
            FillFormField("phone", GetVancouverPhoneNum(privacyOfficer), "app-contact-profile-form");
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            if (_driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Add Technical Support')]") == null)
            {
                _driver.FindTextPatiently("Provide information for a Technical Support Contact");
                Person techSupportPerson = new Person();
                var techSupportNames = new Name();
                var techSupportAddress = new Address();
                expectedTitle = "Technical Support";
                Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
                FillFormField("firstName", techSupportPerson.FirstName);
                FillFormField("lastName", techSupportPerson.LastName);
                FillFormField("jobRoleTitle", techSupportNames.JobTitle());
                FillFormField("email", techSupportPerson.Email);
                FillFormField("phone", GetVancouverPhoneNum(techSupportPerson));
                // Expected to tab 3 times to interact with "Add address manually" button, but instead 4 times works
                _driver.TabAndInteract(GetFormFieldXPath("phone"), 4, Keys.Enter);
                EnterAddress(techSupportAddress);
                CheckLogThenScreenshot(expectedTitle);
                ClickButton("Save and Continue");
            }

            // On "Technical Support Contact(s)" screen
            // TODO: Why is button found but do not advance to next screen without Sleep?
            System.Threading.Thread.Sleep(2000);
            ClickButton("Continue");

            if (_driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Add PharmaNet Administrator')]") == null)
            {
                _driver.FindTextPatiently("Provide information for a PharmaNet Administrator");
                Person pharmanetAdmin = new Person();
                var pharmanetAdminNames = new Name();
                var pharmanetAdminAddress = new Address();
                expectedTitle = "PharmaNet Administrator";
                Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
                FillFormField("firstName", pharmanetAdmin.FirstName);
                FillFormField("lastName", pharmanetAdmin.LastName);
                FillFormField("jobRoleTitle", pharmanetAdminNames.JobTitle());
                FillFormField("email", pharmanetAdmin.Email);
                FillFormField("phone", GetVancouverPhoneNum(pharmanetAdmin));
                // Expected to tab 3 times to interact with "Add address manually" button, but instead 4 times works
                _driver.TabAndInteract(GetFormFieldXPath("phone"), 4, Keys.Enter);
                EnterAddress(pharmanetAdminAddress);
                CheckLogThenScreenshot(expectedTitle);
                ClickButton("Save and Continue");
            }

            // On "PharmaNet Administrator(s)" screen
            // TODO: Why is button found but do not advance to next screen without Sleep?
            System.Threading.Thread.Sleep(2000);
            ClickButton("Continue");
        }
    }
}
