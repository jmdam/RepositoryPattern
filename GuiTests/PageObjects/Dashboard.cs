using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Structure.GuiTests.Utilities;
using System;
using System.Text;
using System.Threading;
using static Structure.GuiTests.SeleniumHelpers.SeleniumUtils;


namespace Structure.GuiTests.PageObjects
{
    public class Dashboard
    {
        private readonly IWebDriver _driver;
        public StringBuilder TestCases = new();

        public Dashboard(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
        
        [FindsBy(How = How.Name, Using = "Username")]
        public IWebElement UsernameInput { get; set; }

        [FindsBy(How = How.Name, Using = "Password")]
        public IWebElement PasswordInput{ get; set; }
                
        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement btnSubmit { get; set; }

        [FindsBy(How = How.ClassName, Using = "pb-3")]
        public IWebElement gridContainer{ get; set; }

        [FindsBy(How = How.Id, Using = "add")]
        public IWebElement btnAddEmployee{ get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='firstName']")]
        public IWebElement firstNameInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='lastName']")]
        public IWebElement lastNameInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='dependants']")]
        public IWebElement dependantsInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='addEmployee']")]
        public IWebElement btnAddEmployeeInfo { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[@id='updateEmployee']")]
        public IWebElement btnUpdateEmployeeInfo { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='deleteEmployee']")]
        public IWebElement btnDeleteEmployee { get; set; }
        [FindsBy(How = How.XPath, Using = "//a[@href='/Prod/Account/LogOut']")]
        public IWebElement lnkLogout{ get; set; }

        public string fname;
        public string lname;
        public void Login(string baseUrl)
        {
            var userName = new StringBuilder(ConfigurationHelper.Get<string>("UserName"));
            var password = new StringBuilder(ConfigurationHelper.Get<string>("Password"));
            _driver.Navigate().GoToUrl(baseUrl);
            UsernameInput.SendKeys(userName.ToString());
            PasswordInput.SendKeys(password.ToString());
            btnSubmit.Click();
            WaitUntilPageIsLoaded(_driver,15);
            WaitUntilElementIsVisible(_driver, By.XPath("//a[contains(.,'Log Out')]"),5);
            Console.WriteLine("User logged in successfully");
        }

        public void AddEmployee()
        {
            Random random = new Random();
            WaitUntilPageIsLoaded(_driver,10);
            btnAddEmployee.Click();
            WaitUntilElementIsVisible(_driver,By.XPath("//h5[@class='modal-title'][contains(.,'Add Employee')]"),10);            
            firstNameInput.SendKeys("FirstName"+ Guid.NewGuid().ToString("N").Substring(0,random.Next(7)));
            fname = firstNameInput.GetAttribute("value");
            lastNameInput.SendKeys("LastName" + Guid.NewGuid().ToString("N").Substring(0, random.Next(7)));
            lname = lastNameInput.GetAttribute("value");
            dependantsInput.SendKeys(random.Next(32).ToString());
            btnAddEmployeeInfo.Click();
            WaitUntilElementIsVisible(_driver, By.XPath("//table[@id='employeesTable']//td[contains(.,'" + fname+ "')]"), 15);
            Console.WriteLine("Employee "+ fname +" "+ lname+" created succesfully");
            WaitUntilPageIsLoaded(_driver,10);

        }

        public void ValidateMonthlyNetPay()
        {
            const double paycheckAmount = 2000.00;
            const double annualEmployeeBenefit = 1000.00;
            const double annualDependentBenefitPer = 500.00;
            var benefitsCost = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')]]/td[7]")).Text;
            var dependants = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')]]/td[4]")).Text;
            var gross = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')]]/td[6]")).Text;
            var netPay = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')]]/td[8]")).Text;

            double expectedTotalAnnualBenefits = Math.Round(annualEmployeeBenefit + (int.Parse(dependants) * annualDependentBenefitPer),2);
            double expectedBiWeeklyBenefits = Math.Round(expectedTotalAnnualBenefits / 26,2);
            double expectedNet = Math.Round(paycheckAmount - expectedBiWeeklyBenefits, 2);
            
            
            Assert.AreEqual(Math.Round(double.Parse(benefitsCost),2), expectedBiWeeklyBenefits, "Benefits cost mismatch");
            Assert.AreEqual(Math.Round(double.Parse(netPay),2),expectedNet, "Net pay mismatch");
            Assert.AreEqual(Math.Round(double.Parse(gross),2), paycheckAmount, "Gross mismatch");

            Console.WriteLine($"Validated: gross={gross}, benefitsCost={benefitsCost}, net={netPay}");
            
        }
        public void EditEmployee()
        {
            Random random = new Random();
            var employeeRow = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')] and td[3][contains(., '" + lname + "')]]//i[@class='fas fa-edit']"));                                                          
            employeeRow.Click();

            Console.WriteLine($"Employee to update: {fname},{lname}");
            firstNameInput.Clear();
            firstNameInput.SendKeys(fname + "Edited");
            fname = firstNameInput.GetAttribute("value");
            lastNameInput.Clear();
            lastNameInput.SendKeys(lname + "Edited");
            lname = lastNameInput.GetAttribute("value");
            dependantsInput.Clear();
            dependantsInput.SendKeys(random.Next(32).ToString());
            btnUpdateEmployeeInfo.Click();
            WaitUntilElementIsVisible(_driver, By.XPath("//table[@id='employeesTable']//td[contains(.,'" + fname + "')]"), 15);
            WaitUntilPageIsLoaded(_driver, 10);
            Console.WriteLine("Employee " + fname + " "+ lname +" edited succesfully");
        }
        public void DeleteEmployee()
        {
            var EmployeeRow = _driver.FindElement(By.XPath("//table[@id='employeesTable']//tr[td[2][contains(., '" + fname + "')] and td[3][contains(., '" + lname + "')]]//i[@class='fas fa-times']"));
            EmployeeRow.Click();
            btnDeleteEmployee.Click();            
            WaitUntilPageIsLoaded(_driver, 10);
            Console.WriteLine("Employee " + fname + " " + lname + " deleted succesfully");
        }

        public void Logout()
        {
            lnkLogout.Click();
            WaitUntilElementIsVisible(_driver, By.Name("Username"), 10);
            Console.WriteLine("Logged out");
        }
    }
}


