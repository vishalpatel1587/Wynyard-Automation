using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using EVE.Functions;
using EVE.Site.BLL;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class FileHashTable : Table
    {
        public FileHashTable(string connectionString) : base(connectionString) { }

        public int DeleteFileHash(int exhibitId, int fileId)
        {
            const string query = @"DELETE FROM FileHash WHERE FileId = @FileId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@FileId", fileId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteFileHash failed [exhibitId = {0}, fileId = {1}] : {2}", exhibitId, fileId, e.Message);
                throw;
            }

        }

        public void InsertFileHash(int exhibitId, int fileId, string hashAlgorithm, Stream fileStream)
        {
            var hashValue = Hashing.GetHash(fileStream, hashAlgorithm);
            FileHash.InsertFileHash(exhibitId, fileId, hashAlgorithm, hashValue);
        }
    }
}
