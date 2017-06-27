﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.ObjectModel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        BetOption betoption = new BetOption();
        Navigation nav = new Navigation();
        public static IWebDriver driver = new FirefoxDriver();
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public static IWebElement element;
        private ReadOnlyCollection<IWebElement> elements;


        public Form1()
        {
            InitializeComponent();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.bet365.gr/#/HO");
            label4.Text = driver.Title;
            var dictionary = File.ReadLines(@"Football.csv").Select(line => line.Split(','));
            foreach (string[] e in dictionary)
            {
                dict.Add(e[0].ToString(), e[1].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IWebElement UserNameText = driver.FindElement(By.CssSelector(".hm-Login_InputField"));
            IWebElement PasswordText = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[1]"));
            PasswordText.Click();
            IWebElement PasswordText2 = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[2]"));
            IWebElement element2 = driver.FindElement(By.CssSelector(".hm-Login_LoginBtn"));
            element2.Click();
        }
        private void Wb_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();
            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {           
            string category = "Ποδόσφαιρο";
            string country = "Ην. Βασίλειο";
            string division = "Αγγλία - Πρέμιερ Λιγκ";
            List<string> z = new List<string>();
            label7.Text = "";
            nav.FindCategory(category);
            betoption.closeOpenDivs();
            navigateBeforeBet(country, division);
            driver.Navigate().GoToUrl("http://www.bet365.gr/#/HO");
        }
        private void navigateBeforeBet(string country, string division)
        {
            bool flag = false;     
            List<string> games = new List<string>();

            try
            {
                if (dict.ContainsKey(country))
                {
                    WaitForElementVisible(By.XPath(dict[country].ToString()));
                    if (dict.ContainsKey(division))
                    {
                        WaitForElementVisible(By.XPath(dict[division]));
                        flag = true;
                    }
                }
            }
            catch (ElementNotVisibleException)
            {
                WaitForElementVisible(By.XPath(dict[division]));
            }
            if (flag)
            {             
                bool NotEnabled = true;
                while (NotEnabled)
                {
                    try
                    {
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                        elements = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".sl-CouponParticipantWithBookCloses_Name ")));
                        if (element != null)
                        {
                            NotEnabled = false;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        NotEnabled = false;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        NotEnabled = false;
                    }
                }
            }
            foreach (IWebElement element in elements)
            {
                if (element.Text == "Άρσεναλ v Λέστερ")
                {
                    element.Click();
                    break;
                }
            }
            betoption.closeOpenDivsBeforeBet();           
            betoption.overUnder("over");
            placeMaxBet();
        }
        public static void sleep(int time)
        {
            System.Threading.Thread.Sleep(time);

        }
        public static void WaitForElementVisible(By locator)
        {
            bool NotEnabled = true;
            while (NotEnabled)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                    if (element.Enabled)
                    {
                        element.Click();
                        NotEnabled = false;
                    }
                }
                catch (Exception)
                {
                    NotEnabled = true;
                }
            }
        }
        private void placeMaxBet()
        {
            bool FindFrame = true;
            while (FindFrame)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));
                    driver.SwitchTo();
                    WaitForElementVisible(By.XPath("html/body/div[1]/div/ul/li[3]/ul/li/div[3]/div[2]/span"));
                    WaitForElementVisible(By.XPath("html/body/div[1]/div/ul/li[8]/a[2]/div"));
                    driver.SwitchTo().DefaultContent();
                    FindFrame = false;
                }
                catch (WebDriverTimeoutException)
                {
                    FindFrame = true;
                }
            }
        }
    }
}
