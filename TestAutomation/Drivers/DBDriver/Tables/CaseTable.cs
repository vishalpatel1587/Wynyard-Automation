using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;
using TestAutomation.Model.DataModel;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class CaseTable : Table
    {
        public CaseTable(string connectionString) : base(connectionString) { }

        public int InsertCase(CaseDataModel caseData)
        {
            const string insertCaseQuery = @"DECLARE @createdDateTime datetime
                                SET @createdDateTime= GETDATE()

                                DECLARE @CaseId int
                                EXEC [spInsertCase] @CaseId output, @PoliceFileNumber, @EclCaseNumber, @Description, " +
                                @"@DistrictId, @SiteId, @OffenceTypeId, @createdBy
                               
                                INSERT INTO UserCase VALUES (@UserId, @CaseId, 7)
                                select @CaseId as CaseId";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PoliceFileNumber", caseData.PoliceFileNumber),
                new SqlParameter("@EclCaseNumber", caseData.EclCaseNumber),
                new SqlParameter("@Description",caseData.Description),
                new SqlParameter("@DistrictId",caseData.DistrictId),
                new SqlParameter("@SiteId",caseData.SiteId),
                new SqlParameter("@OffenceTypeId",caseData.OffenceTypeId),
                new SqlParameter("@OffenceTypeId",caseData.OffenceTypeId),
                new SqlParameter("@UserId",caseData.CreatedById),
            };

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(insertCaseQuery, "CaseId", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("InsertCase failed [caseNumber = {0}] : {1}", caseData.EclCaseNumber, e.Message);
                throw;
            }
        }

        public bool IsCaseFoundByCaseNumber(string caseNumber)
        {
            const string query = @"SELECT count (*) as CaseCount from [Case] where CaseNumber=@CaseNumber";
            var parameters = new List<SqlParameter> { new SqlParameter("@CaseNumber", caseNumber) };

            int result;

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    result = conn.SelectColumn<int>(query, "CaseCount", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("IsCaseFoundByCaseNumber failed [CaseNumber = {0}] : {1}", caseNumber, e.Message);
                throw;
            }

            return result != 0;
        }

        public int DeleteCase(int caseId)
        {
            const string query = "EXEC [spDeleteCase] @CaseId";
            var parameters = new List<SqlParameter> { new SqlParameter("@CaseId", caseId) };

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, parameters);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteCase failed [CaseId = {0}] : {1}", caseId, e.Message);
                throw;
            }
        }

        public int GetCaseId(string caseNumber)
        {
            const string query = "select CaseId from [Case] where CaseNumber=@CaseNumber";
            var parameters = new List<SqlParameter> { new SqlParameter("@CaseNumber", caseNumber) };

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "CaseId", parameters).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetCaseId from Case table failed : {0}", e.Message);
                throw;
            }
        }
    }
}
