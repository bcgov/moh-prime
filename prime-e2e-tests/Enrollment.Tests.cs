using System;
using System.Globalization;
using Bogus;
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

            // Perform enrollment

            string expectedTitle = "Collection of Personal Information Notice";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h1[@class='mb-4']").Text);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Next");

            expectedTitle = "Enrollee Information";
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[@class='title'][1]").Text);
            TypeIntoField("Phone Number", enrollee.Phone);
            TypeIntoField("Email", enrollee.Email);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            expectedTitle = "Care Setting";
            // Note that `Text` returns "add Add Additional Care Setting"
            Assert.IsTrue(_driver.FindPatiently("(//span[@class='mat-button-wrapper'])[2]").Text.Contains("Add Additional Care Setting"));
            // To ensure that the new page is loaded, we search for the unique widget (i.e. "Add Additional Care Setting") BEFORE verifying the title of current page
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            SelectDropdownItem("careSettingCode", "Community Pharmacy");
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            expectedTitle = "College Licence Information";
            _driver.FindPatiently("//mat-select[@formcontrolname='collegeCode']");
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            SelectDropdownItem("collegeCode", "College of Physicians and Surgeons of BC");
            SelectDropdownItem("licenseCode", "Full - Family");
            PickDate("2023", "MAR", "5");
            TypeIntoField("CPSID Number", "20101");
            // TODO: Why does 'Keep Changes and Continue' pop up without Sleep?
            System.Threading.Thread.Sleep(1000);
            CheckLogThenScreenshot(expectedTitle);
            ClickButton("Save and Continue");

            expectedTitle = "Self-declaration";
            _driver.FindPatiently("//mat-radio-group[@formcontrolname='hasRegistrationSuspended']");
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            ClickRadioButton("hasRegistrationSuspended", "No");
            ClickRadioButton("hasConviction", "No");
            // Need to Tab over to click 'No' radio button
            _driver.TabAndInteract("//mat-radio-group[@formcontrolname='hasConviction']//label[div[contains(text(), 'No')]]", 1, Keys.Space);
            ClickRadioButton("hasDisciplinaryAction", "No");
            CheckLogThenScreenshot(expectedTitle);
            // Need to Tab over to click 'Save and Continue' button
            _driver.TabAndInteract("//mat-radio-group[@formcontrolname='hasDisciplinaryAction']//label[div[contains(text(), 'No')]]", 2, Keys.Enter);

            expectedTitle = "Enrolment Review";
            _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Submit Enrolment')]");
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
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
