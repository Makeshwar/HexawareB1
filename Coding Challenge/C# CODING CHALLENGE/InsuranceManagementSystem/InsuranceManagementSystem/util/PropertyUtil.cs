using System;
using System.Data.SqlClient;
using System.IO;

namespace InsuranceManagementSystem.util
{
	public class PropertyUtil
	{
        public static string GetPropertyString(string filePath)
        {
            {
                var lines = File.ReadAllLines(filePath);
                var connectionString = string.Join(";", lines);
                return connectionString;
            }
        }
    }
}

