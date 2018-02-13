using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using EVE.BLL;
using EVE.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Helpers;
using TestAutomation.Model.DataModel;
using TestAutomation.Steps.EndToEndTest.ResultChecks;
using TestAutomation.Steps.EndToEndTest.UFED;

namespace TestAutomation.Steps.EndToEndTest.Forensic
{
    [Binding]
    public class ForensicProcessingSmokeTestSteps:EndtoEndBase
    {
        private static AcquisitionFormatType _type;

        [BeforeFeature, Scope(Feature = "Xry Logical Processing ExhibitDb Tables Check")]
        [BeforeFeature, Scope(Feature = "ForensicProcessingSmokeTest")]
        [BeforeFeature, Scope(Feature = "Ufed Logical Processing ExhibitDb Tables Check")]
        private static void Setup()
        {
           
            _settings = GetSettingsFromConfig();
            _exhibitDetailsDataFilePath = GetPathToExpectedExhibitDetails();
            _mediaDataModel.EvidencePath = Path.GetFullPath(_settings["EvidencePath"].Value);
            _type = AcquisitionFormatType.MobileDeviceUfed;
            string pathToFileWithProcessingTimeData = Path.GetFullPath(_settings["PathToFileWithProcessingTimeData"].Value);
            string processingTimeCsvFileName = Path.GetFullPath(_settings["ProcessingTimeCsvFileName"].Value);
            WriteDefaultValueToProcessingTimeCsvFile(pathToFileWithProcessingTimeData, processingTimeCsvFileName);
            PrepareDbData(_mediaDataModel, _type);
            InitialiseTables();
            _distributedDatabase.ResetDistributedDatabase();
        }

        [When(@"I process an Exhibit")]
        public void WhenIProcessAnExhibit()
        {
            ProcessExhibit();
            _processedExhibit = true;
            

            Console.WriteLine("EXHIBIT ID IS " + _exhibitId);

            Console.WriteLine(@"Exhibit is processed. Start checking for Work FinaliseMedia Handler Completed processing status...");
            WaitAndAssertWorkStatus(WorkStatus.Completed, WorkTable.WorkHandlerType.FinaliseMedia, Convert.ToDouble(_settings["MaxNumberOfMinutesToWaitForWorkFinaliseMediaStatusToBeCompleted"].Value));
        }

        [When(@"I check '(.*)' table")]
        public void WhenICheckTable(string p0)
        {
           
        }

        [When(@"the csv file with the expected Media file details data exists")]
        public void WhenTheCsvFileWithTheExpectedMediaFileDetailsDataExists()
        {
            Assert.True(File.Exists(_exhibitDetailsDataFilePath));
        }

        [Then(@"the processing time has been recorded")]
        public  void ThenTheProcessingTimeHasBeenRecorded()
        {
            if (!_processedExhibit) return;
            var processingTime = CalculateExhibitProcessingTime();
            File.WriteAllText(_processingTimeCsvFileName, @"processingTime" + Environment.NewLine + processingTime.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
        }

        [Then(@"the number of processed files is correct in '(.*)' table")]
        public void ThenTheNumberOfProcessedFilesIsCorrectInTable(string table)
        {
            if (!_processedExhibit)
            {
                ScenarioContext.Current.Pending();
            }

            var settingField = string.Format("ExpectedNumberOfRowsIn{0}", table);
            int expectedValue = 0;
            if (!int.TryParse(_settings[settingField].Value, out expectedValue))
                throw new Exception(string.Format("Setting {0} value {1} can not be converted a count value", settingField, _settings[settingField].Value));

            var dbCheck = DataBaseCheckFactory.GetCheck(table, _exhibitDatabaseConnectionString);
            dbCheck.AssertNumberOfRows(_exhibitId, expectedValue);
        }

        [Then(@"the porn probability values of all processed files are not NULL and not (.*)")]
        public void ThenThePornProbabilityValuesOfAllProcessedFilesAreNotNullAndNot(int errorCode)
        {
            var dbCheck = new ImageMetaDataCheck(_exhibitDatabaseConnectionString);
            dbCheck.AssertPornProbabilityForAllFilesNotNullAndNotTheErrorCode(_exhibitId, errorCode);
        }

        [Then(@"the processed file details are correct in FileMetadata table as per the csv file")]
        public void ThenTheProcessedFileDetailsAreCorrectInFileMetadataTableAsPerTheCsvFile()
        {
            if (!_processedExhibit)
            {
                ScenarioContext.Current.Pending();
            }

            var csvReader = new CsvFileReader(_exhibitDetailsDataFilePath);
            var numberOfExpectedFiles = csvReader.GetNumberOfDataRowsOnWorksheet(0);

            var listOfFilesNotFound = new List<FileMetadataModel>();
            var expectedFileDetailsList = new List<FileMetadataModel>();

            // Rows index (i) starts on 1 to skip header row
            for (var i = 1; i <= numberOfExpectedFiles; i++)
            {
                var row = PopulateExpectedFileDetailsDictionary(csvReader, i);
                expectedFileDetailsList.Add(row);
            }

            foreach (var row in expectedFileDetailsList)
            {
                //var actualNumberOfRowsMatching = _fileMetadataTable.GetNumberOfRowsThatMatchDetails(_exhibitId, row);
                //if (actualNumberOfRowsMatching == 1) continue;
                var _partitionMetadata = 1;
                if (_fileMetadataTable.WasFileProcessed(_exhibitId, _partitionMetadata, row))
                {
                    continue;
                }

                AddNotFoundFileDetailsToDictionary(listOfFilesNotFound, row);
            }

            Assert.True(listOfFilesNotFound.Count.Equals(0),
                "Some expected files were not found after the exhibit (id=" + _exhibitId + ") processing. Those files are:\n" + GetListAsString(listOfFilesNotFound));

        }

       

       [AfterFeature, Scope(Feature = "ForensicProcessingSmokeTest")]
       [AfterFeature, Scope(Feature = "Ufed Logical Processing ExhibitDb Tables Check")]
        private static void DeleteTestDataFromDb()
        {
           /* _distributedDatabase.ResetDistributedDatabase();
            _exhibitDatabase.ResetExhibitDatabase(_exhibitId);
            _exhibitDatabase.DropExhibitDatabase(_exhibitId);
            Exhibit.DeleteExhibit(_caseId, _exhibitId);
            Case.DeleteCase(_caseId);*/
        }
    }
}
