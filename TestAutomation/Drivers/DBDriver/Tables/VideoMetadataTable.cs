using System;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class VideoMetadataTable : Table
    {
        public VideoMetadataTable(string connectionString) : base(connectionString) { }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = "SELECT Count(*) as Count from VideoMetadata";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows failed [exhibit id = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
