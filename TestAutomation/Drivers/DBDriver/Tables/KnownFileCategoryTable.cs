using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestAutomation.Drivers.DBDriver.Infrastructure;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class KnownFileCategoryTable : Table
    {
        public KnownFileCategoryTable(string connectionString) : base(connectionString) { }

        public int InsertKnownFileCategory(KnownFileCategoryModel model)
        {
            //at this stage unable to use helpers as we return the id's... 
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand())
                {
                    conn.Open();
                    
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"DECLARE @id as int; SELECT @id = Max(KnownFileCategoryID)+1 FROM KnownFileCategory; Set @id = ISNull(@id, 1); INSERT INTO KnownFileCategory (KnownFileCategoryId, HashSetId, CategoryName, Vendor, Package, Version, IsAuthenticated, IsNotable, Initials, NumberOfFiles, CategoryDescription, DateLoaded) 
                                        VALUES (@id, @HashSetId, @CategoryName, @Vendor, @Package, @Version, @IsAuthenticated, @IsNotable, @Initials, @NumberOfFiles, @CategoryDescription, GetDate()); SELECT Max(KnownFileCategoryID) FROM KnownFileCategory;";

                    cmd.Parameters.AddWithValue("@HashSetId", model.HashSetId);
                    cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName);
                    cmd.Parameters.AddWithValue("@Vendor", model.Vendor);
                    cmd.Parameters.AddWithValue("@Package", model.Package);
                    cmd.Parameters.AddWithValue("@Version", model.Version);
                    cmd.Parameters.AddWithValue("@IsAuthenticated", "1");
                    cmd.Parameters.AddWithValue("@IsNotable", "0");
                    cmd.Parameters.AddWithValue("@Initials", model.Initials);
                    cmd.Parameters.AddWithValue("@NumberOfFiles",0);
                    cmd.Parameters.AddWithValue("@CategoryDescription", model.CategoryDescription);
                    //cmd.Parameters.AddWithValue("@DateLoaded", model.DateLoaded);
                    var res = cmd.ExecuteScalar();
                    int result = 0;
                    int.TryParse(res.ToString(), out result);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("InsertKnownFileCategory failed {0}", e.Message);
                throw;
            }
        }

        public int DeleteKnownFileCategory(int knownFileCategoryId)
        {
            const string query = @"DELETE FROM KnownFileCategory WHERE KnownFileCategoryId = @KnownFileCategoryId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    //requires an extended timeout since the delete operation is 
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@KnownFileCategoryId", knownFileCategoryId) }, timeout:120);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteKnownFileCategory failed [knownFileCategoryId = {0}] : {1}", knownFileCategoryId, e.Message);
                throw;
            }
        }

        public int GetKnownFileCategoryId(string knownFileCategoryName)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand())
                {
                    conn.Open();

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT KnownFileCategoryId FROM KnownFileCategory WHERE CategoryName = @CategoryName";

                    cmd.Parameters.AddWithValue("@CategoryName", knownFileCategoryName);
                    
                    var res = cmd.ExecuteScalar();
                    int result=0;
                    if (res != null)
                    {
                        int.TryParse(res.ToString(), out result);
                    }
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetKnownFileCategoryId failed {0}", e.Message);
                throw;
            }
        }

    }
}
