using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace WindowsFormsApp1
{
    class Navigation
    {
        IWebDriver driver;
        IWebElement element;
        private ReadOnlyCollection<IWebElement> categories;
        Form1 rest = new Form1();
        public void FindCategory(string category)
        {
            element = Form1.element;
            driver = Form1.driver;
            bool FoundCat = true;
            while (FoundCat)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    categories = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".wn-Classification ")));
                    if (categories != null)
                    {
                        FoundCat = false;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    FoundCat = true;
                }
            }
<<<<<<< HEAD
            foreach (var z in categories)
            {
                if (z.Text == category)
                {
                    z.Click();
=======
            foreach (IWebElement element in categories)
            {
                try
                {
                    if (element.Text == category)
                    {
                        element.Click();
                    }
                }
                catch (InvalidOperationException)
                {
                    continue;
>>>>>>> e2f9e190b8d2f8a49875274176d01f66e5473dfc
                }
            }
        }
    }
}