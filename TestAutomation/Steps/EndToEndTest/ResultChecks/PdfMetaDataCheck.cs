using System;
using NUnit.Framework;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class PdfMetaDataCheck : DataCheckBase, IDataBaseCheck
    {
        public PdfMetaDataCheck(string connectionString) : base(connectionString) { }

        public void AssertNumberOfRows(int exhibitId, int expectedValue)
        {
            var actualNumberOfRowsInPdfMetadataTable = new PdfMetadataTable(ConnectionString).GetNumberOfRows(exhibitId);
            Console.WriteLine("PDF FILES:" + actualNumberOfRowsInPdfMetadataTable + ". Exhibit id is " + exhibitId);

            var expectedNumberOfRowsInPdfMetadataTable = expectedValue;
            Assert.True(actualNumberOfRowsInPdfMetadataTable.Equals(expectedNumberOfRowsInPdfMetadataTable),
                "The number of rows in PdfMetadata table is not equal to the expected number. Expected: " +
                expectedNumberOfRowsInPdfMetadataTable + ", but was: " + actualNumberOfRowsInPdfMetadataTable);
        }
    }
}
