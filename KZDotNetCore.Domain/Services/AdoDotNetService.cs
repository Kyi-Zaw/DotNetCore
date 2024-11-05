using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KZDotNetCore.Domain.Services
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query,params SqlParameterModel[] parameter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query,connection);
            if (parameter is not null)
            {
                foreach (var sqlparameter in parameter)
                {
                    command.Parameters.AddWithValue(sqlparameter.Name, sqlparameter.Value);
                }
            }
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public int Execute(string query, params SqlParameterModel[] parameter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameter is not null)
            {
                foreach (var sqlparameter in parameter)
                {
                    command.Parameters.AddWithValue(sqlparameter.Name, sqlparameter.Value);
                }
            }

            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public SqlParameterModel()
        {
            
        }
        public SqlParameterModel(string name,string value)
        {
            Name = name;
            Value = value;
        }
    }
}
