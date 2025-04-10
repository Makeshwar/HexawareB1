using System;
using dao;
using entity;
using myexceptions;

namespace DigitalAssetManagement.app
{
    public class AssetManagementApp
    {
        public static void Main(string[] args)
        {
            IAssetManagementService service = new AssetManagementServiceImpl();

            while (true)
            {
                Console.WriteLine("--- Digital Asset Management System ---");
                Console.WriteLine("1. Add Asset");
                Console.WriteLine("2. Update Asset");
                Console.WriteLine("3. Delete Asset");
                Console.WriteLine("4. Allocate Asset");
                Console.WriteLine("5. Deallocate Asset");
                Console.WriteLine("6. Perform Maintenance");
                Console.WriteLine("7. Reserve Asset");
                Console.WriteLine("8. Withdraw Reservation");
                Console.Write("Enter your choice: ");

                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Asset newAsset = new Asset();
                            Console.Write("Enter Name: ");
                            newAsset.Name = Console.ReadLine();
                            Console.Write("Enter Type: ");
                            newAsset.Type = Console.ReadLine();
                            Console.Write("Enter Serial Number: ");
                            newAsset.SerialNumber = Console.ReadLine();
                            Console.Write("Enter Purchase Date (yyyy-mm-dd): ");
                            newAsset.PurchaseDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Location: ");
                            newAsset.Location = Console.ReadLine();
                            Console.Write("Enter Status (in use, decommissioned, under maintenance): ");
                            newAsset.Status = Console.ReadLine();
                            Console.Write("Enter Owner ID (or press Enter to skip): ");
                            string ownerInput = Console.ReadLine();
                            newAsset.OwnerId = string.IsNullOrEmpty(ownerInput) ? null : int.Parse(ownerInput);

                            service.AddAsset(newAsset);
                            break;

                        case 2:
                            Console.Write("Enter Asset ID to Update: ");
                            int upId = int.Parse(Console.ReadLine());
                            Asset upAsset = new Asset();
                            upAsset.AssetId = upId;
                            Console.Write("Enter New Name: ");
                            upAsset.Name = Console.ReadLine();
                            Console.Write("Enter New Type: ");
                            upAsset.Type = Console.ReadLine();
                            Console.Write("Enter New Serial Number: ");
                            upAsset.SerialNumber = Console.ReadLine();
                            Console.Write("Enter New Purchase Date (yyyy-mm-dd): ");
                            upAsset.PurchaseDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter New Location: ");
                            upAsset.Location = Console.ReadLine();
                            Console.Write("Enter New Status: ");
                            upAsset.Status = Console.ReadLine();
                            Console.Write("Enter Owner ID (or press Enter to skip): ");
                            string upOwnerInput = Console.ReadLine();
                            upAsset.OwnerId = string.IsNullOrEmpty(upOwnerInput) ? null : int.Parse(upOwnerInput);

                            service.UpdateAsset(upAsset);
                            break;



                        case 3:
                            Console.Write("Enter Asset ID to Delete: ");
                            int delId = int.Parse(Console.ReadLine());
                            service.DeleteAsset(delId);
                            break;

                        case 4:
                            Console.Write("Enter Asset ID: ");
                            int allocAssetId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int allocEmpId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Allocation Date (yyyy-mm-dd): ");
                            string allocDate = Console.ReadLine();

                            service.AllocateAsset(allocAssetId, allocEmpId, allocDate);
                            break;

                        case 5:
                            Console.Write("Enter Asset ID: ");
                            int deallocAssetId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int deallocEmpId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Return Date (yyyy-mm-dd): ");
                            string returnDate = Console.ReadLine();

                            service.DeallocateAsset(deallocAssetId, deallocEmpId, returnDate);
                            break;

                        case 6:
                            Console.Write("Enter Asset ID: ");
                            int maintAssetId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Maintenance Date (yyyy-mm-dd): ");
                            string maintDate = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            string description = Console.ReadLine();
                            Console.Write("Enter Cost: ");
                            double cost = double.Parse(Console.ReadLine());

                            service.PerformMaintenance(maintAssetId, maintDate, description, cost);
                            break;

                        case 7:
                            Console.Write("Enter Asset ID: ");
                            int resAssetId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int resEmpId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Reservation Date (yyyy-mm-dd): ");
                            string reservationDate = Console.ReadLine();
                            Console.Write("Enter Start Date (yyyy-mm-dd): ");
                            string startDate = Console.ReadLine();
                            Console.Write("Enter End Date (yyyy-mm-dd): ");
                            string endDate = Console.ReadLine();

                            service.ReserveAsset(resAssetId, resEmpId, reservationDate, startDate, endDate);
                            break;

                        case 8:
                            Console.Write("Enter Reservation ID to Withdraw: ");
                            int reservationId = int.Parse(Console.ReadLine());
                            service.WithdrawReservation(reservationId);
                            break;



                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (AssetNotFoundException ex)
                {
                    Console.WriteLine("Asset Not Found: " + ex.Message);
                }
                catch (AssetNotMaintainException ex)
                {
                    Console.WriteLine(" Maintenance Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}

