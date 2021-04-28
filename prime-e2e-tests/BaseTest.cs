using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestPrimeE2E
{
    /// <summary>
    /// Common code related to PRIME or Material UI library should belong here (rather than <c>WebDriverExtensions</c>)
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver _driver;


        [SetUp]
        public void TestSetup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }


        protected void LoginWithBCSC()
        {
            _driver.FindPatiently("//span[@class='mat-button-wrapper'][1]").Click();
            _driver.FindPatientlyById("image_virtual_device_div_id").Click();
            _driver.FindPossibleStaleById("csn").SendKeys(TestParameters.BcscId);
            _driver.FindPatientlyById("continue").Click();
            _driver.FindPatientlyById("passcode").SendKeys(TestParameters.BcscPassword);
            _driver.FindPatientlyById("btnSubmit").Click();
            _driver.TakeScreenshot("BCSC_SignIn_Completion");
            _driver.FindPatientlyById("btnSubmit").Click();
        }


        protected void TypeIntoField(string fieldId, string text)
        {
            var field = _driver.FindPatiently($"//input[@data-placeholder='{fieldId}']");
            field.Clear();
            field.SendKeys(text);
        }

        protected void ClickButton(string buttonLabel)
        {
            _driver.FindPatiently($"//span[@class='mat-button-wrapper' and contains(text(), '{buttonLabel}')]").Click();
        }

        protected void SelectItemInDropDown(string dropdownControlName, string itemName)
        {
            _driver.FindPatiently($"//mat-select[@formControlName='{dropdownControlName}']").Click();
            _driver.FindPatiently($"//mat-option/span[contains(.,'{itemName}')]").Click();
        }

        protected void FillFormField(string formControlName, string text)
        {
            var control = _driver.FindPatiently($"//input[@formControlName='{formControlName}']");
            control.Clear();
            control.SendKeys(text);
        }

    }
}
