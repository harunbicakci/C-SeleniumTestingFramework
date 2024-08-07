using OpenQA.Selenium;
using System;

namespace TestFrameworkNUnit.Pages
{
	public class BasePage
	{
		protected IWebDriver Driver;

		protected BasePage(IWebDriver driver)
		{
			Driver = driver;
		}
	}
}

