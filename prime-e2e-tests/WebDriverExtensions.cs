using System;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestPrimeE2E 
{
    /// <summary>
    /// Only generic (non-PRIME) code should belong here
    /// </summary>
    public static class WebDriverExtensions
    {
        public static IWebElement FindPatiently(this IWebDriver driver, string xPath) 
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            Func<IWebDriver, IWebElement> waitForElement = new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
            {
                Console.WriteLine($"Trying to find at {xPath} ...");
                IWebElement element = Web.FindElement(By.XPath(xPath));
                return element;
            });
            return wait.Until(waitForElement);
        }


        // TODO: Don't duplicate code shared with FindPatiently method
        public static IWebElement FindPatientlyById(this IWebDriver driver, string id) 
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            Func<IWebDriver, IWebElement> waitForElement = new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
            {
                Console.WriteLine($"Trying to find '{id}' ...");
                IWebElement element = Web.FindElement(By.Id(id));
                return element;
            });
            return wait.Until(waitForElement);
        }


        public static IWebElement FindPossibleStaleById(this IWebDriver driver, string id) 
        {
            // TODO: Eliminate need for Sleep
            System.Threading.Thread.Sleep(1000);
            try
            {
                return FindPatientlyById(driver, id);
            }
            catch (Exception e) when (e is StaleElementReferenceException)
            {
                Console.WriteLine($"Got {e.Message} finding '{id}' ... trying once again.");
                return FindPatientlyById(driver, id);
            }
        }


        // TODO: Parameter to permit saving to alternate folder rather than working directory 
        public static void TakeScreenshot(this IWebDriver driver, string pageIdentifier) 
        {
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            image.SaveAsFile($"{DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo)}_{pageIdentifier}.png", ScreenshotImageFormat.Png);
        }
    }
}