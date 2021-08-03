using System;
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

        private Faker faker = new Faker();


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
            var submitButton = _driver.FindPatientlyById("btnSubmit");
            // Sometimes screen is not displayed to user?
            if (submitButton != null)
            {
                submitButton.Click();
            }
        }


        protected void LoginWithIdirAccount()
        {
            ClickButton("IDIR Login");
            _driver.FindPatientlyById("user").SendKeys(TestParameters.IdirId);
            _driver.FindPatientlyById("password").SendKeys(TestParameters.IdirPassword);
            _driver.FindPatiently("//input[@name='btnSubmit']").Click();
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


        protected void ClickHamburgerMenuInTable(string uniqueTextOfRow)
        {
            _driver.FindPatiently($"//tr[td[contains(text(), '{uniqueTextOfRow}')]]/td/button/span/mat-icon[contains(text(), 'more_vert')]").Click();
        }


        protected void ClickHamburgerMenuItem(string menuItemLabel)
        {
            _driver.FindPatiently($"//button/span[contains(text(), '{menuItemLabel}')]").Click();
        }


        protected void SelectDropdownItem(string formControlName, string itemLabel)
        {
            _driver.FindPatiently($"//mat-select[@formcontrolname='{formControlName}' or @ng-reflect-name='{formControlName}']//div[contains(@class,'mat-select-value')]").Click();
            _driver.FindPatiently($"//span[@class='mat-option-text' and contains(text(), '{itemLabel}')]").Click();
        }


        /// <param name="month">Three character long, e.g. "MAR"</param>
        protected void PickDate(string xPathToDatePicker, string year, string month, string dayOfMonth)
        {
            month = month.ToUpper();
            _driver.FindPatiently(xPathToDatePicker).Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{year}')]").Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{month}')]").Click();
            _driver.FindPatiently($"//div[contains(@class, 'mat-calendar-body-cell-content') and contains(text(), '{dayOfMonth}')]").Click();
        }


        protected void ClickRadioButton(string formControlName, string radioButtonLabel)
        {
            _driver.FindPatiently($"//mat-radio-group[@formcontrolname='{formControlName}']//label[div[contains(text(), '{radioButtonLabel}')]]").Click();
        }


        /// <summary>
        /// Specifying the <c>ancestorElement</c> can disambiguate the desired control (if necessary)
        /// </summary>
        protected void FillFormField(string formControlName, string text, string ancestorElement = "")
        {
            if (!"".Equals(ancestorElement))
            {
                ancestorElement = "//" + ancestorElement;
            }
            var control = _driver.FindPatiently($"{ancestorElement}//input[@formControlName='{formControlName}']");
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
        /// to confirm the title of an Enrollment page.
        /// </summary>
        protected void VerifyEnrollmentPageTitle(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h2[contains(@class, 'title')]").Text);
        }


        /// <summary>
        /// Due to the structure of most pages, this method can usually be used
        /// to confirm the title of an Admin page.
        /// </summary>
        protected void VerifyAdminPageTitle(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, _driver.FindPatiently("//h1[contains(@class, 'mb-4')]").Text);
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


        protected string GeneratePecLikeString()
        {
            return faker.Random.String2(3).ToUpper();
        }


        protected IWebElement FindDropdownControl(string formControlName)
        {
            return _driver.FindPatiently($"//mat-select[@formcontrolname='{formControlName}']");
        }


        /// <summary>
        /// Credit:  https://stackoverflow.com/questions/2729752/converting-numbers-in-to-words-c-sharp
        /// </summary>
        protected string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        /// <summary>
        /// Works for some but not all screens
        /// </summary>
        protected void EnterAddress(Address address)
        {
            ClickButton("Add address manually");
            SelectDropdownItem("countryCode", "Canada");
            SelectDropdownItem("provinceCode", "British Columbia");
            FillFormField("street", address.StreetAddress());
            FillFormField("city", address.City());
            FillFormField("postal", GetCanadianPostalCode(address));
        }
    }
}
