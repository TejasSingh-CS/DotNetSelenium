using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DotNetSelenium.PageObjects;
using DotNetSelenium.Utilities;  // Correct namespace

namespace DotNetSelenium
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void EAWebsite()
        {
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            var loginPage = new LoginPage(driver);
            loginPage.Login("admin", "password");
            // Capture the screenshot
            ScreenshotHelper.CaptureScreenshot(driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
