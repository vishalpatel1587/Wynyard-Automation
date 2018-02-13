using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EVE.Common;
using EVE.Data;
using EVE.Site.BLL;
using TestAutomation.Drivers.DBDriver.Infrastructure;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class FileMetadataTable : Table
    {
        public FileMetadataTable(string connectionString) : base(connectionString) { }

        public int DeleteFileMetaData(int exhibitId, int fileId)
        {
            const string query = @"DELETE FROM FileMetaData WHERE FileId = @FileId";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@FileId", fileId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteFileMetaData failed [exhibitId = {0}, fileId = {1}] : {2}", exhibitId, fileId, e.Message);
                throw;
            }
        }

        public int InsertFileMetaData(int exhibitId, string fileName, string fileExtension, string fileContent, int partitionMetaDataId, string path = "")
        {

            var fileId = FileMetadata.InsertFileMetadata(
                exhibitId,
                partitionMetaDataId,
                fileName,
                path,
                "",
                fileContent.Length,
                fileExtension,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                "",
                null,
                false,
                null,
                AllocationType.Allocated,
                LinkType.Normal,
                null,
                "",
                true,
                null);

            return fileId;
        }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = @"SELECT count (*) as FileCount from FileMetadata";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "FileCount").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from FileMetaData failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }

        public bool WasFileProcessed(int exhibitId, int partitionMetadata, FileMetadataModel details)
        {
            var fileMetadataRow = FileMetadata.GetFileMetadata_FilePath(exhibitId, partitionMetadata, details.FilePath);
            if (fileMetadataRow == null)
            {
                return false;
            }

            return details.Equals(fileMetadataRow);
        }
        
        //TODO OV review and refactor(maybe combine it with the method above?)
        //public int GetNumberOfRowsThatMatchDetails(int exhibitId, FileMetadataModel details)
        //{

        //    var builder = new StringBuilder();
        //    builder.Append("SELECT count (*) as FileCount from FileMetadata where FileName = @FileName ");
        //    builder.Append("and FilePath=@FilePath ");

        //    builder.Append(details["CreatedDate"].Equals("NULL")
        //        ? "and CreatedDate is NULL "
        //        : "and CreatedDate=@CreatedDate ");

        //    builder.Append(details["LastAccessedDate"].Equals("NULL")
        //      ? "and LastAccessedDate is NULL "
        //      : "and LastAccessedDate=@LastAccessedDate ");

        //    builder.Append(details["LastModifiedDate"].Equals("NULL")
        //     ? "and LastModifiedDate is NULL "
        //     : "and LastModifiedDate=@LastModifiedDate ");

        //    builder.Append(details["FileExtension"].Equals("NULL")
        //     ? "and FileExtension is NULL "
        //     : "and FileExtension=@FileExtension ");

        //    builder.Append(details["FileTypeId"].Equals("NULL")
        //   ? "and FileTypeId is NULL "
        //   : "and FileTypeId=@FileTypeId ");

        //    builder.Append("and FileSize=@FileSize");

        //    var query = builder.ToString();

        //    var parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@FileName", details["FileName"]),
        //        new SqlParameter("@FilePath", details["FilePath"]),
        //        new SqlParameter("@CreatedDate", details["CreatedDate"]),
        //        new SqlParameter("@LastAccessedDate", details["LastAccessedDate"]),  
        //        new SqlParameter("@LastModifiedDate", details["LastModifiedDate"]),
        //        new SqlParameter("@FileTypeId", details["FileTypeId"]),
        //        new SqlParameter("@FileExtension", details["FileExtension"]),
        //        new SqlParameter("@FileSize", details["FileSize"])
        //    };

        //    try
        //    {
        //        using (var conn = new SqlConnection(ConnectionString))
        //        {
        //            return conn.SelectColumn<int>(query, "FileCount", parameters).First();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine("GetNumberOfRows with details from FileMetaData failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
        //        throw;
        //    }
        //}



        public List<string> GetFileNamesFor(string databaseName, int exhibitId)
        {
            var query = string.Format("SELECT [FileName] from {0} t1 INNER JOIN FileMetadata AS fmd ON t1.FileId = fmd.FileId", databaseName);
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<string>(query, "FileName").ToList();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetFileNamesFor from {0} failed [exhibitId = {1}] : {2}", databaseName, exhibitId, e.Message);
                throw;
            }
        }
    }
}
