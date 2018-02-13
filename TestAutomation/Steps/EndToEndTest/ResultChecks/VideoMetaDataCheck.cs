using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class VideoMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public VideoMetaDataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRows = new VideoMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("VIDEO FILES:" + actualNumberOfRows);
            Assert.True(actualNumberOfRows.Equals(expectedValue),
                "The number of rows in VideoMetadata table is not equal to the expected number. Expected: " + expectedValue + ", but was: " + actualNumberOfRows + ". Exhibit id is " + exhibitId);
        }
    }
}
