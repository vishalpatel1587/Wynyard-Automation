using System;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class FileMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public FileMetaDataCheck(string exhibitConnectionString) : base(exhibitConnectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var fileMetadataTable = new FileMetadataTable(ConnectionString);

            var actualNumberOfRowsInFileMetadataTable = fileMetadataTable.GetNumberOfRows(exhibitId);
            Console.WriteLine("NUMBER OF ROWS IN FILE METAFDATA : " + actualNumberOfRowsInFileMetadataTable);

            var expectedNumberOfRowsInFileMetadataTable = expectedValue;
            NUnit.Framework.Assert.True(actualNumberOfRowsInFileMetadataTable.Equals(expectedNumberOfRowsInFileMetadataTable),
                "The number of rows in FileMetadata table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInFileMetadataTable + ", but was: " + actualNumberOfRowsInFileMetadataTable + ". Exhibit id is " + exhibitId);
        }
    }
}
