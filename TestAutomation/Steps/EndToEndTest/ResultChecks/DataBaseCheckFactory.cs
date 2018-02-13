using System;

namespace TestAutomation.Steps.EndToEndTest.ResultChecks
{
    public class DataBaseCheckFactory
    {
        public static IDataBaseCheck GetCheck(string tableName, string connectionString)
        {
            IDataBaseCheck dbCheck;

            switch (tableName)
            {
                case "Entity":
                    dbCheck = new EntityCheck(connectionString);
                    break;
                case "ConnectionEvent":
                    dbCheck = new ConnectionMetadataCheck(connectionString);
                    break;
                case "FileMetadata":
                    dbCheck = new FileMetaDataCheck(connectionString);
                    break;
                case "ImageMetadata":
                    dbCheck = new ImageMetaDataCheck(connectionString);
                    break;
                case "PdfMetadata":
                    dbCheck = new PdfMetaDataCheck(connectionString);
                    break;
                case "VideoMetadata":
                    dbCheck = new VideoMetaDataCheck(connectionString);
                    break;
                case "InternetMetadata":
                    dbCheck = new InternetMetaDataCheck(connectionString);
                    break;
                case "PartitionMetadata":
                    dbCheck = new PartitionMetaDataCheck(connectionString);
                    break;
                default:
                    throw new Exception("'" + tableName + "' table name you supplied is an unexpected value.");
            }
            return dbCheck;
        }
    }
}
