using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotNetSelenium.PageObjects
{
    public class EmployeeList
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EmployeeList(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        private IWebElement employeeListTxtBtn => _wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Employee List')]")));
        private IWebElement createNewBtn => _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]"));

        public void ClickEmployeeList()
        {
            employeeListTxtBtn.Click();
        }

        public void CreateNewBtn()
        {
            createNewBtn.Click();
        }


    }
}
