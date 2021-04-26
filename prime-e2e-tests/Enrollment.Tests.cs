using System;
using System.Globalization;
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
            _driver.Navigate().GoToUrl(TestParameters.EnrollmentUrl);

            LoginWithBCSC();

            // Perform enrollment

            Assert.AreEqual("Collection of Personal Information Notice", _driver.FindPatiently("//h1[@class='mb-4']").Text);
            _driver.TakeScreenshot("Collection_Notice");
            ClickButton("Next");

            Assert.AreEqual("Enrollee Information", _driver.FindPatiently("//h2[@class='title'][1]").Text);
            TypeIntoField("Phone Number", "7805551234");
            TypeIntoField("Email", "a@b.ca");
            _driver.TakeScreenshot("Enrollee_Demographics_Completed");
            ClickButton("Save and Continue");

            // Note that `Text` returns "add Add Additional Care Setting"
            Assert.IsTrue(_driver.FindPatiently("(//span[@class='mat-button-wrapper'])[2]").Text.Contains("Add Additional Care Setting"));
            // To ensure that the new page is loaded, we search for the unique widget (i.e. "Add Additional Care Setting") BEFORE verifying the title of current page
            Assert.AreEqual("Care Setting", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            _driver.FindPatiently("//div[contains(@class,'mat-select-value')]").Click();
            _driver.FindPatiently("//span[@class='mat-option-text' and contains(text(), 'Community Pharmacy')]").Click();
            _driver.TakeScreenshot("Care_Setting_Completed");
            ClickButton("Save and Continue");

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
            TypeIntoField("CPSID Number", "20101");
            // TODO: Why does 'Keep Changes and Continue' pop up without Sleep?
            System.Threading.Thread.Sleep(1000);
            // TODO: Common function given a title
            _driver.TakeScreenshot("College_Licence_Information_Completed");
            ClickButton("Save and Continue");




        }


        [TearDown]
        public void TestTearDown()
        {
//            _driver.Quit();
        }
    }
}
