using System.Globalization;
using System.IO;
using EVE.BLL;
using EVE.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Model.DataModel;
using Exhibit = EVE.BLL.Exhibit;

namespace TestAutomation.Steps.GUI.Workflows
{
    [Binding, Scope(Feature = "SaveCustomWorkflow")]
    //TODO OV this test relies on a certain workflow configuration (no Uac, Video & Image Classification).
    public class SaveCustomWorkflowSteps : DeiGuiTest
    {
        private static string _evidencePath = Path.GetFullPath(@"TestFiles/DEI_Automation.vhd");
        private static HomePage _homePage = new HomePage(Driver);
        private static ExhibitsPage _exhibitsPage;
        private static ExhibitDataModel _exhibitDataModel = new ExhibitDataModel().GetDefault();
        private static CaseDataModel _caseDataModel = new CaseDataModel().GetDefault();
        private static MediaDataModel _mediaDataModel = new MediaDataModel().GetDefault();
        private static WorkflowDataModel _workflowDataModel = new WorkflowDataModel().GetDefault();
        private string _workflowToSelect = WorkflowDataModel.DefaultWorkflowOptionValues.Custom.ToString();
        private static UserWorkflowTable _userWorkflowTable;
        private static ExhibitDatabase _exhibitDatabase;

        private static int _caseId;
        private static int _exhibitId;

        [Before]
        private static void InsertTestDataToDb()
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

            _mediaDataModel.EvidencePath = _evidencePath;


            EVE.Site.BLL.Media.InsertMedia(_exhibitId,
                _mediaDataModel.MediaNumber,
                _mediaDataModel.Description,
                _mediaDataModel.EvidencePath,
                MediaStatus.Defined,
                AcquisitionFormatType.ForensicImage, 
                string.Empty);

            var connectionString = Config.GetConnectionString(DataStore.Central);
            _userWorkflowTable = new UserWorkflowTable(connectionString);

            var exhibitDbConnectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Exhibit).Replace(EVE.Site.DAL.Config.ExhibitPlaceholder, _exhibitId.ToString(CultureInfo.InvariantCulture));
            _exhibitDatabase = new ExhibitDatabase(exhibitDbConnectionString);
        }

        [After]
        private static void DeleteTestDataFromDb()
        {
            Exhibit.DeleteExhibit(_caseId, _exhibitId);
            _exhibitDatabase.DropExhibitDatabase(_exhibitId);
            Case.DeleteCase(_caseId);

            //TODO OV is there a BLL method that can be used to replace this one?
            if (_userWorkflowTable.IsWorkflowFoundByCustomName(_workflowDataModel.CustomWorkflowName))
            {
                var workflowId = _userWorkflowTable.GetWorkflowIdByName(_workflowDataModel.CustomWorkflowName);
                Assert.AreEqual(1, _userWorkflowTable.DeleteUserWorkflow(workflowId));
            }
        }

        [Given(@"I have saved my Custom Workflow with no Images and Deleted Files included")]
        public void GivenIHaveSavedMyCustomWorkflow()
        {
            SetWorkflowData();

            _exhibitsPage = _homePage.GoToCaseExhibits(_caseDataModel.PoliceFileNumber);

            //TODO OV this options list is configurable, maybe as this test setup we need to make sure we configure the workflow to display the expected list of options.
            _exhibitsPage.SelectProcessExhibitOption(_exhibitDataModel.EclExhibitNumber);
            _exhibitsPage = _exhibitsPage.AddCustomWorkflow(_workflowDataModel);
        }

        [When(@"I select that Workflow to process my exhibit")]
        public void WhenISelectThatWorkflowToProcessMyExhibit()
        {
            _exhibitsPage.SelectProcessExhibitOption(_exhibitDataModel.EclExhibitNumber);
            Assert.True(_exhibitsPage.IsWorkflowPresentInList(_workflowDataModel.CustomWorkflowName), "A workflow with name '" + _workflowDataModel.CustomWorkflowName + "' is not present in workflow options list as expected.");
            _exhibitsPage.SelectWorkflow(_workflowDataModel.CustomWorkflowName);
        }

        [Then(@"I can see that Images and Deleted Files are not selected")]
        public void ThenICanSeeThatImagesAndDeletedFilesAreNotSelected()
        {
            Assert.False(_exhibitsPage.IsWorkflowMediaProcessingOptionSelected(WorkflowDataModel.WorkflowMediaProcessingOptions.DeletedFiles),
                "Delete Files option might be selected that is unexpected.");
            Assert.False(_exhibitsPage.IsWorkflowMediaProcessingOptionSelected(WorkflowDataModel.WorkflowMediaProcessingOptions.Images),
                "Images option might be selected that is unexpected.");
            Assert.False(_exhibitsPage.IsWorkflowMediaProcessingOptionSelected(WorkflowDataModel.WorkflowMediaProcessingOptions.ImageRating),
                "Image Rating option might be selected that is unexpected.");
        }

        private void SetWorkflowData()
        {
            _workflowDataModel.WorkflowOption = _workflowToSelect;
            _workflowDataModel.DeletedFiles = false;
            _workflowDataModel.Images = false;
            _workflowDataModel = _workflowDataModel.OverrideDefaultValues(_workflowDataModel);
        }
    }
}
