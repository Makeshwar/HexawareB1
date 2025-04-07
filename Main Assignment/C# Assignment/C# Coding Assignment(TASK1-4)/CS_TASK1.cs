using System;

namespace CS_MAINASSIGNMENT
{
    public class CS_TASK1
    {
        // Q 1: Check Order Status using if-else
        public static void CheckOrderStatus(string status)
        {
            if (status == "Delivered" || status == "delivered")
            {
                Console.WriteLine("Order has been delivered.");
            }
            else if (status == "Processing" || status == "processing")
            {
                Console.WriteLine("Order is still processing.");
            }
            else if (status == "Cancelled" || status == "cancelled")
            {
                Console.WriteLine("Order was cancelled.");
            }
            else
            {
                Console.WriteLine("Invalid status entered.");
            }
        }

        // Q 2: Categorize Parcel using switch-case 
        public static void CategorizeParcel(double weight)
        {
            switch (weight)
            {
                case < 1:
                    Console.WriteLine("Parcel is categorized as: Light");
                    break;
                case >= 1 and <= 5:
                    Console.WriteLine("Parcel is categorized as: Medium");
                    break;
                case > 5:
                    Console.WriteLine("Parcel is categorized as: Heavy");
                    break;
                default:
                    Console.WriteLine("Invalid weight input.");
                    break;
            }
        }

        // Q 3: Simple User Authentication using if-else
        public static void AuthenticateUser(string role)
        {
            if (role == "Employee")
            {
                Console.WriteLine("Welcome, Employee! You have full access.");
            }
            else if (role == "Customer")
            {
                Console.WriteLine("Welcome, Customer! You have limited access.");
            }
            else
            {
                Console.WriteLine("Invalid role entered.");
            }
        }

        // Q 4: Assign Courier using Only Control Flow (No Random)
        public static void AssignCourier()
        {
            string[] couriers = { "Courier A", "Courier B", "Courier C" };
            double[] maxLoads = { 20, 10, 30 };
            int[] distances = { 4, 2, 6 };

            Console.Write("Enter shipment weight (kg): ");
            double shipmentWeight = Convert.ToDouble(Console.ReadLine());

            for (int i = 0; i < distances.Length - 1; i++)
            {
                for (int j = 0; j < distances.Length - i - 1; j++)
                {
                    if (distances[j] > distances[j + 1])
                    {
                        // Swap everything in sync
                        (distances[j], distances[j + 1]) = (distances[j + 1], distances[j]);
                        (maxLoads[j], maxLoads[j + 1]) = (maxLoads[j + 1], maxLoads[j]);
                        (couriers[j], couriers[j + 1]) = (couriers[j + 1], couriers[j]);
                    }
                }
            }

            string assignedCourier = "No Available Courier";
            for (int i = 0; i < couriers.Length; i++)
            {
                if (shipmentWeight <= maxLoads[i])
                {
                    assignedCourier = couriers[i];
                    break;
                }
            }

            Console.WriteLine($"Assigned Courier: {assignedCourier}");
        }
    }
}