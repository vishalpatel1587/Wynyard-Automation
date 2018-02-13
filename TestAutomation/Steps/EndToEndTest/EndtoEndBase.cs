using System;

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using EVE.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Helpers;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Steps.EndToEndTest
{
    public class EndtoEndBase
    {
        public static MediaDataModel _mediaDataModel = new MediaDataModel().GetDefault();

        public static int _exhibitId;
        public static int _caseId;
        public static int _mediaId;
        public static KeyValueConfigurationCollection _settings;

        public static string _processingTimeCsvFileName;
        public static bool _processedExhibit;
        public static FileMetadataTable _fileMetadataTable;
        private static EntityTable _entityTable;
        public static ConnectionMetadataTable _connectionMetadataTable;
        private static MediaTable _mediaTable;
        private static WorkTable _workTable;
        public static DistributedDatabase _distributedDatabase;
        public static ExhibitDatabase _exhibitDatabase;
        public static string _exhibitDatabaseConnectionString;

        public static string _exhibitDetailsDataFilePath;

        public const int FileNameColumnNumberInCsvFile = 0;
        public const int FilePathColumnNumberInCsvFile = 1;
        public const int CreatedDateColumnNumberInCsvFile = 2;
        public const int LastAccessedDateColumnNumberInCsvFile = 3;
        public const int LastModifiedColumnNumberInCsvFile = 4;
        public const int FileTypeColumnNumberInCsvFile = 5;
        public const int FileExtensionColumnNumberInCsvFile = 6;
        public const int FileSizeColumnNumberInCsvFile = 7;

        private const string ForensicFeatureName = "ForensicProcessingSmokeTest";
        private const string UfedFeatureName = "Ufed Logical Processing ExhibitDb Tables Check";
        private const string XRYFeatureName = "Xry Logical Processing ExhibitDb Tables Check";

        private static string _forensicTestConfigFile = Path.GetFullPath(@"..\..\Steps\EndToEndTest\TestData\ForensicSmokeTestExhibit\Test.config");
        private static string _ufedTestConfigFile = Path.GetFullPath(@"..\..\Steps\EndToEndTest\TestData\UfedLogicalProcessingExhibitDbTablesCheck\Test.config");

        private static string _xryTestConfigFile =
            Path.GetFullPath(
                @"..\..\Steps\EndToEndTest\TestData\XRYProcessingStatisticsAndExhibitPropertiesCheck\Test.config");

        //[BeforeFeature]
        public static void CheckPrerequisites()
        {
            Debug.WriteLine("debug - Checking prerequisites. Using config file: " + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            Console.WriteLine("debug - Checking prerequisites. Using config file: " + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            // check if ProcessingAgent is running
            var processingAgent = Process.GetProcessesByName(ConfigurationManager.AppSettings["ProcessingAgentProcessName"]);
            if (processingAgent.Length == 0) throw new ConfigurationErrorsException("Processing Agent service is not running or ProcessingAgentProcessName setting has not been added to the config file");

            // check if GroupController is running
            var groupController = Process.GetProcessesByName(ConfigurationManager.AppSettings["GroupControllerProcessName"]);
            if (groupController.Length == 0) throw new ConfigurationErrorsException("Group Controller service is not running or GroupControllerProcessName setting has not been added to the config file");

            // check if Mount Image Pro is running
            var mip = Process.GetProcessesByName(ConfigurationManager.AppSettings["MountImageProProcessName"]);
            if (mip.Length == 0) throw new ConfigurationErrorsException("Mount Image Pro is not running or MountImageProProcessName setting has not been added to the config file");
        }


        public static KeyValueConfigurationCollection GetSettingsFromConfig()
        {
            ExeConfigurationFileMap configFile;


            switch (FeatureContext.Current.FeatureInfo.Title)
            {
                case ForensicFeatureName:
                    {
                        configFile = new ExeConfigurationFileMap { ExeConfigFilename = _forensicTestConfigFile };
                        break;
                    }
                case UfedFeatureName:
                    {
                        configFile = new ExeConfigurationFileMap { ExeConfigFilename = _ufedTestConfigFile };
                        break;
                    }
                case XRYFeatureName:
                    {
                        configFile = new ExeConfigurationFileMap { ExeConfigFilename = _xryTestConfigFile };
                        break;
                    }

                default:
                    throw new Exception("No config file exist for a feature with name " + FeatureContext.Current.FeatureInfo.Title);
            }

            var config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            return config.AppSettings.Settings;
        }

        public TimeSpan CalculateExhibitProcessingTime()
        {
            var startTime = _workTable.GetCreatedDateForHandlerType(WorkTable.WorkHandlerType.ProcessMedia);
            var finishTime = _workTable.GetUpdatedDateForHandlerType(WorkTable.WorkHandlerType.ProcessMedia);
            return finishTime.Subtract(startTime);
        }

        public void ProcessExhibit()
        {
            var exhibitHelper = new ExhibitHelper();

            switch (FeatureContext.Current.FeatureInfo.Title)
            {
                case ForensicFeatureName:
                    {
                        exhibitHelper.ProcessExhibit(_exhibitId, _mediaId, AcquisitionFormatType.ForensicImage);
                        break;
                    }
                case UfedFeatureName:
                    {
                        exhibitHelper.ProcessExhibit(_exhibitId, _mediaId, AcquisitionFormatType.MobileDeviceUfed);
                        break;
                    }
                case XRYFeatureName:
                    {
                        exhibitHelper.ProcessExhibit(_exhibitId, _mediaId, AcquisitionFormatType.MobileDeviceXry);
                        break;
                    }
                default:
                    throw new Exception("The exhibit (id=" + _exhibitId + ") processing cannot be done for this unexpected feature with name " + FeatureContext.Current.FeatureInfo.Title);

            }
            CheckMediaAndWorkStatuses();
        }

        public void CheckMediaAndWorkStatuses()
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

        public void WaitAndAssertWorkStatus(WorkStatus expectedStatus, WorkTable.WorkHandlerType handlerType, double maxTimeToWait)
        {
            var sw = new Stopwatch();
            sw.Start();

            var expectedWorkStatusId = _workTable.GetWorkStatusIdCodeFromMap(expectedStatus);
            var actualWorkStatusId = 0;
            var erroredWorkStatusId = (int) WorkStatus.Errored;
            while (sw.Elapsed < TimeSpan.FromMinutes(maxTimeToWait))
            {
                //try
                //{
                //    AssertWorkStatus(status, handlerType);
                //    sw.Stop();
                //    return;
                //}
                //catch (AssertionException e)
                //{
                //    error = e.ToString();
                //}

                actualWorkStatusId = _workTable.GetCurrentWorkStatusIdForHandlerType(handlerType);

                if (expectedWorkStatusId != erroredWorkStatusId && actualWorkStatusId == erroredWorkStatusId)
                {
                    Assert.True(actualWorkStatusId.Equals(expectedWorkStatusId), "Work Status is Errored and it was not expected. Expected Work Status: '" + Enum.GetName(typeof(WorkStatus), expectedWorkStatusId) + "'. Waiting time (minutes): " + maxTimeToWait);
                }
                if (actualWorkStatusId.Equals(expectedWorkStatusId))
                {
                    sw.Stop();
                    return;
                }
            }
            sw.Stop();
            Assert.True(actualWorkStatusId.Equals(expectedWorkStatusId), "Work status '" + Enum.GetName(typeof(WorkStatus), actualWorkStatusId) + "' is not equal to the expected one '" + Enum.GetName(typeof(WorkStatus), expectedWorkStatusId) + "'. Waiting time (minutes): " + maxTimeToWait);
            //throw new AssertionException(error);
        }

        
        public bool AssertWorkStatus(WorkStatus status, WorkTable.WorkHandlerType handlerType)
        {
            var expectedWorkStatusId = _workTable.GetWorkStatusIdCodeFromMap(status);
            var actualWorkStatusId = 0;

            actualWorkStatusId = _workTable.GetCurrentWorkStatusIdForHandlerType(handlerType);
            return (actualWorkStatusId.Equals(expectedWorkStatusId));

            //Assert.True(
            //   actualWorkStatusId.Equals(expectedWorkStatusId),
            //    "Work status id '" + actualWorkStatusId + "' is not equal to the expected one '" + expectedWorkStatusId + "'.");
        }


        public void WaitAndAssertMediaStatus(MediaTable.MediaStatus status, double maxTimeToWait)
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

        public void AssertMediaStatus(MediaTable.MediaStatus status)
        {
            var expectedMediaStatus = _mediaTable.GetMediaStatusIdCodeFromMap(status);
            var actualMediaStatusId = _mediaTable.GetCurrentMediaStatusId(status, _mediaId);
            Assert.True(
                actualMediaStatusId.Equals(expectedMediaStatus),
                "Media status id '" + actualMediaStatusId +
                "' is not equal to the expected one '" + expectedMediaStatus +
                "'. Exhibit id is '" + _exhibitId + "'. Media number is ' " + _mediaDataModel.MediaNumber + "'.");
        }

        public static string GetPathToExpectedExhibitDetails()
        {
            return _settings["ExpectedExhibitDetails"].Value;
        }

        public static void WriteDefaultValueToProcessingTimeCsvFile(String pathToFileWithProcessingTimeData, String processingTimeCsvFileName)
        {
            pathToFileWithProcessingTimeData = Path.GetFullPath(_settings["PathToFileWithProcessingTimeData"].Value);
            _processingTimeCsvFileName = Path.GetFullPath(_settings["ProcessingTimeCsvFileName"].Value);

            if (!Directory.Exists(pathToFileWithProcessingTimeData))
            {
                Directory.CreateDirectory(pathToFileWithProcessingTimeData);
                File.Create(processingTimeCsvFileName).Close();
            }

            File.WriteAllText(processingTimeCsvFileName, @"processingTime" + Environment.NewLine + @"-");
        }

        public StringBuilder PrintNotMatchingConnectionTypeIdEntriesDictionary(
         Dictionary<int?, List<int?>> notMatchingConnectionTypeIdEntriesDictionary)
        {
            var builder = new StringBuilder();
            foreach (var element in notMatchingConnectionTypeIdEntriesDictionary)
            {
                builder.Append("ConnectionTypeId is " + element.Key + ":\n");
                var expectedNumberOfRows = element.Value[0];
                var actualNumberOfRows = element.Value[1];
                builder.Append("Expected number of rows is " + expectedNumberOfRows + ". Actual is " +
                               actualNumberOfRows + ".\n");
            }
            return builder;
        }

        public StringBuilder PrintNotMatchingEntityNameEntriesDictionary(
             Dictionary<string, List<int?>> notMachingEntityNameEntriesDictionary)
        {
            var builder = new StringBuilder();
            foreach (var element in notMachingEntityNameEntriesDictionary)
            {
                builder.Append(element.Key + " : \n");
                var expectedNumberOfRows = element.Value[0];
                var actualNumberOfRows = element.Value[1];
                builder.Append("Expected number of rows is " + expectedNumberOfRows + ". Actual is " +
                               actualNumberOfRows + ".\n");
            }
            return builder;
        }

        public static void PrepareDbData(MediaDataModel mediaDataModel, AcquisitionFormatType type)
        {
            mediaDataModel.EvidencePath = Path.GetFullPath(_settings["EvidencePath"].Value);
            var details = new ExhibitHelper().PrepareDbData(mediaDataModel.EvidencePath, type);
            _caseId = details.CaseId;
            _exhibitId = details.ExhibitId;
            _mediaId = details.MediaId;
        }

        public static void InitialiseTables()
        {
            var distributedConnectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Distributed);
            _workTable = new WorkTable(distributedConnectionString);

            _exhibitDatabaseConnectionString =
                EVE.Site.DAL.Config.GetConnectionString(DataStore.Exhibit)
                    .Replace(EVE.Site.DAL.Config.ExhibitPlaceholder, _exhibitId.ToString(CultureInfo.InvariantCulture));
            _fileMetadataTable = new FileMetadataTable(_exhibitDatabaseConnectionString);
            _entityTable = new EntityTable(_exhibitDatabaseConnectionString);
            _connectionMetadataTable = new ConnectionMetadataTable(_exhibitDatabaseConnectionString);
            _mediaTable = new MediaTable(_exhibitDatabaseConnectionString);
            _exhibitDatabase = new ExhibitDatabase(_exhibitDatabaseConnectionString);

            var distributedDbConnctionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Distributed);
            _distributedDatabase = new DistributedDatabase(distributedDbConnctionString);
        }

        public StringBuilder GetListAsString(IList<FileMetadataModel> notFoundRowDetails)
        {
            var builder = new StringBuilder();
            foreach (var file in notFoundRowDetails)
            {

                builder.Append(file);
                builder.Append("\n\n");
            }
            return builder;
        }

        public void AddNotFoundFileDetailsToDictionary(IList<FileMetadataModel> resultList, FileMetadataModel detailsToAdd)
        {
            if (resultList.SingleOrDefault(item => item.FilePath.Equals(detailsToAdd.FilePath)) == null)
            {
                resultList.Add(detailsToAdd);
            }
        }


        public FileMetadataModel PopulateExpectedFileDetailsDictionary(CsvFileReader csvReader, int rowNumber)
        {
            var expectedDetails = new FileMetadataModel
            {
                FileName = csvReader.GetStringValue(rowNumber, FileNameColumnNumberInCsvFile),
                FilePath = csvReader.GetStringValue(rowNumber, FilePathColumnNumberInCsvFile),
                CreatedDate = csvReader.GetDateTimeValue(rowNumber, CreatedDateColumnNumberInCsvFile),
                LastAccessedDate = csvReader.GetDateTimeValue(rowNumber, LastAccessedDateColumnNumberInCsvFile),
                LastModifiedDate = csvReader.GetDateTimeValue(rowNumber, LastModifiedColumnNumberInCsvFile),
                FileTypeId = csvReader.GetIntValue(rowNumber, FileTypeColumnNumberInCsvFile),
                FileExtension = csvReader.GetStringValue(rowNumber, FileExtensionColumnNumberInCsvFile),
                FileSize = long.Parse(csvReader.GetStringValue(rowNumber, FileSizeColumnNumberInCsvFile))
            };

            return expectedDetails;
        }

    }
}
