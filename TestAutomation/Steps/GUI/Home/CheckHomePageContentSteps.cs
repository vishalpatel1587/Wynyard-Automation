using System;
using Microsoft.Web.Services3.Messaging.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Drivers.PageDriver.RecentActivity;
using TestAutomation.Drivers.PageDriver.System_Messages;

namespace TestAutomation.Steps.GUI.Home
{
    [Binding, Scope(Feature = "CheckHomePageContent")]
    public class CheckHomePageContentSteps : DeiGuiTest
    {
        private const string RecentActivityText = "Recent Activity";
        private const string ActionText = "Action";
        private const string DescriptionText = "Description";
        private const string SystemMessagesText = "System Messages";

        private static HomePage _homePage = new HomePage(Driver);
        private static CasesPage _casepage= new CasesPage(Driver);
        private static RecentActivityPage _recentActivityPage;
        private static SystemMessagesPage _systemMessagesPage;

        [When(@"I look at the Home page")]
        public void WhenILookAtTheHomePage()
        {
        }


        [When(@"I go to the '(.*)' Page")]
        public void WhenIGoToThePage(string PageLink)
        {
            switch (PageLink)
            {
                case RecentActivityText:
                    _recentActivityPage=_casepage.GoToRecentActivity();
                    break;
                case SystemMessagesText:
                   _systemMessagesPage=_casepage.GoToSystemMessages();
                    break;
                default:
                    throw new Exception("Unexpected PageLink '" + PageLink + "' was supplied in the feature file.");
            }
        }

        [Then(@"the '(.*)' page should contain '(.*)' and '(.*)'")]
        public void ThenThePageShouldContainAnd(string pageName, string expectedColumn1Text, string expectedColumn2Text)
        {
            switch (pageName)
            {
                case RecentActivityText:
                    _recentActivityPage.ValidateRecentActivityPage(expectedColumn1Text, expectedColumn2Text);
                    break;

                case SystemMessagesText:
                    _systemMessagesPage.ValidateSystemMessagesPage(expectedColumn1Text, expectedColumn2Text);
                    break;

            }
            
        }

 


        [Then(@"I should be able to see '(.*)' text")]
        public void ThenIShouldBeAbleToSeeText(string expectedText)
        {
            CheckCasesTextIsCorrect(expectedText);

            /* switch (expectedText)
            {
                case RecentActivityText:
                    CheckRecentActivityTextIsCorrect(expectedText);
                    break;
                case ActionText:
                    CheckActionTextIsCorrect(expectedText);
                    break;
                case DescriptionText:
                    CheckDescriptionTextIsCorrect(expectedText);
                    break;
                case SystemMessagesText:
                    CheckSystemMessagesTextIsCorrect(expectedText);
                    break;
                default:
                    throw new Exception("Unexpected expectedText '" + expectedText + "' was supplied in the feature file.");
            }*/
        }


        [Then(@"I should be able to see '(.*)' Links")]
        public void ThenIShouldBeAbleToSeeLinks(string expectedLinks)
        {
            switch (expectedLinks)
            {
                
                case RecentActivityText:
                    CheckRecentActivityLinkIsPresent(expectedLinks);
                    break;
                case SystemMessagesText:
                    CheckSystemMessagesLinkIsPresent(expectedLinks);
                    break;
                default:
                    throw new Exception("Unexpected expectedText '" + expectedLinks + "' was supplied in the feature file.");
            }
        }

        private static void CheckRecentActivityLinkIsPresent(string expectedLinks)
        {
            //var actualRecentActivityText = _casepage.GetExpectedLink(expectedText);
            //AssertText(expectedText, actualRecentActivityText);
            Assert.True(_casepage.GetExpectedLink(expectedLinks));
        }
        private static void CheckCasesTextIsCorrect(string expectedText)
        {
            var actualRecentActivityText = _casepage.GetCasePageHeader();
            AssertText(expectedText, actualRecentActivityText);

        }

        private static void CheckSystemMessagesLinkIsPresent(string expectedLinks)
        {
            //var actualActionText = _casepage.GetExpectedLink(expectedText);
            //AssertText(expectedText, actualActionText);
            Assert.True(_casepage.GetExpectedLink(expectedLinks));
        }

        private static void CheckDescriptionTextIsCorrect(string expectedText)
        {
            var actualDescriptionText = _homePage.GetDescriptionText();
            AssertText(expectedText, actualDescriptionText);
        }

        private static void CheckSystemMessagesTextIsCorrect(string expectedText)
        {
            var actualSystemMessagesText = _homePage.GetSystemMessagesText();
            AssertText(expectedText, actualSystemMessagesText);
        }

        private static void AssertText(string expectedText, string actualText)
        {
            Assert.True(actualText.Equals(expectedText),
                "Actual text present on the Home page '" + actualText +
                "' is not equal to the expected one '" + expectedText + "'.");
        }


    }
}
