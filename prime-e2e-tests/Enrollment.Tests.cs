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
            SelectDropdownItem("careSettingCode", "Community Pharmacy");
            _driver.TakeScreenshot("Care_Setting_Completed");
            ClickButton("Save and Continue");

            // TODO: Eliminate need for Sleep ... is there a XPath that will match on College Licence Information page but NOT on Care Setting page?
            System.Threading.Thread.Sleep(1500);
            Assert.AreEqual("College Licence Information", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            SelectDropdownItem("collegeCode", "College of Physicians and Surgeons of BC");
            SelectDropdownItem("licenseCode", "Full - Family");
            PickDate("2023", "MAR", "5");
            TypeIntoField("CPSID Number", "20101");
            // TODO: Why does 'Keep Changes and Continue' pop up without Sleep?
            System.Threading.Thread.Sleep(1000);
            // TODO: Common function given a title
            _driver.TakeScreenshot("College_Licence_Information_Completed");
            ClickButton("Save and Continue");

            _driver.FindPatiently("//mat-radio-group[@formcontrolname='hasRegistrationSuspended']");
            Assert.AreEqual("Self-declaration", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            ClickRadioButton("hasRegistrationSuspended", "No");
            ClickRadioButton("hasConviction", "No");
            // Why does this not select "No"?!?
            // _driver.FindPatiently("//mat-radio-group[@formcontrolname='hasPharmaNetSuspended']//label[div[contains(text(), 'No')]]").Click();
            _driver.TabAndInteract("//mat-radio-group[@formcontrolname='hasConviction']//label[div[contains(text(), 'No')]]", 1, Keys.Space);
            ClickRadioButton("hasDisciplinaryAction", "No");
            // The following causes:
            // OpenQA.Selenium.ElementClickInterceptedException : element click intercepted: Element <button _ngcontent-iey-c270="" mat-flat-button="" type="button" color="primary" class="mat-focus-indicator mat-flat-button mat-button-base mat-primary">...</button> is not clickable at point (613, 798). Other element would receive the click: <div id="cdk-overlay-6" class="cdk-overlay-pane" style="pointer-events: auto; position: static; margin-bottom: 0px;">...</div>
            // _driver.FindPatiently("//button[span[contains(text(), 'Save and Continue')]]").Click();
            _driver.TabAndInteract("//mat-radio-group[@formcontrolname='hasDisciplinaryAction']//label[div[contains(text(), 'No')]]", 2, Keys.Enter);

            _driver.FindPatiently("//span[@class='mat-button-wrapper' and contains(text(), 'Submit Enrolment')]");
            Assert.AreEqual("Enrolment Review", _driver.FindPatiently("//h2[contains(@class,'title')]").Text);
            // The following causes:
            // OpenQA.Selenium.ElementClickInterceptedException : element click intercepted: Element <label class="mat-checkbox-layout" for="mat-checkbox-1-input">...</label> is not clickable at point (430, 852). Other element would receive the click: <snack-bar-container class="mat-snack-bar-container ng-tns-c30-52 ng-trigger ng-trigger-state mat-snack-bar-center ng-star-inserted" role="alert" style="transform: scale(1); opacity: 1;">...</snack-bar-container>
            // _driver.FindPatiently("//label[@class='mat-checkbox-layout']").Click();
            _driver.TabAndInteract("//button[@mattooltip='Edit Self-declaration']", 1, Keys.Space);
            // The following causes:
            // OpenQA.Selenium.ElementClickInterceptedException : element click intercepted: Element <button _ngcontent-bwq-c268="" mat-flat-button="" color="primary" class="mat-focus-indicator mat-flat-button mat-button-base mat-primary">...</button> is not clickable at point (616, 822). Other element would receive the click: <snack-bar-container class="mat-snack-bar-container ng-tns-c30-52 ng-trigger ng-trigger-state mat-snack-bar-center ng-star-inserted" role="alert" style="transform: scale(1); opacity: 1;">...</snack-bar-container>
            // _driver.FindPatiently("//button[span[contains(text(), 'Submit Enrolment')]]").Click();
            _driver.TabAndInteract("//button[@mattooltip='Edit Self-declaration']", 2, Keys.Enter);
            // TODO: XPath should be clear that confirm button is clicked
            _driver.FindPatiently("(//app-confirm-dialog/mat-dialog-actions/button)[2]").Click();
        }


        [TearDown]
        public void TestTearDown()
        {
//            _driver.Quit();
        }
    }
}
