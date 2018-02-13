using System;
using System.Data.SqlClient;
using System.Diagnostics;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class ExhibitDatabase : Table
    {
        public ExhibitDatabase(string connectionString) : base(connectionString) { }

        public void ResetExhibitDatabase(int exhibitId)
        {
            var resetExhibitQuery = @"DELETE FROM VideoMetadata;
                                    DELETE FROM ImageMetadata;
                                    DELETE FROM PdfMetadata;
                                    DELETE FROM FileActivity;
                                    DELETE FROM FileContent;
                                    DELETE FROM FileClassification;
                                    DELETE FROM FileHash;
                                    DELETE FROM FileWarning;
                                    DELETE FROM ConnectionRelationship;
                                    DELETE FROM ConnectionMetadata;
                                    DELETE FROM MountPoint;

                                    
                                    DELETE FROM FileMetadata;
                                    DBCC CHECKIDENT('FileMetadata', RESEED, 0);
                                    DELETE FROM PartitionMetadata;
                                    DBCC CHECKIDENT('PartitionMetadata', RESEED, 0);

                                    DELETE FROM EVE3_Distributed..GroupWork;
                                    DELETE FROM EVE3_Distributed..WorkItem;
                                    DELETE FROM EVE3_Distributed..Work;

                                    DBCC CHECKIDENT('EVE3_Distributed..WorkItem', RESEED, 0);" +
                                    "EXEC sp_filestream_force_garbage_collection @dbname = N'ExhibitDB_" + exhibitId + "';" +

                                    "UPDATE Media SET MediaStatusId = 1";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Delete(resetExhibitQuery);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("ResetExhibit failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }

        public void DropExhibitDatabase(int exhibitId)
        {
            var query = "USE MASTER; ALTER DATABASE ExhibitDB_" + exhibitId +
               " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE ExhibitDB_" + exhibitId;

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Delete(query);
                    Console.Out.WriteLine("successfuly dropped ExhibitDB_" + exhibitId);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DropExhibitDatabase failed [exhibitId = {0}] : {1}", exhibitId, e.Message);
                throw;
            }
        }
    }
}
