using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabSampleProject.Models;
using SwagLabSampleProject.POM;
using SwagLabSampleProject.Reports;
using System.Text.Json;

namespace SwagLabSampleProject.UnitTests
{
    public class TestUserLoggin
    {
        private IWebDriver driver;
        private CommonPage commonPage;
        private UserLogginPage logginPage;
        private ProductsDetailsPage productsDetailsPage;
        private UserLoggingModel logginUserLoggingModel;
        private static string usersFile;
        private ExtentReports _extent;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void SetupExtent()
        {
            _extent = ExtentManager.GetExtent("userLogginExtentReport");
        }


        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            commonPage = new CommonPage(driver);
            logginPage = new UserLogginPage(driver);
            productsDetailsPage = new ProductsDetailsPage(driver);
            logginUserLoggingModel = new UserLoggingModel();
            usersFile = "";
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        [TestCaseSource(nameof(LoadValidUsers))]
        public void TestValidateLoggingUsingValidCredentials_TC_001(UserLoggingModel loggingModel)
        {
            try
            {
                logginPage.ValidateUserLoggingWithValidCredentials(loggingModel);
                Assert.That(productsDetailsPage.GetProductsDetailsPageUrl(), Is.EqualTo("https://www.saucedemo.com/inventory.html"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        [Test]
        [TestCaseSource(nameof(LoadInvalidUsers))]
        public void TestValidateLoggingUsingInvalidUserCredentials_TC_002(UserLoggingModel loggingModel)
        {
            try
            {
                Assert.That(logginPage.ValidateUserLoggingWithInvalidCredentials(loggingModel), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
                _test.Log(Status.Pass, "Test Pass");
            }
            catch
            {
                _test.Log(Status.Fail, "Test Fail");
            }
        }

        public static IEnumerable<UserLoggingModel> LoadValidUsers()
        {
            usersFile = "ValidCredentials.json";
            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT", usersFile);

            string users = File.ReadAllText(usersDataPath);

            var userModel = JsonSerializer.Deserialize<UserLoggingModel>(users, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            yield return userModel;
        }

        public static IEnumerable<UserLoggingModel> LoadInvalidUsers()
        {
            usersFile = "InvalidUsernameValidPassword.json";
            string usersDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DDT\\" + usersFile + "");

            string users = File.ReadAllText(usersDataPath);

            var userModel = JsonSerializer.Deserialize<UserLoggingModel>(users, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            yield return userModel;
        }


        [TearDown]
        public void OneTimeTearDown()
        {
            driver.Dispose();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void CleanExtent()
        {
            _extent.Flush();
        }

    }
}
