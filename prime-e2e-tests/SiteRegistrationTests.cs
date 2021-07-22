using System;
using Bogus;
using Bogus.DataSets;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestPrimeE2E.SiteRegistration
{
    public class SiteRegistrationTests : BaseTest
    {
        private Person _contact = new Person();
        private Name _name = new Name();
        private Company _company = new Company();
        private Address _address = new Address();

        [SetUp]
        public void Init()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _driver.Navigate().GoToUrl(TestParameters.SiteRegistrationUrl);

            LoginWithBCSC();
        }

        [TearDown]
        public void CleanUp()
        {
            //            _driver.Quit();
        }

        [Test]
        public void SiteRegInitial()
        {
            //Collection of Personal Information Notice
            Assert.AreEqual("Collection of Personal Information Notice", _driver.FindPatiently("//h1[@class='mb-4']").Text);
            _driver.TakeScreenshot("Collection_Notice");
            ClickButton("Next");

            // check if org information is already registered,
            if (_driver.FindPatiently("//h1[@class='mb-4']").Text == "Site Management")
            {
                ClickButton("Add Site");
            }
            else
            {
                //Signing Authority Information
                Assert.AreEqual("PharmaNet Site Registration", _driver.FindPatiently("//h1[@class='mb-4']").Text);
                // Wait for page to fully load
                _driver.FindPatiently("//input[@formControlName='phone']");
                Assert.AreEqual("Signing Authority Information", _driver.FindPatiently("//h2[@class='title']").Text);
                TypeIntoField("Job Title", _name.JobTitle());
                TypeIntoField("Phone Number", "5555555555");
                TypeIntoField("Email", _contact.Email);
                TypeIntoField("Mobile Phone (Optional)", "5555555555");
                TypeIntoField("Fax (Optional)", "5555555555");
                _driver.TakeScreenshot("Signing_Authority_Information");
                ClickButton("Save and Continue");

                _driver.FindTextPatiently("or claim an existing Organization");
                Assert.AreEqual("Claim Organization", _driver.FindPatiently("//h2[@class='title']").Text);
                // Choosing to create organization
                ClickButton("Continue");

                //Organization Information
                TypeIntoField("Organization Name (Legal Entity Operating Site)", "ROGERS");
                TypeIntoField("Doing Business As (Optional)", _company.CompanyName());
                _driver.TakeScreenshot("Organization_Information");
                ClickButton("Save and Continue");
            }

            //Care setting
            // choose private community health practice
            SelectDropdownItem("careSettingCode", "Community Pharmacy");
            // pick vendor
            _driver.FindPatiently("//mat-radio-group[@formcontrolname='vendorCode']//label[div[contains(text(), 'BDM')]]").Click();
            _driver.TakeScreenshot("Care_Setting");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("//mat-radio-button[label/div[contains(text(), 'WinRx')]]", 2, Keys.Enter);

            //site business licence
            _driver.FindPatiently("//input[@type='file']").SendKeys(TestParameters.BusinessLicencePath);
            _driver.FindPatiently("//*[contains(text(), 'Upload complete')]");
            // Input business licence expiry date
            PickDate("//input[@formcontrolname='businessLicenceExpiry']", "2023", "JUL", "22");
            TypeIntoField("Site Name (Doing Business As)", _company.CompanyName());
            var siteId = _driver.FindPatiently("//input[@formcontrolname='pec']");
            siteId.Clear();
            siteId.SendKeys(GeneratePecLikeString());
            _driver.TakeScreenshot("Site_Business_Licence");
            ClickButton("Save and Continue");

            //site address
            ClickButton("Add address manually");
            TypeIntoField("Street Address", _address.StreetAddress());
            TypeIntoField("City", _address.City());
            var postal = _driver.FindPatiently("//input[@formControlName='postal']");
            postal.Clear();
            postal.SendKeys(_address.ZipCode("?#? #?#"));
            _driver.TakeScreenshot("Site_Address");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("//input[@formcontrolname='postal']", 2, Keys.Enter);

            //hours of operation
            _driver.FindPatiently("//mat-slide-toggle").Click();
            _driver.TakeScreenshot("Hours_of_Operation");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("(//mat-slide-toggle)[7]", 2, Keys.Enter);

            //pharmanet administrator
            FillContactProfileForm();
            _driver.TakeScreenshot("Pharmanet_Administrator");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("//input[@formcontrolname='postal']", 2, Keys.Enter);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            //Privacy officer
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Privacy Officer')]")));
            FillContactProfileForm();
            _driver.TakeScreenshot("Privacy_Officer");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("//input[@formcontrolname='postal']", 2, Keys.Enter);

            //technical support contact
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Technical Support Contact')]")));
            FillContactProfileForm();
            _driver.TakeScreenshot("Technical_Support_Contact");
            // Need to tab over and click "Save and Continue" button
            _driver.TabAndInteract("//input[@formcontrolname='postal']", 2, Keys.Enter);

            // wait until page load complete
            wait.Until(d => d.Url.EndsWith("organization-agreement") || d.Url.EndsWith("site-review"));
            // The button right before the checkbox
            var lastButtonIndex = 8;
            //organization agreement
            if (_driver.Url.EndsWith("organization-agreement"))
            {
                // Cannot interact with checkbox directly and tabbing from slider to do it does not seem to tick off the checkbox
                // so tab from email link instead
                _driver.TabAndInteract("//a[@href='mailto:PRIMESupport@gov.bc.ca']", 3, Keys.Space);
                _driver.TakeScreenshot("Organization_Agreement");
                // click accept button
                _driver.TabAndInteract("//a[@href='mailto:PRIMESupport@gov.bc.ca']", 5, Keys.Enter);
                // confirm
                _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Accept Organization Agreement')]]").Click();
                //there are more buttons when first creating the organization
                lastButtonIndex = 13;
            }
            //information review
            // tick checkbox
            _driver.TabAndInteract($"(//button)[{lastButtonIndex}]", 1, Keys.Space);
            _driver.TakeScreenshot("Information_Review");
            // click accept button
            _driver.TabAndInteract($"(//button)[{lastButtonIndex}]", 2, Keys.Enter);
            // confirm
            _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Save Site')]]").Click();

            // Last page
            // wait until page load complete
            wait.Until(d => d.Url.EndsWith("organizations") || d.Url.EndsWith("next-steps"));
            _driver.TakeScreenshot(_driver.Url.EndsWith("organizations") ? "Site_Management" : "Submitted");
        }

        private void FillContactProfileForm()
        {
            FillFormField("firstName", _name.FirstName());
            FillFormField("lastName", _name.LastName());
            FillFormField("jobRoleTitle", _name.JobTitle());
            FillFormField("email", _contact.Email);
            FillFormField("phone", "5555555555");
            FillFormField("fax", "5555555555");
            FillFormField("smsPhone", "5555555555");

            // Need to tab over and click "Add address manually" button
            _driver.TabAndInteract("//input[@formcontrolname='smsPhone']", 4, Keys.Enter);
            // Need to tab over and activate Country drop-down
            _driver.TabAndInteract("//input[@formcontrolname='smsPhone']", 4, Keys.Enter);
            // Tab over and activate Province drop-down, selecting first item in Country drop-down in the process
            _driver.TabAndInteract("//input[@formcontrolname='smsPhone']", 5, Keys.Enter);
            // Tab over, selecting first item in Province drop-down in the process
            _driver.TabAndInteract("//input[@formcontrolname='smsPhone']", 5, Keys.Enter);
            FillFormField("street", _address.StreetAddress());
            FillFormField("city", _address.City());
            FillFormField("postal", GetCanadianPostalCode(_address));
        }
    }
}
