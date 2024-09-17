using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabSampleProject.POM
{
    public class CommonPage
    {
        private readonly IWebDriver _driver;

        public CommonPage(IWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement shoppingCart => _driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));
        IWebElement noOfShoppingCartItems => _driver.FindElement(By.XPath("//span[@class='shopping_cart_badge']"));
        IWebElement pageTitle => _driver.FindElement(By.XPath("//span[@class='title']"));
        


        public string GetPageTitle()
        {
            return pageTitle.Text;
        }
        public void ClickElement(IWebElement element)
        {
            element.Click();
        }
        public IWebElement GetElement(IWebElement element)
        {
            return element;
        }

        public IWebElement GetShoppingCart()
        {
            return GetElement(shoppingCart);
        }

        public IWebElement GetNoOfShoppingCartItems()
        {
           return GetElement(noOfShoppingCartItems);
        }

        public void TakeScreenShot(string directoryName,string fileName)
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)_driver;
            Screenshot takeScreenshot = screenshot.GetScreenshot();

            string screenshotDirectory = @"E:\Youtube_Videos\QA\Automation Testing\TestProject\SampleProjectForGit\SwagLabSampleProject\SwagLabSampleProject\Screenshots\"+ directoryName+"";
            string screenshotPath = Path.Combine(screenshotDirectory, fileName + "." + ImageFormat.Png);
            takeScreenshot.SaveAsFile(screenshotPath);
        }
    }
}
