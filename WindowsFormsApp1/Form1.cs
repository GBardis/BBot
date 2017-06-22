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
            var dictionary = File.ReadLines(@"C:\Users\John\Documents\Visual Studio 2017\Projects\WindowsFormsApp1\Football.csv").Select(line => line.Split(','));
            foreach (string[] e in dictionary)
            {
                dict.Add(e[0].ToString() , e[1].ToString());                
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
            navigateToFootball();

            System.Threading.Thread.Sleep(1000);
            closeOpenDivs();
            navigateBeforeBet("Ην. Βασίλειο","Αγγλία - Πρέμιερ Λιγκ");
        }

        private void navigateBeforeBet(string country, string division)
        {
            bool flag = false;
            int i;
            IWebElement element;
            string printer="";
            List<string> games = new List<string>();
            try
            {
                if (dict.ContainsKey(country))
                {
                    element = driver.FindElement(By.XPath(dict[country].ToString()));
                    element.Click();
                    sleep300();
                    if (dict.ContainsKey(division))
                    {
                        element = driver.FindElement(By.XPath(dict[division]));
                        element.Click();
                        flag = true;
                    }
                }
            }catch(NoSuchElementException ex)
            {
             //   MessageBox.Show("1");
            }
            if (flag)
            {
                i = 2;
                try
                {
                    while (true)
                    {
                        sleep300();
                        sleep300();
                        element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div/div[1]/div[2]/div/div[1]/div[" + i + "]/div[2]/div"));
                        i++;
                        games.Add(element.Text);
                        printer += element.Text;
                        printer += "\n";
                    }
                }
                catch (NoSuchElementException ex)
                {
                  //  MessageBox.Show("2");
                }        

            }

 
            label6.Text = printer;
        }

        private void closeOpenDivs()
        {
            IWebElement element;
            element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
            element.Click();
            sleep300();
            element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[1]/div"));
            element.Click();
            System.Threading.Thread.Sleep(300);
            element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]"));
            element.Click();
            System.Threading.Thread.Sleep(300);
            element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[8]/div[1]"));
            element.Click();
        }

        private static void sleep300()
        {
            System.Threading.Thread.Sleep(300);
        }

        private void navigateToFootball()
        {
            IWebElement element = driver.FindElement(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[1]/div/div/div[15]"));
            element.Click();
        }

        private void oneXTwo(string result)
        {
            sleep300();
            IWebElement element;
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

        private void overUnder(string overunder)
        {
            sleep300();
            IWebElement element;
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
    }
}
