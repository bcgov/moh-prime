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
            Console.WriteLine($"Trying to find at {xPath} ...");
            return FindPatientlyByX(driver, By.XPath(xPath));
        }


        public static IWebElement FindPatientlyById(this IWebDriver driver, string id)
        {
            Console.WriteLine($"Trying to find by {id} ...");
            return FindPatientlyByX(driver, By.Id(id));
        }


        private static IWebElement FindPatientlyByX(this IWebDriver driver, By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                Func<IWebDriver, IWebElement> waitForElement = new Func<IWebDriver, IWebElement>((IWebDriver Web) =>
                {
                    IWebElement element = Web.FindElement(locator);
                    return element;
                });
                return wait.Until(waitForElement);
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException e)
            {
                Console.WriteLine(e.InnerException.Message);
                return null;
            }
        }


        /// <summary>
        /// This method is useful for waiting until a page fully loads before performing assertions,
        /// filling fields, selecting items, etc.
        /// </summary>
        public static IWebElement FindTextPatiently(this IWebDriver driver, string someText)
        {
            return FindPatiently(driver, $"//*[contains(text(), '{someText}')]");
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


        /// <summary>
        /// In some cases this method may be a less cumbersome alternative to <c>TabAndInteract</c>.
        /// TODO: More usage required.  
        /// </summary>
        public static void ClickWithJavaScript(this IWebDriver driver, string xPathToElement)
        {
            IWebElement elementToClick = driver.FindPatiently(xPathToElement);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", elementToClick);
        }
    }
}
