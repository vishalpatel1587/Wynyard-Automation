using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class InternetMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public InternetMetaDataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInInternetMetadataTable = new InternetMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("INTERNET FILES:" + actualNumberOfRowsInInternetMetadataTable + ". Exhibit id is " + exhibitId);

            var expectedNumberOfRowsInInternetMetadataTable = expectedValue;
            Assert.True(actualNumberOfRowsInInternetMetadataTable.Equals(expectedNumberOfRowsInInternetMetadataTable),
                "The number of rows in InternetMetadata table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInInternetMetadataTable + ", but was: " + actualNumberOfRowsInInternetMetadataTable);
        }
    }
}
