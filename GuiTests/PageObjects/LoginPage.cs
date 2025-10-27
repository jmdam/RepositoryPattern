using OpenQA.Selenium;
using Structure.GuiTests.Locators;
using Structure.GuiTests.Utilities;
using System;
using static Structure.GuiTests.SeleniumHelpers.SeleniumUtils;

namespace Structure.GuiTests.PageObjects
{
    /// <summary>
    /// Page Object para la página de Login
    /// Usa el repositorio centralizado de locators (Locators.Login)
    /// </summary>
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        #region Actions

        /// <summary>
        /// Realiza el login en la aplicación
        /// </summary>
        /// <param name="url">URL base de la aplicación</param>
        /// <param name="username">Usuario (opcional, se lee de config si no se provee)</param>
        /// <param name="password">Contraseña (opcional, se lee de config si no se provee)</param>
        public void Login(string url, string username = null, string password = null)
        {
            try
            {
                // Usar credenciales de configuración si no se proporcionan
                var user = username ?? ConfigurationHelper.Get<string>("UserName");
                var pass = password ?? ConfigurationHelper.Get<string>("Password");

                // Navegar a la URL
                _driver.Navigate().GoToUrl(url);
                WaitUntilPageIsLoaded(_driver, 15);

                // Ingresar credenciales usando locators centralizados                
                var usernameInput = _driver.FindElement(Locator.Login.UsernameInput);
                //var usernameInput = _driver.FindElement(Login.
                usernameInput.Clear();
                usernameInput.SendKeys(user);

                var passwordInput = _driver.FindElement(Locator.Login.PasswordInput);
                passwordInput.Clear();
                passwordInput.SendKeys(pass);

                // Hacer clic en submit
                var submitButton = _driver.FindElement(Locator.Login.SubmitButton);
                submitButton.Click();

                // Esperar a que la página cargue y aparezca el link de logout
                WaitUntilPageIsLoaded(_driver, 15);
                WaitUntilElementIsVisible(_driver, Locator.Login.LogoutLink, 10);

                Console.WriteLine($"User '{user}' logged in successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Ingresa el nombre de usuario
        /// </summary>
        public void EnterUsername(string username)
        {
            try
            {
                var usernameInput = _driver.FindElement(Locator.Login.UsernameInput);
                usernameInput.Clear();
                usernameInput.SendKeys(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to enter username: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Ingresa la contraseña
        /// </summary>
        public void EnterPassword(string password)
        {
            try
            {
                var passwordInput = _driver.FindElement(Locator.Login.PasswordInput);
                passwordInput.Clear();
                passwordInput.SendKeys(password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to enter password: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Hace clic en el botón de submit
        /// </summary>
        public void ClickSubmit()
        {
            try
            {
                var submitButton = _driver.FindElement(Locator.Login.SubmitButton);
                submitButton.Click();
                WaitUntilPageIsLoaded(_driver, 15);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click submit button: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Verifica si la página de login está visible
        /// </summary>
        public bool IsLoginPageDisplayed()
        {
            try
            {
                var usernameVisible = _driver.FindElement(Locator.Login.UsernameInput).Displayed;
                var passwordVisible = _driver.FindElement(Locator.Login.PasswordInput).Displayed;
                var submitVisible = _driver.FindElement(Locator.Login.SubmitButton).Displayed;

                return usernameVisible && passwordVisible && submitVisible;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si hay un mensaje de error visible
        /// </summary>
        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return _driver.FindElement(Locator.Login.ErrorMessage).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene el texto del mensaje de error
        /// </summary>
        public string GetErrorMessage()
        {
            try
            {
                WaitUntilElementIsVisible(_driver, Locator.Login.ErrorMessage, 5);
                return _driver.FindElement(Locator.Login.ErrorMessage).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene el título de la página
        /// </summary>
        public string GetPageTitle()
        {
            return _driver.Title;
        }

        #endregion
    }
}
