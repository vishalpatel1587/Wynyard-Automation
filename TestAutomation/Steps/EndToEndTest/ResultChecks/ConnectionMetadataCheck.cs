using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    class ConnectionMetadataCheck : DataCheckBase, IDataBaseCheck
    {
        public ConnectionMetadataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInConnectionEventTable = new ConnectionMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("NUMBER OF ROWS IN CONNECTION EVENT TABLE: " + actualNumberOfRowsInConnectionEventTable);

            var expectedNumberOfRowsInConnectionEventTable = expectedValue;
            Assert.True(actualNumberOfRowsInConnectionEventTable.Equals(expectedNumberOfRowsInConnectionEventTable),
                "The number of rows in ConnectionEvent table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInConnectionEventTable + ", but was: " + actualNumberOfRowsInConnectionEventTable + ". Exhibit id is " + exhibitId);
        }
    }
}
