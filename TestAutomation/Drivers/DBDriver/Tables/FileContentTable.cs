using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EVE.Site.BLL;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class FileContentTable :Table
    {
        public FileContentTable(string connectionString) : base(connectionString) { }

        public void InsertFileContent(int exhibitId, int fileId, string fileContent)
        {
             FileContent.InsertFileContent(exhibitId, fileId, fileContent, ".txt");
        }


        public int DeleteFileContent(int exhibitId, int fileId)
        {
            const string query = @"DELETE FROM FileContent WHERE FileId = @FileId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString)) {  return conn.Delete(query, new List<SqlParameter>{new SqlParameter("@FileId", fileId)}); }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteFileContent failed [exhibitId = {0}, fileId = {1}] : {2}", exhibitId, fileId, e.Message);
                throw;
            }
        }
    }
}
