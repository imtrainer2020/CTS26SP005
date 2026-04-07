using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbLayer.Common
{
    internal class DBOps
    {
        static readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SP005Project;Trusted_Connection=True;TrustServerCertificate=True";

        [Obsolete]
        public static DataTable GetDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        [Obsolete]
        public static int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        [Obsolete]
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            // Use your existing connection logic here
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // This loop attaches the parameters to the command
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    return command.ExecuteNonQuery();
                }
            }
        }

        [Obsolete]
        public static object ExecuteScalar(string query)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    result = command.ExecuteScalar();
                }
            }
            return result;
        }

        [Obsolete]
        public static DataSet GetDataSet(string query)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }

        [Obsolete]
        public static SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        [Obsolete]
        public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> data = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T item = new T();
                    foreach (DataColumn column in dt.Columns)
                    {
                        var property = typeof(T).GetProperty(column.ColumnName);
                        if (property != null && row[column] != DBNull.Value)
                        {
                            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            var safeValue = Convert.ChangeType(row[column], targetType);
                            property.SetValue(item, safeValue);
                        }
                    }
                    data.Add(item);
                }
            }
            return data;
        }

        [Obsolete]
        public static T ConvertReaderToObject<T>(SqlDataReader reader) where T : new()
        {
            T obj = new T();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var property = typeof(T).GetProperty(reader.GetName(i));
                if (property != null && !reader.IsDBNull(i))
                {
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var safeValue = Convert.ChangeType(reader.GetValue(i), targetType);
                    property.SetValue(obj, safeValue);
                }
            }
            return obj;
        }

        [Obsolete]
        public static T ConvertDataRowToObject<T>(DataRow row) where T : new()
        {
            T obj = new T();
            foreach (DataColumn column in row.Table.Columns)
            {
                var property = typeof(T).GetProperty(column.ColumnName);
                if (property != null && row[column] != DBNull.Value)
                {
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var safeValue = Convert.ChangeType(row[column], targetType);
                    property.SetValue(obj, safeValue);
                }
            }
            return obj;
        }

    }
}