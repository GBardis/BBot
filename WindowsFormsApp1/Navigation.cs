using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace WindowsFormsApp1
{
    class Navigation
    {
        private ReadOnlyCollection<IWebElement> categories;
        IWebDriver driver;       
        public void FindCategory(string category)
        {
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
                }
            }
        }
        public void FindAllBetCategories(string category1)
        {
            ReadOnlyCollection<IWebElement> betcategories = null ;
            driver = Form1.driver;
            bool FoundCat = true;
            while (FoundCat)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    betcategories = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".gl-MarketGroupButton_Text")));
                    if (betcategories != null)
                    {
                        FoundCat = false;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    FoundCat = true;
                }
            }
            foreach (IWebElement betcategory in betcategories)
            {
                try
                {
                    if (betcategory.Text == category1)
                    {
                        betcategory.Click();
                    }
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
            }
        }

    }
}
