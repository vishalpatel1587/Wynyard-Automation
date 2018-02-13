using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Drivers.PageDriver.Login;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.GUI.Login
{
    [Binding, Scope(Feature = "Login")]
    public class LoginSteps : DeiGuiTest
    {
        private static LoginPage _loginPage = new LoginPage(Driver);
        private static HomePage _homePage;
        private static CasesPage _casePage;


        private LoginDataModel _loginDataModel;


        [When(@"I try to log in with valid details")]
        public void WhenITryToLogInWithValidDetails()
        {
            _loginDataModel = new LoginDataModel().BuildModel(LoginDataModel.DataInstance.ValidDetails);
            _casePage = _loginPage.LogInWithValidDetails(_loginDataModel);
        }

        [When(@"I try to log in with invalid details")]
        public void WhenITryToLogInWithInvalidDetails()
        {
            _loginDataModel = new LoginDataModel().BuildModel(LoginDataModel.DataInstance.InvalidDetails);
            _loginPage.LogInWithInvalidDetails(_loginDataModel);
        }

        [Then(@"the page displayed contains '(.*)' heading")]
        public void ThenThePageDisplayedContainsHeading(string expectedRecentActivityText)
        {
            //var actualRecentActivityText = _homePage.GetRecentActivityText();
            var casePageHeaderText=_casePage.GetCasePageHeader();
            Assert.True(casePageHeaderText.Equals(expectedRecentActivityText), "Actual recent activity text present on a screen '" + casePageHeaderText + "' is not equal to the expected one '" + expectedRecentActivityText + "'.");
        }

        [Then(@"the '(.*)' link is displayed")]
        public void ThenTheLinkIsDisplayed(string expectedLogoutLinkText)
        {
            var actualLogoutLinkText = _casePage.GetLogoutLinkText();
            Assert.True(actualLogoutLinkText.Equals(expectedLogoutLinkText), "Actual Logout link text present on a screen '" + actualLogoutLinkText + "' is not equal to the expected one '" + expectedLogoutLinkText + "'.");

        }

        [Then(@"the error message '(.*)' is shown")]
        public void ThenTheErrorMessageIsShown(string expectedErrorMessage)
        {
            var actualErrorMessage = _loginPage.GetInvalidUsernameOrPasswordErrorMessage();
            Assert.True(actualErrorMessage.Equals(expectedErrorMessage), "Actual error message present on a screen '" + actualErrorMessage + "' is not equal to the expected one '" + expectedErrorMessage + "'.");
        }

        [When(@"I try to log in with invalid password")]
        public void WhenITryToLogInWithInvalidPassword()
        {
            _loginDataModel = new LoginDataModel().BuildModel(LoginDataModel.DataInstance.InvalidPassword);
            _loginPage.LogInWithInvalidDetails(_loginDataModel);
        }

        [When(@"I try to log in with invalid username")]
        public void WhenITryToLogInWithInvalidUsername()
        {
            _loginDataModel = new LoginDataModel().BuildModel(LoginDataModel.DataInstance.InvalidUsername);
            _loginPage.LogInWithInvalidDetails(_loginDataModel);
        }


    }
}
