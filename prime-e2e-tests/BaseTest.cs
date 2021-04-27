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


        protected void SelectDropdownItem(string formControlName, string itemLabel)
        {
            _driver.FindPatiently($"//mat-select[@formcontrolname='{formControlName}']//div[contains(@class,'mat-select-value')]").Click();
            _driver.FindPatiently($"//span[@class='mat-option-text' and contains(text(), '{itemLabel}')]").Click();
        }


        /// <param name="month">Three character long, e.g. "MAR"</param>
        protected void PickDate(string year, string month, string dayOfMonth)
        {
            // TODO: Support more than one visible calendar control
            month = month.ToUpper();
            _driver.FindPatiently("//mat-datepicker-toggle//span[@class='mat-button-wrapper']").Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{year}')]").Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{month}')]").Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{dayOfMonth}')]").Click();
        }


        protected void ClickRadioButton(string formControlName, string radioButtonLabel)
        {
            _driver.FindPatiently($"//mat-radio-group[@formcontrolname='{formControlName}']//label[div[contains(text(), '{radioButtonLabel}')]]").Click();
        }
    }
}
