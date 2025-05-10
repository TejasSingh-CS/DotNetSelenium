using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DotNetSelenium.PageObjects;
using DotNetSelenium.Utilities;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using DotNetSelenium.Helper;  // Correct namespace

namespace DotNetSelenium
{
    public class Tests
    {
        private IWebDriver driver = null!;
        private helperClass browserHelper;

        [SetUp]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void Setup()
        {
            //driver = new ChromeDriver();
            browserHelper = new helperClass();
            driver = browserHelper.GetBrowseTypes(helperClass.BrowserType.Chrome);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void EAWebsite()
        {
            var loginPage = new LoginPage(driver);
            var employeeList = new EmployeeList(driver);
            loginPage.Login("admin", "password");
            employeeList.ClickEmployeeList();
            employeeList.CreateNewBtn();
            // Capture the screenshot
            ScreenshotHelper.CaptureScreenshot(driver);
        }

        [TearDown]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
