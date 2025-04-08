using System;
using System.Collections.Generic;
using CourierManagementSystem.DB;
using CourierManagementSystem.Entities;

class CourierApp
{
    static void Main(string[] args)
    {
        CourierServiceDb service = new CourierServiceDb();

        while (true)
        {
            Console.WriteLine("\n==== Courier Management ====");
            Console.WriteLine("1. Insert Courier");
            Console.WriteLine("2. Update Courier Status");
            Console.WriteLine("3. Get Courier by Tracking Number");
            Console.WriteLine("4. Get Delivery History by User ID");
            Console.WriteLine("5. Generate Shipment Status Report");
            Console.WriteLine("6. Generate Revenue Report");
            Console.Write("Select an option: ");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Courier newCourier = new Courier();

                    Console.Write("Sender Name: ");
                    newCourier.SenderName = Console.ReadLine();

                    Console.Write("Sender Address: ");
                    newCourier.SenderAddress = Console.ReadLine();

                    Console.Write("Receiver Name: ");
                    newCourier.ReceiverName = Console.ReadLine();

                    Console.Write("Receiver Address: ");
                    newCourier.ReceiverAddress = Console.ReadLine();

                    Console.Write("Weight (kg): ");
                    newCourier.Weight = double.Parse(Console.ReadLine());

                    Console.Write("Tracking Number: ");
                    newCourier.TrackingNumber = Console.ReadLine();

                    Console.Write("Status: ");
                    newCourier.Status = Console.ReadLine();

                    Console.Write("Assigned Staff ID: ");
                    newCourier.AssignedStaffId = int.Parse(Console.ReadLine());

                    Console.Write("Delivery Location ID: ");
                    newCourier.DeliveryLocationID = int.Parse(Console.ReadLine());

                    Console.Write("Service ID: ");
                    newCourier.ServiceID = int.Parse(Console.ReadLine());

                    Console.Write("User ID: ");
                    newCourier.UserId = int.Parse(Console.ReadLine());

                    service.InsertCourier(newCourier);
                    break;

                case 2:
                    Console.Write("Enter Tracking Number to update: ");
                    string trackNum = Console.ReadLine();
                    Console.Write("Enter New Status: ");
                    string newStatus = Console.ReadLine();

                    service.UpdateCourierStatus(trackNum, newStatus);
                    Console.WriteLine("Courier status updated.\n");
                    break;

                case 3:
                    Console.Write("Enter Tracking Number: ");
                    string searchTrack = Console.ReadLine();

                    var courier = service.GetCourierByTrackingNumber(searchTrack);
                    if (courier != null)
                    {
                        Console.WriteLine("\nCourier Details:");
                        Console.WriteLine($"Tracking#: {courier.TrackingNumber}");
                        Console.WriteLine($"Status: {courier.Status}");
                        Console.WriteLine($"Sender: {courier.SenderName} -> Receiver: {courier.ReceiverName}");
                        Console.WriteLine($"Weight: {courier.Weight} kg | Delivery Date: {courier.DeliveryDate}\n");
                    }
                    else
                    {
                        Console.WriteLine("Courier not found.\n");
                    }
                    break;

                case 4:
                    Console.Write("Enter User ID: ");
                    int userId = int.Parse(Console.ReadLine());

                    List<Courier> history = service.GetDeliveryHistory(userId);
                    Console.WriteLine($"\nDelivery History for User ID {userId}:");
                    foreach (var item in history)
                    {
                        Console.WriteLine($"Tracking#: {item.TrackingNumber} | Status: {item.Status} | Delivered On: {item.DeliveryDate}");
                    }
                    Console.WriteLine();
                    break;

                case 5:
                    service.GenerateStatusReport();
                    break;

                case 6:
                    service.GenerateRevenueReport();
                    break;

               

                default:
                    Console.WriteLine("Invalid option! Try again.\n");
                    break;
            }
        }
    }
}