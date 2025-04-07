using System;
namespace CS_MAINASSIGNMENT
{
	public class CS_TASK2
	{
        // Q 5: Display Orders for a Specific Customer using for loop
        public static void DisplayCustomerOrders()
        {
            string customerName;
            Console.Write("Enter customer name: ");
            customerName = Console.ReadLine();

            string[] orderCustomers = { "Makesh", "Mithun", "Alex", "Makesh", "Mithun" };
            string[] orderDetails = { "Order #1: Books", "Order #2: Laptop", "Order #3: Headphones", "Order #4: Phone", "Order #5: Keyboard" };

            Console.WriteLine($"Orders for {customerName}:");
            for (int i = 0; i < orderCustomers.Length; i++)
            {
                if (orderCustomers[i].Equals(customerName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(orderDetails[i]);
                }
            }
        }

        // Q 6: Track Real-Time Location of a Courier using a While Loop
        public static void TrackCourier()
        {
            int currentLocation = 0;
            int destination = 10;

            Console.WriteLine("Tracking courier...");

            while (currentLocation < destination)
            {
                Console.WriteLine($"Courier is at location: {currentLocation}");
                currentLocation++;
                Thread.Sleep(500);
            }

            Console.WriteLine("Courier has reached the destination!");
        }
    }
}

