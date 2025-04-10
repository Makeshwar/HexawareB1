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
                        DateTime pur_date = (DateTime)reader["purchase_date"];

                        Console.WriteLine($"Asset-ID: {reader["asset_id"]}, Name: {reader["name"]}, Type: {reader["type"]}, SerialNumber: {reader["serial_number"]}, PurchaseDate: {pur_date.ToString("yyyy-MM-dd")}, Location:  {reader["location"]}, Status: {reader["status"]}, Owner-Id: {reader["owner_id"]} ");
                    }
                    
                    return true;
                }

                else
                {
                    Console.WriteLine(" Failed to add asset. Ensure your records are right");

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

                string checkQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

                using var checkCmd = new SqlCommand(checkQuery, conn);

                checkCmd.Parameters.AddWithValue("@AssetId", asset.AssetId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new AssetNotFoundException($"Asset with ID {asset.AssetId} not found.");
                }

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
                        DateTime pur_date = (DateTime)reader["purchase_date"];

                        Console.WriteLine($"Asset-ID: {reader["asset_id"]}, Name: {reader["name"]}, Type: {reader["type"]}, SerialNumber: {reader["serial_number"]}, PurchaseDate: {pur_date.ToString("yyyy-MM-dd")}, Location:  {reader["location"]}, Status: {reader["status"]}, Owner-Id: {reader["owner_id"]} ");
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
                string assetcheckQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

                using var assetcheckCmd = new SqlCommand(assetcheckQuery, conn);
                assetcheckCmd.Parameters.AddWithValue("@AssetId", assetId);

                int assetcount = (int)assetcheckCmd.ExecuteScalar();

                if (assetcount == 0)
                {
                    throw new AssetNotFoundException($"Asset with ID {assetId} not found.");
                }

                string allocationCheckQuery = @"
            SELECT COUNT(*) FROM asset_allocations 
            WHERE asset_id = @AssetId AND return_date IS NULL";

                using (var allocCmd = new SqlCommand(allocationCheckQuery, conn))
                {
                    allocCmd.Parameters.AddWithValue("@AssetId", assetId);
                    int activeAllocations = (int)allocCmd.ExecuteScalar();

                    if (activeAllocations > 0)
                    {
                        Console.WriteLine("Cannot delete asset: It is currently allocated.");
                        return false;
                    }
                }

                string reservationCheckQuery = @"
            SELECT COUNT(*) FROM reservations 
            WHERE asset_id = @AssetId AND status = 'pending'";

                using (var resCmd = new SqlCommand(reservationCheckQuery, conn))
                {
                    resCmd.Parameters.AddWithValue("@AssetId", assetId);
                    int pendingReservations = (int)resCmd.ExecuteScalar();

                    if (pendingReservations > 0)
                    {
                        Console.WriteLine("Cannot delete asset: It has pending reservations.");
                        return false;
                    }
                }


                string statusCheckQuery = "SELECT status FROM assets WHERE asset_id = @AssetId";

                using (var statusCmd = new SqlCommand(statusCheckQuery, conn))
                {
                    statusCmd.Parameters.AddWithValue("@AssetId", assetId);
                    string? status = statusCmd.ExecuteScalar()?.ToString();

                    if (status == "in use")
                    {
                        Console.WriteLine("Cannot delete asset: Status is 'in use'.");
                        return false;
                    }
                }
                
               
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

                string assetcheckQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

                using var assetcheckCmd = new SqlCommand(assetcheckQuery, conn);

                assetcheckCmd.Parameters.AddWithValue("@AssetId", assetId);

                int assetcount = (int)assetcheckCmd.ExecuteScalar();

                if (assetcount == 0)
                {
                    throw new AssetNotFoundException($"Asset with ID {assetId} not found.");
                }

                string employeecheckQuery = "SELECT COUNT(*) FROM employees WHERE employee_id = @EmployeeId";

                using (var employeecheckCmd = new SqlCommand(employeecheckQuery, conn))

                {
                    employeecheckCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    int employeecount = (int)employeecheckCmd.ExecuteScalar();

                    if (employeecount == 0)

                        throw new AssetNotFoundException($"Employee with ID {employeeId} not found.");
                }


                string checkAllocationQuery = @"SELECT COUNT(*) FROM asset_allocations 
                                        WHERE asset_id = @AssetId AND return_date IS NULL";

                using (var checkAllocCmd = new SqlCommand(checkAllocationQuery, conn))
                {
                    checkAllocCmd.Parameters.AddWithValue("@AssetId", assetId);

                    int activeAllocations = (int)checkAllocCmd.ExecuteScalar();

                    if (activeAllocations > 0)
                    {
                        Console.WriteLine("Asset is already allocated and not yet returned.");
                        return false;
                    }
                }


                string statusCheckQuery = "SELECT status FROM assets WHERE asset_id = @AssetId";

                using (var statusCmd = new SqlCommand(statusCheckQuery, conn))
                {
                    statusCmd.Parameters.AddWithValue("@AssetId", assetId);

                    string? status = statusCmd.ExecuteScalar()?.ToString();

                    if (status != "in use")
                    {
                        Console.WriteLine("Asset status must be 'in use' to allocate.");
                        return false;
                    }
                }

                string conflictReservationQuery = @"
                SELECT COUNT(*) FROM reservations
                WHERE asset_id = @AssetId
                AND status = 'pending'
                AND @AllocationDate BETWEEN start_date AND end_date";

                using (var resCmd = new SqlCommand(conflictReservationQuery, conn))
                {
                    resCmd.Parameters.AddWithValue("@AssetId", assetId);
                    resCmd.Parameters.AddWithValue("@AllocationDate", DateTime.Parse(allocationDate));

                    int conflictCount = (int)resCmd.ExecuteScalar();

                    if (conflictCount > 0)
                    {
                        Console.WriteLine("Asset has a pending reservation on this date and cannot be allocated.");
                        return false;
                    }
                }

                string maintenanceQuery = "SELECT TOP 1 maintenance_date FROM maintenance_records WHERE asset_id = @AssetId ORDER BY maintenance_date DESC";

                using (var maintenanceCmd = new SqlCommand(maintenanceQuery, conn))
                {
                    maintenanceCmd.Parameters.AddWithValue("@AssetId", assetId);

                    object? result = maintenanceCmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        DateTime lastMaintenanceDate = (DateTime)result;

                        if (lastMaintenanceDate < DateTime.Now.AddYears(-2))
                        {
                            throw new AssetNotMaintainException($"Asset {assetId} has not been maintained in the last 2 years.");
                        }
                    }
                    else
                    {
                        throw new AssetNotMaintainException($"No maintenance record found for asset {assetId}.");
                    }
                }

                string insertQuery = @"INSERT INTO asset_allocations (asset_id, employee_id, allocation_date) 
                               VALUES (@AssetId, @EmployeeId, @AllocationDate)";

                using (var insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@AssetId", assetId);
                    insertCmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    insertCmd.Parameters.AddWithValue("@AllocationDate", DateTime.Parse(allocationDate));

                    int rows = insertCmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Console.WriteLine("Asset allocated.");

                        string selectQuery = @"SELECT * FROM asset_allocations 
                                       WHERE asset_id = @AssetId AND employee_id = @EmployeeId 
                                       ORDER BY allocation_date DESC";

                        using (var selectCmd = new SqlCommand(selectQuery, conn))
                        {
                            selectCmd.Parameters.AddWithValue("@AssetId", assetId);
                            selectCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                            using (var reader = selectCmd.ExecuteReader())
                            {
                                Console.WriteLine("Allocated Record:");
                                if (reader.Read())
                                {
                                    DateTime allocDate = (DateTime)reader["allocation_date"];
                                    Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, AllocationDate: {allocDate:yyyy-MM-dd}");
                                }
                            }
                        }

                        return true;
                    }

                    else
                    {
                        Console.WriteLine("Allocation failed.");
                        return false;
                    }
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
            try
            {

                string assetcheckQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

                using var assetcheckCmd = new SqlCommand(assetcheckQuery, conn);

                assetcheckCmd.Parameters.AddWithValue("@AssetId", assetId);

                int assetcount = (int)assetcheckCmd.ExecuteScalar();

                if (assetcount == 0)
                {
                    throw new AssetNotFoundException($"Asset with ID {assetId} not found.");
                }

                string employeecheckQuery = "SELECT COUNT(*) FROM employees WHERE employee_id = @EmployeeId";

                using (var employeecheckCmd = new SqlCommand(employeecheckQuery, conn))

                {
                    employeecheckCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    int employeecount = (int)employeecheckCmd.ExecuteScalar();

                    if (employeecount == 0)

                        throw new AssetNotFoundException($"Employee with ID {employeeId} not found.");
                }
                string checkQuery = "SELECT COUNT(*) FROM asset_allocations WHERE asset_id = @AssetId AND employee_id = @EmployeeId AND return_date IS NULL";

            using (var checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@AssetId", assetId);
                checkCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    Console.WriteLine("No active allocation found for this asset and employee or this asset has been already deallocated.");
                    return false;
                }
            }

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
                        DateTime? returndate = reader["return_date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["return_date"];

                        Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, ReturnDate: {returndate?.ToString("yyyy-MM-dd") ?? "Not returned"}");
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
                    DateTime maintaindate = (DateTime)reader["maintenance_date"];
                    Console.WriteLine($"AssetID: {reader["asset_id"]}, MaintenanceDate: {maintaindate.ToString("yyyy-MM-dd")}, Description: {reader["description"]}, Cost: {reader["cost"]}");
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
            string assetcheckQuery = "SELECT COUNT(*) FROM assets WHERE asset_id = @AssetId";

            using var assetcheckCmd = new SqlCommand(assetcheckQuery, conn);

            assetcheckCmd.Parameters.AddWithValue("@AssetId", assetId);

            int assetcount = (int)assetcheckCmd.ExecuteScalar();

            if (assetcount == 0)
            {
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");
            }

            string employeecheckQuery = "SELECT COUNT(*) FROM employees WHERE employee_id = @EmployeeId";

            using (var employeecheckCmd = new SqlCommand(employeecheckQuery, conn))

            {
                employeecheckCmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int employeecount = (int)employeecheckCmd.ExecuteScalar();

                if (employeecount == 0)

                    throw new AssetNotFoundException($"Employee with ID {employeeId} not found.");
            }

            string statusCheckQuery = "SELECT status FROM assets WHERE asset_id = @AssetId";

            using (var statusCmd = new SqlCommand(statusCheckQuery, conn))
            {
                statusCmd.Parameters.AddWithValue("@AssetId", assetId);

                string? status = statusCmd.ExecuteScalar()?.ToString();

                if (status != "in use")
                { 
                    Console.WriteLine("Asset status must be 'in_use' to allocate.");
                    return false;
                }
            }

            string reservationConflictQuery = @"
            SELECT COUNT(*) 
            FROM reservations 
            WHERE asset_id = @AssetId 
            AND status IN ('pending', 'approved')
            AND NOT (end_date < @StartDate OR start_date > @EndDate) ";

            using (var resCmd = new SqlCommand(reservationConflictQuery, conn))
            {
                resCmd.Parameters.AddWithValue("@AssetId", assetId);

                resCmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));

                resCmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));

                int reservationConflict = (int)resCmd.ExecuteScalar();

                if (reservationConflict > 0)
                {
                    Console.WriteLine("Asset already has a reservation in this time range.");
                    return false;
                }
            }

            string allocationConflictQuery = @"
            SELECT COUNT(*) 
            FROM asset_allocations 
            WHERE asset_id = @AssetId 
              AND return_date IS NULL
              AND allocation_date <= @EndDate";

            using (var allocCmd = new SqlCommand(allocationConflictQuery, conn))
            {
                allocCmd.Parameters.AddWithValue("@AssetId", assetId);
                allocCmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));

                int allocationConflict = (int)allocCmd.ExecuteScalar();
                if (allocationConflict > 0)
                {
                    Console.WriteLine("Asset is already allocated during the reservation time.");
                    return false;
                }
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
                    DateTime reservedate = (DateTime)reader["reservation_date"];
                    DateTime stdate = (DateTime)reader["start_date"];
                    DateTime endate = (DateTime)reader["end_date"];

                    Console.WriteLine($"AssetID: {reader["asset_id"]}, EmployeeID: {reader["employee_id"]}, ReservationDate: {reservedate.ToString("yyyy-MM-dd")}, StartDate: {stdate.ToString("yyyy-MM-dd")}, EndDate: {endate.ToString("yyyy-MM-dd")}, Status: {reader["status"]}");
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