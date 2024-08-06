using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;

namespace TestFrameworkNUnit.Drivers
{
	public class WebDriverInitializer
	{
		private static IWebDriver _driver;

		public static IWebDriver InitializeDriver(string browser)
		{
			switch (browser.ToLower())
			{
				case "chrome":
					_driver = new ChromeDriver();
					break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "forefox":
                    _driver = new FirefoxDriver();
                    break;
				default:
					throw new ArgumentException("Browser NOT supported!");
            }
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			return _driver;
		}
		public static void QuitDriver()
		{

		}
	}
}

