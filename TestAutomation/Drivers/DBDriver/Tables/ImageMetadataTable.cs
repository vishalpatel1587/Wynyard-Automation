using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class ImageMetadataTable : Table
    {
        public ImageMetadataTable(string connectionString) : base(connectionString) { }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = "SELECT count (*) as Count FROM ImageMetadata";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from ImageMetadata failed [exhibit id {0}]: {1}", exhibitId, e.Message);
                throw;
            }
        }

        public int DeleteImageMetaData(int exhibitId, int fileId)
        {
            const string query = @"DELETE FROM ImageMetaData WHERE FileId = @FileId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString)) { return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@FileId", fileId) }); }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteImageMetaData failed [exhibitId = {0}, fileId = {1}] : {2}", exhibitId, fileId, e.Message);
                throw;
            }
        }

        public void InsertImageMetaData(int exhibitId, int fileId, int width, int height, byte[] picture, byte[] thumbnail)
        {
            EVE.Site.BLL.ImageMetadata.InsertImageMetadata(exhibitId, fileId, width, height, picture,
                thumbnail, false, null);
        }

        public double GetPornProbability(int exhibitId, int fileId)
        {
            var imageMetadata = EVE.Site.BLL.ImageMetadata.GetImageMetadata(exhibitId, fileId, false, false);
            return imageMetadata.PornProbability;
        }

        public List<int> GetFileIdList(int exhibitId)
        {
            const string query = "SELECT FileId FROM ImageMetadata";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return (List<int>)conn.SelectColumn<int>(query, "FileId");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetImageIdList from ImageMetadata failed [exhibit id {0}]: {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
