using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Exceptions;
using TestAutomation.Model.DataModel;

#pragma warning disable 649

namespace TestAutomation.Drivers.PageDriver.Login
{
    public class LoginPage : PageBase
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "txtUserName")]
        private IWebElement _username;

        [FindsBy(How = How.Id, Using = "txtPassword")]
        private IWebElement _password;

        [FindsBy(How = How.Id, Using = "btnLogin")]
        private IWebElement _loginButton;

        [FindsBy(How = How.CssSelector, Using = "#vsSummary ul>li")]
        private IWebElement _invalidUsernameOrPasswordErrorMessage;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            try
            {
                WaitForElementDisplayed(_driver, _loginButton);
            }
            catch (Exception e)
            {
                throw new ElementNotDisplayedException("Login page is expected to be shown with Login button (id='" + _loginButton.GetAttribute("id") + "') displayed. Exception is:" + e.ToString());
            }
        }

        public CasesPage LogInWithValidDetails(LoginDataModel loginDataModel)
        {
            PopulateLoginDetailsAndSubmit(loginDataModel);
            return new CasesPage(_driver);
        }

        public void LogInWithInvalidDetails(LoginDataModel loginDataModel)
        {
            PopulateLoginDetailsAndSubmit(loginDataModel);
        }

        public string GetInvalidUsernameOrPasswordErrorMessage()
        {
            WaitForElementDisplayed(_driver,_invalidUsernameOrPasswordErrorMessage);
            return _invalidUsernameOrPasswordErrorMessage.Text;
        }

        private void PopulateLoginDetailsAndSubmit(LoginDataModel loginDataModel)
        {
            WaitForElementDisplayed(_driver,_username);
            TypeInto(_username, loginDataModel.Username);
            TypeInto(_password, loginDataModel.Password);
            _loginButton.Click();
        }
    }
}
