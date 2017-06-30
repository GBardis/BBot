using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WindowsFormsApp1
{
    class BetOption
    {
        public void oneXTwo(string result)
        {
            try
            {
                switch (result)
                {
                    case "1":
                        Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[1]"));
                        break;
                    case "X":
                        Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[2]"));
                        break;
                    case "2":
                        Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[3]"));
                        break;
                    default:
                        break;
                }
            }
            catch (NoSuchElementException)
            {

            }
        }
        public void overUnder(string overunder)
        {
            try
            {
                switch (overunder)
                {
                    case "over":
                        Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[2]/div[2]"));
                        break;
                    case "under":
                        Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[3]/div[2]"));
                        break;
                    default:
                        break;
                }
            }
            catch (NoSuchElementException)
            {

            }
        }
        public void closeOpenDivs()
        {
            IWebDriver driver;
            ReadOnlyCollection<IWebElement> elements = null;
            driver = Form1.driver;
            bool NotEnabled = true;
            while (NotEnabled)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    elements = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".sm-Market_HeaderOpen")));
                    if (elements != null)
                    {
                        NotEnabled = false;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    NotEnabled = true;
                }
            }

            foreach (IWebElement element in elements)
            {
                try
                {
                    element.Click();
                }
                catch (InvalidOperationException)
                {
                    //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    //js.ExecuteScript("window.scrollTo(0,", element.Location.Y + ")");
                    element.Click();
                }
            }
        }
    }
}
