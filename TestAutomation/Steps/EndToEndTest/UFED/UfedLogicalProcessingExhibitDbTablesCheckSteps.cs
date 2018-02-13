using System;
using System.Collections.Generic;
using System.IO;
using EVE.BLL;
using EVE.Common;
using EVE.Site.BLL;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestAutomation.Helpers;
using Exhibit = EVE.BLL.Exhibit;

namespace TestAutomation.Steps.EndToEndTest.UFED
{
    [Binding, Scope(Feature = "Ufed Logical Processing ExhibitDb Tables Check")]
    public class UfedLogicalProcessingExhibitDbTablesCheckSteps : EndtoEndBase
    {
        /*[BeforeFeature, Scope(Feature = "Ufed Logical Processing ExhibitDb Tables Check")]
        private static void Setup()
        {

            if (!FeatureContext.Current.ContainsKey("BeforeFeatureExecuted"))
            {
                _settings = GetSettingsFromConfig();
                _exhibitDetailsDataFilePath = GetPathToExpectedExhibitDetails();
                _mediaDataModel.EvidencePath = Path.GetFullPath(_settings["EvidencePath"].Value);
                _type = AcquisitionFormatType.MobileDeviceUfed;
                string pathToFileWithProcessingTimeData =
                    Path.GetFullPath(_settings["PathToFileWithProcessingTimeData"].Value);
                string processingTimeCsvFileName = Path.GetFullPath(_settings["ProcessingTimeCsvFileName"].Value);
                WriteDefaultValueToProcessingTimeCsvFile(pathToFileWithProcessingTimeData, processingTimeCsvFileName);
                PrepareDbData(_mediaDataModel, _type);
                InitialiseTables();
                _distributedDatabase.ResetDistributedDatabase();

                FeatureContext.Current.Set(true, "BeforeFeatureExecuted");
            }

        }*/
        [Then(@"the number of expected EntityName entries is correct in '(.*)' table")]
        public void ThenTheNumberOfExpectedEntityNameEntriesIsCorrectInTable(string table)
        {
            {
                if (!_processedExhibit)
                {
                    ScenarioContext.Current.Pending();
                }

                var expectedEntityDataFilePath = Path.GetFullPath(_settings["ExpectedEntityData"].Value);
                Assert.True(File.Exists(expectedEntityDataFilePath));

                var csvReader = new CsvFileReader(expectedEntityDataFilePath);
                var totalNumberOfExpectedEntityNames = csvReader.GetNumberOfDataRowsOnWorksheet(0);

                var expectedEntityNameEntiresDictionary = new Dictionary<string, int?>();
                var notMachingEntityNameEntriesDictionary = new Dictionary<string, List<int?>>();

                for (var i = 0; i < totalNumberOfExpectedEntityNames; i++)
                {
                    var entityName = csvReader.GetStringValue(i, 0);
                    int? numberOfEntries = csvReader.GetIntValue(i, 1);
                    expectedEntityNameEntiresDictionary.Add(entityName, numberOfEntries);
                }
                foreach (var entity in expectedEntityNameEntiresDictionary)
                {
                    var entityName = entity.Key;
                    var expectedNumberOfRows = entity.Value;

                    var actualNumberOfRows = 0;
                    try
                    {
                        var listOfEntitiesWithName = Entity.GetEntity_Name(_exhibitId, entityName);
                        actualNumberOfRows = listOfEntitiesWithName.Count;
                    }
                    catch (Exception)
                    {
                    }

                    if (!actualNumberOfRows.Equals(expectedNumberOfRows))
                    {
                        notMachingEntityNameEntriesDictionary.Add(entityName,
                            new List<int?> { expectedNumberOfRows, actualNumberOfRows });
                    }
                }

                Assert.True(notMachingEntityNameEntriesDictionary.Count.Equals(0),
                    "There were some Entity Names in Entity table where the number of their actual entries" +
                    " do not match the expected one. " +
                    "Those Entity Names are:\n" + PrintNotMatchingEntityNameEntriesDictionary(notMachingEntityNameEntriesDictionary));
            }
        }

        [Then(@"the number of expected ConnectionTypeId entries is correct in '(.*)' table")]
        public void ThenTheNumberOfExpectedConnectionTypeIdEntriesIsCorrectInTable(string p0)
        {
            if (!_processedExhibit)
            {
                ScenarioContext.Current.Pending();
            }

            var expectedConnectionEventDataFilePath = Path.GetFullPath(_settings["ExpectedConnectionEventData"].Value);
            Assert.True(File.Exists(expectedConnectionEventDataFilePath));

            var csvReader = new CsvFileReader(expectedConnectionEventDataFilePath);
            var totalNumberOfRowsInFile = csvReader.GetNumberOfDataRowsOnWorksheet(0);

            var expectedConnectionTypeIdEntriesDictionary = new Dictionary<int?, int?>();
            var notMachingConnectionTypeIdEntriesDictionary = new Dictionary<int?, List<int?>>();

            for (var i = 0; i < totalNumberOfRowsInFile; i++)
            {
                int? connectionTypeId = csvReader.GetIntValue(i, 0);
                int? numberOfEntries = csvReader.GetIntValue(i, 1);
                expectedConnectionTypeIdEntriesDictionary.Add(connectionTypeId, numberOfEntries);
            }
            foreach (var connectionEvent in expectedConnectionTypeIdEntriesDictionary)
            {
                int? connectionTypeId = connectionEvent.Key;
                int? expectedNumberOfRows = connectionEvent.Value;
                int? actualNumberOfRows = _connectionMetadataTable.CountRowsWithConnectionTypeId(_exhibitId,
                        connectionTypeId);

                if (!actualNumberOfRows.Equals(expectedNumberOfRows))
                {
                    notMachingConnectionTypeIdEntriesDictionary.Add(connectionTypeId,
                        new List<int?> { expectedNumberOfRows, actualNumberOfRows });
                }
            }

            Assert.True(notMachingConnectionTypeIdEntriesDictionary.Count.Equals(0),
                "There were some rows in ConnectionEvent table where the actual number of certain ConnectionTypeId entries is not equal tot he expected one." +
                "Those ConnectionTypeIds are:\n" + PrintNotMatchingConnectionTypeIdEntriesDictionary(notMachingConnectionTypeIdEntriesDictionary));

        }


      /*  [AfterFeature, Scope(Feature = "Ufed Logical Processing ExhibitDb Tables Check")]
        private static void DeleteTestDataFromDb()
        {
            if (!FeatureContext.Current.ContainsKey("AfterFeatureExecuted"))
            {
                _distributedDatabase.ResetDistributedDatabase();
                _exhibitDatabase.ResetExhibitDatabase(_exhibitId);
                _exhibitDatabase.DropExhibitDatabase(_exhibitId);
                Exhibit.DeleteExhibit(_caseId, _exhibitId);
                Case.DeleteCase(_caseId);
                FeatureContext.Current.Set(true, "AfterFeatureExecuted");
            }
        }*/
    }
}
