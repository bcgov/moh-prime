using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
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
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
            _driver = new ChromeDriver(options);
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


        /// <summary>
        /// Where possible, use <c>FillFormField</c> instead, as a label is more likely to change
        /// than an internal <c>formControlName</c>.
        /// </summary>
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

        protected void FillFormField(string formControlName, string text)
        {
            var control = _driver.FindPatiently($"//input[@formControlName='{formControlName}']");
            control.Clear();
            control.SendKeys(text);
        }


        protected void CheckLogThenScreenshot(string pageTitle)
        {
            // See https://stackoverflow.com/questions/36455533/c-sharp-selenium-access-browser-log
            // The following works with Selenium.WebDriver version 4.0.0-beta2 but not the stable version 3.141.0
            // System.Collections.Generic.List<LogEntry> logs = _driver.Manage().Logs.GetLog(LogType.Browser).ToList();
            // foreach (LogEntry log in logs)
            // {
            //     Console.Error.WriteLine(log.Message);
            // }
            // if (logs.Count > 0)
            // {
            //     throw new Exception($"Received {logs.Count} error messages on the page '{pageTitle}'.");
            // }

            _driver.TakeScreenshot(String.Concat(pageTitle.Replace(' ', '_'), "_Completed"), TestParameters.ScreenshotsArchivePath);
        }


        /// <summary>
        /// Due to the structure of most pages, this method can usually be used
        /// to confirm the title of a page.
        /// </summary>
        protected void VerifyTitle(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
        }


        protected string GetVancouverPhoneNum(Person aPerson)
        {
            // Vancouver-like phone number
            return String.Concat("604", aPerson.Phone.Substring(3));
        }


        protected string GetCanadianPostalCode(Address anAddress)
        {
            return anAddress.ZipCode("?#? #?#");
        }


        protected IWebElement FindDropdownControl(string formControlName)
        {
            return _driver.FindPatiently($"//mat-select[@formcontrolname='{formControlName}']");
        }
    }
}
