using System;
using System.Data.SqlClient;
using System.IO;
namespace DigitalAssetManagement.util
{
    public class DBConnection
    {
        private static SqlConnection connection = null;

        public static SqlConnection GetConnection(string filePath)
        {
            if (connection == null)
            {
                try
                {
                    var lines = File.ReadAllLines(filePath);

                    var connectionString = string.Join(";", lines);

                    connection = new SqlConnection(connectionString);

                    connection.Open();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                }
            }


            return connection;
        }
    }
}