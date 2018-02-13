using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using EVE.BLL;
using EVE.Common;
using EVE.Data;
using EVE.Functions.Extensions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Drivers.PageDriver.Cases;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Drivers.PageDriver.Search;
using TestAutomation.Drivers.PageDriver.Viewer;
using TestAutomation.Helpers;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.EndToEndTest.XRY
{
    [Binding, Scope(Feature = "Xry Logical Processing and Search")]
    public class XryProcessingStatisticsAndExhibitPropertiesCheckSteps : DeiGuiTest
    {

        private static ExhibitsPage _exhibitsPage;
        private static HomePage _homePage = new HomePage(Driver);
        private static CasesPage _casesPage = new CasesPage(Driver);
        private static SearchPage _searchPage = new SearchPage(Driver);
        private static ExhibitDataModel _exhibitDataModel = new ExhibitDataModel().GetDefault();
        private static CaseDataModel _caseDataModel = new CaseDataModel().GetDefault();
        private static SearchDataModel _searchDataModel = new SearchDataModel().GetDefault();
        private static ViewerPage _viewerPage;
        private static int _caseId;
        private static int _exhibitId;
        private static int _mediaId;
        private static KeyValueConfigurationCollection _settings;
        private static readonly string ConfigFile = Path.GetFullPath(@"..\..\Steps\EndToEndTest\TestData\XRYProcessingAndSearch\Test.config");
        private static MediaTable _mediaTable;
        private static WorkTable _workTable;
        private static DistributedDatabase _distributedDatabase;
        private static string _exhibitDatabaseConnectionString;
        private static MediaDataModel _mediaDataModel = new MediaDataModel().GetDefault();
        private static string _casePoliceNumber;// = "Automation Exhibits";
        private static string _eclExhibitNumber;// = "XRY exhibit";
        private static string _mediaNumber;
        private static MediaPage _mediaPage;

        private static int verifyFileMetadataIndex = 2;

        private const string ExpectedSource = TestConstants.ExhibitMetadataSource;
        [Given(@"an exhibit of a version (.*)")]
        public void GivenAnExhibitOfAVersion(string version)
        {
            switch (version)
            {
                case "3.9":
                    _mediaDataModel.EvidencePath = Path.GetFullPath(_settings["3.9_EvidencePath"].Value);
                    break;
                case "4.1":
                    _mediaDataModel.EvidencePath = Path.GetFullPath(_settings["4.1_EvidencePath"].Value);
                    break;
                case "Logical":
                    _mediaDataModel.EvidencePath = Path.GetFullPath(_settings["Logical"].Value);
                    Console.WriteLine("LOGICAL Path :" + Path.GetFullPath(_settings["Logical"].Value));
                    break;
                default:
                    throw new Exception("The version supplied from the feature " + version + " is not expected, and no evidence file for it exist.");
            }

            PrepareDbData();

            _exhibitDatabaseConnectionString =
               EVE.Site.DAL.Config.GetConnectionString(DataStore.Exhibit)
                   .Replace(EVE.Site.DAL.Config.ExhibitPlaceholder, _exhibitId.ToString(CultureInfo.InvariantCulture));
            _mediaTable = new MediaTable(_exhibitDatabaseConnectionString);
        }

        [When(@"I process that Exhibit")]
        public void WhenIProcessThatExhibit()
        {
            ProcessExhibit();

            Console.WriteLine(@"Exhibit is processed. Start checking for Work FinaliseMedia Handler Completed processing status...");
            WaitAndAssertWorkStatus(WorkStatus.Completed, WorkTable.WorkHandlerType.FinaliseMedia, Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForWorkFinaliseMediaStatusToBeCompleted"].Value));
            Console.WriteLine("Case Id: " + _caseId);
            Console.WriteLine("Case Name: " + _casePoliceNumber);

        }


        [Then(@"the Video is visible to user\.")]
        public void ThenTheVideoIsVisibleToUser_()
        {
            try
            {
                _viewerPage.SwitchToChild();
                Assert.True(_viewerPage.ValidateVideoPlayable());
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();

            }
            catch (Exception exception)
            {

                throw new Exception("Unable to Play the video in the viewer. Exception" + exception);
            }
        }


        [Then(@"I can see that '(.*)' it appears in Video search results")]
        public void ThenICanSeeThatItAppearsInVideoSearchResults(string expectedText)
        {
            try
            {
                Assert.True(_searchPage.IsSearchKeywordPresentInVideoSearchedResults(expectedText));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by the Image name = " + expectedText + ", the image may not have been listed in search results. Exception is: " + e.ToString());
            }
        }

        private static void AssertExhibitProperties(string detectedManufacturer, string detectedModel, string imei,
            string deviceInfoPhoneDateTime, ExhibitMetadataList.ExhibitMetadataRow deviceInfo)
        {
            Assert.True(deviceInfo.Metadata.Contains(detectedManufacturer),
                "DetectedManufacturer '" + detectedManufacturer + "' was not found in Exibit Properties. Exhibit id is " +
                _exhibitId);
            Assert.True(deviceInfo.Metadata.Contains(detectedModel),
                "DetectedModel '" + detectedModel + "' was not found in Exibit Properties. Exhibit id is " + _exhibitId);
            Assert.True(deviceInfo.Metadata.Contains(imei),
                "IMEI number '" + imei + "' was not found in Exibit Properties. Exhibit id is " + _exhibitId);
            Assert.True(deviceInfo.Metadata.Contains(deviceInfoPhoneDateTime),
                "DeviceInfoPhoneDateTime '" + deviceInfoPhoneDateTime + "' was not found in Exibit Properties. Exhibit id is " +
                _exhibitId);
        }

        private static void AssertExhibitSource(ExhibitMetadataList.ExhibitMetadataRow deviceInfo)
        {
            var actualSource = deviceInfo.Source;
            Assert.True(actualSource.Equals(ExpectedSource),
                "Exhibit properties source is not equal to the expected value. Expected: '" + ExpectedSource + "', actual: '" +
                actualSource + "'.");
        }


        [When(@"i am on the exhibit page")]
        public void WhenIAmOnTheExhibitPage()
        {
            _casesPage = _homePage.GoToCases();
            //Select the case with the exhibit that was processed in the previous step
            _exhibitsPage = _casesPage.SelectCaseByPoliceFileNumber(_casePoliceNumber);
            _exhibitsPage.SelectProcessedExhibitFromListByEclExhibitNumber(_eclExhibitNumber);
        }

        [Then(@"the Device info should be dispalyed")]
        public void ThenTheDeviceInfoShouldBeDispalyed()
        {
            _exhibitsPage.IsDeviceInfoDisplayed();
        }


        [Then(@"the Device info should have the following values")]
        public void ThenTheDeviceInfoShouldHaveTheFollowingValues(Table expectedTable)
        {
            _exhibitsPage.IsDeviceInfoValid(expectedTable);

        }


        [When(@"I search for a '(.*)' in '(.*)'")]
        public void WhenISearchForAIn(string expectedText, string plugin)
        {
            //Navigate to cases page
            _casesPage = _homePage.GoToCases();
            //Select the case with the exhibit that was processed in the previous step
            _casesPage.SelectCaseByPoliceFileNumber(_casePoliceNumber);
            //Navigate to search page
            _searchPage = _casesPage.GoToSearchPage();


            switch (plugin)
            {
                case "Mobile Content":
                    //Navigate to Mobile content search
                    _searchPage = _searchPage.GoToSearchMobileContentPage();
                    //Search mobile content for a keyword
                    _searchPage.SearchMobileContent(expectedText);
                    break;

                case "File Metadata":
                    //Navigate to Filemetadata search
                    _searchPage = _searchPage.GoToSearchFileMetadataPage();
                    //Search Filemetadata for a keyword
                    _searchPage.SearchFileMetadata(expectedText);
                    break;

                case "Images plugin":
                    //Navigate to Image search
                    _searchPage = _searchPage.GoToSearchImagesPlugin();
                    //Search Image for a keyword
                    _searchPage.SearchImages(expectedText);
                    break;

                case "Video plugin":
                    //Navigate to Image search
                    _searchPage = _searchPage.GoToSearchVideoPlugin();
                    //Search Image for a keyword
                    _searchPage.SearchVideo(expectedText);
                    break;

                case "Content Search":
                    //Navigate to Content search
                    _searchPage = _searchPage.GoToSearchContentPlugin();
                    //Search Content for a keyword
                    _searchPage.SearchContent(expectedText);
                    break;
            }
        }


        [When(@"i click on the search result")]
        public void WhenIClickOnTheSearchResult()
        {
            _searchPage.OpenViewer();
            _viewerPage = _searchPage.GetParentAndChildHandlers();

        }

        [When(@"i click on the image or video search result")]
        public void WhenIClickOnTheImageOrVideoSearchResult()
        {
           _searchPage.OpenImageVideoViewer();

           _viewerPage = _searchPage.GetParentAndChildHandlers();
        }


        [Then(@"the viewer should open")]
        public void ThenTheViewerShouldOpen()
        {
            _viewerPage.IsViewerOpen();
        }

        [Then(@"the '(.*)','(.*)' and '(.*)' are present in the header of viewer\.")]
        public void ThenTheAndAreValidForImagesAndVideos_(string textItem1, string textItem2, string textItem3)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewerHeader(textItem1));
                Assert.True(_viewerPage.FindTextinViewerHeader(textItem2));
                Assert.True(_viewerPage.FindTextinViewerHeader(textItem3));
             
            }
            catch (Exception exception)
            {
                Console.Write(@"Either of {0},{1} or {2} were not found in the header of viewer.", textItem1, textItem2, textItem3);
                throw new Exception("Exception raised:" + exception);
            }
        }

        [Then(@"the '(.*)', '(.*)' and '(.*)' should be present in viewer text\.")]
        public void ThenTheAndShouldBeValid_(string textItem1, string textItem2, string textItem3)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewer(textItem1));
                Assert.True(_viewerPage.FindTextinViewer(textItem2));
                Assert.True(_viewerPage.FindTextinViewer(textItem3));
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();

            }

            catch (Exception exception)
            {
                Console.Write(@"Either of {0},{1} or {2} were not found in the viewer.", textItem1, textItem2, textItem3);
                throw new Exception("Exception raised:" + exception);
            }
        }


        [Then(@"the '(.*)', '(.*)', '(.*)' and '(.*)' are valid for Chat\.")]
        public void ThenTheAndAreValidForChat_(string expectedStartDate, string expectedMessage, string expectedTo, string expectedFrom)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewer(expectedMessage));
                Assert.True(_viewerPage.FindTextinViewer(expectedStartDate));
                Assert.True(_viewerPage.FindTextinViewer(expectedTo));
                Assert.True(_viewerPage.FindTextinViewer(expectedFrom));
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();
            }

            catch (Exception exception)
            {
                Console.Write(@"Either of {0}, {1}, {2} or {3} were not found in the viewer.", expectedMessage, expectedTo, expectedFrom, expectedStartDate);
                throw new Exception("Exception raised:" + exception);
            }
        }



        [Then(@"the '(.*)','(.*)', '(.*)' and '(.*)' are valid\.")]
        public void ThenTheAndAreBeValid_(string expectedTime, string expectedDuration, string expectedCallee, string expectedDirection)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewer(expectedTime));
                Assert.True(_viewerPage.FindTextinViewer(expectedDuration));
                Assert.True(_viewerPage.FindTextinViewer(expectedDirection));
                Assert.True(_viewerPage.FindTextinViewer(expectedCallee));
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();

            }
            catch (Exception exception)
            {
                Console.Write(@"Either of {0},{1}, {2} or {3} were not found in the viewer.", expectedDirection, expectedDuration, expectedTime, expectedCallee);
                throw new Exception("Exception raised:" + exception);
            }
        }

        [Then(@"the '(.*)','(.*)', '(.*)', '(.*)' and '(.*)' are valid for calendar\.")]
        public void ThenTheAndAreValidForCalendar_(string expectedStartDate, string expectedStopDate, string expectedLocation, string expectedDetails, string expectedSubject)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewer(expectedStartDate));
                Assert.True(_viewerPage.FindTextinViewer(expectedStopDate));
                Assert.True(_viewerPage.FindTextinViewer(expectedLocation));
                Assert.True(_viewerPage.FindTextinViewer(expectedDetails));
                Assert.True(_viewerPage.FindTextinViewer(expectedSubject));
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();

            }
            catch (Exception exception)
            {
                Console.Write(@"Either of {0},{1}, {2}, {3} or {4} were not found in the viewer.", expectedStartDate, expectedStopDate, expectedLocation, expectedDetails, expectedSubject);
                throw new Exception("Exception raised:" + exception);
            }
        }

        [Then(@"the Image is visible to user\.")]
        public void ThenTheImageIsVisibleToUser_()
        {
            try
            {
                _viewerPage.SwitchToChild();
                Assert.True(_viewerPage.ValidateImageVisible());
                _viewerPage.CloseViewer();
                _viewerPage.SwitchToParent();

            }
            catch (Exception exception)
            {

                throw new Exception("Unable to view the image in the viewer. Exception" + exception);
            }
        }

        [Then(@"the  dates '(.*)' '(.*)' are valid in the search result")]
        public void ThenTheDatesAreValid(string expectedStartDate, string dateType)
        {
            var index = 0;

            switch (dateType)
            {
                case "Start Date":
                    index = 6;
                    break;

                case "Stop Date":
                    index = 7;
                    break;
            }

            try
            {
                Assert.True(_searchPage.ValidateDates(expectedStartDate, index));
            }
            catch (Exception exception)
            {
                throw new Exception("The Dates did not match to the expected Dates" + exception);
            }
        }

        [Then(@"I can see that '(.*)' or '(.*)' appears in Mobile Content search results")]
        public void ThenICanSeeThatItAppearsInMobileContentSearchResults(string expectedText, string searchType)
        {
            try
            {
                int verifyMobileContentIndex = 0;
                switch (searchType)
                {

                    case "Message":
                        verifyMobileContentIndex = 8;
                        break;

                    case "From":
                    case "Name":
                        verifyMobileContentIndex = 4;
                        break;

                    case "To":
                        verifyMobileContentIndex = 5;
                        break;
                    case "Subject":
                        verifyMobileContentIndex = 9;
                        break;
                }

                Assert.True(_searchPage.IsSearchKeywordPresentInMobileContentSearchedResults(expectedText, verifyMobileContentIndex));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by the Contact name = " + expectedText + ", the contact may not have been listed in search results. Exception is: " + e.ToString());
            }
        }

        [Then(@"the '(.*)','(.*)', '(.*)', '(.*)', '(.*)' and '(.*)' are valid for MMS\.")]
        public void ThenTheAndAreValidForMMS_(string expectedStartDate, string expectedSubject, string expectedMMS, string expectedTo, string expectedAttachment, string expectedFrom)
        {
            try
            {
                Assert.True(_viewerPage.FindTextinViewer(expectedStartDate));
                Assert.True(_viewerPage.FindTextinViewer(expectedMMS));
                Assert.True(_viewerPage.FindTextinViewer(expectedTo));
                Assert.True(_viewerPage.FindTextinViewer(expectedFrom));
                Assert.True(_viewerPage.FindTextinViewer(expectedSubject));
                Assert.True(_viewerPage.FindTextinViewer(expectedAttachment));
            }
            catch (Exception exception)
            {
                Console.Write(@"Either of {0},{1}, {2}, {3}, {4} or {5} were not found in the viewer.", expectedStartDate, expectedFrom, expectedMMS, expectedTo, expectedSubject, expectedAttachment);
                throw new Exception("Exception raised:" + exception);
            }
        }

        [Then(@"I can see that '(.*)' appears in File Metadata search results")]
        public void VerifyFileMetadataSearchResults(string expectedText)
        {
            try
            {
                Assert.True(_searchPage.IsSearchKeywordPresentInFileMetadataSearchedResults(expectedText, verifyFileMetadataIndex));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by the Contact name = " + expectedText + ", the contact may not have been listed in search results. Exception is: " + e.ToString());
            }
        }

        [Then(@"I can see that '(.*)' appears in Content search results")]
        public void VerifyContentSearchResults(string expectedText)
        {
            try
            {
                Assert.True(_searchPage.IsSearchKeywordPresentInContentSearchedResults(expectedText));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by the Contact name = " + expectedText + ", the contact may not have been listed in search results. Exception is: " + e.ToString());
            }
        }


        [Then(@"the user should be able to open the '(.*)'\.")]
        public void ThenTheUserShouldBeAbleToOpenThe_(string AttachmentName)
        {
            _viewerPage.OpenMMSAttachment(AttachmentName);
            _viewerPage.CloseViewer();
            _viewerPage.SwitchToParent();
        }


        [Then(@"I can see that '(.*)' it appears in Images search results")]
        public void VerifyImagesSearchResults(string expectedText)
        {
            try
            {
                Assert.True(_searchPage.IsSearchKeywordPresentInImagesSearchedResults(expectedText));
            }
            catch (Exception e)
            {
                throw new Exception("When searching by the Image name = " + expectedText + ", the image may not have been listed in search results. Exception is: " + e.ToString());
            }
        }


        // *** Local functions ***

        private static void AssertProcessingStatisticsCategoryFileNumber(string calenderNum, string callNum, string chatNum, string contactNum, string documentNum)
        {

            var actualNumberOfCalender = _mediaPage.GetProcessingStatisticsNumberOfCalenders();
            var actualNumberOfCalls = _mediaPage.GetProcessingStatisticsNumberOfCalls();
            var actualNumberOfChats = _mediaPage.GetProcessingStatisticsNumberOfChats();
            var actualNumberOfContacts = _mediaPage.GetProcessingStatisticsNumberOfContacts();
            var actualNumberOfDocumentFiles = _mediaPage.GetProcessingStatisticsNumberOfDocumentFiles();

            Assert.True(actualNumberOfCalender.Equals(calenderNum),
                "The number of Calender items in Processing statistics is not equal to the expected one. Expected: " + calenderNum +
                ", actual: " + actualNumberOfCalender + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfCalls.Equals(callNum),
               "The number of calls in Processing statistics is not equal to the expected one. Expected: " + callNum +
               ", actual: " + actualNumberOfCalls + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfChats.Equals(chatNum),
             "The number of calls in Processing statistics is not equal to the expected one. Expected: " + chatNum +
             ", actual: " + actualNumberOfChats + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfContacts.Equals(contactNum),
             "The number of Contacts in Processing statistics is not equal to the expected one. Expected: " + contactNum +
             ", actual: " + actualNumberOfContacts + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfDocumentFiles.Equals(documentNum),
            "The number of Documents in Processing statistics is not equal to the expected one. Expected: " + documentNum +
            ", actual: " + actualNumberOfDocumentFiles + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);
        }

        private static void AssertProcessingStatisticsCategoryLabels(string calenderLabel, string callLabel, string chatLabel, string contactLabel, string documentLabel)
        {
            var actualProcessingStatisticsLabels = _mediaPage.GetProcessingStatisticsLabels();
            var expectedLabelsList = new List<string>()
            {
                calenderLabel,
                callLabel,
                chatLabel,
                contactLabel,
                documentLabel
            };
            foreach (var label in expectedLabelsList)
            {
                Assert.True(actualProcessingStatisticsLabels.Contains(label),
                    "Processing Statistics for Media id=" + _mediaId + " (exhibit id =" + _exhibitId + ") does not contain the expected label '" + label + "'.");
            }
        }

        private static void SelectFirstMedia()
        {
            var actualFirstMediaNumber = _exhibitsPage.GetFirstMediaLabel();
            Assert.True(actualFirstMediaNumber.Equals(_mediaNumber),
                "The first media on Case Exhibits page is not the extected one. Expected media with number '" + _mediaNumber +
                "' but was '" + actualFirstMediaNumber + "'.");
            _mediaPage = _exhibitsPage.SelectFirstMediaOnCaseExhibitsPage();
        }

        private void ProcessExhibit()
        {
            var exhibitHelper = new ExhibitHelper();

            Console.WriteLine("Acquisition format '" + AcquisitionFormatType.MobileDeviceXry + "' will be processed for ExhibitId :" + _exhibitId);

            exhibitHelper.ProcessExhibit(_exhibitId, _mediaId, AcquisitionFormatType.MobileDeviceXry);

            CheckMediaAndWorkStatuses();
        }

        private void CheckMediaAndWorkStatuses()
        {
            WaitAndAssertWorkStatus(WorkStatus.Created, WorkTable.WorkHandlerType.ProcessMedia,
             Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForWorkProcessMediaStatusToBeCreated"].Value));
            WaitAndAssertMediaStatus(MediaTable.MediaStatus.Queued,
                Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForMediaStatusToBeQueued"].Value));

            WaitAndAssertWorkStatus(WorkStatus.Processing, WorkTable.WorkHandlerType.ProcessMedia,
                Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForWorkProcessMediaStatusToBeProcessing"].Value));
            WaitAndAssertMediaStatus(MediaTable.MediaStatus.Processing,
                Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForMediaStatusToBeProcessing"].Value));

            //wait before start checking whether an exhibit is processed.
            Thread.Sleep(Convert.ToInt32(_settings["NumberOfMinutesToWaitBeforeStartCheckingForExhibitToBeProcessed"].Value));
            WaitAndAssertWorkStatus(WorkStatus.Completed, WorkTable.WorkHandlerType.ProcessMedia,
                Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForWorkProcessMediaStatusToBeCompleted"].Value));
            WaitAndAssertMediaStatus(MediaTable.MediaStatus.Processed,
                Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForMediaStatusToBeProcessed"].Value));
        }

        private static void AssertMediaStatus(MediaTable.MediaStatus status)
        {
            var expectedMediaStatus = _mediaTable.GetMediaStatusIdCodeFromMap(status);
            var actualMediaStatusId = _mediaTable.GetCurrentMediaStatusId(status, _mediaId);
            Assert.True(
                actualMediaStatusId.Equals(expectedMediaStatus),
                "Media status id '" + actualMediaStatusId +
                "' is not equal to the expected one '" + expectedMediaStatus +
                "'. Exhibit id is '" + _exhibitId + "'. Media number is ' " + _mediaDataModel.MediaNumber + "'.");
        }

        private static void WaitAndAssertMediaStatus(MediaTable.MediaStatus status, double maxTimeToWait)
        {
            var error = string.Empty;
            var sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed < TimeSpan.FromMinutes(maxTimeToWait))
            {
                try
                {
                    AssertMediaStatus(status);
                    sw.Stop();
                    return;
                }
                catch (AssertionException e)
                {
                    error = e.ToString();
                }
            }
            sw.Stop();
            throw new AssertionException(error);
        }

        private static void WaitAndAssertWorkStatus(WorkStatus status, WorkTable.WorkHandlerType handlerType, double maxTimeToWait)
        {
            var error = string.Empty;
            var sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed < TimeSpan.FromMinutes(maxTimeToWait))
            {
                try
                {
                    AssertWorkStatus(status, handlerType);
                    sw.Stop();
                    return;
                }
                catch (AssertionException e)
                {
                    error = e.ToString();
                }
            }
            sw.Stop();
            throw new AssertionException(error);
        }

        private static void AssertWorkStatus(WorkStatus status, WorkTable.WorkHandlerType handlerType)
        {
            var expectedWorkStatusId = _workTable.GetWorkStatusIdCodeFromMap(status);
            var actualWorkStatusId = 0;

            actualWorkStatusId = _workTable.GetCurrentWorkStatusIdForHandlerType(handlerType);
            Assert.True(
               actualWorkStatusId.Equals(expectedWorkStatusId),
                "Work status id '" + actualWorkStatusId + "' is not equal to the expected one '" + expectedWorkStatusId + "'.");
        }

        [BeforeFeature]
        private static void Setup()
        {
            _settings = GetSettingsFromConfig();

            var distributedConnectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Distributed);
            _distributedDatabase = new DistributedDatabase(distributedConnectionString);
            _workTable = new WorkTable(distributedConnectionString);
        }

        [BeforeScenario]
        private static void BeforeEachScenario()
        {
            _distributedDatabase.ResetDistributedDatabase();
        }

        /*  [AfterScenario]
          private static void DeleteDataFromDb()
          {
              _distributedDatabase.ResetDistributedDatabase();

              if (_exhibitId != 0)
              {
                  _exhibitDatabase = new ExhibitDatabase(_exhibitDatabaseConnectionString);
                  _exhibitDatabase.ResetExhibitDatabase(_exhibitId);
                  _exhibitDatabase.DropExhibitDatabase(_exhibitId);
                  Exhibit.DeleteExhibit(_caseId, _exhibitId);
              }

              if (_caseId != 0)
              {
                  Case.DeleteCase(_caseId);
              }
          }*/

        private static KeyValueConfigurationCollection GetSettingsFromConfig()
        {
            var configFile = new ExeConfigurationFileMap { ExeConfigFilename = ConfigFile };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            return config.AppSettings.Settings;
        }

        private static void PrepareDbData()
        {
            var details = new ExhibitHelper().PrepareDbData(_mediaDataModel.EvidencePath, AcquisitionFormatType.MobileDeviceXry);
            _caseId = details.CaseId;
            _exhibitId = details.ExhibitId;
            _mediaId = details.MediaId;
            _casePoliceNumber = details.CasePoliceNumber;
            _eclExhibitNumber = details.EclExhibitNumber;
            _mediaNumber = details.MediaNumber;
        }
    }
}