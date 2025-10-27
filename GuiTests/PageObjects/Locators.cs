using OpenQA.Selenium;

namespace Structure.GuiTests.Locators
{
    /// <summary>
    /// Repositorio centralizado de todos los locators de la aplicación
    /// Ventajas:
    /// - Un solo lugar para mantener todos los selectores
    /// - Fácil actualización cuando cambia el UI
    /// - Reutilización en múltiples Page Objects
    /// - Separación clara entre locators y lógica de negocio
    /// </summary>
    public static class Locator
    {
        /// <summary>
        /// Locators para la página de Login
        /// </summary>
        public static class Login
        {
         
            public static By UsernameInput => By.Name("Username");
            public static By PasswordInput => By.Name("Password");

         
            public static By SubmitButton => By.XPath("//button[@type='submit']");

         
            public static By LogoutLink => By.XPath("//a[contains(.,'Log Out')]");

         
            public static By ErrorMessage => By.ClassName("error-message");
            public static By ValidationMessage => By.ClassName("validation-message");
        }

        /// <summary>
        /// Locators para la página de Dashboard
        /// </summary>
        public static class Dashboard
        {
            
            public static By GridContainer => By.ClassName("pb-3");
            public static By EmployeeTable => By.Id("employeesTable");

            
            public static By AddEmployeeButton => By.Id("add");
            public static By AddEmployeeModalButton => By.Id("addEmployee");
            public static By UpdateEmployeeButton => By.Id("updateEmployee");
            public static By DeleteEmployeeButton => By.Id("deleteEmployee");

            
            public static By FirstNameInput => By.Id("firstName");
            public static By LastNameInput => By.Id("lastName");
            public static By DependantsInput => By.Id("dependants");

            
            public static By AddEmployeeModal => By.XPath("//h5[@class='modal-title'][contains(.,'Add Employee')]");
            public static By EditEmployeeModal => By.XPath("//h5[@class='modal-title'][contains(.,'Edit Employee')]");
            public static By DeleteConfirmationModal => By.XPath("//h5[@class='modal-title'][contains(.,'Delete Employee')]");

            
            public static By LogoutLink => By.XPath("//a[@href='/Prod/Account/LogOut']");

            /// <summary>
            /// Locator dinámico para una fila de empleado específico
            /// </summary>
            /// <param name="firstName">Nombre del empleado</param>
            /// <param name="lastName">Apellido del empleado</param>
            /// <returns>Locator de la fila del empleado</returns>
            public static By GetEmployeeRow(string firstName, string lastName)
            {
                return By.XPath($"//table[@id='employeesTable']//tr[td[2][contains(., '{firstName}')] and td[3][contains(., '{lastName}')]]");
            }

            /// <summary>
            /// Locator dinámico para el botón de editar de un empleado
            /// </summary>
            public static By GetEmployeeEditIcon(string firstName, string lastName)
            {
                return By.XPath($"//table[@id='employeesTable']//tr[td[2][contains(., '{firstName}')] and td[3][contains(., '{lastName}')]]//i[@class='fas fa-edit']");
            }

            /// <summary>
            /// Locator dinámico para el botón de eliminar de un empleado
            /// </summary>
            public static By GetEmployeeDeleteIcon(string firstName, string lastName)
            {
                return By.XPath($"//table[@id='employeesTable']//tr[td[2][contains(., '{firstName}')] and td[3][contains(., '{lastName}')]]//i[@class='fas fa-times']");
            }

            /// <summary>
            /// Locator dinámico para una celda específica de la tabla de empleados
            /// </summary>
            /// <param name="firstName">Nombre del empleado</param>
            /// <param name="lastName">Apellido del empleado</param>
            /// <param name="columnIndex">Índice de la columna (1-based)</param>
            /// <returns>Locator de la celda</returns>
            public static By GetEmployeeCell(string firstName, string lastName, int columnIndex)
            {
                return By.XPath($"//table[@id='employeesTable']//tr[td[2][contains(., '{firstName}')] and td[3][contains(., '{lastName}')]]/td[{columnIndex}]");
            }

            /// <summary>
            /// Locators para columnas específicas de la tabla de empleados
            /// Usar con GetEmployeeCell()
            /// </summary>
            public static class EmployeeTableColumns
            {
                public const int Id = 1;
                public const int FirstName = 2;
                public const int LastName = 3;
                public const int Dependants = 4;
                public const int Salary = 5;
                public const int Gross = 6;
                public const int BenefitsCost = 7;
                public const int NetPay = 8;
                public const int Actions = 9;
            }
        }

        /// <summary>
        /// Locators comunes usados en múltiples páginas
        /// </summary>
        public static class Common
        {
            // Loaders y spinners
            public static By LoadingSpinner => By.ClassName("spinner");
            public static By LoadingOverlay => By.ClassName("loading-overlay");

            // Mensajes
            public static By SuccessMessage => By.ClassName("alert-success");
            public static By ErrorMessage => By.ClassName("alert-danger");
            public static By WarningMessage => By.ClassName("alert-warning");
            public static By InfoMessage => By.ClassName("alert-info");

            // Botones comunes
            public static By ConfirmButton => By.XPath("//button[contains(text(),'Confirm') or contains(text(),'OK')]");
            public static By CancelButton => By.XPath("//button[contains(text(),'Cancel')]");
            public static By CloseButton => By.XPath("//button[contains(@class,'close')]");

            // Modales
            public static By ModalDialog => By.ClassName("modal-dialog");
            public static By ModalContent => By.ClassName("modal-content");
            public static By ModalHeader => By.ClassName("modal-header");
            public static By ModalBody => By.ClassName("modal-body");
            public static By ModalFooter => By.ClassName("modal-footer");

            /// <summary>
            /// Locator dinámico para cualquier botón por texto
            /// </summary>
            public static By GetButtonByText(string buttonText)
            {
                return By.XPath($"//button[contains(text(),'{buttonText}')]");
            }

            /// <summary>
            /// Locator dinámico para cualquier link por texto
            /// </summary>
            public static By GetLinkByText(string linkText)
            {
                return By.XPath($"//a[contains(text(),'{linkText}')]");
            }

            /// <summary>
            /// Locator dinámico para cualquier elemento por data-testid
            /// </summary>
            public static By GetByTestId(string testId)
            {
                return By.XPath($"//*[@data-testid='{testId}']");
            }
        }

        /// <summary>
        /// Locators para reportes (si existieran)
        /// </summary>
        public static class Reports
        {
            public static By ReportTitle => By.ClassName("report-title");
            public static By ReportTable => By.Id("reportTable");
            public static By ExportButton => By.Id("exportReport");
            public static By FilterButton => By.Id("filterReport");

            public static By GetReportRow(int rowIndex)
            {
                return By.XPath($"//table[@id='reportTable']//tr[{rowIndex}]");
            }
        }

        /// <summary>
        /// Locators para navegación general de la aplicación
        /// </summary>
        public static class Navigation
        {
            public static By HomeLink => By.XPath("//a[@href='/']");
            public static By DashboardLink => By.XPath("//a[contains(text(),'Dashboard')]");
            public static By SettingsLink => By.XPath("//a[contains(text(),'Settings')]");
            public static By HelpLink => By.XPath("//a[contains(text(),'Help')]");

            // Breadcrumbs
            public static By Breadcrumb => By.ClassName("breadcrumb");

            public static By GetBreadcrumbItem(string itemText)
            {
                return By.XPath($"//ol[@class='breadcrumb']//a[contains(text(),'{itemText}')]");
            }
        }
    }
}