using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class KnownFileTable : Table
    {
        public KnownFileTable(string connectionString) : base(connectionString) { }

        public int InsertKnownFile(string fileName, string fileHash, int knownFileCategoryId)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand())
                {
                    conn.Open();

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"DECLARE @id as int; SELECT @id = Max(KnownFileID)+1 FROM KnownFile; Set @id = ISNull(@id, 1); INSERT INTO KnownFile (KnownFileId, FileName, FileHash, KnownFileCategoryId) 
                                        VALUES (@id, @FileName, @FileHash, @KnownFileCategoryId); SELECT TOP 1 KnownFileID FROM KnownFile ORDER BY KnownFileID DESC;";

                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FileHash", fileHash);
                    cmd.Parameters.AddWithValue("@KnownFileCategoryId", knownFileCategoryId);
                    var res = cmd.ExecuteScalar();
                    int result;
                    int.TryParse(res.ToString(), out result);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("InsertKnownFile failed {0}", e.Message);
                throw;
            }
        }

        public int DeleteKnownFile(int knownFileId)
        {
            const string query = @"DELETE FROM KnownFile WHERE KnownFileID = @KnownFileID";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@KnownFileID", knownFileId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteKnownFile failed [knownFileId = {0}] : {1}", knownFileId, e.Message);
                throw;
            }
        }
    }
}
