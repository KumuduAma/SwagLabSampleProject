using OpenQA.Selenium;
using SwagLabSampleProject.Models;

namespace SwagLabSampleProject.POM
{
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;
        private readonly CommonPage commonPage;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            commonPage = new CommonPage(driver);
        }

        IWebElement txtFirstName => _driver.FindElement(By.XPath("//input[@id='first-name']"));
        IWebElement txtLastName => _driver.FindElement(By.XPath("//input[@id='last-name']"));
        IWebElement txtZipCode => _driver.FindElement(By.XPath("//input[@id='postal-code']"));
        IWebElement btnContinue => _driver.FindElement(By.XPath("//input[@id='continue']"));
        IWebElement btnCancel => _driver.FindElement(By.XPath("//button[@id='cancel']"));
        IWebElement txtError => _driver.FindElement(By.XPath("//div[@class='error-message-container error']//h3"));


        
        public void ValidateCheckoutWithValidInformation(CheckoutInformationModel informationModel)
        {
            txtFirstName.SendKeys(informationModel.FirstName);
            txtLastName.SendKeys(informationModel.LastName);
            txtZipCode.SendKeys(informationModel.ZipCode);
            commonPage.ClickElement(btnContinue);
        }

        public string ValidateCheckoutWithEmptyFields()
        {
            commonPage.ClickElement(btnContinue);
            return txtError.Text;
        }
        public string ValidateCheckoutWithEmptyFirstName()
        {
            txtLastName.SendKeys("John");
            txtZipCode.SendKeys("1254");
            commonPage.ClickElement(btnContinue);
            return txtError.Text;
        }
        public string ValidateCheckoutWithOnlyFirstName()
        {
            txtFirstName.SendKeys("Ann");
            commonPage.ClickElement(btnContinue);
            return txtError.Text;
        }
        public string ValidateCheckoutWithoutLastName()
        {
            txtFirstName.SendKeys("Ann");
            txtZipCode.SendKeys("1025");
            commonPage.ClickElement(btnContinue);
            return txtError.Text;
        }
        public string ValidateCheckoutWithoutZipCode()
        {
            txtFirstName.SendKeys("Ann");
            txtLastName.SendKeys("John");
            commonPage.ClickElement(btnContinue);
            return txtError.Text;
        }
        
        public string GetFirstNameFieldPlaceHolder()
        {
            return txtFirstName.GetAttribute("placeholder");
        }
        public string GetLasttNameFieldPlaceHolder()
        {
            return txtLastName.GetAttribute("placeholder");
        }
        public string GetZipCodeFieldPlaceHolder()
        {
            return txtZipCode.GetAttribute("placeholder");
        }

        public string CheckCancelButton()
        {
            commonPage.ClickElement(btnCancel);
            return commonPage.GetPageTitle();
        }
    }
}
