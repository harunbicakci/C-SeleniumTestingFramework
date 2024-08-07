using OpenQA.Selenium;
using System;

namespace TestFrameworkNUnit.Pages
{
	public class LoginPage : BasePage
	{
		public LoginPage(IWebDriver driver) : base(driver){}

        private IWebElement usernameField => Driver.FindElement(By.Id("username"));
        private IWebElement passwordField => Driver.FindElement(By.Id("password"));
        private IWebElement loginButton => Driver.FindElement(By.Id("loginButton"));

		public void EnterUsername(string username)
		{
			usernameField.SendKeys(username);
		}

        public void EnterPassword(string password)
        {
            passwordField.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            loginButton.Click();
        }
    }
}

