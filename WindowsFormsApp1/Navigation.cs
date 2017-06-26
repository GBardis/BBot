using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WindowsFormsApp1
{
    class Navigation
    {
        IWebDriver driver;
        IWebElement element;
        Form1 rest = new Form1();
        public void FindCategory(string category)
        {
            driver = Form1.driver;
            element = Form1.element;
            int i = 1;
            List<IWebElement> categories = new List<IWebElement>();
            bool FoundCat = true;
            while (FoundCat)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    element = wait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[1]/div/div/div[" + i + "]")));
                    categories.Add(element);
                    i++;
                }
                catch (NoSuchElementException ex)
                {
                    FoundCat = false;
                }
                catch (WebDriverTimeoutException ex)
                {
                    FoundCat = false;
                }
            }
            foreach (var element in categories)
            {
                if (element.Text == category)
                {
                    element.Click();
                }
            }
        }
    }
}
