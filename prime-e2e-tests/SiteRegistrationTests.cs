using System;
using System.IO;
using Bogus;
using Bogus.DataSets;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            _driver.Navigate().GoToUrl(TestParameters.SiteRegistrationUrl);

            LoginWithBCSC();
        }

        [TearDown]
        public void CleanUp()
        {
            //_driver.Quit();
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
                ClickButton("Save and Continue");

                //Organization Information
                TypeIntoField("Organization Name (Legal Entity Operating Site)", "ROGERS");
                TypeIntoField("Doing Business As (Optional)", _company.CompanyName());
                ClickButton("Save and Continue");
            }

            //Care setting
            // choose private community health practice
            SelectDropdownItem("careSettingCode", "Private Community Health Practice");
            // pick vendor
            _driver.FindPatiently("//mat-radio-group[@formcontrolname='vendorCode']//label[div[contains(text(), 'CareConnect')]]").Click();
            ClickButton("Save and Continue");

            //site business licence
            _driver.FindPatiently("//input[@type='file']").SendKeys(TestParameters.BusinessLicencePath);
            _driver.FindPatiently("//*[contains(text(), 'Upload complete')]");
            TypeIntoField("Site Name (Doing Business As)", _company.CompanyName());
            var siteId = _driver.FindPatiently("//input[@formcontrolname='pec']");
            siteId.Clear();
            siteId.SendKeys(_company.CompanyName());
            ClickButton("Save and Continue");

            //site address
            ClickButton("Add address manually");
            TypeIntoField("Street Address", _address.StreetAddress());
            TypeIntoField("City", _address.City());
            var postal = _driver.FindPatiently("//input[@formControlName='postal']");
            postal.Clear();
            postal.SendKeys(_address.ZipCode("?#? #?#"));
            ClickButton("Save and Continue");

            //hours of operation
            _driver.FindPatiently("//mat-slide-toggle").Click();
            ClickButton("Save and Continue");

            //pharmanet administrator
            FillContactForm();
            ClickButton("Save and Continue");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            //Privacy officer
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Privacy Officer')]")));
            FillContactForm();
            ClickButton("Save and Continue");

            //technical support contact
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Technical Support Contact')]")));
            FillContactForm();
            ClickButton("Save and Continue");

            //organization agreement
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Organization Agreement')]")));
            // tick checkbox
            _driver.TabAndInteract("//mat-slide-toggle", 5, Keys.Space);
            // click accept button
            _driver.TabAndInteract("//mat-slide-toggle", 7, Keys.Enter);
            // confirm
            _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Accept Organization Agreement')]]").Click();

            //information review
            wait.Until(ExpectedConditions.ElementExists(
                By.XPath("//h2[@class='title' and contains(text(),'Information Review')]")));
            // tick checkbox
            _driver.TabAndInteract("//button", 13, Keys.Space);
            // click accept button
            _driver.TabAndInteract("//button", 14, Keys.Enter);
            // confirm
            _driver.FindPatiently("//app-confirm-dialog/mat-dialog-actions/button[span[contains(text(), 'Save Site')]]").Click();
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
