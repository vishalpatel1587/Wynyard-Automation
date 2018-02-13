using System;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class InternetMetadataTable : Table
    {
        public InternetMetadataTable(string connectionString) : base(connectionString) { }
        public int GetNumberOfRows(int exhibitId)
        {
            const string query = "SELECT count (*) as Count FROM InternetMetadata";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from InternetMetadata failed [exhibit id {0}]: {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
