using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


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
            catch (NoSuchElementException ex)
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
            catch (NoSuchElementException ex)
            {

            }
        }
        public void closeOpenDivs()
        {
            Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
            Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/div"));
            Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]"));
            Form1.WaitForElementVisible(By.XPath("html/body/div[1]/div/div[2]/div[1]/div/div[2]/div[2]/div[1]/div[3]/div[2]/div[8]/div[1]"));
        }
    }
}
