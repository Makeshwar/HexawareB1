using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CourierManagementSystem.Entities;
using CourierManagementSystem.DB;

namespace CourierManagementSystem.DB
{
    public class CourierServiceDb
    {
        // Insert a new courier record
        public void InsertCourier(Courier courier)
        {
            string query = @"INSERT INTO Courier 
                (SenderName, SenderAddress, ReceiverName, ReceiverAddress, Weight, Status, TrackingNumber, SentDate, DeliveryDate, AssignedEmployeeID, DeliveryLocationID, ServiceID, UserId)
                VALUES 
                (@SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, @Weight, @Status, @TrackingNumber, @SentDate, @DeliveryDate, @AssignedEmployeeID, @DeliveryLocationID, @ServiceID, @UserId)";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SenderName", courier.SenderName);
                cmd.Parameters.AddWithValue("@SenderAddress", courier.SenderAddress);
                cmd.Parameters.AddWithValue("@ReceiverName", courier.ReceiverName);
                cmd.Parameters.AddWithValue("@ReceiverAddress", courier.ReceiverAddress);
                cmd.Parameters.AddWithValue("@Weight", courier.Weight);
                cmd.Parameters.AddWithValue("@Status", courier.Status);
                cmd.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);
                cmd.Parameters.AddWithValue("@SentDate", courier.SentDate);
                cmd.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate);
                cmd.Parameters.AddWithValue("@AssignedEmployeeID", courier.AssignedStaffId);
                cmd.Parameters.AddWithValue("@DeliveryLocationID", courier.DeliveryLocationID);
                cmd.Parameters.AddWithValue("@ServiceID", courier.ServiceID);
                cmd.Parameters.AddWithValue("@UserId", courier.UserId);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Courier inserted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting courier: " + ex.Message);
                }
            }
        }

        // Update courier status by tracking number
        public void UpdateCourierStatus(string trackingNumber, string status)
        {
            string query = "UPDATE Courier SET Status = @Status WHERE TrackingNumber = @TrackingNumber";

            using var connection = DBConnection.GetConnection("config/db.properties");
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Courier status updated successfully.");
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