using System;
using EVE.BLL;
using EVE.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.GUI.Cases
{
    [Binding, Scope(Feature = "AddCase"), Scope(Feature = "Test")]
    public class AddCaseSteps : DeiGuiTest
    {
        private static HomePage _homePage = new HomePage(Driver);
        private static CasesPage _casesPage;

        private static CaseDataModel _caseDataModel = new CaseDataModel().GetDefault();
        private static CaseTable _caseTable;

        [Before]
        private static void Setup()
        {
            var connectionString = Config.GetConnectionString(DataStore.Central);
            _caseTable = new CaseTable(connectionString);
        }

        [After]
        private static void DeleteAddedCaseFromDb()
        {
            Case.DeleteCase(_caseTable.GetCaseId(_caseDataModel.EclCaseNumber));
        }

        [Given(@"I am on Cases page")]
        public void GivenIAmOnCasesPage()
        {
            _casesPage = _homePage.GoToCases();
        }

        [When(@"I add a new case")]
        public void WhenIAddANewCase()
        {
            _casesPage.AddCase(_caseDataModel);
        }

        [Then(@"I can see it appears in Cases list")]
        public void ThenICanSeeItAppearsInCasesList()
        {
            try
            {
                Assert.True(_casesPage.IsCasePresentInListByPoliceFileNumber(_caseDataModel.PoliceFileNumber));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by Police File Number = " + _caseDataModel.PoliceFileNumber + ", the case might not be found in Cases list after it has just been added. Exception is: " + e.ToString());
            }
        }

    }
}
