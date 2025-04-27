using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DotNetSelenium.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        //Elements
        private IWebElement LoginLink => _driver.FindElement(By.LinkText("Login"));
        private IWebElement UserNameField => _driver.FindElement(By.Name("UserName"));
        private IWebElement PasswordField => _driver.FindElement(By.Name("Password"));
        private IWebElement SubmitButton => _driver.FindElement(By.XPath("//input[@type='submit']"));

        //Actions
        public void ClickLoginLink()
        {
            LoginLink.Click();
        }

        public void EnterUsername(string username)
        {
            UserNameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordField.SendKeys(password);
        }

        public void ClickSubmit()
        {
            SubmitButton.Click();
        }

        public void Login(string username, string password)
        {
            ClickLoginLink();
            EnterUsername(username);
            EnterPassword(password);
            ClickSubmit();
        }

    }
}
