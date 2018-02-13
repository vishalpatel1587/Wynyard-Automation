using System;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;
namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class PdfMetadataTable : Table
    {
        public PdfMetadataTable(string connectionString) : base(connectionString) { }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = @"SELECT Count(*) As Count FROM PdfMetaData";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from PdfMetaData failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
