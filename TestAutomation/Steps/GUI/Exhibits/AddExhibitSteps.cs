using System;
using System.Globalization;
using EVE.BLL;
using EVE.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.GUI.Exhibits
{
    [Binding, Scope(Feature = "AddExhibit")]
    public class AddExhibitSteps : DeiGuiTest
    {
        private static ExhibitsPage _exhibitsPage;
        private static HomePage _homePage = new HomePage(Driver);
        private static ExhibitDataModel _exhibitDataModel = new ExhibitDataModel().GetDefault();
        private static CaseDataModel _caseDataModel = new CaseDataModel().GetDefault();
        private static int _caseId;

        [Before]
        private static void AddCaseToDb()
        {
            _caseId = Case.InsertCase(_caseDataModel.PoliceFileNumber,
                  _caseDataModel.EclCaseNumber,
                  _caseDataModel.Description,
                  _caseDataModel.OfficerInChargeId,
                  _caseDataModel.DistrictId,
                  _caseDataModel.SiteId,
                  _caseDataModel.OffenceTypeId,
                  _caseDataModel.CreatedById);
        }

        [After]
        private static void DeleteTestDataFromDb()
        {
            var connectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Site);
            var exhibitTable = new ExhibitTable(connectionString);

            var exhibitId = exhibitTable.GetExhibitIdByExhibitNumber(_exhibitDataModel.EclExhibitNumber);

            var exhibitDbConnectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Exhibit).Replace(EVE.Site.DAL.Config.ExhibitPlaceholder, exhibitId.ToString(CultureInfo.InvariantCulture));
            var exhibitDatabase = new ExhibitDatabase(exhibitDbConnectionString);

            Exhibit.DeleteExhibit(_caseId, exhibitId);
            exhibitDatabase.DropExhibitDatabase(exhibitId);
            Case.DeleteCase(_caseId);
        }

        [When(@"I add a new Exhibit")]
        public void WhenIAddANewExhibit()
        {
            _exhibitsPage = _homePage.GoToCaseExhibits(_caseDataModel.PoliceFileNumber);
            _exhibitsPage.AddExhibit(_exhibitDataModel);
        }

        [Then(@"I can see it appears in Case Exhibits list")]
        public void ThenICanSeeItAppearsInCaseExhibitsList()
        {
            var expectedExhibitNumber = _exhibitDataModel.EclExhibitNumber;
            Assert.True(_exhibitsPage.DoesExhibitExistInListByExhibitNumber(expectedExhibitNumber), "An exhibit with Exhibit Number='" + expectedExhibitNumber + "' might not exist in Exhibits list on Case Exhibits page.");
        }
    }
}
