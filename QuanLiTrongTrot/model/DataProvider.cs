using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QuanLiTrongTrot.Model
{
    public class DataProvider
    {
        
        // Thay connection string theo máy bạn
        public static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=KTPM;Integrated Security=True";
        public static DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    string[] listParam = query.Split(' ');
                    int i = 0;
                    foreach (string item in listParam)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }

            return data;
        }
        public static int ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        // Method để thực thi INSERT, TRUY VẤN , DELETE
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }
        public static string data_list(string[] list)
        {
            string data = string.Join(", ", list);
            return data;
        }
        public static int INSERT_DATA(string[] values, string[] data_names, string table)
        {
            if (values.Length != data_names.Length)
                throw new ArgumentException("Số lượng giá trị và tên cột phải bằng nhau!");

            string columns = data_list(data_names);
            string parameters = string.Join(", ", data_names.Select(n => "@" + n));
            string query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";
            var sqlParams = data_names.Select((n, i) => new SqlParameter($"@{n}", values[i])).ToArray();
            return ExecuteNonQuery(query, sqlParams);
        }
        public static int DELETE_DATA(string name, string data_name, string table)
        {
            int a = ExecuteNonQuery($"DELETE FROM {table} WHERE {data_name} = @{data_name}", new SqlParameter($"@{data_name}", name));
            return a;
        }
        public static bool CHECK_SAME_ROW_DATA(string[] values, string[] data_names, string table)
        {
            string conditions = string.Join(" AND ", data_names.Select(n => $"{n} = @{n}"));
            string query = $"SELECT COUNT(*) FROM {table} WHERE {conditions}";
            var sqlParams = data_names.Select((n, i) => new SqlParameter($"@{n}", values[i])).ToArray();
            int count = ExecuteScalar(query, sqlParams);
            return count > 0;
        }
        public static bool CHECK_DATA_EXISTS(string name, string data_name, string table)
        {
            int count = ExecuteScalar($"SELECT COUNT(*) FROM {table} WHERE {data_name} = @{data_name}", new SqlParameter($"@{data_name}", name));
            return count > 0;
        }
        public static int CHANGE_DATA(string[] values, string[] data_names, string table)
        {
            int b = DELETE_DATA(values[0], data_names[0], table);
            int a = INSERT_DATA(values, data_names, table);
            return a;
        }
    }
}