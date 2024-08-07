using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFrameworkNUnit.Drivers;
using TestFrameworkNUnit.Pages;
using System;

namespace TestFrameworkNUnit.StepDefinitions
{
	[Binding]
	public class LoginSteps
	{
		private IWebDriver _driver;
		private LoginPage _loginPage;

		[BeforeScenario]
		public void Setup()
		{
			_driver = WebDriverInitializer.InitializeDriver("chrome");
			_loginPage = new LoginPage(_driver);
		}

		[AfterScenario]
		public void TearDown()
		{
			WebDriverInitializer.QuitDriver();

		}

		[Given(@"I enter username '(.*)'")]
		public void GivenIEnterUsername(string username)
		{
			_loginPage.EnterUsername(username);
		}

        [Given(@"I enter password '(.*)'")]
        public void GivenIEnterPassword(string password)
        {
			_loginPage.EnterPassword(password);
        }

		[When(@"I click on login button")]
		public void WhenIClickOnLoginButton()
		{
			_loginPage.ClickLoginButton();
		}

		[Then(@"I should see the home page")]
		public void ThenIShouldSeeTheHomePage()
		{
			Assert.IsTrue(_driver.Title.Contains("Home Page"));
		}


    }
}

