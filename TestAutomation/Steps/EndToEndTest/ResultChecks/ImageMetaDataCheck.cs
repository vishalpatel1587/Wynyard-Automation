using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class ImageMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public ImageMetaDataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInImageMetadataTable = new ImageMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("IMAGES: " + actualNumberOfRowsInImageMetadataTable);

            var expectedNumberOfRowsInImageMetadataTable = expectedValue; //Convert.ToInt32(settings["ExpectedNumberOfRowsInImageMetadataTable"].Value);
            Assert.True(actualNumberOfRowsInImageMetadataTable.Equals(expectedNumberOfRowsInImageMetadataTable),
                "The number of rows in ImageMetadata table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInImageMetadataTable + ", but was: " + actualNumberOfRowsInImageMetadataTable + ". Exhibit id is " + exhibitId);
        }

        public void AssertPornProbabilityForAllFilesNotNullAndNotTheErrorCode(int exhibitId, int errorCode)
        {
            var listOfFileIdsWithNullPornProbability = new List<int>();
            var listOfFileIdsWithPornProbabilityEqualToErrorCode = new List<int>();
            var imageMetadataTable = new ImageMetadataTable(ConnectionString);
            var listOfImageFileIds = imageMetadataTable.GetFileIdList(exhibitId);
            Assert.IsNotEmpty(listOfImageFileIds, @"There are no FileId values found in ImageMetadata table.
                    The table might be empty or the FileId column name has changed and the query for it is not valid anymore.");

            foreach (var fileId in listOfImageFileIds)
            {
                var probability = imageMetadataTable.GetPornProbability(exhibitId, fileId);

                if (probability.Equals(null))
                {
                    listOfFileIdsWithNullPornProbability.Add(fileId);
                }
                if (probability.Equals(errorCode))
                {
                    listOfFileIdsWithPornProbabilityEqualToErrorCode.Add(fileId);
                }
            }

            Assert.IsEmpty(listOfFileIdsWithNullPornProbability, "There were some images in the exhibit (id=" + exhibitId + ") with NULL PornProbability that is unexpected." +
                                                                 " Those images FileIds are: " +
                                                                 String.Join(",", listOfFileIdsWithNullPornProbability));
            Assert.IsEmpty(listOfFileIdsWithPornProbabilityEqualToErrorCode, "There were some images in the exhibit (id=" + exhibitId + ") with " + errorCode + " PornProbability that is unexpected." +
                                                                 " Those images FileIds are: " +
                                                                  String.Join(",", listOfFileIdsWithPornProbabilityEqualToErrorCode));
        }
    }
}
