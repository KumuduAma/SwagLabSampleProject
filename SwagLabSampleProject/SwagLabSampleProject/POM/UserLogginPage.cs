using OpenQA.Selenium;
using SwagLabSampleProject.Models;

namespace SwagLabSampleProject.POM
{
    public class UserLogginPage
    {
        private readonly IWebDriver _driver;
        private CommonPage commonPage;
        private static string usersFile = "";


        public UserLogginPage(IWebDriver driver)
        {
            _driver = driver;
            commonPage = new CommonPage(driver);

        }

        IWebElement txtUsername => _driver.FindElement(By.Id("user-name"));
        IWebElement txtPassword => _driver.FindElement(By.Id("password"));
        IWebElement btnLoggin => _driver.FindElement(By.Id("login-button"));

        IWebElement txtError => _driver.FindElement(By.CssSelector("h3[data-test='error']"));
        public string PageTitle()
        {
            return _driver.Title;
        }

        public void LoggingToPageWithValidDefaultUser(string username, string password)
        {
            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
            commonPage.TakeScreenShot("UserLoggin", "ValidateUserLoggingWithValidCredentials");
            btnLoggin.Click();
        }
        public void ValidateUserLoggingWithValidCredentials(UserLoggingModel loggingModel)
        {
            txtUsername.SendKeys(loggingModel.Username);
            txtPassword.SendKeys(loggingModel.Password);
            commonPage.TakeScreenShot("UserLoggin", "ValidateUserLoggingWithValidCredentials");

            btnLoggin.Click();
        }
        public string ValidateUserLoggingWithInvalidCredentials(UserLoggingModel loggingModel)
        {
            txtUsername.SendKeys(loggingModel.Username);
            txtPassword.SendKeys(loggingModel.Password);
            btnLoggin.Click();
            commonPage.TakeScreenShot("UserLoggin", "ValidateUserLoggingWithInvalidCredentials");

            return txtError.Text;
        }
    }
}
