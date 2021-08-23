using System;
using System.Globalization;
using Bogus;
using Bogus.DataSets;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestPrimeE2E.Enrollment
{
    public class EnrollmentTests : BaseTest
    {
        [Test]
        public void FullFamilyPhysician_HappyPath()
        {
            Person enrollee = new Person();

            _driver.Navigate().GoToUrl(TestParameters.EnrollmentUrl);

            LoginWithBCSC();

            CompleteCollectionNoticePage();

            CompleteEnrolleeInformationPage(enrollee);

            CompleteCareSettingPage_CommunityPharmacy();

            CompleteCollegeLicencePage_FullFamilyPhysician();

            CompleteSelfDeclarationPage();

            CompleteEnrolmentReviewPage();
        }


        [Test]
        public void MedicalOfficeAssistant_HappyPath()
        {
            Person enrollee = new Person();

            _driver.Navigate().GoToUrl(TestParameters.EnrollmentUrl);

            LoginWithBCSC();

            CompleteCollectionNoticePage();

            CompleteEnrolleeInformationPage(enrollee);

            CompleteCareSettingPage_CommunityPharmacy();

            CompleteCollegeLicencePage_NoLicence();

            CompleteSiteInformationPage_MOA_SingleNonHealthAuthority();

            CompleteSelfDeclarationPage();

            CompleteEnrolmentReviewPage();

            // Next Steps
            ClickButton("Continue");

            // Terms of Access
            // Need to Tab over to check "I have read the PharmaNet User Terms of Access."
            _driver.TabAndInteract("//a[@href='mailto:PRIMESupport@gov.bc.ca']", 1, Keys.Space);
            // Need to Tab over to press "Accept Terms of Access" button
            _driver.TabAndInteract("//a[@href='mailto:PRIMESupport@gov.bc.ca']", 2, Keys.Enter);
            _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Accept Terms of Access')]]").Click();
        }


        private void CompleteCollectionNoticePage()
        {
            string expectedTitle = "Collection of Personal Information Notice";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h1[@class='mb-4']").Text);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Next");
        }


        private void CompleteEnrolleeInformationPage(Person enrollee)
        {
            string expectedTitle = "Enrollee Information";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[@class='title'][1]").Text);
            TypeIntoField("Phone Number", GetVancouverPhoneNum(enrollee));
            TypeIntoField("Email", enrollee.Email);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");
        }


        private void CompleteCareSettingPage_CommunityPharmacy()
        {
            string expectedTitle = "Care Setting";
            // Note that `Text` returns "add Add Additional Care Setting"
            Assert.IsTrue(_driver.FindPatiently("(//span[@class='mat-button-wrapper'])[2]").Text.Contains("Add Additional Care Setting"));
            // To ensure that the new page is loaded, we search for the unique widget (i.e. "Add Additional Care Setting") BEFORE verifying the title of current page
            VerifyEnrollmentPageTitle(expectedTitle);
            SelectDropdownItem("careSettingCode", "Community Pharmacy");
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");
        }


        private void CompleteCollegeLicencePage_FullFamilyPhysician()
        {
            string expectedTitle = "College Licence Information";
            FindDropdownControl("collegeCode");
            VerifyEnrollmentPageTitle(expectedTitle);
            SelectDropdownItem("collegeCode", "College of Physicians and Surgeons of BC");
            SelectDropdownItem("licenseCode", "Full - Family");
            PickDate("//mat-datepicker-toggle//span[@class='mat-button-wrapper']", "2023", "MAR", "5");
            TypeIntoField("CPSID Number", "20101");
            // TODO: Why does 'Keep Changes and Continue' pop up without Sleep?
            System.Threading.Thread.Sleep(1000);
            CheckLogThenScreenshot(expectedTitle);
            // Need to Tab over to click ''Save and Continue' button
            _driver.TabAndInteract(GetInputFieldXPath("CPSID Number"), 3, Keys.Enter);
        }


        private void CompleteCollegeLicencePage_NoLicence()
        {
            string expectedTitle = "College Licence Information";
            FindDropdownControl("collegeCode");
            VerifyEnrollmentPageTitle(expectedTitle);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");
        }


        private void CompleteSiteInformationPage_MOA_SingleNonHealthAuthority()
        {
            Address siteAddress = new Address();

            string expectedTitle = "Site Information";
            FindDropdownControl("provinceCode");
            VerifyEnrollmentPageTitle(expectedTitle);
            FillFormField("siteName", siteAddress.BuildingNumber());
            FillFormField("jobTitle", "Medical office assistant");
            FillFormField("street", siteAddress.StreetAddress());
            FillFormField("city", siteAddress.City());
            // Leave Province as default, British Columbia
            FillFormField("postal", GetCanadianPostalCode(siteAddress));
            CheckLogThenScreenshot(expectedTitle);
            // Need to Tab over to click ''Save and Continue' button
            _driver.TabAndInteract(GetFormFieldXPath("pec"), 3, Keys.Enter);
        }


        private void CompleteSelfDeclarationPage()
        {
            string expectedTitle = "Self-declaration";
            _driver.FindPatiently("//mat-radio-group[@formcontrolname='hasRegistrationSuspended']");
            VerifyEnrollmentPageTitle(expectedTitle);
            ClickRadioButton("hasRegistrationSuspended", "No");
            // Need to Tab over to click 'No' radio button for Has Conviction?
            _driver.TabAndInteract(GetRadioButtonXPath("hasRegistrationSuspended", "No"), 1, Keys.Space);
            // Need to Tab over to click 'No' radio button for "hasPharmaNetSuspended"
            _driver.TabAndInteract(GetRadioButtonXPath("hasRegistrationSuspended", "No"), 2, Keys.Space);
            ClickRadioButton("hasDisciplinaryAction", "No");
            CheckLogThenScreenshot(expectedTitle);
            // Need to Tab over to click 'Save and Continue' button
            _driver.TabAndInteract(GetRadioButtonXPath("hasDisciplinaryAction", "No"), 2, Keys.Enter);
        }


        private void CompleteEnrolmentReviewPage()
        {
            string expectedTitle = "Enrolment Review";
            _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Submit Enrolment')]");
            VerifyEnrollmentPageTitle(expectedTitle);
            // Need to Tab over to tick checkbox
            _driver.TabAndInteract("//button[@mattooltip='Edit Self-declaration']", 1, Keys.Space);
            // Need to Tab over to click 'Submit Enrolment' button
            _driver.TabAndInteract("//button[@mattooltip='Edit Self-declaration']", 2, Keys.Enter);
            CheckLogThenScreenshot(expectedTitle);
            _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Submit Enrolment')]]").Click();
        }


        [TearDown]
        public void TestTearDown()
        {
            //            _driver.Quit();
        }
    }
}
