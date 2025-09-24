using OpenQA.Selenium;
using System;
using Structure.GuiTests.PageObjects;
using TestLink = Structure.GuiTests.PageObjects.TestLink;
using Structure.GuiTests.Utilities;

namespace Structure.GuiTests
{
    internal class Class1
    {
        private static IWebDriver _driver;
        private static string _baseUrl;


        private static void Main(string[] args)
        {
            var pullTestPlan = new PullTestPlan();
            pullTestPlan.SetupTest();
            _driver = pullTestPlan._driver;
            _baseUrl = pullTestPlan._baseUrl;

            var jiraPage = new Jira(_driver);
            jiraPage.LoginToJira(_baseUrl);
            jiraPage.JiraIssue();
            jiraPage.SearchJiraTestCases(_driver);
            Console.WriteLine(jiraPage._testCases);

            var testLink = new TestLink(_driver);
            testLink.LoginToTestLink(ConfigurationHelper.Get<string>("TestLinkUrl"));
            testLink.SelectProject();
            testLink.CreateTestPlan();
            testLink.CreateBuild();
            testLink.AddPlatform();
            testLink.AddTestCase(jiraPage._testCases);
        }
    }
}