using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabSampleProject.POM;
using SwagLabSampleProject.Reports;

namespace SwagLabSampleProject.UnitTests
{
    [TestFixture("standard_user", "secret_sauce")]
    public class TestProductsPage
    {
        private IWebDriver driver;
        private CommonPage commonPage;
        private UserLogginPage logginPage;
        private ProductsDetailsPage productsDetailsPage;
        private ItemDetailsPage itemDetailsPage;
        private ExtentReports _extent;
        private ExtentTest _test;
        private readonly string _username;
        private readonly string _password;

        [OneTimeSetUp]
        public void SetupExtent()
        {
            _extent = ExtentManager.GetExtent("productsExtentReport");
        }

        public TestProductsPage(string _username, string _password)
        {
            this._username = _username;
            this._password = _password;
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            commonPage = new CommonPage(driver);
            logginPage = new UserLogginPage(driver);
            productsDetailsPage = new ProductsDetailsPage(driver);
            itemDetailsPage = new ItemDetailsPage(driver);

            logginPage.LoggingToPageWithValidDefaultUser(_username, _password);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestValidateProductImageIsDisplayedInTheProductCard_TC_Pro_001()
        {
            try
            {
                Assert.That(productsDetailsPage.CheckInventoryImage(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateProductNameIsDisplayedInTheProductCard_TC_Pro_002()
        {
            try
            {
                Assert.That(productsDetailsPage.CheckInventoryItemName(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateProductPriceIsDisplayedInTheProductCard_TC_Pro_003()
        {
            try
            {
                Assert.That(productsDetailsPage.CheckInventoryItemPrice(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }
        [Test]
        public void TestValidateProductDescriptionIsDisplayedInTheProductCard_TC_Pro_004()
        {
            try
            {
                Assert.That(productsDetailsPage.CheckInventoryItemDescription(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }
        [Test]
        public void TestValidateAddToCartButtonIsDisplayedInTheProductCard_TC_Pro_005()
        {
            try
            {
                Assert.That(productsDetailsPage.CheckInventoryItemAddToCartButton(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateProductsPageTitle_TC_Pro_006()
        {
            try
            {
                Assert.That(productsDetailsPage.GetPageTitle(), Is.EqualTo("Products"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateShoppingCartIsDisplayed_TC_Pro_007()
        {
            try
            {
                Assert.That(productsDetailsPage.ShoppingCartIsDisplayed(), Is.EqualTo(true));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateProductNameLink_TC_Pro_008()
        {
            try
            {
                productsDetailsPage.CheckInventoryItemNameLink();
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateProductImageLink_TC_Pro_009()
        {
            try
            {
                productsDetailsPage.CheckInventoryItemImageLink();
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateWorkingOfAddToCartButton_TC_Pro_010()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateAddToCartButton(), Is.EqualTo(true));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }
        [Test]
        public void TestValidateWorkingOfRemoveButton_TC_Pro_011()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateRemoveButton(), Is.EqualTo(true));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateWorkingOfAddToCartInProductDetailsPage_TC_Pro_012()
        {
            try
            {
                int noOfItems = productsDetailsPage.GetNoOfItems();
                for (int i = 0; i < noOfItems; i++)
                {
                    productsDetailsPage.ClickItemName(i);
                    itemDetailsPage.ClickAddToCart();
                    itemDetailsPage.ClickBtnBackToProduct();
                }
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }

        }

        [Test]
        public void TestValidateSortingProductsByNameAtoZ_TC_Pro_013()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateSortByNameAtoZ(), Is.EqualTo(true));
                commonPage.TakeScreenShot("Products", "SortingProductsByNameAtoZ");
                _test.Log(Status.Pass, "Products Sorted by Name A to Z");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }

        }

        [Test]
        public void TestValidateSortingProductsByNameZtoA_TC_Pro_014()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateSortByNameZtoA(), Is.EqualTo(true));
                commonPage.TakeScreenShot("Products", "SortingProductsByNameZtoA");
                _test.Log(Status.Pass, "Products Sorted by Name Z to A");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }

        }

        [Test]
        public void TestValidateSortingProductsByPriceLowToHigh_TC_Pro_015()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateSortByPriceLowToHigh(), Is.EqualTo(true));
                commonPage.TakeScreenShot("Products", "SortingProductsByPriceLowToHigh");
                _test.Log(Status.Pass, "Products Sorted by Price Low to High");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateSortingProductsByPriceHighToLow_TC_Pro_016()
        {
            try
            {
                Assert.That(productsDetailsPage.ValidateSortByPriceHighToLow(), Is.EqualTo(true));
                commonPage.TakeScreenShot("Products", "SortingProductsByPriceHighToLow");
                _test.Log(Status.Pass, "Products Sorted by Price High to Low");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }

        }


        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void ClearExtent()
        {
            _extent.Flush();
        }

    }
}
