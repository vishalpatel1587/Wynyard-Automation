using System;
using System.Data.SqlClient;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class DistributedDatabase : Table
    {
        public DistributedDatabase(string connectionString) : base(connectionString) { }

        public void ResetDistributedDatabase()
        {
            const string resetExhibitQuery = @"DELETE FROM GroupWork;
                                    DELETE FROM WorkItem;
                                    DELETE FROM Work;";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Delete(resetExhibitQuery);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("ResetDistributedDatabase failed : {0}", e.Message);
                throw;
            }
        }
    }
}
