using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class MediaTable : Table
    {
        public enum MediaStatus
        {
            Defined,
            Queued,
            ValidationError,
            Processing,
            Reprocessing,
            Processed,
            ProcessingError
        }

        private Dictionary<MediaStatus, int> MediaStatusMap { get; set; }

        public MediaTable(string connectionString)
            : base(connectionString)
        {
            MediaStatusMap = new Dictionary<MediaStatus, int>();
            InitialiseMediaStatusMap();
        }

        public int InsertMedia(MediaDataModel mediaDataModel)
        {
            const string insertMediaQuery = @"DECLARE @MediaId int;
                                    EXEC spInsertMedia @MediaId output, @MediaNumber, @Description, @EvidencePath, 1, 1;
                                    select @MediaId as MediaId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@MediaNumber", mediaDataModel.MediaNumber),
                new SqlParameter("@Description", mediaDataModel.Description),
                new SqlParameter("@EvidencePath", mediaDataModel.EvidencePath )
            };

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(insertMediaQuery, "MediaId", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("InsertMedia failed [media number = {0}] : {1}", mediaDataModel.MediaNumber, e.Message);
                throw;
            }
        }

        public int GetMediaIdByMediaNumber(string mediaNumber)
        {
            const string query = "select MediaId from Media where MediaNumber=@MediaNumber";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "MediaId", new List<SqlParameter> { new SqlParameter("@MediaNumber", mediaNumber) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetMediaIdByMediaNumber failed [media number = {0}] : {1}", mediaNumber, e.Message);
                throw;
            }
        }

        public int GetCurrentMediaStatusId(MediaStatus mediaStatus, int mediaId)
        {
            const string query = "select MediaStatusId from Media where MediaId=@MediaId";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "MediaStatusId", new List<SqlParameter> { new SqlParameter("@MediaId", mediaId) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetCurrentMediaStatusId failed [media id = {0}] : {1}", mediaId, e.Message);
                throw;
            }
        }

        public bool IsMediaFoundByMediaNumber(string mediaNumber)
        {
            const string query = "select count (*) as MediaCount from Media where MediaNumber=@MediaNumber";
            int result;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    result = conn.SelectColumn<int>(query, "MediaCount", new List<SqlParameter> { new SqlParameter("@MediaNumber", mediaNumber) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("IsMediaFoundByMediaNumber failed [media number = {0}] : {1}", mediaNumber, e.Message);
                throw;
            }
            return result != 0;
        }

        public int GetMediaStatusIdCodeFromMap(MediaStatus status)
        {
            int statusId;
            MediaStatusMap.TryGetValue(status, out statusId);
            return statusId;
        }

        public int DeleteMedia(int mediaId)
        {
            const string deleteMediaQuery = @"DECLARE @mountPointId int;
                                    SET @mountPointId=(select MountPointId from MountPoint where MediaId=@MediaId);
                                    EXEC spDeleteMountPoint @mountPointId;
                                    EXEC spDeleteMedia @MediaId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(deleteMediaQuery, new List<SqlParameter> { new SqlParameter("@MediaId", mediaId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteMedia failed [media id = {0}] : {1}", mediaId, e.Message);
                throw;
            }
        }

        private void InitialiseMediaStatusMap()
        {
            MediaStatusMap.Add(MediaStatus.Defined, 1);
            MediaStatusMap.Add(MediaStatus.Queued, 2);
            MediaStatusMap.Add(MediaStatus.ValidationError, 3);
            MediaStatusMap.Add(MediaStatus.Processing, 4);
            MediaStatusMap.Add(MediaStatus.Reprocessing, 5);
            MediaStatusMap.Add(MediaStatus.Processed, 6);
            MediaStatusMap.Add(MediaStatus.ProcessingError, 7);
        }

        public int GetFirstActiveMediaId()
        {
            const string query = "select top 1 MediaId from Media";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "MediaId").FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetFirstActiveMediaId failed : {0}", e.Message);
                throw;
            }
        }
    }
}
