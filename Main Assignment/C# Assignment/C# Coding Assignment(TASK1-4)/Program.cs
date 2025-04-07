namespace CS_MAINASSIGNMENT;

class Program
{
    static void Main(string[] args)
    {
        //TASK1
        Console.WriteLine("TASK 1 OPTIONS");
        Console.WriteLine("1. Check Order Status");
        Console.WriteLine("2. Categorize Parcel");
        Console.WriteLine("3. Authenticate User");
        Console.WriteLine("4. Assign Courier");
        Console.Write("Enter your choice (1-4): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
           case 1:
               Console.Write("Enter Order Status (Delivered / Processing / Cancelled): ");
               string status = Console.ReadLine();
               CS_TASK1.CheckOrderStatus(status);
               break;

           case 2:
               Console.Write("Enter Parcel Weight (in kg): ");
               double weight = Convert.ToDouble(Console.ReadLine());
               CS_TASK1.CategorizeParcel(weight);
               break;

           case 3:
               Console.Write("Enter User Role (Employee / Customer): ");
               string role = Console.ReadLine();
               CS_TASK1.AuthenticateUser(role);
               break;

           case 4:
               CS_TASK1.AssignCourier();
               break;

           default:
               Console.WriteLine("Invalid choice.");
               break;
        }

        Console.WriteLine();

        //TASK 2

        Console.WriteLine("TASK 2 OPTIONS");
        Console.WriteLine("5. Display Orders for a Specific Customer");
        Console.WriteLine("6. Track Real-Time Location of a Courier");
        Console.Write("Enter your choice (5-6): ");

        int task2Choice = Convert.ToInt32(Console.ReadLine());

        switch (task2Choice)
        {
           case 5:
               CS_TASK2.DisplayCustomerOrders();
               break;
           case 6:
               CS_TASK2.TrackCourier();
               break;
           default:
               Console.WriteLine("Invalid choice.");
               break;
        }

        Console.WriteLine();

        //TASK3

        Console.WriteLine("TASK 3 OPTIONS");
        Console.WriteLine("7. Display Parcel Tracking History");
        Console.WriteLine("8. Find Nearest Available Courier");
        Console.Write("Enter your choice (7-8): ");

        int task3Choice = Convert.ToInt32(Console.ReadLine());

        switch (task3Choice)
        {
            case 7:
                CS_TASK3.StoreTrackingHistory();
                break;
            case 8:
                CS_TASK3.FindNearestCourier();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        Console.WriteLine();


        //TASK4

        Console.WriteLine("TASK 4 OPTIONS");
        Console.WriteLine("9. Parcel Tracking");
        Console.WriteLine("10. Customer Data Validation");
        Console.WriteLine("11. Address Formatting");
        Console.WriteLine("12. Order Confirmation Email");
        Console.WriteLine("13. Calculate Shipping Cost");
        Console.WriteLine("14. Generate Secure Password");
        Console.WriteLine("15. Find Similar Addresses");
        Console.Write("Enter your choice: ");
        int task4Choice = Convert.ToInt32(Console.ReadLine());

        switch (task4Choice)
        {
            case 9:
                CS_TASK4.ParcelTracking();
                break;

            case 10:
                Console.Write("Enter Data to Validate: ");
                string data = Console.ReadLine();
                Console.Write("Enter Detail Type (name/address/phone): ");
                string detail = Console.ReadLine();
                CS_TASK4.ValidateCustomerData(data, detail);
                break;

            case 11:
                Console.Write("Enter Street: ");
                string street = Console.ReadLine();
                Console.Write("Enter City: ");
                string city = Console.ReadLine();
                Console.Write("Enter State: ");
                string state = Console.ReadLine();
                Console.Write("Enter ZIP Code: ");
                string zip = Console.ReadLine();
                CS_TASK4.FormatAddress(street, city, state, zip);
                break;

            case 12:
                Console.Write("Enter Customer Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Order Number: ");
                string orderNo = Console.ReadLine();
                Console.Write("Enter Delivery Address: ");
                string addr = Console.ReadLine();
                Console.Write("Enter Delivery Date: ");
                string date = Console.ReadLine();
                CS_TASK4.GenerateOrderEmail(name, orderNo, addr, date);
                break;

            case 13:
                Console.Write("Enter Source Address: ");
                string from = Console.ReadLine();
                Console.Write("Enter Destination Address: ");
                string to = Console.ReadLine();
                Console.Write("Enter Parcel Weight (in kg): ");
                double weight = Convert.ToDouble(Console.ReadLine());
                double cost = CS_TASK4.CalculateShippingCost(from, to, weight);
                if (cost > 0)
                {
                    Console.WriteLine($"\nShipping cost from {from} to {to} for {weight} kg is ₹{cost:F2}");
                }
                break;

            case 14:
                Console.Write("Enter Desired Password Length (minimum 6): ");
                int len = Convert.ToInt32(Console.ReadLine());
                if (len < 6) len = 6;
                CS_TASK4.GeneratePassword(len);
                break;

            case 15:
                string[] savedAddresses = {
                            "123 Main Street, Chennai",
                            "456 Lake Road, Madurai",
                            "789 Park Ave, Chennai",
                            "12 Gandhi Street, Coimbatore"
                        };
                Console.Write("Enter partial address to search: ");
                string target = Console.ReadLine();
                CS_TASK4.FindSimilarAddresses(savedAddresses, target);
                break;

            default:
                Console.WriteLine("Invalid Choice.");
                break;
        }
    }
}