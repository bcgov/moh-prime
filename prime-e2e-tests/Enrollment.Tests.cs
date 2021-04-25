using System;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestPrimeE2E.Enrollment
{
    public class EnrollmentTests
    {
        private IWebDriver _driver;


        [SetUp]
        public void TestSetup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        [Test]
        public void FullFamilyPhysician_HappyPath()
        {
            _driver.Navigate().GoToUrl(TestParameters.EnrollmentUrl);

            // Login
            _driver.FindPatiently("//span[@class='mat-button-wrapper'][1]").Click();
            _driver.FindPatientlyById("image_virtual_device_div_id").Click();
            _driver.FindPossibleStaleById("csn").SendKeys(TestParameters.BcscId);
            _driver.FindPatientlyById("continue").Click();
            _driver.FindPatientlyById("passcode").SendKeys(TestParameters.BcscPassword);
            _driver.FindPatientlyById("btnSubmit").Click();
            _driver.TakeScreenshot("BCSC_SignIn_Completion");
            _driver.FindPatientlyById("btnSubmit").Click();

            // Perform enrollment

            Assert.AreEqual("Collection of Personal Information Notice", _driver.FindPatiently("//h1[@class='mb-4']").Text);
            _driver.TakeScreenshot("Collection_Notice");
            _driver.FindPatiently("//span[@class='mat-button-wrapper']").Click();

            Assert.AreEqual("Enrollee Information", _driver.FindPatiently("//h2[@class='title'][1]").Text);
            var phoneNumberInput = _driver.FindPatiently("//input[@data-placeholder='Phone Number']");
            phoneNumberInput.Clear();
            phoneNumberInput.SendKeys("7805551234");
            var emailInput = _driver.FindPatiently("//input[@data-placeholder='Email']");
            emailInput.Clear();
            emailInput.SendKeys("a@b.ca");
            _driver.TakeScreenshot("Enrollee_Demographics_Completed");
            _driver.FindPatiently("//span[@class='mat-button-wrapper']").Click();

            // Note that `Text` returns "add Add Additional Care Setting"
            Assert.IsTrue(_driver.FindPatiently("(//span[@class='mat-button-wrapper'])[2]").Text.Contains("Add Additional Care Setting"));
            // To ensure that the new page is loaded, we search for the unique widget (i.e. "Add Additional Care Setting") BEFORE verifying the title of current page
            Assert.AreEqual("Care Setting", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            _driver.FindPatiently("//div[contains(@class,'mat-select-value')]").Click();
            _driver.FindPatiently("//span[@class='mat-option-text' and contains(text(), 'Community Pharmacy')]").Click();
            _driver.TakeScreenshot("Care_Setting_Completed");
            _driver.FindPatiently("(//span[@class='mat-button-wrapper'])[4]").Click();

            // TODO: Eliminate need for Sleep ... is there a XPath that will match on College Licence Information page but NOT on Care Setting page?
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual("College Licence Information", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            _driver.FindPatiently("(//div[contains(@class,'mat-select-value')])[1]").Click();
            _driver.FindPatiently("//span[@class='mat-option-text' and contains(text(), 'College of Physicians and Surgeons of BC')]").Click();
            _driver.FindPatiently("(//div[contains(@class,'mat-select-value')])[2]").Click();
            _driver.FindPatiently("//span[@class='mat-option-text' and contains(text(), 'Full - Family')]").Click();
            _driver.FindPatiently("(//span[@class='mat-button-wrapper'])[1]").Click();
            _driver.FindPatiently("//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '2023')]").Click();
            _driver.FindPatiently("//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), 'MAR')]").Click();
            _driver.FindPatiently("//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '5')]").Click();

            // var nextRenewalDateInput = _driver.FindPatiently("//input[@data-placeholder='Next Renewal Date']");
            // // nextRenewalDateInput.Clear();
            // nextRenewalDateInput.SendKeys("12 Oct 2028");

            // TODO: Refactor to common function?
            var cpsidNumberInput = _driver.FindPatiently("//input[@data-placeholder='CPSID Number']");
            cpsidNumberInput.Clear();
            cpsidNumberInput.SendKeys("20101");

            System.Threading.Thread.Sleep(1000);

            // TODO: Common function given a title
            _driver.TakeScreenshot("College_Licence_Information_Completed");
            // TODO: Common function given a button label
            _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Save and Continue')]").Click();
            // TODO: Why does this pop up?
            // _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Keep Changes and Continue')]").Click();
            // _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Save and Continue')]").Click();




        }


        [TearDown]
        public void TestTearDown()
        {
//            _driver.Quit();
        }
    }
}
