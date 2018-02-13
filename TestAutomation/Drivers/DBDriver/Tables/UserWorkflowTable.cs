using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class UserWorkflowTable : Table
    {
        public UserWorkflowTable(string connectionString) : base(connectionString) { }

        public bool IsWorkflowFoundByCustomName(string workflowCustomName)
        {
            const string query = "SELECT Count(*) as Count FROM UserWorkflow where CustomName=@WorkflowCustomName";
            int result;
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    result = conn.SelectColumn<int>(query, "Count", new List<SqlParameter> { new SqlParameter("@WorkflowCustomName", workflowCustomName) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("IsWorkflowFoundByCustomName failed [workflow name = {0}] : {1}", workflowCustomName, e.Message);
                throw;
            }

            return result != 0;
        }

        public int DeleteUserWorkflow(int workflowId)
        {
            const string query = "DELETE from UserWorkflow where UserWorkflowId=@WorkflowId";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.Delete(query, new List<SqlParameter> { new SqlParameter("@WorkflowId", workflowId) });
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("DeleteUserWorkflow failed [workflow id = {0}] : {1}", workflowId, e.Message);
                throw;
            }
        }

        public int GetWorkflowIdByName(string workflowName)
        {
            const string query = "SELECT UserWorkflowId FROM UserWorkflow WHERE CustomName=@WorkflowName";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<int>(query, "UserWorkflowId",
                        new List<SqlParameter> { new SqlParameter("@WorkflowName", workflowName) }).First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetWorkflowIdByName failed [workflow name = {0}] : {1}", workflowName, e.Message);
                throw;
            }
        }
    }
}