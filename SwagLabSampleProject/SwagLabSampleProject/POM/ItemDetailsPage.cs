using OpenQA.Selenium;

namespace SwagLabSampleProject.POM
{
    public class ItemDetailsPage
    {
        private readonly IWebDriver _driver;
        public ItemDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement itemName => _driver.FindElement(By.XPath("//div[@class='inventory_details_name large_size']"));
        IWebElement itemDes => _driver.FindElement(By.XPath("//div[@class='inventory_details_desc large_size']"));
        IWebElement itemPrice => _driver.FindElement(By.XPath("//div[@class='inventory_details_price']"));
        IWebElement btnBackToProduct => _driver.FindElement(By.XPath("//button[@id='back-to-products']"));
        IWebElement btnAddToCart => _driver.FindElement(By.XPath("//button[@id='add-to-cart']"));
        IWebElement btnRemove => _driver.FindElement(By.XPath("//button[@id='remove']"));

        public string GetItemName()
        {
            return itemName.Text;
        }
        public string GetItemDes()
        {
            return itemDes.Text;
        }
        public string GetItemPrice()
        {
            return itemPrice.Text;
        }

        public void ClickBtnBackToProduct()
        {
            btnBackToProduct.Click();
        }

        public void ClickAddToCart()
        {
            btnAddToCart.Click();

        }
    }
}
