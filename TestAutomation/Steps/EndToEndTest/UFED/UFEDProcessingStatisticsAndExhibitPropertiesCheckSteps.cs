using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using EVE.BLL;
using EVE.Common;
using EVE.Data;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Drivers.PageDriver.Exhibits;
using TestAutomation.Drivers.PageDriver.Home;
using TestAutomation.Helpers;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.EndToEndTest.UFED
{
    [Binding, Scope(Feature = "Ufed Processing Statistics And Exhibit Properties Check")]
    public class UfedProcessingStatisticsAndExhibitPropertiesCheckSteps : DeiGuiTest
    {

        private static int _exhibitId;
        private static int _caseId;
        private static int _mediaId;
        private static KeyValueConfigurationCollection _settings;
        private static readonly string ConfigFile = Path.GetFullPath(@"..\..\Steps\EndToEndTest\TestData\UFEDProcessingStatisticsAndExhibitPropertiesCheck\Test.config");
        private static MediaTable _mediaTable;
        private static WorkTable _workTable;
        private static DistributedDatabase _distributedDatabase;
        private static ExhibitDatabase _exhibitDatabase;
        private static string _exhibitDatabaseConnectionString;
        private static MediaDataModel _mediaDataModel = new MediaDataModel().GetDefault();
        private static HomePage _homePage = new HomePage(Driver);
        private static ExhibitsPage _exhibitsPage;
        private static string _casePoliceNumber;
        private static string _eclExhibitNumber;
        private static string _mediaNumber;
        private static MediaPage _mediaPage;

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
        }

        [Then(@"the Exhibit Properties are populated: '(.*)' DeviceInfoDetectedManufacturer, '(.*)' DeviceInfoDetectedModel, (.*) IMEI, '(.*)' DeviceInfoPhoneDateTime")]
        public void ThenTheExhibitPropertiesArePopulatedCorrectly(string detectedManufacturer, string detectedModel, string imei, string deviceInfoPhoneDateTime)
        {
            var deviceInfo = ExhibitMetadata.GetExhibitMetadata_Case(_caseId, _exhibitId).First();
            AssertExhibitSource(deviceInfo);
            AssertExhibitProperties(detectedManufacturer, detectedModel, imei, deviceInfoPhoneDateTime, deviceInfo);
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

        [Then(@"the Processing Statistics details are: (.*) '(.*)', (.*) '(.*)', (.*) '(.*)', (.*) '(.*)'")]
        public void ThenProcessingStatisticsDetailsAre(string imageNum, string imageLabel, string multimediaNum, string multimediaLabel, string contactNumber, string contactLabel, string documentNum, string documentLabel)
        {
            _exhibitsPage = _homePage.GoToCaseExhibits(_casePoliceNumber);
            _exhibitsPage.SelectProcessedExhibitFromListByEclExhibitNumber(_eclExhibitNumber);
            SelectFirstMedia();
            AssertProcessingStatisticsCategoryLabels(imageLabel, multimediaLabel, contactLabel, documentLabel);
            AssertProcessingStatisticsCategoryFileNumber(imageNum, multimediaNum, contactNumber, documentNum);
        }

        private static void AssertProcessingStatisticsCategoryFileNumber(string imageNum, string multimediaNum, string contactNumber, string documentNum)
        {
            var actualNumberOfImages = _mediaPage.GetProcessingStatisticsNumberOfImages();
            var actualNumberOfMultimedia = _mediaPage.GetProcessingStatisticsNumberOfMultimedia();
            var actualNumberOfContacts = _mediaPage.GetProcessingStatisticsNumberOfContacts();
            var actualNumberOfDocumentFiles = _mediaPage.GetProcessingStatisticsNumberOfDocumentFiles();

            Assert.True(actualNumberOfImages.Equals(imageNum),
                "The number of Images in Processing statistics is not equal to the expected one. Expected: " + imageNum +
                ", actual: " + actualNumberOfImages + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfMultimedia.Equals(multimediaNum),
               "The number of Multimedia in Processing statistics is not equal to the expected one. Expected: " + multimediaNum +
               ", actual: " + actualNumberOfMultimedia + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfContacts.Equals(contactNumber),
             "The number of Contacts in Processing statistics is not equal to the expected one. Expected: " + contactNumber +
             ", actual: " + actualNumberOfContacts + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);

            Assert.True(actualNumberOfDocumentFiles.Equals(documentNum),
          "The number of Documents in Processing statistics is not equal to the expected one. Expected: " + documentNum +
          ", actual: " + actualNumberOfDocumentFiles + ". Exhibit id is " + _exhibitId + ", media id is " + _mediaId);
        }

        private static void AssertProcessingStatisticsCategoryLabels(string imageLabel, string multimediaLabel,
            string contactLabel, string documentLabel)
        {
            var actualProcessingStatisticsLabels = _mediaPage.GetProcessingStatisticsLabels();
            var expectedLabelsList = new List<string>()
            {
                imageLabel,
                multimediaLabel,
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
            exhibitHelper.ProcessExhibit(_exhibitId, _mediaId, AcquisitionFormatType.MobileDeviceUfed);

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

        [AfterScenario]
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
        }

        private static KeyValueConfigurationCollection GetSettingsFromConfig()
        {
            var configFile = new ExeConfigurationFileMap { ExeConfigFilename = ConfigFile };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            return config.AppSettings.Settings;
        }

        private static void PrepareDbData()
        {
            var details = new ExhibitHelper().PrepareDbData(_mediaDataModel.EvidencePath, AcquisitionFormatType.MobileDeviceUfed);
            _caseId = details.CaseId;
            _exhibitId = details.ExhibitId;
            _mediaId = details.MediaId;
            _casePoliceNumber = details.CasePoliceNumber;
            _eclExhibitNumber = details.EclExhibitNumber;
            _mediaNumber = details.MediaNumber;
        }
    }
}