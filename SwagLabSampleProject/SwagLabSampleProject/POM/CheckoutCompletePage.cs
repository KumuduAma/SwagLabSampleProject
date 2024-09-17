using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabSampleProject.POM
{
    public class CheckoutCompletePage
    {
        private readonly IWebDriver _driver;
        private readonly CommonPage commonPage;

        public CheckoutCompletePage(IWebDriver driver)
        {
            _driver = driver;
            commonPage=new CommonPage(driver);
        }

        IWebElement btnHome => _driver.FindElement(By.Id("back-to-products"));

        public string ValidateBackHomeButton()
        {
            commonPage.ClickElement(btnHome);
            return commonPage.GetPageTitle();
        }
    }
}
