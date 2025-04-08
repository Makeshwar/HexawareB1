using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CourierManagementSystem.Entities;
using CourierManagementSystem.DB;

namespace CourierManagementSystem.DB
{
    public class CourierServiceDb
    {
        //Insert Courier
        public void InsertCourier(Courier courier)
        {
            string insertQuery = @"INSERT INTO Courier 
        (SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, SentDate, DeliveryDate, AssignedEmployeeID, DeliveryLocationID, ServiceID, UserId)
        VALUES 
        (@SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, @Weight, @Status, @TrackingNumber, @SentDate, @DeliveryDate, @AssignedEmployeeID, @DeliveryLocationID, @ServiceID, @UserId)";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@SenderName", courier.SenderName);
                insertCmd.Parameters.AddWithValue("@SenderAddress", courier.SenderAddress);
                insertCmd.Parameters.AddWithValue("@ReceiverName", courier.ReceiverName);
                insertCmd.Parameters.AddWithValue("@ReceiverAddress", courier.ReceiverAddress);
                insertCmd.Parameters.AddWithValue("@Weight", courier.Weight);
                insertCmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(courier.Status) ? "In Transit" : courier.Status);
                insertCmd.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);
                insertCmd.Parameters.AddWithValue("@SentDate", courier.SentDate);
                insertCmd.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate);
                insertCmd.Parameters.AddWithValue("@AssignedEmployeeID", courier.AssignedStaffId);
                insertCmd.Parameters.AddWithValue("@DeliveryLocationID", courier.DeliveryLocationID);
                insertCmd.Parameters.AddWithValue("@ServiceID", courier.ServiceID);
                insertCmd.Parameters.AddWithValue("@UserId", courier.UserId);

                try
                {
                    connection.Open();
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Courier inserted successfully.");

                    // Display the inserted record
                    string selectQuery = "SELECT * FROM Courier WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                    selectCmd.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);

                    using SqlDataReader reader = selectCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("Inserted Courier Details:");
                        Console.WriteLine($"TrackingNumber: {reader["TrackingNumber"]}, Sender: {reader["SenderName"]},SenderAddress: {reader["SenderAddress"]}, Receiver: {reader["ReceiverName"]},ReceiverAddress: {reader["ReceiverAddress"]}, Status: {reader["Status"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting courier: " + ex.Message);
                }
            }
        }
        //Update Courier
        public void UpdateCourierStatus(string trackingNumber, string status)
        {
            string updateQuery = "UPDATE Courier SET Status = @Status WHERE TrackingNumber = @TrackingNumber";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
            {
                updateCmd.Parameters.AddWithValue("@Status", status);
                updateCmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                try
                {
                    connection.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Courier status updated successfully.");

                        // Display the updated record
                        string selectQuery = "SELECT * FROM Courier WHERE TrackingNumber = @TrackingNumber";
                        SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                        selectCmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                        using SqlDataReader reader = selectCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Console.WriteLine("Updated Courier Details:");
                            Console.WriteLine($"TrackingNumber: {reader["TrackingNumber"]}, Sender: {reader["SenderName"]}, SenderAddress: {reader["SenderAddress"]}, Receiver: {reader["ReceiverName"]}, ReceiverAddress: {reader["ReceiverAddress"]}, Status: {reader["Status"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No courier found with that tracking number.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating status: " + ex.Message);
                }
            }
        }
        // Get a courier by tracking number
        public Courier GetCourierByTrackingNumber(string trackingNumber)
        {
            Courier courier = null;
            string query = "SELECT * FROM Courier WHERE TrackingNumber = @TrackingNumber";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            courier = new Courier
                            {
                                SenderName = reader["SenderName"].ToString(),
                                ReceiverName = reader["ReceiverName"].ToString(),
                                Status = reader["Status"].ToString(),
                                Weight = Convert.ToInt32(reader["Weight"]),
                                TrackingNumber = reader["TrackingNumber"].ToString(),
                                DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching courier: " + ex.Message);
                }
            }

            return courier;
        }

        // Get delivery history for a specific user
        public List<Courier> GetDeliveryHistory(int userId)
        {
            List<Courier> history = new List<Courier>();
            string query = "SELECT * FROM Courier WHERE UserId = @UserId";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            history.Add(new Courier
                            {
                                SenderName = reader["SenderName"].ToString(),
                                ReceiverName = reader["ReceiverName"].ToString(),
                                Status = reader["Status"].ToString(),
                                TrackingNumber = reader["TrackingNumber"].ToString(),
                                DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching history: " + ex.Message);
                }
            }

            return history;
        }

        // Generate a report showing number of couriers by status
        public void GenerateStatusReport()
        {
            string query = "SELECT Status, COUNT(*) AS Count FROM Courier GROUP BY Status";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nCourier Status Report:");
                        Console.WriteLine("----------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Status"]}: {reader["Count"]} couriers");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error generating report: " + ex.Message);
                }
            }
        }

        // Generate revenue report from Payment table
        public void GenerateRevenueReport()
        {
            string query = "SELECT SUM(Amount) AS Revenue FROM Payment";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    decimal revenue = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    Console.WriteLine($"\nTotal Revenue from Payments: ₹{revenue}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error generating revenue report: " + ex.Message);
                }
            }
        }


    }
}