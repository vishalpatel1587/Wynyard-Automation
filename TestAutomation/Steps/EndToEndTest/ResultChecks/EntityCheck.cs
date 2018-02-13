using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    class EntityCheck : DataCheckBase, IDataBaseCheck
    {
        public EntityCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInEntityTable = new EntityTable(ConnectionString).GetNumberOfRows(exhibitId);

            var expectedNumberOfRowsInEntityTable = expectedValue;
            Assert.True(actualNumberOfRowsInEntityTable.Equals(expectedNumberOfRowsInEntityTable),
                "The number of rows in Entity table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInEntityTable + ", but was: " + actualNumberOfRowsInEntityTable + ". Exhibit id is " + exhibitId);
        }

        public void AssertDataIntegrity(int exhibitId)
        {
        }
    }
}
