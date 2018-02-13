using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EVE.Common;
using EVE.Site.BLL;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class PartitionMetadataTable : Table
    {
        public PartitionMetadataTable(string connectionString) : base(connectionString) { }

        public int InsertPartitionMetadata(int exhibitId, int mediaId)
        {
           return PartitionMetadata.InsertPartitionMetadata(exhibitId, 1, mediaId, "", "IntegrationTest", FileSystemType.FAT);
        }

        public int GetPartitionMetadataId(int exhibitId)
        {
            var partitionMetaData = PartitionMetadata.GetPartitionMetadata_All(exhibitId);

            if (partitionMetaData == null || partitionMetaData.Count == 0)
                return 0;

            return partitionMetaData.First().PartitionMetadataId;
        }

        public int DeletePartionMetaData(int partitionMetaDataId)
        {
            const string query = @"DELETE FROM PartitionMetaData WHERE PartitionMetaDataId = @PartitionMetaDataId ";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@PartitionMetaDataId ", partitionMetaDataId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeletePartionMetaData failed [partitionMetaDataId = {0}] : {1}", partitionMetaDataId, e.Message);
                throw;
            }
        }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = @"SELECT Count(*) As Count FROM PartitionMetadata";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from PartitionMetadata failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
