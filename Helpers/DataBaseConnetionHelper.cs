using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace PropertyExpertTask.Helpers
{
    public class DatabaseConnectionHelper
    {
        private readonly string _connectionString;

        // Constructor to initialize connection string
        public DatabaseConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to establish a connection to the database
        public SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open(); // Optional: If you want to immediately open the connection
                return connection;
            }
            catch (SqlException ex)
            {
                // Handle exception if the connection fails
                Console.WriteLine($"Error opening connection: {ex.Message}");
                throw; // Re-throw the exception if needed, or handle it as per your logic
            }
        }
    }
}