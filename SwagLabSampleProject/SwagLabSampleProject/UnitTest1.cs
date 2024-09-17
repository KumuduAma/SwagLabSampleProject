//using AventStack.ExtentReports;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using SwagLabSampleProject.Models;
//using SwagLabSampleProject.POM;
//using SwagLabSampleProject.Reports;
//using System.Text.Json;

//namespace SwagLabSampleProject
//{
//    [TestFixture("standard_user", "secret_sauce")]
//    public class Tests
//    {
//        private IWebDriver driver;
//        private UserLogginPage logginPage;
//        private CommonPage commonPage;
//        private ProductsDetailsPage productsDetailsPage;
//        private ItemDetailsPage itemDetailsPage;
//        private YourCartPage yourCartPage;
//        private CheckoutPage checkoutPage;
//        private OverviewPage overviewPage;
//        private CheckoutCompletePage checkoutCompletePage;
//        private static string usersFile;
//        private readonly string _username;
//        private readonly string _password;
//        private ExtentReports _extent;
//        private ExtentTest _test;

//        [OneTimeSetUp]
//        public void SetupExtent()
//        {
//            _extent = ExtentManager.GetExtent();
//        }

//        public Tests(string _username, string _password)
//        {
//            this._username = _username;
//            this._password = _password;
//        }

//        [SetUp]
//        public void Setup()
//        {
//            driver = new ChromeDriver();
//            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
//            driver.Manage().Window.Maximize();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
//            logginPage = new UserLogginPage(driver);
//            commonPage = new CommonPage(driver);
//            productsDetailsPage = new ProductsDetailsPage(driver);
//            itemDetailsPage = new ItemDetailsPage(driver);
//            yourCartPage = new YourCartPage(driver);
//            checkoutPage = new CheckoutPage(driver);
//            overviewPage = new OverviewPage(driver);
//            checkoutCompletePage = new CheckoutCompletePage(driver);

//            logginPage.LoggingToPageVithValidDefaultUser(_username, _password);
//            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
//        }

//        [Test]
//        [TestCaseSource(nameof(LoadUsers))]
//        public void TestValidateLoggingUsingValidCredentials_TC_001(UserLoggingModel loggingModel)
//        {
//            logginPage.ValidateUserLoggingWithValidCredentials(loggingModel);

//            Assert.That(productsDetailsPage.GetProductsDetailsPageUrl(), Is.EqualTo("https://www.saucedemo.com/inventory.html"));
//        }

//        [Test]
//        [TestCaseSource(nameof(LoadInvalidUsers))]
//        public void TestValidateLoggingUsingInvalidUsernameAndValidPassword_TC_002(UserLoggingModel loggingModel)
//        {
//            Assert.That(logginPage.ValidateUserLoggingWithInvalidCredentials(loggingModel), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
//        }

//        public static IEnumerable<UserLoggingModel> LoadUsers()
//        {
//            usersFile = "ValidCredentials.json";
//            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT", usersFile);

//            string users = File.ReadAllText(usersDataPath);

//            var userModel = JsonSerializer.Deserialize<UserLoggingModel>(users, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });

//            yield return userModel;
//        }

//        public static IEnumerable<UserLoggingModel> LoadInvalidUsers()
//        {
//            usersFile = "InvalidUsernameValidPassword.json";
//            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT\\" + usersFile + "");

//            string users = File.ReadAllText(usersDataPath);

//            var userModel = JsonSerializer.Deserialize<UserLoggingModel>(users, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });

//            yield return userModel;
//        }

//        public void PerformInitialSteps()
//        {
//            productsDetailsPage.AddItemsToCart(1);
//            productsDetailsPage.AddItemsToCart(3);
//            commonPage.GetShoppingCart().Click();
//            yourCartPage.ClickCheckout();
//        }

//        //Product Details
//        [Test]
//        public void TestValidateProductImageIsDisplayedInTheProductCard_TC_012()
//        {
//            Assert.That(productsDetailsPage.CheckInventoryImage(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
//        }

//        [Test]
//        public void TestValidateProductNameIsDisplayedInTheProductCard_TC_013()
//        {
//            Assert.That(productsDetailsPage.CheckInventoryItemName(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
//        }
//        [Test]
//        public void TestValidateProductPriceIsDisplayedInTheProductCard_TC_014()
//        {
//            Assert.That(productsDetailsPage.CheckInventoryItemPrice(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
//        }
//        [Test]
//        public void TestValidateProductDescriptionIsDisplayedInTheProductCard_TC_015()
//        {
//            Assert.That(productsDetailsPage.CheckInventoryItemDescription(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
//        }
//        [Test]
//        public void TestValidateAddToCartButtonIsDisplayedInTheProductCard_TC_016()
//        {
//            Assert.That(productsDetailsPage.CheckInventoryItemAddToCartButton(), Is.EqualTo(productsDetailsPage.GetNoOfItems()));
//        }
//        [Test]
//        public void TestValidateProductsPageTitle_TC_017()
//        {
//            commonPage.TakeScreenShot("ProductPageTitle");
//            Assert.That(productsDetailsPage.GetPageTitle(), Is.EqualTo("Products"));
//        }
//        [Test]
//        public void TestValidateShoppingCartIsDisplayed_TC_018()
//        {
//            Assert.That(productsDetailsPage.ShoppingCartIsDisplayed(), Is.EqualTo(true));
//        }
//        [Test]
//        public void TestValidateProductNameLink_TC_019()
//        {
//            productsDetailsPage.CheckInventoryItemNameLink();
//        }
//        [Test]
//        public void TestValidateProductImageLink_TC_020()
//        {
//            productsDetailsPage.CheckInventoryItemImageLink();
//        }
//        [Test]
//        public void TestValidateWorkingOfAddToCartButton_TC_021()
//        {
//            Assert.That(productsDetailsPage.ValidateAddToCartButton(), Is.EqualTo(true));
//        }
//        [Test]
//        public void TestValidateWorkingOfRemoveButton_TC_022()
//        {
//            Assert.That(productsDetailsPage.ValidateRemoveButton(), Is.EqualTo(true));
//        }

//        //ItemDetails page
//        [Test]
//        public void TestValidateWorkingOfAddToCartInProductDetailsPage_TC_023()
//        {
//            int noOfItems = productsDetailsPage.GetNoOfItems();
//            for (int i = 0; i < noOfItems; i++)
//            {
//                productsDetailsPage.ClickItemName(i);
//                itemDetailsPage.ClickAddToCart();
//                itemDetailsPage.ClickBtnBackToProduct();
//            }
//        }

//        //Your cart page
//        [Test]
//        public void TestValidateWorkingOfShoppingCartIcon_TC_024()
//        {
//            Assert.That(yourCartPage.ValidateShoppingCart(), Is.EqualTo(true));
//        }
//        [Test]
//        public void TestValidateWorkingOfRemoveButtonOnYourCartPage_TC_025()
//        {
//            Assert.That(yourCartPage.ValidateRemoveButton(), Is.EqualTo(true));
//        }

//        [Test]

//        //Sort items
//        public void TestValidateSortingProductsByNameAtoZ_TC_26()
//        {
//            try
//            {
//                Assert.That(productsDetailsPage.ValidateSortByNameAtoZ(), Is.EqualTo(true));
//                commonPage.TakeScreenShot("SortingProductsByNameAtoZ");
//                _test.Log(Status.Pass, "Products Sorted by Name A to Z");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }

//        }

//        [Test]
//        public void TestValidateSortingProductsByNameZtoA_TC_27()
//        {
//            try
//            {
//                Assert.That(productsDetailsPage.ValidateSortByNameZtoA(), Is.EqualTo(true));
//                commonPage.TakeScreenShot("SortingProductsByNameZtoA");
//                _test.Log(Status.Pass, "Products Sorted by Name Z to A");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }

//        }

//        [Test]
//        public void TestValidateSortingProductsByPriceLowToHigh_TC_28()
//        {
//            try
//            {
//                Assert.That(productsDetailsPage.ValidateSortByPriceLowToHigh(), Is.EqualTo(true));
//                commonPage.TakeScreenShot("SortingProductsByPriceLowToHigh");
//                _test.Log(Status.Pass, "Products Sorted by Price Low to High");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }
//        }

//        [Test]
//        public void TestValidateSortingProductsByPriceHighToLow_TC_29()
//        {
//            try
//            {
//                Assert.That(productsDetailsPage.ValidateSortByPriceHighToLow(), Is.EqualTo(true));
//                commonPage.TakeScreenShot("SortingProductsByPriceHighToLow");
//                _test.Log(Status.Pass, "Products Sorted by Price High to Low");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }

//        }

//        [Test]
//        public void TestValidateCheckoutButtonWhenShoppingCartHasItems_TC_30()
//        {
//            productsDetailsPage.AddItemsToCart(1);
//            productsDetailsPage.AddItemsToCart(3);
//            commonPage.ClickElement(commonPage.GetShoppingCart());
//            Assert.That(yourCartPage.ValidateCheckOutButton(), Is.EqualTo(true));
//        }

//        [Test]
//        public void TestValidateCheckoutButtonWhenShoppingCartIsEmpty_TC_31()
//        {
//            try
//            {
//                commonPage.ClickElement(commonPage.GetShoppingCart());
//                Assert.That(yourCartPage.ValidateCheckOutButton(), Is.EqualTo(true));
//                _test.Log(Status.Pass, "Test Pass");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }

//        }

//        [Test]
//        public void TestValidateContinuShoppingButtion_TC_32()
//        {
//            try
//            {
//                productsDetailsPage.AddItemsToCart(1);
//                productsDetailsPage.AddItemsToCart(3);
//                commonPage.ClickElement(commonPage.GetShoppingCart());
//                Assert.That(yourCartPage.CheckContinueShoppingButton(), Is.EqualTo("Products"));
//                _test.Log(Status.Pass, "Test Pass");
//            }
//            catch (Exception ex)
//            {
//                _test.Log(Status.Fail, "Test Fail");
//            }

//        }

//        //Checkout Page
//        public static IEnumerable<CheckoutInformationModel> LoadUserInformations()
//        {
//            usersFile = "ValidCheckoutInformations.json";
//            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT", usersFile);

//            string users = File.ReadAllText(usersDataPath);

//            var informationModel = JsonSerializer.Deserialize<CheckoutInformationModel>(users, new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            });

//            yield return informationModel;
//        }

//        [Test]
//        [TestCaseSource(nameof(LoadUserInformations))]
//        public void TestValidateWorkingCheckoutWithValidInformation_TC_033(CheckoutInformationModel informationModel)
//        {
//            PerformInitialSteps();
//            checkoutPage.ValidateCheckoutWithValidInformation(informationModel);

//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Overview"));
//        }

//        [Test]
//        public void TestValidateWorkingCheckoutWithEmptyInformation_TC_034()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.ValidateCheckoutWithEmptyFields(), Is.EqualTo("Error: First Name is required"));
//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
//            commonPage.TakeScreenShot("CheckoutWithEmptyInformation");
//        }

//        [Test]
//        public void TestValidateWorkingCheckoutWithEmptyFirstName_TC_035()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.ValidateCheckoutWithEmptyFirstName(), Is.EqualTo("Error: First Name is required"));
//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
//            commonPage.TakeScreenShot("CheckoutWithEmptyFirstName");
//        }
//        [Test]
//        public void TestValidateWorkingCheckoutWithOnlyFirstName_TC_036()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.ValidateCheckoutWithOnlyFirstName(), Is.EqualTo("Error: Last Name is required"));
//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
//            commonPage.TakeScreenShot("CheckoutWithOnlyFirstName");
//        }


//        [Test]
//        public void TestValidateWorkingCheckoutWithoutLastName_TC_037()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.ValidateCheckoutWithoutLastName(), Is.EqualTo("Error: Last Name is required"));
//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
//            commonPage.TakeScreenShot("CheckoutWithoutLastName");
//        }

//        [Test]
//        public void TestValidateWorkingCheckoutWithoutZipCode_TC_038()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.ValidateCheckoutWithoutZipCode(), Is.EqualTo("Error: Postal Code is required"));
//            Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
//            commonPage.TakeScreenShot("CheckoutWithoutZipCode");
//        }

//        [Test]
//        public void TestCheckoutInformationFieldsPlaceHolders_TC_039()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.GetFirstNameFieldPlaceHolder(), Is.EqualTo("First Name"));
//            Assert.That(checkoutPage.GetLasttNameFieldPlaceHolder(), Is.EqualTo("Last Name"));
//            Assert.That(checkoutPage.GetZipCodeFieldPlaceHolder(), Is.EqualTo("Zip/Postal Code"));
//            commonPage.TakeScreenShot("CheckoutInformationFieldsPlaceHolders");
//        }


//        //Overview Page
//        [Test]
//        [TestCaseSource(nameof(LoadUserInformations))]
//        public void TestValidateItemTotal_TC_040(CheckoutInformationModel checkoutModel)
//        {
//            PerformInitialSteps();
//            checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
//            Assert.That(overviewPage.ValidateItemTotal(), Is.EqualTo(true));
//        }


//        [Test]
//        [TestCaseSource(nameof(LoadUserInformations))]
//        public void TestValidateTotalPrice_TC_041(CheckoutInformationModel checkoutModel)
//        {
//            PerformInitialSteps();
//            checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
//            Assert.That(overviewPage.ValidatePriceTotal(), Is.EqualTo(true));
//        }

//        [Test]
//        public void TestValidateCancelButtonInCheckoutPage_TC_042()
//        {
//            PerformInitialSteps();
//            Assert.That(checkoutPage.CheckCancelButton(), Is.EqualTo("Your Cart"));
//            commonPage.TakeScreenShot("ClickCancelButtonInCheckoutPage");
//        }

//        [Test]
//        [TestCaseSource(nameof(LoadUserInformations))]
//        public void TestValidateFinishOrder_TC_043(CheckoutInformationModel checkoutModel)
//        {
//            PerformInitialSteps();
//            checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
//            Assert.That(overviewPage.ValidateFinishButton(), Is.EqualTo("Checkout: Complete!"));
//            Assert.That(commonPage.GetShoppingCart().Text, Is.EqualTo(""));
//        }

//        [Test]
//        [TestCaseSource(nameof(LoadUserInformations))]
//        public void TestValidateBackHomeButton_TC_044(CheckoutInformationModel checkoutModel)
//        {
//            PerformInitialSteps();
//            checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
//            Assert.That(overviewPage.ValidateFinishButton(), Is.EqualTo("Checkout: Complete!"));
//            Assert.That(checkoutCompletePage.ValidateBackHomeButton(), Is.EqualTo("Products"));
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            driver.Dispose();
//            driver.Quit();
//        }

//        [OneTimeTearDown]
//        public void CleanExtent()
//        {
//            _extent.Flush();
//        }
//    }
//}