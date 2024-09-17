using OpenQA.Selenium;

namespace SwagLabSampleProject.POM
{
    public class YourCartPage
    {
        private readonly IWebDriver _driver;
        private CommonPage commonPage;
        private bool status = false;

        public YourCartPage(IWebDriver driver)
        {
            _driver = driver;
            commonPage = new CommonPage(driver);
        }

        IList<IWebElement> cartItems => _driver.FindElements(By.XPath("//div[@class='cart_item']"));
        IList<IWebElement> removeButtons => _driver.FindElements(By.XPath("//div[@class='item_pricebar']//button"));
        IWebElement btnCheckOut => _driver.FindElement(By.XPath("//button[@id='checkout']"));
        IWebElement btnContinue => _driver.FindElement(By.XPath("//button[@id='continue-shopping']"));


        public bool ValidateShoppingCart()
        {
            bool status = false;
            commonPage.GetShoppingCart().Click();
            Thread.Sleep(1000);
            //if (cartItems.Count.Equals(Convert.ToInt16(commonPage.GetNoOfShoppingCartItems().Text)))
            if (cartItems.Count.Equals(Convert.ToInt16(commonPage.GetShoppingCart().Text)))
            {
                status = true;
                Console.WriteLine($"Items in List: " + cartItems.Count.ToString() + " Cart Items: " + commonPage.GetNoOfShoppingCartItems().Text);
            }

            return status;
        }

        public bool ValidateRemoveButton()
        {
            bool status = false;

            commonPage.GetShoppingCart().Click();

            //int shoppingCartItems = Convert.ToInt16(commonPage.GetNoOfShoppingCartItems().Text);
            int shoppingCartItems = Convert.ToInt16(commonPage.GetShoppingCart().Text);

            int count = 0;

            if (shoppingCartItems > 0)
            {
                foreach (var item in removeButtons)
                {
                    item.Click();
                    Thread.Sleep(500);
                    count++;
                }
                int currentShoppingCartItems;
                if (commonPage.GetShoppingCart().Text == "")
                    currentShoppingCartItems = 0; 
                else
                currentShoppingCartItems = Convert.ToInt16(commonPage.GetShoppingCart().Text);

                if (currentShoppingCartItems.Equals(shoppingCartItems - count))
                    status = true;

                //Console.WriteLine($"Remove button was clicked " + count + " times / Shopping cart items: " + currentShoppingCartItems);
            }
            return status;

        }

        public void ClickCheckout()
        {
            commonPage.ClickElement(btnCheckOut);
        }

        public string CheckContinueShoppingButton()
        {
            commonPage.ClickElement(btnContinue);
            return commonPage.GetPageTitle();
        }

        public string ValidateCheckOutButton()
        {
            commonPage.ClickElement(commonPage.GetShoppingCart());
            commonPage.ClickElement(btnCheckOut);
            //if (cartItems.Count == 0)
            //{
            //    if (btnCheckOut.Enabled)
            //    {
            //        status = false;
            //    }
                
            //}
            //else
            //{
            //    ClickCheckout();
            //    status = true;
            //}

            return commonPage.GetPageTitle();
        }
    }
}
