using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using EVE.Common;
using TestAutomation.Drivers.DBDriver.Infrastructure;

namespace TestAutomation.Drivers.DBDriver.Tables
{
    public class WorkTable : Table
    {
        private const double MaxNumberOfMinutesToWaitForWorkStatusIdPopulated = 2;
        private Dictionary<WorkStatus, int> WorkStatusMap { get; set; }
        private Dictionary<WorkHandlerType, string> WorkHandlerTypeMap { get; set; }

        //public enum WorkStatus
        //{
        //    Created,
        //    Processing,
        //    Completed,
        //    Errored
        //}

        public enum WorkHandlerType
        {
            ProcessMedia,
            FinaliseMedia,
            WaitOnAll,
            WaitOnWorkItems,
            FinaliseWork
        }

        public WorkTable(string connectionString)
            : base(connectionString)
        {
            WorkStatusMap = new Dictionary<WorkStatus, int>();
            WorkHandlerTypeMap = new Dictionary<WorkHandlerType, string>();
            InitialiseWorkStatusMap();
            InitialiseWorkHandlerTypeMap();
        }

        public int GetWorkStatusIdCodeFromMap(WorkStatus status)
        {
            int statusId;
            WorkStatusMap.TryGetValue(status, out statusId);
            return statusId;
        }

        public int GetCurrentWorkStatusIdForHandlerType(WorkHandlerType handlerType)
        {
            int actualStatusValue;
            var errorMessage = string.Empty;
            var sw = new Stopwatch();
            sw.Start();

            string handlerValue;
            WorkHandlerTypeMap.TryGetValue(handlerType, out handlerValue);

            var query = "SELECT WorkStatusId FROM Work where HandlerType LIKE '%" + handlerValue + "%'";

            while (sw.Elapsed < TimeSpan.FromMinutes(MaxNumberOfMinutesToWaitForWorkStatusIdPopulated))
            {
                try
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        actualStatusValue = conn.SelectColumn<int>(query, "WorkStatusId").First();
                    }
                    sw.Stop();
                    return actualStatusValue;
                }
                catch (Exception e)
                {
                    errorMessage = e.ToString();
                }
            }
            sw.Stop();
            Console.Error.WriteLine("GetCurrentWorkStatusIdForProcessMediaHandlerType failed : {0}", errorMessage);
            throw new Exception(errorMessage.ToString());
        }

        public DateTime GetCreatedDateForHandlerType(WorkHandlerType type)
        {
            string handlerValue;
            WorkHandlerTypeMap.TryGetValue(type, out handlerValue);
            var query = "SELECT CreatedDate FROM Work WHERE HandlerType LIKE '%" + handlerValue + "%'";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<DateTime>(query, "CreatedDate").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetCreatedDateForHandlerType failed: {0}", e.Message);
                throw;
            }
        }

        public DateTime GetUpdatedDateForHandlerType(WorkHandlerType type)
        {
            string handlerValue;
            WorkHandlerTypeMap.TryGetValue(type, out handlerValue);
            var query = "SELECT UpdatedDate FROM Work WHERE HandlerType LIKE '%" + handlerValue + "%'";

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    return conn.SelectColumn<DateTime>(query, "UpdatedDate").First();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("GetUpdatedDateForHandlerType failed: {0}", e.Message);
                throw;
            }
        }

        private void InitialiseWorkStatusMap()
        {
            WorkStatusMap.Add(WorkStatus.Created, 1);
            WorkStatusMap.Add(WorkStatus.Processing, 2);
            WorkStatusMap.Add(WorkStatus.Completed, 3);
            WorkStatusMap.Add(WorkStatus.Errored, 4);
        }

        private void InitialiseWorkHandlerTypeMap()
        {
            WorkHandlerTypeMap.Add(WorkHandlerType.FinaliseMedia, "FinaliseMedia");
            WorkHandlerTypeMap.Add(WorkHandlerType.ProcessMedia, "ProcessMedia");
            WorkHandlerTypeMap.Add(WorkHandlerType.FinaliseWork, "FinaliseWork");
            WorkHandlerTypeMap.Add(WorkHandlerType.WaitOnAll, "WaitOnAll");
            WorkHandlerTypeMap.Add(WorkHandlerType.WaitOnWorkItems, "WaitOnWorkItems");
        }
    }
}
