using System;
namespace CS_MAINASSIGNMENT
{
	public class CS_TASK3
	{
        // Q 7: Create an array to store tracking history
        public static void StoreTrackingHistory()
        {
            string[] trackingHistory = new string[5];
            trackingHistory[0] = "Parcel picked up - Theni";
            trackingHistory[1] = "Left facility - Madurai";
            trackingHistory[2] = "Arrived at sorting center - Trichy";
            trackingHistory[3] = "Out for delivery - Chennai";
            trackingHistory[4] = "Delivered - Chennai";

            Console.WriteLine("Parcel Tracking History:");
            foreach (string update in trackingHistory)
            {
                Console.WriteLine(update);
            }
        }

        // Q 8: Find the nearest available courier
        public static void FindNearestCourier()
        {
            string[] couriers = { "Courier A - 2 km", "Courier B - 5 km", "Courier C - 1 km" };
            string nearestCourier = couriers[0];

            foreach (string courier in couriers)
            {
                if (int.Parse(courier.Split('-')[1].Trim().Split(' ')[0]) < int.Parse(nearestCourier.Split('-')[1].Trim().Split(' ')[0]))
                {
                    nearestCourier = courier;
                }
            }
            Console.WriteLine("Nearest Available Courier: " + nearestCourier);
        }
    }
}

