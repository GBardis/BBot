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

            System.Threading.Thread.Sleep(1000);
            closeOpenDivs();
            navigateBeforeBet("Ην. Βασίλειο", "Αγγλία - Πρέμιερ Λιγκ");
        }

        private void navigateBeforeBet(string country, string division)
        {
            bool flag = false;
            int i;
            IWebElement element;
            string printer = "";
            List<string> games = new List<string>();
            List<IWebElement> elements = new List<IWebElement>();
            sleep();
            try
            {
                if (dict.ContainsKey(country))
                {
                    element = driver.FindElement(By.XPath(dict[country].ToString()));
                    element.Click();
                    sleep();
                    if (dict.ContainsKey(division))
                    {
                        element = driver.FindElement(By.XPath(dict[division]));
                        element.Click();
                        flag = true;
                    }
                }
            }
            catch (ElementNotVisibleException ex)
            {
                sleep();
                element = driver.FindElement(By.XPath(dict[division]));
                element.Click();
                //   MessageBox.Show("1");
            }
            if (true)
            {
                i = 2;
                try
                {
                    sleep();
                    while (true)
                    {
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[1]/div[2]/div/div[1]/div[" + i + "]/div[2]/div"));
                        i++;
                        elements.Add(element);
                        printer += element.Text;
                        printer += "\n";
                    }
                }
                catch (NoSuchElementException ex)
                {

                }

            }

            label6.Text = printer;
            elements[0].Click();
            overUnder("over");
            placeMaxBet();
        }

        private void closeOpenDivs()
        {
            IWebElement element;
            try
            {
                WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
                WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
                WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]"));
                WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[8]/div[1]"));
            }
            catch (NoSuchElementException ex)
            { }
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
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(50));
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(locator));
                    if (element.Enabled)
                    {
                        NotEnabled = false;
                        element.Click();
                        break;
                    }
                }
                catch(TimeoutException ex)
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
            sleep();
            IWebElement element;
            try
            {
                switch (result)
                {
                    case "1":
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[1]"));
                        element.Click();
                        break;
                    case "X":
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[2]"));
                        element.Click();
                        break;
                    case "2":
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[3]/div[2]/div/div/div[3]"));
                        element.Click();
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
            List<IWebElement> LOT = new List<IWebElement>();
            sleep();

            LOT = driver.FindElements(By.XPath(".//*")).ToList<IWebElement>();
            sleep();
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));
            sleep();
            IWebElement iframeElement = driver.FindElement(By.XPath("html/body/div[1]/div/ul/li[3]/ul/li/div[3]/div[2]/span"));
            iframeElement.Click();
            iframeElement = driver.FindElement(By.XPath("html/body/div[1]/div/ul/li[8]/a[2]/div"));
            iframeElement.Click();
            sleep();
            driver.SwitchTo().DefaultContent();
        }
        private void overUnder(string overunder)
        {
            sleep();
            IWebElement element;
            try
            {
                switch (overunder)
                {
                    case "over":
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[2]/div[2]"));
                        element.Click();
                        break;
                    case "under":
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[8]/div[2]/div/div[3]/div[2]"));
                        element.Click();
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
