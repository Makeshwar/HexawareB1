using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagementSystem.DB
{
    public class DBConnection
    {
        public static SqlConnection GetConnection(string relativePath)
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException("Could not find db.properties at: " + fullPath);
                }

                var properties = new Dictionary<string, string>();
                foreach (var line in File.ReadAllLines(fullPath))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#")) continue;

                    int separatorIndex = line.IndexOf('=');
                    if (separatorIndex > 0)
                    {
                        string key = line.Substring(0, separatorIndex).Trim();
                        string value = line.Substring(separatorIndex + 1).Trim();
                        properties[key] = value;
                    }
                }

                if (!properties.ContainsKey("connectionString"))
                {
                    throw new InvalidOperationException("connectionString not found in db.properties");
                }

                string connectionString = properties["connectionString"];

                var connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to DB: " + ex.Message);
                return null;
            }
        }
    }
}