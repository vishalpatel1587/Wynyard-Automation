using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class ExhibitTable : Table
    {
        public ExhibitTable(string connectionString) : base(connectionString) { }

        public bool IsExhibitFoundByExhibitNumber(string exhibitNumber)
        {
            var parameters = new List<SqlParameter> { new SqlParameter("@ExhibitNumber", exhibitNumber) };
            const string query = @"SELECT count (*) as ExhibitCount from Exhibit where ExhibitNumber=@ExhibitNumber";

            int result;

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    result = conn.SelectColumn<int>(query, "ExhibitCount", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("IsExhibitFoundByExhibitNumber failed [ExhibitNumber = {0}] : {1}", exhibitNumber, e.Message);
                throw;
            }
            return result != 0;
        }

        public int GetExhibitIdByExhibitNumber(string exhibitNumber)
        {
            const string query = "select ExhibitId from Exhibit where ExhibitNumber=@ExhibitNumber";
            var parameters = new List<SqlParameter> { new SqlParameter("@ExhibitNumber", exhibitNumber) };

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "ExhibitId", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetExhibitIdByExhibitNumber from Exhibit table failed : {0}", e.Message);
                throw;
            }
        }

        public int GetFirstActiveExhibitId()
        {
            const string query = @"SELECT top 1 ExhibitID from Exhibit WHERE IsDeleted = 0";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "ExhibitID").FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetFirstExhibitId failed [ExhibitNumber = {0}] : {1}", e.Message);
                throw;
            }
        }

        public int DeleteExhibit(int exhibitId)
        {
            const string query = "EXEC [spDeleteExhibit] @ExhibitId";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@ExhibitId", exhibitId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteExhibit from Exhibit table failed : {0}", e.Message);
                throw;
            }
        }
    }
}
