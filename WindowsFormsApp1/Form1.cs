using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        /*WebBrowser wb = new WebBrowser();
        public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;*/
        IWebDriver driver = new ChromeDriver();
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public static IWebElement element;
        public Form1()
        {
            InitializeComponent();
            driver.Navigate().GoToUrl("http://www.bet365.gr/#/HO");
            IWebElement element = driver.FindElement(By.Id("TopPromotionButton"));
            element.Click();
            System.Threading.Thread.Sleep(3000);
            label4.Text = driver.Title;
            var dictionary = File.ReadLines(@"C:\Users\George-PC\Documents\Visual Studio 2017\Projects\BBot\BBot\Football.csv").Select(line => line.Split(','));
            foreach (string[] e in dictionary)
            {
                dict.Add(e[0].ToString(), e[1].ToString());
            }
            //System.Threading.Thread.Sleep(3000);
            //browserView.Browser.LoadURL("http://www.google.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* wb.DocumentCompleted +=
       new WebBrowserDocumentCompletedEventHandler(Wb_DocumentCompleted);*/
            // IWebElement element2 = driver.FindElement(By.CssSelector(".hm-Login_InputField"));
            //element2.Click();
            IWebElement UserNameText = driver.FindElement(By.CssSelector(".hm-Login_InputField"));
            // UserNameText.SendKeys(textUserName.Text);             
            IWebElement PasswordText = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[1]"));
            PasswordText.Click();
            IWebElement PasswordText2 = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[2]"));
            // PasswordText2.SendKeys(textPassword.Text);
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
            List<string> z = new List<string>();
            label7.Text = "";
            navigateToFootball();
            closeOpenDivs();
            navigateBeforeBet("Ην. Βασίλειο", "Αγγλία - Πρέμιερ Λιγκ");
        }

        private void navigateBeforeBet(string country, string division)
        {
            bool flag = false;
            int i;
            string printer = "";
            List<string> games = new List<string>();
            List<IWebElement> elements = new List<IWebElement>();
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
            catch (ElementNotVisibleException ex)
            {
                WaitForElementVisible(By.XPath(dict[division]));
            }
            if (flag)
            {
                i = 2;
                bool NotEnabled = true;
                while (NotEnabled)
                {
                    try
                    {
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                        // element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[1]/div[2]/div/div[1]/div[" + i + "]/div[2]/div"));
                        IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[1]/div[2]/div/div[1]/div[" + i + "]/div[2]/div")));
                        elements.Add(element);
                        printer += element.Text;
                        printer += "\n";
                        i++;
                    }
                    catch (NoSuchElementException ex)
                    {
                        NotEnabled = false;
                    }
                    catch (WebDriverTimeoutException ex)
                    {
                        NotEnabled = false;
                    }
                }
            }

            label6.Text = printer;
            elements[0].Click();
            overUnder("over");
            placeMaxBet();
        }

        private void closeOpenDivs()
        {
            WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
            WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
            WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]"));
            WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[8]/div[1]"));
        }

        private void sleep()
        {
            System.Threading.Thread.Sleep(300);

        }
        public void WaitForElementVisible(By locator)
        {
            bool NotEnabled = true;
            while (NotEnabled)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(100));
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(locator));
                    if (element.Enabled)
                    {                       
                        element.Click();
                        NotEnabled = false;
                    }                   
                }
                catch (Exception ex)
                {
                    NotEnabled = true;
                }
            }
        }

        private void navigateToFootball()
        {
            WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[1]/div/div/div[15]"));
        }

        private void oneXTwo(string result)
        {
            try
            {
                switch (result)
                {
                    case "1":
                        WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[1]"));
                        break;
                    case "X":
                        WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[2]"));
                        break;
                    case "2":
                        WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[3]"));
                        break;
                    default:
                        break;
                }
            }
            catch (NoSuchElementException ex)
            {

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
                catch (WebDriverTimeoutException ex)
                {
                    FindFrame = true;
                }
            }

        }
        private void overUnder(string overunder)
        {
            try
            {
                switch (overunder)
                {
                    case "over":
                        WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[2]/div[2]"));
                        break;
                    case "under":
                        WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[3]/div[2]"));
                        break;
                    default:
                        break;
                }
            }
            catch (NoSuchElementException ex)
            {

            }
        }
    }
}
