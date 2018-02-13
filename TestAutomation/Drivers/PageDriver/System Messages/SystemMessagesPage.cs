using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestAutomation.Drivers.PageDriver.System_Messages
{
    public class SystemMessagesPage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.ClassName, Using = "title")]
        //[FindsBy(How = How.CssSelector, Using = "#recentItems .title h2")]
        private IWebElement _title;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvSystemMessages']/tbody/tr[1]/th[2]")]
        private IWebElement _dateColumnName;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvSystemMessages']/tbody/tr[1]/th[3]")]
        private IWebElement _subjectColumnName;
        public SystemMessagesPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            try
            {
                WaitForElementDisplayed(_driver, _title);
            }
            catch (Exception exception)
            {

                throw new Exception("Unable to open the System Messages Page. " + exception);
            }
        }
        public void ValidateSystemMessagesPage(string expectedDateText, string expectedSubjectText)
        {
            Assert.True(expectedDateText.Equals(_dateColumnName.Text),
                "The expected string " + expectedDateText + " doesn't match the column name which is " + _dateColumnName.Text);
            Assert.True(expectedSubjectText.Equals(_subjectColumnName.Text),
                "The expected string " + expectedSubjectText + " doesn't match the column name which is " + _subjectColumnName.Text);
        }

    }


}

