using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;

namespace TestFrameworkNUnit.Utils
{
    public class CommonMethods
    {
        private static IWebDriver driver;

        public CommonMethods(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        // This method clears the textbox and sends another text
        public static void SendText(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        // This method checks if radio/checkbox is enabled and then clicks on the element that has the value we want
        public static void ClickRadioOrCheckbox(List<IWebElement> listElement, string value)
        {
            string actualValue;

            foreach (IWebElement el in listElement)
            {
                actualValue = el.GetAttribute("value").Trim();
                if (el.Enabled && actualValue.Equals(value))
                {
                    el.Click();
                    break;
                }
            }
        }

        // This method checks if the text is found in the dropdown element and only then it selects it
        public static void SelectDropdown(IWebElement element, string textToSelect)
        {
            try
            {
                SelectElement select = new SelectElement(element);

                foreach (IWebElement el in select.Options)
                {
                    if (el.Text.Equals(textToSelect))
                    {
                        select.SelectByText(textToSelect);
                        break;
                    }
                }
            }
            catch (UnexpectedTagNameException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method checks if the index is valid and only then selects it
        public static void SelectDropdown(IWebElement element, int index)
        {
            try
            {
                SelectElement select = new SelectElement(element);

                int size = select.Options.Count;

                if (size > index)
                {
                    select.SelectByIndex(index);
                }
            }
            catch (UnexpectedTagNameException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method accepts alerts and catches exception if alert in not present
        public static void AcceptAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method will dismiss the alert after checking if alert is present
        public static void DismissAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();
            }
            catch (NoAlertPresentException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method returns the alert text. If no alert is present exception is caught and null is returned.
        public static string GetAlertText()
        {
            string alertText = null;

            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alertText = alert.Text;
            }
            catch (NoAlertPresentException e)
            {
                Debug.WriteLine(e.Message);
            }

            return alertText;
        }

        // This method send text to the alert. NoAlertPresentException is handled.
        public static void SendAlertText(string text)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.SendKeys(text);
            }
            catch (NoAlertPresentException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method switches to a frame by using name or id
        public static void SwitchToFrame(string nameOrId)
        {
            try
            {
                driver.SwitchTo().Frame(nameOrId);
            }
            catch (NoSuchFrameException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method switches to a frame by using an index
        public static void SwitchToFrame(int index)
        {
            try
            {
                driver.SwitchTo().Frame(index);
            }
            catch (NoSuchFrameException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method switches to a frame by using a WebElement
        public static void SwitchToFrame(IWebElement element)
        {
            try
            {
                driver.SwitchTo().Frame(element);
            }
            catch (NoSuchFrameException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method switches focus to a child window
        public static void SwitchToChildWindow()
        {
            string mainWindow = driver.CurrentWindowHandle;
            var windows = driver.WindowHandles;

            foreach (string window in windows)
            {
                if (!window.Equals(mainWindow))
                {
                    driver.SwitchTo().Window(window);
                }
            }
        }

        // This method creates a WebDriverWait object and returns it
        public static WebDriverWait GetWaitObject()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.EXPLICIT_WAIT_TIME));
        }

        // This method waits for an item to be clickable
        public static IWebElement WaitForClickability(IWebElement element)
        {
            return GetWaitObject().Until(ExpectedConditions.ElementToBeClickable(element));
        }

        // This method waits for an element to be visible
        public static IWebElement WaitForVisibility(IWebElement element)
        {
            return GetWaitObject().Until(ExpectedConditions.VisibilityOf(element));
        }

        // This method clicks on an element and has wait implemented on it
        public static void Click(IWebElement element)
        {
            WaitForClickability(element);
            element.Click();
        }

        public static void Wait(int seconds)
        {
            try
            {
                Thread.Sleep(seconds * 1000);
            }
            catch (ThreadInterruptedException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        // This method casts the driver to a JavascriptExecutor and returns it
        public static IJavaScriptExecutor GetJSObject()
        {
            return (IJavaScriptExecutor)driver;
        }

        // This method will click on the element passed to it using JavascriptExecutor
        public static void JsClick(IWebElement element)
        {
            GetJSObject().ExecuteScript("arguments[0].click()", element);
        }

        // This method will scroll the page until the element passed to it becomes visible
        public static void ScrollToElement(IWebElement element)
        {
            GetJSObject().ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        // This method will scroll the page down based on the passed pixel parameter
        public static void ScrollDown(int pixel)
        {
            GetJSObject().ExecuteScript($"window.scrollBy(0,{pixel});");
        }

        // This method will scroll the page up based on the passed pixel parameter
        public static void ScrollUp(int pixel)
        {
            GetJSObject().ExecuteScript($"window.scrollBy(0,-{pixel});");
        }

        // This method will select a date from the calendar
        public static void SelectCalendarDate(List<IWebElement> elements, string text)
        {
            foreach (IWebElement day in elements)
            {
                if (day.Enabled && day.Text.Equals(text))
                {
                    day.Click();
                    break;
                }
            }
        }

        public static byte[] TakeScreenshot(string filename)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;

            byte[] picBytes = ts.GetScreenshot().AsByteArray;

            string destination = $"{Constants.SCREENSHOT_FILEPATH}{filename}{GetTimeStamp()}.png";

            try
            {
                File.WriteAllBytes(destination, ts.GetScreenshot().AsByteArray);
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.Message);
            }

            return picBytes;
        }

        // Method to return the current time stamp in a String
        public static string GetTimeStamp()
        {
            DateTime date = DateTime.Now;
            return date.ToString("yyyy_MM_dd_HH_mm_ss");
        }
    }
}

