using AventStack.ExtentReports;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabSampleProject.POM;
using SwagLabSampleProject.Reports;

namespace SwagLabSampleProject.UnitTests
{
    [TestFixture("standard_user", "secret_sauce")]
    public class TestYourCart
    {
        private IWebDriver driver;
        private CommonPage commonPage;
        private UserLogginPage logginPage;
        private ProductsDetailsPage productsDetailsPage;
        private YourCartPage yourCartPage;
        private ExtentReports _extent;
        private ExtentTest _test;
        private readonly string _username;
        private readonly string _password;

        [OneTimeSetUp]
        public void SetupExtent()
        {
            _extent = ExtentManager.GetExtent("yourCartExtentReport");
        }
        public TestYourCart(string _username, string _password)
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

            commonPage=new CommonPage(driver);
            logginPage = new UserLogginPage(driver);
            productsDetailsPage = new ProductsDetailsPage(driver);
            yourCartPage = new YourCartPage(driver);

            logginPage.LoggingToPageWithValidDefaultUser(_username, _password);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestValidateWorkingOfShoppingCartIcon_TC_YC_001()
        {
            try
            {
                productsDetailsPage.AddItemsToCart(1);
                productsDetailsPage.AddItemsToCart(3);
                Assert.That(yourCartPage.ValidateShoppingCart(), Is.EqualTo(true));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        public void TestValidateWorkingOfRemoveButtonOnYourCartPage_TC_YC_002()
        {
            try
            {
                productsDetailsPage.AddItemsToCart(1);
                productsDetailsPage.AddItemsToCart(3);

                Assert.That(yourCartPage.ValidateRemoveButton(), Is.EqualTo(true));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateCheckoutButtonWhenShoppingCartHasItems_TC_YC_003()
        {
            try
            {
                productsDetailsPage.AddItemsToCart(1);
                productsDetailsPage.AddItemsToCart(3);
                Assert.That(yourCartPage.ValidateCheckOutButton(), Is.EqualTo("Checkout: Your Information"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }

        }

        [Test]
        public void TestValidateCheckoutButtonWhenShoppingCartIsEmpty_TC_YC_004()
        {
            try
            {
                Assert.That(yourCartPage.ValidateCheckOutButton(), Is.EqualTo("Your Cart"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "Test Fail");
            }

        }

        [Test]
        public void TestValidateContinuShoppingButtion_TC_YC_005()
        {
            try
            {
                productsDetailsPage.AddItemsToCart(1);
                productsDetailsPage.AddItemsToCart(3);
                commonPage.ClickElement(commonPage.GetShoppingCart());
                Assert.That(yourCartPage.CheckContinueShoppingButton(), Is.EqualTo("Products"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
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
