using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Structure.GuiTests.PageObjects;
using Structure.GuiTests.SeleniumHelpers;
using Structure.GuiTests.Utilities;
using System;
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
            try
            {
                _driver.Quit();
                _driver.Close();
            }
            catch (Exception)
            {
                // Ignore errors if we are unable to close the browser
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


