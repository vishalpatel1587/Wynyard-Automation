using System;
using EVE.BLL;
using Microsoft.Web.Services3.Messaging;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.GUI.Media
{
    [Binding, Scope(Feature = "CheckMedia")]
    public class CheckMediaSteps : DeiGuiTest
    {
        private static CaseDataModel _caseDataModel = new CaseDataModel().GetDefault();
        private static ExhibitDataModel _exhibitDataModel = new ExhibitDataModel().GetDefault();
        private static MediaDataModel _mediaDataModel = new MediaDataModel().GetDefaultUfed();
        private static ExhibitsPage _exhibitsPage;
        private static HomePage _homePage = new HomePage(Driver);

        private static int _caseId;
        private static int _exhibitId;
        private bool isEnabled;


        [BeforeFeature]
        private static void InsertIntoDb()
        {
            _caseId = Case.InsertCase(_caseDataModel.PoliceFileNumber,
                _caseDataModel.EclCaseNumber,
                _caseDataModel.Description,
                _caseDataModel.OfficerInChargeId,
                _caseDataModel.DistrictId,
                _caseDataModel.SiteId,
                _caseDataModel.OffenceTypeId,
                _caseDataModel.CreatedById);

            _exhibitId = Exhibit.CreateExhibit(_caseId,
                _caseDataModel.CreatedById,
                _exhibitDataModel.EclExhibitNumber,
                _exhibitDataModel.Description,
                _exhibitDataModel.PoliceExhibitNumber);
        }

        [Given(@"I am on the exhibit page")]
        public void GivenIAmOnTheExhibitPage()
        {
            _exhibitsPage = _homePage.GoToCaseExhibits(_caseDataModel.PoliceFileNumber);
        }

        [When(@"I click on Add media")]
        public void WhenIClickOnAddMedia()
        {
            _exhibitsPage.AddMedia(_mediaDataModel, out isEnabled);
        }

        [Then(@"TimeZone should appear and user should be able to save it\.")]
        public void ThenTimeZoneShouldAppearAndUserShouldBeAbleToSaveIt_()
        {
            Assert.True(isEnabled, "It may be that the drop down list is not present for the user selection");
        }

        [When(@"I click on Add media and click save")]
        public void WhenIClickOnAddMediaAndClickSave()
        {
            _exhibitsPage.AddMedia(_mediaDataModel, out isEnabled);
            _exhibitsPage.AddMediaDetailsAndSave(_mediaDataModel);
        }

        [Then(@"Media should be added.")]
        public void ThenMediaShouldBeAdded_()
        {
            var expectedMediaNumber = _mediaDataModel.MediaNumber;
            _exhibitsPage.IsMediaPresent(expectedMediaNumber);
           
        }
    }
}
