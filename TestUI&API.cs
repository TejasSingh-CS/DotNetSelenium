using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DotNetSelenium.PageObjects;
using DotNetSelenium.Utilities;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System.Diagnostics;  // Correct namespace

namespace DotNetSelenium
{
    public class Tests2
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void EAWebsite()
        {
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            var loginPage = new LoginPage(driver);
            loginPage.Login("admin", "password");
            // Capture the screenshot
            ScreenshotHelper.CaptureScreenshot(driver);
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_SortByName()
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

            if (isSorted)
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

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_VerifyProducListPage()
        {
            driver.Navigate().GoToUrl("http://live.techpanda.org/index.php/");
            driver.Title.Contains("Home page");
            driver.FindElement(By.XPath("//a[normalize-space()='Mobile']")).Click();
            driver.Title.Contains("Mobile");


            // Capture the screenshot
            ScreenshotHelper.CaptureScreenshot(driver);
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_RadioButton()
        {
            //driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form/");

            ////Get list of counts - Male, Female & Other
            //var radioList = driver.FindElements(By.XPath("//input[@name='gender']"));
            //Trace.WriteLine("RadioButton count: " + radioList.Count);

            //var genderValue = "Male";
            //foreach (var option in radioList)
            //{
            //    if (option.GetAttribute("value").Equals(genderValue, StringComparison.OrdinalIgnoreCase))
            //    {
            //        var selectValue = driver.FindElement(By.XPath($"//input[@value='{genderValue}']//following-sibling::label[@class='custom-control-label']"));
            //        //var selectValue = driver.FindElement(By.XPath($"//label[@for='{option.GetAttribute("id")}']"));
            //        selectValue.Click();
            //    }
            //}
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_Checkbox()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form/");

            var checkboxList = driver.FindElements(By.XPath("//input[@type='checkbox']/following-sibling::label"));
            Trace.WriteLine("Checkbox count: " + checkboxList.Count);

            foreach (var i in checkboxList)
            {
                js.ExecuteScript("arguments[0].scrollIntoView(true);", i);
                i.Click();
            }
        }

        [Test]
        public void DynamicWebTables()
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Programming_languages_used_in_most_popular_websites");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IList<IWebElement> tableTitle = driver.FindElements(By.XPath("//table[@class='wikitable sortable sort-under col2right jquery-tablesorter']//tr/th"));

            IList<IWebElement> rowDetails = driver.FindElements(By.XPath("//table[@class='wikitable sortable sort-under col2right jquery-tablesorter']//tbody//tr"));

            //Printing Table Title
            foreach (var title in tableTitle)
            {
                //Console.WriteLine(title.Text);
            }

            for (int i = 1; i < rowDetails.Count; i++)
            {
                try
                {
                    // Get the 3rd column (Front-end) text for this row
                    IWebElement fronEndColumn = driver.FindElement(By.XPath($"//table[@class='wikitable sortable sort-under col2right jquery-tablesorter']//tbody//tr[{i}]//td[3]"));

                    if (fronEndColumn.Text.Contains("JavaScript"))
                    {
                        // Get the first column link in the same row
                        IWebElement link = driver.FindElement(By.XPath($"//table[@class='wikitable sortable sort-under col2right jquery-tablesorter']//tbody//tr[{i}]//td[1]//a"));

                        Console.WriteLine("Clicking on website using TypeScript: " + link.Text);
                        link.Click();

                        Thread.Sleep(2000); // Let the new page load
                        driver.Navigate().Back(); // Go back to table page
                        Thread.Sleep(2000); // Allow table to reload

                        // Re-fetch all rows again after navigating back (DOM refreshes)
                        //rowDetails = driver.FindElements(By.XPath("//table[@class='wikitable sortable sort-under col2right jquery-tablesorter']//tbody//tr"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                }
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_RestSharpAPI_GET()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("posts", Method.Get);

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine("API call was successful.");
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                Console.WriteLine("API call failed. Status code: " + response.StatusCode);
                Console.WriteLine("Error message: " + response.ErrorMessage);
            }
        }

        [Test]
        [Ignore("Skipping due to CAPTCHA issue on the site")]
        public void TC_RestSharpAPI_POST()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                title = "foo",
                body = "bar",
                userId = 1
            });
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine("API call was successful.");
                Console.WriteLine("Response content: " + response.Content);
            }
            else
            {
                Console.WriteLine("API call failed. Status code: " + response.StatusCode);
                Console.WriteLine("Error message: " + response.ErrorMessage);
            }

        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                //driver.Quit();
                driver.Dispose();
                Trace.Flush();
            }
        }
    }
}
