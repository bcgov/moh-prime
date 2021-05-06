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
            _driver.Quit();
        }

        [Test]
        public void SiteRegInitial()
        {
            //Collection of Personal Information Notice
            Assert.AreEqual("Collection of Personal Information Notice", _driver.FindPatiently("//h1[@class='mb-4']").Text);
            _driver.TakeScreenshot("Collection_Notice");
            ClickButton("Next");

            // check if org information is already registered,
            if (_driver.FindPatiently("//h1[@class='mb-4']").Text == "Site Management") {
                ClickButton("Add Site");
            }
            else {
                //Signing Authority Information
                Assert.AreEqual("PharmaNet Site Registration", _driver.FindPatiently("//h1[@class='mb-4']").Text);
                Assert.AreEqual("Signing Authority Information", _driver.FindPatiently("//h2[@class='title']").Text);
                TypeIntoField("Job Title", _name.JobTitle());
                TypeIntoField("Phone Number", "5555555555");
                TypeIntoField("Email", _contact.Email);
                TypeIntoField("Mobile Phone (Optional)", "5555555555");
                TypeIntoField("Fax (Optional)", "5555555555");
                _driver.TakeScreenshot("Signing_Authority_Information");
                ClickButton("Save and Continue");

                //Organization Information
                TypeIntoField("Organization Name (Legal Entity Operating Site)", "ROGERS");
                TypeIntoField("Doing Business As (Optional)", _company.CompanyName());
                _driver.TakeScreenshot("Organization_Information");
                ClickButton("Save and Continue");
            }

            //Care setting
            // choose private community health practice
            SelectDropdownItem("careSettingCode", "Private Community Health Practice");
            // pick vendor
            _driver.FindPatiently("//mat-radio-group[@formcontrolname='vendorCode']//label[div[contains(text(), 'CareConnect')]]").Click();
            _driver.TakeScreenshot("Care_Setting");
            ClickButton("Save and Continue");

            //site business licence
            _driver.FindPatiently("//input[@type='file']").SendKeys(TestParameters.BusinessLicencePath);
            _driver.FindPatiently("//*[contains(text(), 'Upload complete')]");
            TypeIntoField("Site Name (Doing Business As)", _company.CompanyName());
            var siteId = _driver.FindPatiently("//input[@formcontrolname='pec']");
            siteId.Clear();
            siteId.SendKeys(_company.CompanyName());
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
            ClickButton("Save and Continue");

            //hours of operation
            _driver.FindPatiently("//mat-slide-toggle").Click();
            _driver.TakeScreenshot("Hours_of_Operation");
            ClickButton("Save and Continue");

            //pharmanet administrator
            FillContactForm();
            _driver.TakeScreenshot("Pharmanet_Administrator");
            ClickButton("Save and Continue");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            //Privacy officer
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Privacy Officer')]")));
            FillContactForm();
            _driver.TakeScreenshot("Privacy_Officer");
            ClickButton("Save and Continue");

            //technical support contact
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Technical Support Contact')]")));
            FillContactForm();
            _driver.TakeScreenshot("Technical_Support_Contact");
            ClickButton("Save and Continue");

            // wait until page load complete
            wait.Until(d => d.Url.EndsWith("organization-agreement") || d.Url.EndsWith("site-review"));
            // The button right before the checkbox
            var lastButtonIndex = 8;
            //organization agreement
            if (_driver.Url.EndsWith("organization-agreement"))
            {
                // tick checkbox
                _driver.TabAndInteract("//mat-slide-toggle", 5, Keys.Space);
                _driver.TakeScreenshot("Organization_Agreement");
                // click accept button
                _driver.TabAndInteract("//mat-slide-toggle", 7, Keys.Enter);
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

        private void FillContactForm()
        {
            FillFormField("firstName", _name.FirstName());
            FillFormField("lastName", _name.LastName());
            FillFormField("jobRoleTitle", _name.JobTitle());
            FillFormField("email", _contact.Email);
            FillFormField("phone", "5555555555");
            FillFormField("fax", "5555555555");
            FillFormField("smsPhone", "5555555555");
        }
    }
}
