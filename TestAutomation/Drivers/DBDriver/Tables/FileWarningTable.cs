using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class FileWarningTable : Table
    {
        public FileWarningTable(string connectionString) : base(connectionString) { }

        public int DeleteFileWarning(int exhibitId, int fileId)
        {
            const string query = @"DELETE FROM FileWarning WHERE FileId = @FileId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@FileId", fileId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteFileWarning failed [exhibitId = {0}, fileId = {1}] : {2}", exhibitId, fileId, e.Message);
                throw;
            }

        }
    }
}
