using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Structure.GuiTests.SeleniumHelpers
{
    public class SeleniumUtils
    {
        public static IWebDriver _driver;
        public SeleniumUtils(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            var alert = _driver.SwitchTo().Alert();
            alert.Accept();
            return alert.Text;
        }

        public static void WaitUntilElementIsVisible(IWebDriver _driver, By locator, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }


        public static void WaitUntilPageIsLoaded(IWebDriver _driver, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public enum FrameEnum
        {
            Title,
            Main,
            Tree,
            Work
        }

        public static void GoToFrame(FrameEnum frameEnum, IWebDriver _driver1)
        {
            _driver1.SwitchTo().DefaultContent();
            switch (frameEnum)
            {
                case FrameEnum.Title:
                    SelectFrame("titlebar", _driver1);
                    break;
                case FrameEnum.Main:
                    SelectFrame("mainframe", _driver1);
                    break;
                case FrameEnum.Tree:
                    SelectFrame("mainframe", _driver1);
                    SelectFrame("treeframe", _driver1);
                    break;
                case FrameEnum.Work:
                    SelectFrame("mainframe", _driver1);
                    SelectFrame("workframe", _driver1);
                    break;
                default:
                    break;
            }
        }

        public static void SelectFrame(string frame, IWebDriver _driver1)
        {
            _driver1.SwitchTo().Frame(frame);
        }

        public static void SelectDropdownOption(IWebElement dropdownElement, string selectionMethod, string selectionValue)
        {
            var select = new SelectElement(dropdownElement);

            switch (selectionMethod.ToLower())
            {
                case "text":
                    select.SelectByText(selectionValue);
                    break;
                case "value":
                    select.SelectByValue(selectionValue);
                    break;
                case "index":
                    var index = int.Parse(selectionValue);
                    select.SelectByIndex(index);
                    break;
                default:
                    throw new ArgumentException("Invalid selection method specified.");
            }
        }
    }
}

