using InsuranceManagementSystem.dao;
using InsuranceManagementSystem.entity;
using System.Data.SqlClient;
using InsuranceManagementSystem.util;

public class InsuranceServiceImpl : IPolicyService
{
    private readonly string configPath = "config/db.properties";

    public bool CreatePolicy(Policy policy)
    {
        using var connection = DBConnection.GetConnection(configPath);

        var query = "INSERT INTO Policy (PolicyNumber, PolicyType, StartDate, EndDate) " +
                    "VALUES (@PolicyNumber, @PolicyType, @StartDate, @EndDate)";

        using var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);
        cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType);
        cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);

        return cmd.ExecuteNonQuery() > 0;
    }

    public Policy GetPolicy(int policyId)
    {
        using var connection = DBConnection.GetConnection(configPath);

        var query = "SELECT * FROM Policy WHERE PolicyId = @PolicyId";
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@PolicyId", policyId);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Policy
            {
                PolicyId = Convert.ToInt32(reader["PolicyId"]),
                PolicyNumber = reader["PolicyNumber"].ToString(),
                PolicyType = reader["PolicyType"].ToString(),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"])
            };
        }

        return null;
    }

    public List<Policy> GetAllPolicies()
    {
        var policies = new List<Policy>();
        using var connection = DBConnection.GetConnection(configPath);
        var query = "SELECT * FROM Policy";

        using var cmd = new SqlCommand(query, connection);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            policies.Add(new Policy
            {
                PolicyId = Convert.ToInt32(reader["PolicyId"]),
                PolicyNumber = reader["PolicyNumber"].ToString(),
                PolicyType = reader["PolicyType"].ToString(),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"])
            });
        }

        return policies;
    }

    public bool UpdatePolicy(Policy policy)
    {
        using var connection = DBConnection.GetConnection(configPath);
        var query = "UPDATE Policy SET PolicyNumber = @PolicyNumber, PolicyType = @PolicyType, " +
                    "StartDate = @StartDate, EndDate = @EndDate WHERE PolicyId = @PolicyId";

        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@PolicyNumber", policy.PolicyNumber);
        cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType);
        cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
        cmd.Parameters.AddWithValue("@PolicyId", policy.PolicyId);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool DeletePolicy(int policyId)
    {
        using var connection = DBConnection.GetConnection(configPath);
        var query = "DELETE FROM Policy WHERE PolicyId = @PolicyId";
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@PolicyId", policyId);

        return cmd.ExecuteNonQuery() > 0;
    }
}