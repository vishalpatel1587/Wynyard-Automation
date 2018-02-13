using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class PartitionMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public PartitionMetaDataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInPartitionMetadataTable = new PartitionMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("PARTITION ROWS NUMBER:" + actualNumberOfRowsInPartitionMetadataTable + ". Exhibit id is " + exhibitId);

            Assert.True(actualNumberOfRowsInPartitionMetadataTable.Equals(expectedValue),
                "The number of rows in PartitionMetadata table is not equal to the expected number. Expected: " +
                expectedValue + ", but was: " + actualNumberOfRowsInPartitionMetadataTable);
        }
    }
}
