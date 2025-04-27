using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DotNetSelenium.PageObjects;
using DotNetSelenium.Utilities;
using OpenQA.Selenium.Support.UI;  // Correct namespace

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

        //[Test]
        //public void EAWebsite()
        //{
        //    driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        //    var loginPage = new LoginPage(driver);
        //    loginPage.Login("admin", "password");
        //    // Capture the screenshot
        //    ScreenshotHelper.CaptureScreenshot(driver);
        //}

        [Test]
        public void TC01_SortByName()
        {
            driver.Navigate().GoToUrl("http://live.techpanda.org/index.php/");
            driver.Title.Contains("Home page");
            driver.FindElement(By.XPath("//a[normalize-space()='Mobile']")).Click();
            driver.Title.Contains("Mobile");

            //Dropdown
            IWebElement dropdownElement = driver.FindElement(By.XPath("//select[@title='Sort By']"));
            SelectElement select = new SelectElement(dropdownElement);
            select.SelectByText("Name");

            //Verify all products are sorted by Name
            //Find all products name elements
            IList<IWebElement> productElements = driver.FindElements(By.XPath("//h2[@class='product-name']/a"));
            List<string> actualProductNames = new List<string>();

            for (int i = 0; i < productElements.Count; i++)
            {
                var product = productElements[i];
                var productName = product.Text;
                //Console.WriteLine(productName);
                actualProductNames.Add(productName.Trim());
                //Console.WriteLine($"Output {productName}");
                // Verify that the product name is not empty
                Assert.IsFalse(string.IsNullOrEmpty(productName), "Product name is empty.");
            }

            //actualProductNames.Sort();

            List<string> expectedProductNames = new List<string>(actualProductNames);
            expectedProductNames.Sort();

            Console.WriteLine("Tejas " + string.Join(", ", expectedProductNames));

            bool isSorted = actualProductNames.SequenceEqual(expectedProductNames);

            if(isSorted)
            {
                Console.WriteLine("Products are sorted by Name correctly.");
            }
            else
            {
                Console.WriteLine("Products are NOT sorted by Name.");
            }
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
