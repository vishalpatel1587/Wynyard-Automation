using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{

    public class SiteTable : Table
    {
        public SiteTable(string connectionString) : base(connectionString) { }

        public int GetSiteId()
        {
            const string query = @"SELECT SiteId FROM [Site]";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<Int32>(query, "SiteId").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetSiteId from SiteTable failed : {0}", e.Message);
                throw;
            }
        }
    }
}
