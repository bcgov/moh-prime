using System;
using System.Globalization;
using System.IO;
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
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                Func<IWebDriver, IWebElement> waitForElement = new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
                {
                    Console.WriteLine($"Trying to find '{id}' ...");
                    IWebElement element = Web.FindElement(By.Id(id));
                    return element;
                });
                return wait.Until(waitForElement);
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException e)
            {
                Console.WriteLine(e);
                return null;
            }
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


        public static void TakeScreenshot(this IWebDriver driver, string pageIdentifier, string archiveFolder = null)
        {
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            // Save to archive folder if provided, otherwise save to working directory
            archiveFolder = (archiveFolder != null ? $"{archiveFolder}{Path.DirectorySeparatorChar}" : "");
            image.SaveAsFile($"{archiveFolder}{DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo)}_{pageIdentifier}.png", ScreenshotImageFormat.Png);
        }


        /// <summary>
        /// This method may be useful to interact with elements that can't be clicked on (e.g. getting OpenQA.Selenium.ElementClickInterceptedException instead)
        /// or don't respond to being clicked on through Selenium API.
        /// </summary>
        /// <param name="xPathToStartElement">XPath expression to obtain reference to "start" page element (before "end" element in the tab sequence order)</param>
        /// <param name="numTabsToEndElement">Number of Tab presses to get to the "end" element</param>
        /// <param name="keyToInteractAtEnd">Which keyboard key to press to interact with the "end" element (see <c>OpenQA.Selenium.Keys</c>)</param>
        public static void TabAndInteract(this IWebDriver driver, string xPathToStartElement, int numTabsToEndElement, string keyToInteractAtEnd)
        {
            IWebElement currentElement = FindPatiently(driver, xPathToStartElement);
            for (int i = 0; i < numTabsToEndElement; i++)
            {
                currentElement.SendKeys(Keys.Tab);
                currentElement = driver.SwitchTo().ActiveElement();
            }
            currentElement.SendKeys(keyToInteractAtEnd);
        }
    }
}
