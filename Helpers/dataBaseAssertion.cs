using System;
using System.Data;
using MySql.Data.MySqlClient;

public class dataBaseAssertion
{
    private readonly string _connectionString;

    public dataBaseAssertion(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<(string Username, string ProductName, decimal TotalPrice, DateTime OrderDate)>
        GetLatestOrderFromDatabase(string username)
    {
        using (var connection = new MySqlConnection(_connectionString)) // Use MySqlConnection here
        {
            await connection.OpenAsync();

            // MySQL uses LIMIT instead of TOP 1
            string query = @"SELECT * FROM orders";

            // First, insert the data
            string insertQuery = @"
                    INSERT INTO Orders (Username, ProductName, TotalPrice, OrderDate)
                    VALUES (@Username, @ProductName, @TotalPrice, @OrderDate)";

            // Create a MySQL command for insertion
            using (var command = new MySqlCommand(insertQuery, connection))
            {
                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Username", "Mustafa");
                command.Parameters.AddWithValue("@ProductName", "productName");
                command.Parameters.AddWithValue("@TotalPrice", 10);
                command.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime("10/10/2010"));

                // Execute the insertion query
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Order inserted successfully.");
            }

            // Now select data from the orders table
            query = @"SELECT * FROM orders";
            using (var command = new MySqlCommand(query, connection))
            {
                // Using MySqlDataReader to fetch data
                using (var reader = command.ExecuteReader()) // Executes the query and gets the reader
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read()) // Read each row
                        {
                            string dbUsername = reader.GetString("Username");
                            string dbProductName = reader.GetString("ProductName");
                            decimal dbTotalPrice = reader.GetDecimal("TotalPrice");
                            DateTime dbOrderDate = reader.GetDateTime("OrderDate");

                            // Return the values as a tuple
                            return (dbUsername, dbProductName, dbTotalPrice, dbOrderDate);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found.");
                    }
                }
            }
        }

        // If no order is found, return default values
        return (null, null, 0, DateTime.MinValue);
    }
}
