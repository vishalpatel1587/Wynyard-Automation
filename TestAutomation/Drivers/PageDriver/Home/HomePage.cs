using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Search;
using TestAutomation.Exceptions;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Home
{
    public class HomePage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "#recentItems .title h2")]
        private IWebElement _recentActivityHeading;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvRecentActivity']//tr[1]/th[1]")]
        private IWebElement _actionColumnHeading;

        [FindsBy(How = How.XPath, Using = "//*[@id='MainContent_gvRecentActivity']//tr[1]/th[2]")]
        private IWebElement _descriptionColumnHeading;

        [FindsBy(How = How.CssSelector, Using = "#systemMessages h2")]
        private IWebElement _systemMessagesHeading;

        [FindsBy(How = How.Id, Using = "UpdatePanel1")]
        private IWebElement _updatePanel;

        [FindsBy(How = How.LinkText, Using = "Case")]
        private IWebElement _caseLink;
        

        [FindsBy(How = How.CssSelector, Using = "#liSearch a")]
        private IWebElement _searchLink;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            try
            {
                WaitForElementDisplayed(_driver, _updatePanel);
            }
            catch (Exception e)
            {
                throw new ElementNotDisplayedException("Home page is expected to be shown with the element (id='" + _updatePanel.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }
        }

        public string GetRecentActivityText()
        {
            return _recentActivityHeading.Text;
        }

    
        public string GetActionText()
        {
            return _actionColumnHeading.Text;
        }

        public string GetDescriptionText()
        {
            return _descriptionColumnHeading.Text;
        }

        public string GetSystemMessagesText()
        {
            return _systemMessagesHeading.Text;
        }

        public CasesPage GoToCases()
        {
            _caseLink.Click();
            return new CasesPage(_driver);
        }

        public ExhibitsPage GoToCaseExhibits(string casePoliceFileNumber)
        {
            var casesPage = GoToCases();
            casesPage.SelectCaseByPoliceFileNumber(casePoliceFileNumber);
            return new ExhibitsPage(_driver);
        }

        // Navigates the user from the home page to the search page 
        public SearchPage GoToSearchPage()
        {
            _searchLink.Click();
            return new SearchPage(_driver);
        }
    }
}
