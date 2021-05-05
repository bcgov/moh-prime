using System;
using NUnit.Framework;

namespace TestPrimeE2E.Enrollment
{
    /// <summary>
    /// Tests for <c>BaseTest</c>  :-)
    /// </summary>
    public class BaseTestTest : BaseTest
    {
        [Test]
        public void ConsoleLogReadingTest()
        {
            // The following works with Selenium.WebDriver version 4.0.0-beta2 (yes, there are some errors logged to the browser console such as
            // `https://api.nuget.org/v3-flatcontainer/selenium.webdriver/3.141.0/icon - Failed to load resource: the server responded with a status of 404 (Not Found)`)
            // but not the stable version 3.141.0
            // _driver.Navigate().GoToUrl("https://www.nuget.org/packages/Selenium.WebDriver/3.141.0");
            // Assert.Throws<Exception>(() => CheckLogThenScreenshot("Nuget Selenium.WebDriver"));
        }
    }
}
