using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class ConnectionMetadataTable : Table
    {
        public ConnectionMetadataTable(string connectionString) : base(connectionString) { }

        public int GetNumberOfRows(int exhibitId)
        {
            const string query = @"SELECT Count(*) As Count FROM ConnectionMetadata";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetNumberOfRows from ConnectionMetadata failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }

        public int CountRowsWithConnectionTypeId(int exhibitId, int? connectionTypeId)
        {
            const string query = @"SELECT Count(*) as Count from ConnectionMetadata where ConnectionTypeId=@ConnectionTypeId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "Count", new List<SqlParameter> { new SqlParameter("@ConnectionTypeId", connectionTypeId) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("CountRowsWithConnectionTypeId from ConnectionEvent failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
