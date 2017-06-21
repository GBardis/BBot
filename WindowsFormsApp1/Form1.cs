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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        /*WebBrowser wb = new WebBrowser();
        public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;*/
        IWebDriver driver = new ChromeDriver(); 
        public Form1()
        {
            InitializeComponent();
            driver.Navigate().GoToUrl("http://www.bet365.gr/#/HO");
            IWebElement element = driver.FindElement(By.Id("TopPromotionButton"));
            element.Click();
            System.Threading.Thread.Sleep(3000);
            label4.Text = driver.Title;



            //browserView.Browser.LoadURL("http://www.google.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* wb.DocumentCompleted +=
       new WebBrowserDocumentCompletedEventHandler(Wb_DocumentCompleted);*/
            
            
           // IWebElement element2 = driver.FindElement(By.CssSelector(".hm-Login_InputField"));
            //element2.Click();
            
            IWebElement UserNameText = driver.FindElement(By.CssSelector(".hm-Login_InputField"));
            UserNameText.SendKeys(textUserName.Text);
            IWebElement PasswordText = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[1]"));
            PasswordText.Click();
            IWebElement PasswordText2 = driver.FindElement(By.XPath("html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div[2]/input[2]"));
            PasswordText2.SendKeys(textPassword.Text);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
