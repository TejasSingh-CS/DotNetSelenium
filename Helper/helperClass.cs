using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace DotNetSelenium.Helper
{
    public class helperClass
    {

        public enum BrowserType
        {
            Chrome,
            Firefox,
            Edge
        }

        public IWebDriver GetBrowseTypes(BrowserType browserType)
        {

            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    return new ChromeDriver();
            }
            //return driver;
        }
    }
}
