using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SwagLabSampleProject.POM
{
    public class ProductsDetailsPage
    {
        private readonly IWebDriver _driver;
        private int noOfItems = 0;
        private int noOfImages = 0;
        private int noOfItemNames = 0;
        private int noOfItemPrices = 0;
        private int noOfItemDes = 0;
        private int noOfAddToCartButtons = 0;
        private ItemDetailsPage itemDetailsPage;
        private CommonPage commonPage;
        private string itemName = "";
        private string price = "";
        private string description = "";
        private bool status = false;
        private int i = 0;
        public ProductsDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            itemDetailsPage = new ItemDetailsPage(_driver);
            commonPage = new CommonPage(_driver);
        }

        IWebElement pageTitle => _driver.FindElement(By.XPath("//span[@class='title']"));
        IList<IWebElement> inventoryItems => _driver.FindElements(By.XPath("//div[@class='inventory_list']//div[@class='inventory_item']"));
        IList<IWebElement> inventoryImages => _driver.FindElements(By.XPath("//div[@class='inventory_item_img']"));
        IList<IWebElement> inventoryItemNames => _driver.FindElements(By.XPath("//div[@class='inventory_item_name ']"));
        IList<IWebElement> inventoryItemPrices => _driver.FindElements(By.XPath("//div[@class='inventory_item_price']"));
        IList<IWebElement> inventoryItemDescriptions => _driver.FindElements(By.XPath("//div[@class='inventory_item_desc']"));
        IList<IWebElement> noOfActiveAddToCartButtons => _driver.FindElements(By.XPath("//div[@class='pricebar']//button[contains(text(),'Add to cart')]"));
        IList<IWebElement> noOfActiveRemoveButtons => _driver.FindElements(By.XPath("//div[@class='pricebar']//button[contains(text(),'Remove')]"));
        IWebElement btnAddToCart => _driver.FindElement(By.XPath("//button[@id='add-to-cart-sauce-labs-backpack']"));
        IList<IWebElement> btnAddToCartSelected => _driver.FindElements(By.XPath("//div[@class='inventory_item']//button"));
        IWebElement btnRemove => _driver.FindElement(By.XPath("//button[@id='remove-sauce-labs-backpack']"));

        IWebElement sortingDropdown => _driver.FindElement(By.XPath("//select[@class='product_sort_container']"));

        public string GetProductsDetailsPageUrl()
        {
            return _driver.Url;
        }

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

        public int GetNoOfItems()
        {
            noOfItems = inventoryItems.Count();
            return noOfItems;
        }
        public int CheckInventoryImage()
        {
            noOfImages = inventoryImages.Count();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryImages[i].Displayed)
                    Console.WriteLine($"Item number " + i + " image is not displayed");
            }
            return noOfImages;
        }
        public int CheckInventoryItemName()
        {
            noOfItemNames = inventoryItemNames.Count();
            Console.WriteLine("Product Items\n");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItemNames[i].Displayed)
                    Console.WriteLine($"Item number " + i + " Name is not displayed");
                else
                {
                    Console.WriteLine($"Item " + i + " - " + inventoryItemNames[i].Text);
                }
            }
            return noOfItemNames;
        }
        public int CheckInventoryItemPrice()
        {
            noOfItemPrices = inventoryItemPrices.Count();
            Console.WriteLine("\nProduct Prices\n");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItemPrices[i].Displayed)
                    Console.WriteLine($"Item number " + i + " Price is not displayed");
                else
                {
                    Console.WriteLine($"Item " + i + " - " + inventoryItemPrices[i].Text);
                }
            }
            return noOfItemPrices;
        }
        public int CheckInventoryItemDescription()
        {
            noOfItemDes = inventoryItemDescriptions.Count();
            Console.WriteLine("\nProduct Descriptions\n");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItemDescriptions[i].Displayed)
                    Console.WriteLine($"Item number " + i + " Description is not displayed");
                else
                {
                    Console.WriteLine($"Item " + i + " - " + inventoryItemDescriptions[i].Text);
                }
            }
            return noOfItemDes;
        }
        public int CheckInventoryItemAddToCartButton()
        {
            noOfAddToCartButtons = noOfActiveAddToCartButtons.Count();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!noOfActiveAddToCartButtons[i].Displayed)
                    Console.WriteLine($"Item number " + i + " Add  To Cart button is not displayed");
            }
            return noOfAddToCartButtons;
        }

        public bool ShoppingCartIsDisplayed()
        {
            bool isDisplayed = false;
            if (commonPage.GetShoppingCart().Displayed)
                isDisplayed = true;
            return isDisplayed;
        }

        public void ClickItemName(int i)
        {
            inventoryItemNames[i].Click();
            Thread.Sleep(1000);

        }
        public void CheckInventoryItemNameLink()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                itemName = inventoryItemNames[i].Text;
                price = inventoryItemPrices[i].Text;
                description = inventoryItemDescriptions[i].Text;

                inventoryItemNames[i].Click();
                Thread.Sleep(1000);

                ValidateItemDetails();
                commonPage.TakeScreenShot("ProductDetails", "CheckInventoryItemNameLink");
                itemDetailsPage.ClickBtnBackToProduct();

            }
        }
        public void CheckInventoryItemImageLink()
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                itemName = inventoryItemNames[i].Text;
                price = inventoryItemPrices[i].Text;
                description = inventoryItemDescriptions[i].Text;

                inventoryImages[i].Click();
                Thread.Sleep(1000);

                ValidateItemDetails();
                commonPage.TakeScreenShot("ProductDetails", "CheckInventoryItemImageLink");

                itemDetailsPage.ClickBtnBackToProduct();

            }
        }

        public void ValidateItemDetails()
        {
            if (!itemDetailsPage.GetItemName().Equals(itemName))
                Console.WriteLine($"Item" + itemName + "Name is missmatched with selected");
            else if (!itemDetailsPage.GetItemDes().Equals(description))
                Console.WriteLine($"Item" + itemName + "Description is missmatched with selected");
            else if (!itemDetailsPage.GetItemPrice().Equals(price))
                Console.WriteLine($"Item" + itemName + "Pric is missmatched with selected");
        }

        public bool ValidateAddToCartButton()
        {
            int count = 0;
            bool status = false;
            foreach (var item in noOfActiveAddToCartButtons)
            {
                item.Click();
                count++;
                Thread.Sleep(1000);
                if (btnRemove.Displayed)
                    status = true;
            }
            Console.WriteLine($"Add to Cart icon is clicked " + count + " times");
            Console.WriteLine($"No. of shopping cart items: " + commonPage.GetNoOfShoppingCartItems().Text);

            int cartItems = Convert.ToInt32(commonPage.GetNoOfShoppingCartItems().Text);
            if (!cartItems.Equals(count))
                Console.WriteLine("No. of Shopping cart items incorrect!");

            return status;

        }
        public bool ValidateRemoveButton()
        {
            ValidateAddToCartButton();
            int startedCartItems = Convert.ToInt32(commonPage.GetNoOfShoppingCartItems().Text);
            int count = 0;
            status = false;
            foreach (var item in noOfActiveRemoveButtons)
            {
                item.Click();
                count++;
                startedCartItems--;
                Thread.Sleep(1000);
                if (btnAddToCart.Displayed)
                    status = true;
            }
            Console.WriteLine($"Remove button is clicked " + count + " times");
            Console.WriteLine($"No. of items in the Cart " + startedCartItems);
            return status;
        }


        public void LoadSortingOption(string sortingOption)
        {
            sortingDropdown.Click();
            SelectElement sortItems = new SelectElement(sortingDropdown);
            sortItems.SelectByText(sortingOption);
            Thread.Sleep(500);
        }

        public List<string> GetSortedOriginalItemNames()
        {
            List<string> itemNames = new List<string>();
            foreach (var item in inventoryItemNames)
            {
                itemNames.Add(item.Text);
            }
            itemNames.Sort();
            return itemNames;
        }
        public List<double> GetSortedOriginalItemPrices()
        {
            List<double> itemPrices = new List<double>();
            foreach (var item in inventoryItemPrices)
            {
                itemPrices.Add(Convert.ToDouble((item.Text).Substring(1).Trim()));
            }
            itemPrices.Sort();
            return itemPrices;
        }

        public bool ValidateSortByName(string sortingOption, List<string> sortedOriginalList)
        {
            status = false;
            LoadSortingOption(sortingOption);

            for (int i = 0; i < sortedOriginalList.Count; i++)
            {
                if (!sortedOriginalList[i].Equals(inventoryItemNames[i].Text))
                {
                    status = false;
                    break;
                }
                else
                    status = true;

            }
            Console.WriteLine("Sorted by" + sortingOption + "\n");
            for (int i = 0; i < sortedOriginalList.Count; i++)
            {
                Console.WriteLine($"Original:" + sortedOriginalList[i] + "    Sorted :" + inventoryItemNames[i].Text);
            }

            return status;
        }

        public bool ValidateSortByNameAtoZ()
        {
            if (ValidateSortByName("Name (A to Z)", GetSortedOriginalItemNames()).Equals(true))
            {
                status = true;
            }
            return status;
        }
        public bool ValidateSortByNameZtoA()
        {
            List<string> getReverseList = GetSortedOriginalItemNames();
            getReverseList.Reverse();

            if (ValidateSortByName("Name (Z to A)", getReverseList).Equals(true))
                status = true;

            return status;
        }


        public bool ValidateSortByPrice(string sortingOption, List<double> sortedOriginalList)
        {
            status = false;
            LoadSortingOption(sortingOption);

            for (int i = 0; i < sortedOriginalList.Count; i++)
            {
                if (!sortedOriginalList[i].Equals(Convert.ToDouble((inventoryItemPrices[i].Text).Substring(1).Trim())))
                {
                    status = false;
                    break;
                }
                else
                    status = true;

            }
            Console.WriteLine("Sorted by" + sortingOption + "\n");
            for (int i = 0; i < sortedOriginalList.Count; i++)
            {
                Console.WriteLine($"Original:" + sortedOriginalList[i] + "    Sorted :" + (inventoryItemPrices[i].Text).Substring(1));
            }

            return status;
        }


        public bool ValidateSortByPriceLowToHigh()
        {

            if (ValidateSortByPrice("Price (low to high)", GetSortedOriginalItemPrices()).Equals(true))
                status = true;
            return status;
        }

        public bool ValidateSortByPriceHighToLow()
        {
            List<double> getReversPrices = GetSortedOriginalItemPrices();
            getReversPrices.Reverse();
            if (ValidateSortByPrice("Price (high to low)", getReversPrices).Equals(true))
                status = true;
            return status;
        }

        public void AddItemsToCart(int item)
        {
            commonPage.ClickElement(btnAddToCartSelected[item]);
        }
    }
}
