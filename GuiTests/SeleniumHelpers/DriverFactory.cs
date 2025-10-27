using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Structure.GuiTests.Utilities;
using System;

namespace Structure.GuiTests.SeleniumHelpers
{
    public enum DriverToUse
    {        
        Edge,
        Chrome        
    }

    public class DriverFactory
    {

        public IWebDriver Create()
        {
            DriverToUse driverToUse = ConfigurationHelper.Get<DriverToUse>("DriverToUse");
                        
            InternetExplorerOptions IEoptions = new InternetExplorerOptions();
            IWebDriver driver = driverToUse switch
            {
                DriverToUse.Edge => new EdgeDriver(
                    AppDomain.CurrentDomain.BaseDirectory,
                    new EdgeOptions(),
                    TimeSpan.FromSeconds(3000)),

                DriverToUse.Chrome => new ChromeDriver(
                    AppDomain.CurrentDomain.BaseDirectory,
                    new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    },
                    TimeSpan.FromSeconds(30)),
                

                _ => throw new ArgumentOutOfRangeException()
            };

            driver.Manage().Window.Maximize();
            var timeouts = driver.Manage().Timeouts();
            timeouts.ImplicitWait = TimeSpan.FromSeconds(ConfigurationHelper.Get<int>("ImplicitlyWait"));
            timeouts.PageLoad = TimeSpan.FromSeconds(ConfigurationHelper.Get<int>("PageLoadTimeout"));

            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            return driver;
        }
    }
}