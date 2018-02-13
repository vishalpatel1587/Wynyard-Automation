using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class FileCategoryTable : Table
    {
        public FileCategoryTable(string connectionString) : base(connectionString) { }

        public int GetCategoryIdByFileCategoryCode(string code)
        {
            const string query = @"SELECT FileCategoryId FROM FileCategory WHERE FileCategoryCode = @FileCategoryCode";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "FileCategoryId", new List<SqlParameter> { new SqlParameter("@FileCategoryCode", code) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetCategoryIdByFileCategoryCode from FileCategoryTable failed [code = {0}] : {1}", code, e.Message);
                throw;
            }
        }
    }
}
