using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TestAutomation.Drivers.DBDriver.Infrastructure
{
    public static class QueryHelper
    {
        //this class is quite basic in what is provided, consider moving to a more full featured library.

        public static int Delete(this SqlConnection connection, string query,
            IEnumerable<SqlParameter> parameters = null, int? timeout = null)
        {
            return ExecuteNonQuery(connection, query, parameters, timeout);
        }

        public static int Insert(this SqlConnection connection, string query,
            IEnumerable<SqlParameter> parameters = null, int? timeout = null)
        {
            return ExecuteNonQuery(connection, query, parameters, timeout);
        }

        public static int Update(this SqlConnection connection, string query,
            IEnumerable<SqlParameter> parameters = null, int? timeout = null)
        {
            return ExecuteNonQuery(connection, query, parameters, timeout);
        }

        private static int ExecuteNonQuery(this SqlConnection connection, string query,
            IEnumerable<SqlParameter> parameters = null, int? timeout = null)
        {
            int numChangedRows;
            connection.Open();
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;

                if (parameters != null) cmd.Parameters.AddRange(parameters.ToArray());
                if (timeout.HasValue) cmd.CommandTimeout = timeout.Value;

                numChangedRows = cmd.ExecuteNonQuery();
            }
            connection.Close();
            return numChangedRows;
        }

        public static IEnumerable<T> SelectColumn<T>(this SqlConnection connection, string query,
            string columnName, IEnumerable<SqlParameter> parameters = null, int? timeout = null)
        {
            connection.Open();
            var results = new List<T>();
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;

                if (parameters != null) cmd.Parameters.AddRange(parameters.ToArray());
                if (timeout.HasValue) cmd.CommandTimeout = timeout.Value;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int ord = reader.GetOrdinal(columnName);
                        while (reader.Read())
                        {
                            results.Add(reader.GetFieldValue<T>(ord));
                        }
                    }
                }
            }
            connection.Close();
            return results;
        }
    }
}
