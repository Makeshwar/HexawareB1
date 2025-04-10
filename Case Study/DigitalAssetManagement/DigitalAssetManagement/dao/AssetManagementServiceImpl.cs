using System;
using System.Data.SqlClient;
using entity;
using myexceptions;
using DigitalAssetManagement.util;

namespace dao
{
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        private readonly string configPath = "config/db.properties";

        private SqlConnection conn;

        public AssetManagementServiceImpl()
        {
            conn = DBConnection.GetConnection(configPath);
        }

        public bool AddAsset(Asset asset)
        {
            try
            {
                string query = @"INSERT INTO assets (name, type, serial_number, purchase_date, location, status, owner_id) 
                         VALUES (@Name, @Type, @SerialNumber, @PurchaseDate, @Location, @Status, @OwnerId)";

                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", asset.Name);

                cmd.Parameters.AddWithValue("@Type", asset.Type);

                cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);

                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);

                cmd.Parameters.AddWithValue("@Location", asset.Location);

                cmd.Parameters.AddWithValue("@Status", asset.Status);

                cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId ?? (object)null);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    Console.WriteLine("Asset added successfully.");

                    string selectQuery = "SELECT * FROM assets WHERE serial_number = @SerialNumber";

                    using var selectCmd = new SqlCommand(selectQuery, conn);

                    selectCmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);

                    using var reader = selectCmd.ExecuteReader();
                    Console.WriteLine("Inserted Record");
                    if (reader.Read())
                    {
                        Console.WriteLine($"Asset-ID: {reader["asset_id"]}, Name: {reader["name"]}, Type: {reader["type"]}, SerialNumber: {reader["serial_number"]}, PurchaseDate: {reader["purchase_date"]}, Location:  {reader["location"]}, Status: {reader["status"]}, Owner-Id: {reader["owner_id"]} ");
                    }


                    return true;
                }

                else
                {
                    Console.WriteLine(" Failed to add asset.");
                    return false;
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool UpdateAsset(Asset asset)
        {
            try
            {

                string query = @"UPDATE assets SET name = @Name, type = @Type, serial_number = @SerialNumber, 
                         purchase_date = @PurchaseDate, location = @Location, status = @Status, owner_id = @OwnerId 
                         WHERE asset_id = @AssetId";

                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", asset.Name);

                cmd.Parameters.AddWithValue("@Type", asset.Type);

                cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);

                cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);

                cmd.Parameters.AddWithValue("@Location", asset.Location);

                cmd.Parameters.AddWithValue("@Status", asset.Status);

                cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId ?? (object)null);

                cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    Console.WriteLine("Asset updated successfully.");

                    string selectQuery = "SELECT * FROM assets WHERE serial_number = @SerialNumber";

                    using var selectCmd = new SqlCommand(selectQuery, conn);

                    selectCmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);

                    using var reader = selectCmd.ExecuteReader();
                    Console.WriteLine("Updated Record");
                    if (reader.Read())
                    {
                        Console.WriteLine($"Asset-ID: {reader["asset_id"]}, Name: {reader["name"]}, Type: {reader["type"]}, SerialNumber: {reader["serial_number"]}, PurchaseDate: {reader["purchase_date"]}, Location:  {reader["location"]}, Status: {reader["status"]}, Owner-Id: {reader["owner_id"]} ");
                    }


                    return true;
                }

                else
                {
                    Console.WriteLine(" Failed to add asset.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteAsset(int assetId)
        {
            try
            {
                using var cmd = new SqlCommand("DELETE FROM assets WHERE asset_id = @AssetId", conn);

                cmd.Parameters.AddWithValue("@AssetId", assetId);


                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? " Asset deleted." : "Asset not found.");

                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool AllocateAsset(int assetId, int employeeId, string allocationDate)
        {
            try
            {
                string query = @"INSERT INTO asset_allocations (asset_id, employee_id, allocation_date) 
                         VALUES (@AssetId, @EmployeeId, @AllocationDate)";

                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@AssetId", assetId);

                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                cmd.Parameters.AddWithValue("@AllocationDate", DateTime.Parse(allocationDate));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    Console.WriteLine("Asset allocated.");

                    string selectQuery = "SELECT * FROM asset_allocations WHERE asset_id = @AssetId AND employee_id = @EmployeeId";

                    using var selectCmd = new SqlCommand(selectQuery, conn);

                    selectCmd.Parameters.AddWithValue("@AssetId", assetId);

                    selectCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using var reader = selectCmd.ExecuteReader();

                    Console.WriteLine("Allocated Record");

                    if (reader.Read())
                    {
                        Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, AllocationDate: {reader["allocation_date"]}");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Allocation failed.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool DeallocateAsset(int assetId, int employeeId, string returnDate)
        {
            string checkQuery = "SELECT COUNT(*) FROM asset_allocations WHERE asset_id = @AssetId AND employee_id = @EmployeeId AND return_date IS NULL";

            using (var checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@AssetId", assetId);
                checkCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    Console.WriteLine("No active allocation found for this asset and employee.");
                    return false;
                }
            }

            try
            {
                string query = @"UPDATE asset_allocations 
                         SET return_date = @ReturnDate 
                         WHERE asset_id = @AssetId AND employee_id = @EmployeeId AND return_date IS NULL";

                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@AssetId", assetId);

                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Parse(returnDate));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    Console.WriteLine("Asset deallocated.");

                    string selectQuery = "SELECT * FROM asset_allocations WHERE asset_id = @AssetId AND employee_id = @EmployeeId";
                    using var selectCmd = new SqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@AssetId", assetId);
                    selectCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using var reader = selectCmd.ExecuteReader();

                    Console.WriteLine("DeAllocated Record");

                    if (reader.Read())
                    {
                        Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, ReturnDate: {reader["return_date"]}");
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine("Asset not allocated or already deallocated.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool PerformMaintenance(int assetId, string date, string description, double cost)
        {
            string checkQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

            using var checkCmd = new SqlCommand(checkQuery, conn);

            checkCmd.Parameters.AddWithValue("@AssetId", assetId);

            int count = (int)checkCmd.ExecuteScalar();

            if (count == 0)
            {
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");
            }

            string query = @"INSERT INTO maintenance_records (asset_id, maintenance_date, description, cost) 
                         VALUES (@AssetId, @Date, @Description, @Cost)";

                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@AssetId", assetId);

                cmd.Parameters.AddWithValue("@Date", DateTime.Parse(date));

                cmd.Parameters.AddWithValue("@Description", description);

                cmd.Parameters.AddWithValue("@Cost", cost);

                int rows = cmd.ExecuteNonQuery();


            if (rows > 0)
            {
                Console.WriteLine("Maintenance recorded.");

                string selectQuery = "SELECT * FROM maintenance_records WHERE asset_id = @AssetId ORDER BY maintenance_date DESC";

                using var selectCmd = new SqlCommand(selectQuery, conn);

                selectCmd.Parameters.AddWithValue("@AssetId", assetId);

                using var reader = selectCmd.ExecuteReader();

                Console.WriteLine("Maintenance Record");


                if (reader.Read())
                {
                    Console.WriteLine($"AssetID: {reader["asset_id"]}, MaintenanceDate: {reader["maintenance_date"]}, Description: {reader["description"]}, Cost: {reader["cost"]}");
                }

                return true;
            }

            else
            {
                Console.WriteLine("Failed to record maintenance.");
                return false;
            }
        }

        public bool ReserveAsset(int assetId, int employeeId, string reservationDate, string startDate, string endDate)

        {

            string checkQuery = "SELECT COUNT(*) FROM employees WHERE employee_id = @EmployeeId";

            using (var checkCmd = new SqlCommand(checkQuery, conn))

            {
                checkCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)

                    throw new AssetNotFoundException($"Employee with ID {employeeId} not found.");
            }

            string query = @"INSERT INTO reservations (asset_id, employee_id, reservation_date, start_date, end_date, status) 
                     VALUES (@AssetId, @EmployeeId, @ReservationDate, @StartDate, @EndDate, @Status)";

            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@AssetId", assetId);

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            cmd.Parameters.AddWithValue("@ReservationDate", DateTime.Parse(reservationDate));

            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));

            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));

            cmd.Parameters.AddWithValue("@Status", "pending"); 

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Asset reserved.");

                string selectQuery = "SELECT * FROM reservations WHERE asset_id = @AssetId AND employee_id = @EmployeeId";

                using var selectCmd = new SqlCommand(selectQuery, conn);

                selectCmd.Parameters.AddWithValue("@AssetId", assetId);

                selectCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                using var reader = selectCmd.ExecuteReader();

                Console.WriteLine("Reservation Record");

                if (reader.Read())
                {
                    Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, ReservationDate: {reader["reservation_date"]}, StartDate: {reader["start_date"]}, EndDate: {reader["end_date"]}, Status: {reader["status"]}");
                }

                return true;
            }
            else
            {
                Console.WriteLine("Reservation failed.");
                return false;
            }
        }

        public bool WithdrawReservation(int reservationId)
        {
            try
            {
                string query = @"DELETE FROM reservations WHERE reservation_id = @ReservationId";

                using var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ReservationId", reservationId);

                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Reservation withdrawn." : "Reservation not found.");

                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}