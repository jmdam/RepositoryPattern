using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Structure.GuiTests.PageObjects;
using Structure.GuiTests.SeleniumHelpers;
using Structure.GuiTests.Utilities;
using System;
using System.IO;
using System.Text;

namespace Structure.GuiTests
{
    [TestFixture]
    public class RunTestPlan
    {
        public IWebDriver _driver;
        private StringBuilder _verificationErrors;
        public string _baseUrl;
        private LoginPage _loginPage;

        [SetUp]
        public void SetupTest()
        {
            _driver = new DriverFactory().Create();
            _baseUrl = ConfigurationHelper.Get<string>("TargetUrl");
            _verificationErrors = new StringBuilder();
            _loginPage = new LoginPage(_driver);
            Console.WriteLine("Starting!!");
        }

        [TearDown]
        public void TeardownTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    var testName = TestContext.CurrentContext.Test.Name;
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var fileName = $"{testName}_{timestamp}.png";
                    var filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fileName);
                    screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                    Console.WriteLine($"🖼️ Screenshot saved: {filePath}");
                    _driver.Quit();
                    _driver.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Failed to capture screenshot: {ex.Message}");
                }

                Console.WriteLine($"❌ Test failed: {TestContext.CurrentContext.Result.Message}");
            }
            else
            {
                Console.WriteLine("✅ Test passed");
            }
          
            _verificationErrors.ToString().Should().BeEmpty("No verification errors are expected.");
        }

        [Test]
        public void Login_Tests()
        {
            _loginPage.Login(_baseUrl);
            Console.WriteLine("Test completed!!");
        }
    }
}