using System;
using System.Linq;
using Microsoft.SqlServer.Server;
using Microsoft.Web.Services3.Referral;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.RecentActivity;
using TestAutomation.Drivers.PageDriver.Search;
using TestAutomation.Drivers.PageDriver.System_Messages;
using TestAutomation.Exceptions;
using TestAutomation.Model.DataModel;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Cases
{
    public class CasesPage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "MainContent_updPanel")]
        private IWebElement _mainContentPanel;

        [FindsBy(How = How.LinkText, Using = "Add Case")]
        private IWebElement _addCaseLink;

        [FindsBy(How = How.CssSelector, Using = "#liSearch a")]
        private IWebElement _searchLink;

        [FindsBy(How = How.Id, Using = "MainContent_gvCases")]
        private IWebElement _casesTable;

        [FindsBy(How = How.Id, Using = "MainContent_pager_ddlPerPage")]
        private IWebElement _casesDropdown;

        [FindsBy(How = How.Id, Using = "MainContent_pager_imgScrollLast")]
        private IWebElement _scrollLast;

        [FindsBy(How = How.Id, Using = "MainContent_pager_imgScrollFirst")]
        private IWebElement _scrollFirst;

        [FindsBy(How = How.Id, Using = "lblPageTitle")]
        private IWebElement _casePageTitle;

        [FindsBy(How = How.Id, Using = "lnkLogout")]
        private IWebElement _logoutLink;

        [FindsBy(How = How.LinkText, Using = "Recent Activity")]
        private IWebElement _recentActivity;

        [FindsBy(How = How.LinkText, Using = "System Messages")]
        private IWebElement _systemMessages;

        private const string CasesDropDownItems = "100";
            
        private const int PoliceFileNumberHtmlColumnIndex = 3;

        public CasesPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
           // IWebElement addlink = _driver.FindElement(By.Id("MainContent_divAddCase"));
            try
            {
                WaitForElementDisplayed(_driver, _casePageTitle);
                
            }
             catch (Exception e)
            {
                throw new ElementNotDisplayedException(
                    "Cases page is expected to be shown with the cases list panel (id='" +
                    _mainContentPanel.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }
            
           
        }

        public RecentActivityPage GoToRecentActivity()
        {
            WaitForElementDisplayed(_driver,_recentActivity);
            _recentActivity.Click();
            return new RecentActivityPage(_driver);
        }

        public SystemMessagesPage GoToSystemMessages()
        {
            WaitForElementDisplayed(_driver, _systemMessages);
            _systemMessages.Click();
            return new SystemMessagesPage(_driver);
        }
        public string GetCasePageHeader()
        {
            return _casePageTitle.Text;
        }

        public bool GetExpectedLink(string expectedlink)
        {
            return (_driver.FindElement(By.LinkText("" + expectedlink + "")).Displayed);
        }
        public string GetLogoutLinkText()
        {
            return _logoutLink.Text;
        }

        public CasesPage AddCase(CaseDataModel caseDataModel)
        {
            var addCasePage = ClickAddCaseLink();
            return addCasePage.AddCase(caseDataModel);
        }

        public AddCasePage ClickAddCaseLink()
        {
            _addCaseLink.Click();
            return new AddCasePage(_driver);
        }

        // Navigates the user from the home page to the search page 
        public SearchPage GoToSearchPage()
        {
            _searchLink.Click();
            return new SearchPage(_driver);
        }

        public bool IsCasePresentInListByPoliceFileNumber(string expectedPoliceFileNumber)
        {
            try
            {
                //finds if the Scroll Last button is prensent or not. If present, it clicks on it and goes to the last page of case.
                int t = _driver.FindElements(By.Id("MainContent_pager_imgScrollLast")).Count();
                if (t > 0)
                {
                    _scrollLast.Click();
                    WaitForElementDisplayed(_driver, _scrollFirst);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Could not find scroll last element. " + e.ToString());

            }      
            return DoesColumnWithExpectedIndexAndTextExistInTable(_casesTable, PoliceFileNumberHtmlColumnIndex, expectedPoliceFileNumber);
        }
        
        public ExhibitsPage SelectCaseByPoliceFileNumber(string policeFileNumber)
        {
      
            try
            {
               /* try
                {
                    //finds if the Scroll Last button is prensent or not. If present, it clicks on it and goes to the last page of case.
                    int t = _driver.FindElements(By.Id("MainContent_pager_imgScrollLast")).Count();
                    int dropdownpage = _driver.FindElements(By.Id("MainContent_pager_ddlPerPage")).Count;

                    if (dropdownpage > 0)
                    {
                        SelectFromDropdown(_casesDropdown, CasesDropDownItems);
                    }
                    
                    if (t > 0)
                    {
                        _scrollLast.Click();
                        WaitForElementDisplayed(_driver,_scrollFirst);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Couldnot find scroll last element. " + e.ToString());
                    
                }
                */
                
                DoubleClickOnTableColumnWithExpectedIndexAndText(_driver, _casesTable, PoliceFileNumberHtmlColumnIndex, policeFileNumber);
                return new ExhibitsPage(_driver);
            }
            catch (Exception e)
            {
                throw new Exception("Could not select a case with PoliceFileNumber='" + policeFileNumber + "' from cases list. Exception is: " + e.ToString());

            }
        }
    }
}
