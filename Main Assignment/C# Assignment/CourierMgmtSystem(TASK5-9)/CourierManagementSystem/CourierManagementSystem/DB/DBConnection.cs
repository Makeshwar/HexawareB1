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
            var lines = File.ReadAllLines(relativePath);
            var connectionString = string.Join(";", lines);
            return new SqlConnection(connectionString);
        }
    }
}