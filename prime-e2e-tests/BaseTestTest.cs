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


        [Test]
        public void NumberToWordsTest()
        {
            Assert.AreEqual("zero", this.NumberToWords(0));
            Assert.AreEqual("one", this.NumberToWords(1));
            Assert.AreEqual("nine", this.NumberToWords(9));
            Assert.AreEqual("ten", this.NumberToWords(10));
            Assert.AreEqual("eleven", this.NumberToWords(11));
            Assert.AreEqual("twelve", this.NumberToWords(12));
            Assert.AreEqual("thirteen", this.NumberToWords(13));
            Assert.AreEqual("nineteen", this.NumberToWords(19));
            Assert.AreEqual("twenty", this.NumberToWords(20));
            Assert.AreEqual("twenty-one", this.NumberToWords(21));
            Assert.AreEqual("twenty-nine", this.NumberToWords(29));
            Assert.AreEqual("thirty", this.NumberToWords(30));
            Assert.AreEqual("thirty-one", this.NumberToWords(31));
            Assert.AreEqual("thirty-nine", this.NumberToWords(39));
        }
    }
}
