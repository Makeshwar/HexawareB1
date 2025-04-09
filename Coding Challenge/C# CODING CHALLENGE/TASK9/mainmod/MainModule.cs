using System;
using System.Data.SqlClient;
using System.Globalization;
using InsuranceManagementSystem.dao;
using InsuranceManagementSystem.util;
using InsuranceManagementSystem.entity;
using InsuranceManagementSystem.myexceptions;

namespace InsuranceManagementSystem.mainmod
{
    class MainModule
    {
        static void Main(string[] args)
        {
            string configPath = "config/db.properties";
            IPolicyService policyService = new InsuranceServiceImpl();

            while (true)
            {
                Console.WriteLine(" Insurance Management System ");
                Console.WriteLine("1. Create Policy");
                Console.WriteLine("2. Get Policy by ID");
                Console.WriteLine("3. Update Policy");
                Console.WriteLine("4. Get All Policies");
                Console.WriteLine("5. Delete Policy");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int choice = Convert.ToInt32(input);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Policy Number: ");
                            string policyNumber = Console.ReadLine();

                            Console.Write("Enter Policy Type: ");
                            string policyType = Console.ReadLine();

                            Console.Write("Enter Start Date (yyyy-MM-dd): ");
                            DateTime startDate = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Enter End Date (yyyy-MM-dd): ");
                            DateTime endDate = Convert.ToDateTime(Console.ReadLine());

                            Policy newPolicy = new Policy(0, policyNumber, policyType, startDate, endDate);
                            bool created = policyService.CreatePolicy(newPolicy);

                            Console.WriteLine("Policy created: " + created);

                            if (created)
                            {
                                using (var conn = DBConnection.GetConnection(configPath))
                                {
                                    var query = "SELECT * FROM Policy WHERE PolicyNumber = @PolicyNumber";
                                    var cmd = new SqlCommand(query, conn);

                                    cmd.Parameters.AddWithValue("@PolicyNumber", policyNumber);

                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {

                                            Console.WriteLine("\nInserted Policy Details:");

                                            Console.WriteLine($"PolicyId: {reader["PolicyId"]}, PolicyNumber: {reader["PolicyNumber"]}, PolicyType: {reader["PolicyType"]}, StartDate: {((DateTime)reader["StartDate"]).ToShortDateString()}, EndDate: {((DateTime)reader["EndDate"]).ToShortDateString()}");
                                        }
                                    }
                                }
                            }
                            break;

                        case 2:
                            Console.Write("Enter Policy ID to search: ");
                            int searchId = int.Parse(Console.ReadLine());
                            Policy foundPolicy = policyService.GetPolicy(searchId);

                            if (foundPolicy != null)
                            {

                                Console.WriteLine("Policy Found:");
                                foundPolicy.PrintInfo();
                            }
                            else
                            {
                                Console.WriteLine("Policy not found.");
                            }
                            break;

                        case 3:
                            Console.Write("Enter Policy ID to update: ");

                            int updateId = int.Parse(Console.ReadLine());
                            Policy updatePolicy = policyService.GetPolicy(updateId);

                            if (updatePolicy != null)
                            {
                                Console.Write("Enter new Policy Type: ");
                                updatePolicy.PolicyType = Console.ReadLine();

                                Console.Write("Enter new Start Date: ");
                                updatePolicy.StartDate = Convert.ToDateTime(Console.ReadLine());

                                Console.Write("Enter new End Date: ");
                                updatePolicy.EndDate = Convert.ToDateTime(Console.ReadLine());

                                bool updated = policyService.UpdatePolicy(updatePolicy);
                                Console.WriteLine("Policy updated: " + updated);

                                if (updated)
                                {

                                    using (var conn = DBConnection.GetConnection(configPath))
                                    {
                                        var query = "SELECT * FROM Policy WHERE PolicyId = @PolicyId";
                                        var cmd = new SqlCommand(query, conn);
                                        cmd.Parameters.AddWithValue("@PolicyId", updateId);

                                        using (var reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                Console.WriteLine("\nUpdated Policy Details:");
                                                Console.WriteLine($"PolicyId: {reader["PolicyId"]}, PolicyNumber: {reader["PolicyNumber"]}, PolicyType: {reader["PolicyType"]}, StartDate: {((DateTime)reader["StartDate"]).ToShortDateString()}, EndDate: {((DateTime)reader["EndDate"]).ToShortDateString()}");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Policy not found.");
                            }
                            break;

                        case 4:
                            var allPolicies = policyService.GetAllPolicies();

                            Console.WriteLine("All Policies:");

                            foreach (var p in allPolicies)
                            {
                                p.PrintInfo();
                            }
                            break;

                        case 5:
                            Console.Write("Enter Policy ID to delete: ");
                            int deleteId = int.Parse(Console.ReadLine());

                            bool deleted = policyService.DeletePolicy(deleteId);
                            Console.WriteLine($"Policy with ID {deleteId} deleted: " + deleted);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (PolicyNotFoundException ex)
                {
                    Console.WriteLine("Policy Error: " + ex.Message);
                }
                
            }
        }
    }
}