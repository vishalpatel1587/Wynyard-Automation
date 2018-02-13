using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Drivers.PageDriver;

namespace TestAutomation.Drivers.PageDriver.RecentActivity
{
    public class RecentActivityPage:PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.ClassName, Using = "title")]
        //[FindsBy(How = How.CssSelector, Using = "#recentItems .title h2")]
        private IWebElement _title;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvRecentActivity']/tbody/tr[1]/th[1]")]
        private IWebElement _actionColumnName;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvRecentActivity']/tbody/tr[1]/th[2]")]
        private IWebElement _descriptionColumnName;
        public RecentActivityPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            try
            {
                WaitForElementDisplayed(_driver, _title);
                
                
            }
            catch (Exception exception)
            {
                
                throw new Exception("Unable to open Recent Activity Page. "+ exception);
            }
            
        }

        public void  ValidateRecentActivityPage(string expectedActionText,string expectedDescriptionText)
        {
            Console.WriteLine(_actionColumnName.Text);
            Assert.True(expectedActionText.Equals(_actionColumnName.Text),
                "The expected string "+expectedActionText+" doesn't match the column name which is "+_actionColumnName.Text);
            Assert.True(expectedDescriptionText.Equals(_descriptionColumnName.Text),
                "The expected string " + expectedDescriptionText + " doesn't match the column name which is " + _descriptionColumnName.Text);
            
        }

    }
}
