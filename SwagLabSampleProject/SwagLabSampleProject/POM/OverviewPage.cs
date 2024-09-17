using OpenQA.Selenium;

namespace SwagLabSampleProject.POM
{
    public class OverviewPage
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _element;
        private readonly CommonPage commonPage;
        private bool status;

        public OverviewPage(IWebDriver driver)
        {
            _driver = driver;
            status = false;
            commonPage = new CommonPage(driver);
        }

        IList<IWebElement> getItems => _driver.FindElements(By.XPath("//div[@class='cart_list']//div[@class='cart_item']"));
        IList<IWebElement> getItemPrices => _driver.FindElements(By.XPath("//div[@class='cart_list']//div[@class='cart_item']//div[@class='inventory_item_price']"));
        IWebElement itemTotal => _driver.FindElement(By.XPath("//div[@class='summary_subtotal_label']"));
        IWebElement taxPrice => _driver.FindElement(By.XPath("//div[@class='summary_tax_label']"));
        IWebElement priceTotal => _driver.FindElement(By.XPath("//div[@class='summary_total_label']"));
        IWebElement btnFinish => _driver.FindElement(By.XPath("//button[@id='finish']"));


        
        public bool ValidateItemTotal()
        {
            bool status = false;
            double total = 0;
            int i = 0;
            foreach (var item in getItemPrices)
            {
                i = item.Text.IndexOf("$");
                Console.WriteLine((item.Text).Substring(i + 1));
                double itemValue = Convert.ToDouble((item.Text).Substring(i+1));
                
                total = total + itemValue;
            }
            Console.WriteLine(total);
            int j = 0;
            j = itemTotal.Text.IndexOf("$");
            Console.WriteLine(Convert.ToDouble((itemTotal.Text).Substring(j + 1)));
            if (total == Convert.ToDouble((itemTotal.Text).Substring(j+1)))
                status = true;
            else status = false;
            return status;

        }

        public bool ValidatePriceTotal()
        {
            int i= itemTotal.Text.IndexOf("$");
            int j= taxPrice.Text.IndexOf("$");
            int k = priceTotal.Text.IndexOf("$");
            double itemTotalValue = Convert.ToDouble(itemTotal.Text.Substring(i+1));
            double taxPriceValue = Convert.ToDouble(taxPrice.Text.Substring(j + 1));
            double priceTotalValue = Convert.ToDouble(priceTotal.Text.Substring(k+1));

            if (priceTotalValue == itemTotalValue + taxPriceValue)
                status = true;
            else status = false;
            return status;
        }

        public string ValidateFinishButton()
        {
            commonPage.ClickElement(btnFinish);
            return commonPage.GetPageTitle();
        }

        
    }
}
