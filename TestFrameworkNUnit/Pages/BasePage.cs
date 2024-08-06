using OpenQA.Selenium;
using System;

namespace TestFrameworkNUnit.Pages
{
	public class BasePage
	{
		protected class IWebDriver Driver;

		public BasePage(IWebDriver driver)
		{
			Driver = driver;
		}
	}
}

