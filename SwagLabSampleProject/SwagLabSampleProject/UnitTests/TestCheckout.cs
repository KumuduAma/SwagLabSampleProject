using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabSampleProject.Models;
using SwagLabSampleProject.POM;
using SwagLabSampleProject.Reports;
using System.Text.Json;

namespace SwagLabSampleProject.UnitTests
{
    [TestFixture("standard_user", "secret_sauce")]
    public class TestCheckout
    {
        private IWebDriver driver;
        private CommonPage commonPage;
        private CheckoutInformationModel informationModel;
        private UserLogginPage logginPage;
        private ProductsDetailsPage productsDetailsPage;
        private ItemDetailsPage itemDetailsPage;
        private YourCartPage yourCartPage;
        private CheckoutPage checkoutPage;
        private OverviewPage overviewPage;
        private CheckoutCompletePage checkoutCompletePage;

        private ExtentReports _extent;
        private ExtentTest _test;
        private readonly string _username;
        private readonly string _password;
        private static string usersFile;

        [OneTimeSetUp]
        public void SetupExtent()
        {
            _extent = ExtentManager.GetExtent("checkoutExtentReport");
        }

        public TestCheckout(string _username, string _password)
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
            informationModel = new CheckoutInformationModel();
            productsDetailsPage = new ProductsDetailsPage(driver);
            yourCartPage = new YourCartPage(driver);
            checkoutPage = new CheckoutPage(driver);
            overviewPage = new OverviewPage(driver);
            checkoutCompletePage = new CheckoutCompletePage(driver);

            logginPage.LoggingToPageWithValidDefaultUser(_username, _password);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void PerformInitialSteps()
        {
            productsDetailsPage.AddItemsToCart(1);
            productsDetailsPage.AddItemsToCart(3);
            commonPage.GetShoppingCart().Click();
            yourCartPage.ClickCheckout();
        }

        public static IEnumerable<CheckoutInformationModel> LoadUserInformations()
        {
            usersFile = "ValidCheckoutInformations.json";
            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT", usersFile);

            string users = File.ReadAllText(usersDataPath);

            var informationModel = JsonSerializer.Deserialize<CheckoutInformationModel>(users, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            yield return informationModel;
        }

        [Test]
        [TestCaseSource(nameof(LoadUserInformations))]
        public void TestValidateWorkingCheckoutWithValidInformation_TC_033(CheckoutInformationModel informationModel)
        {
            try
            {
                PerformInitialSteps();
                checkoutPage.ValidateCheckoutWithValidInformation(informationModel);

                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Overview"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateWorkingCheckoutWithEmptyInformation_TC_CH_001()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.ValidateCheckoutWithEmptyFields(), Is.EqualTo("Error: First Name is required"));
                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
                commonPage.TakeScreenShot("Checkout", "CheckoutWithEmptyInformation");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateWorkingCheckoutWithEmptyFirstName_TC_CH_002()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.ValidateCheckoutWithEmptyFirstName(), Is.EqualTo("Error: First Name is required"));
                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
                commonPage.TakeScreenShot("Checkout", "CheckoutWithEmptyFirstName");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }

        }
        [Test]
        public void TestValidateWorkingCheckoutWithOnlyFirstName_TC_CH_003()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.ValidateCheckoutWithOnlyFirstName(), Is.EqualTo("Error: Last Name is required"));
                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
                commonPage.TakeScreenShot("Checkout", "CheckoutWithOnlyFirstName");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }


        [Test]
        public void TestValidateWorkingCheckoutWithoutLastName_TC_CH_004()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.ValidateCheckoutWithoutLastName(), Is.EqualTo("Error: Last Name is required"));
                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
                commonPage.TakeScreenShot("Checkout", "CheckoutWithoutLastName");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateWorkingCheckoutWithoutZipCode_TC_CH_005()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.ValidateCheckoutWithoutZipCode(), Is.EqualTo("Error: Postal Code is required"));
                Assert.That(commonPage.GetPageTitle(), Is.EqualTo("Checkout: Your Information"));
                commonPage.TakeScreenShot("Checkout", "CheckoutWithoutZipCode");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestCheckoutInformationFieldsPlaceHolders_TC_CH_006()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.GetFirstNameFieldPlaceHolder(), Is.EqualTo("First Name"));
                Assert.That(checkoutPage.GetLasttNameFieldPlaceHolder(), Is.EqualTo("Last Name"));
                Assert.That(checkoutPage.GetZipCodeFieldPlaceHolder(), Is.EqualTo("Zip/Postal Code"));
                commonPage.TakeScreenShot("Checkout", "CheckoutInformationFieldsPlaceHolders");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }


        [Test]
        [TestCaseSource(nameof(LoadUserInformations))]
        public void TestValidateItemTotal_TC_CH_007(CheckoutInformationModel checkoutModel)
        {
            try
            {
                PerformInitialSteps();
                checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
                Assert.That(overviewPage.ValidateItemTotal(), Is.EqualTo(true));
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }

        }


        [Test]
        [TestCaseSource(nameof(LoadUserInformations))]
        public void TestValidateTotalPrice_TC_CH_008(CheckoutInformationModel checkoutModel)
        {
            try
            {
                PerformInitialSteps();
                checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
                Assert.That(overviewPage.ValidatePriceTotal(), Is.EqualTo(true));
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }
        }

        [Test]
        public void TestValidateCancelButtonInCheckoutPage_TC_CH_009()
        {
            try
            {
                PerformInitialSteps();
                Assert.That(checkoutPage.CheckCancelButton(), Is.EqualTo("Your Cart"));
                commonPage.TakeScreenShot("Checkout", "ClickCancelButtonInCheckoutPage");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }

        }

        [Test]
        [TestCaseSource(nameof(LoadUserInformations))]
        public void TestValidateFinishOrder_TC_CH_010(CheckoutInformationModel checkoutModel)
        {
            try
            {
                PerformInitialSteps();
                checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
                Assert.That(overviewPage.ValidateFinishButton(), Is.EqualTo("Checkout: Complete!"));
                Assert.That(commonPage.GetShoppingCart().Text, Is.EqualTo(""));
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, ex.ToString());
            }

        }


        [Test]
        [TestCaseSource(nameof(LoadUserInformations))]
        public void TestValidateBackHomeButton_TC_CH_011(CheckoutInformationModel checkoutModel)
        {
            try
            {
                PerformInitialSteps();
                checkoutPage.ValidateCheckoutWithValidInformation(checkoutModel);
                Assert.That(overviewPage.ValidateFinishButton(), Is.EqualTo("Checkout: Complete!"));
                Assert.That(checkoutCompletePage.ValidateBackHomeButton(), Is.EqualTo("Products"));
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
