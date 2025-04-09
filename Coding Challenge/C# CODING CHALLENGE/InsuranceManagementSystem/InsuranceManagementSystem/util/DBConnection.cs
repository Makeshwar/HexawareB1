using System;
using System.Data.SqlClient;

namespace InsuranceManagementSystem.util
{
	public class DBConnection
	{
        private static SqlConnection connection;

        public static SqlConnection GetConnection(string configpath)
        {
            string connString = PropertyUtil.GetPropertyString(configpath);
            connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }
	}
}

