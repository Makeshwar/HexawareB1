using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CS_MAINASSIGNMENT
{
	public class CS_TASK4
	{

        /* Q9: Parcel Tracking: Create a program that allows users to input a parcel tracking number.Store the
        tracking number and Status in 2d String Array.Initialize the array with values. Then, simulate the
        tracking process by displaying messages like "Parcel in transit," "Parcel out for delivery," or "Parcel
        delivered" based on the tracking number's status.*/

        public static void ParcelTracking()
        {
            string[,] parcels = {
                {"TRK123", "In Transit"},
                {"TRK456", "Out for Delivery"},
                {"TRK789", "Delivered"}
            };

            Console.Write("Enter Tracking Number: ");
            string input = Console.ReadLine();
            bool found = false;
            for (int i = 0; i < parcels.GetLength(0); i++)
            {
                if (parcels[i, 0] == input)
                {
                    Console.WriteLine($"Status: Parcel {parcels[i, 1]}");
                    found = true;
                    break;
                }
            }
            if (!found)
                Console.WriteLine("Tracking number not found.");
        }

        /* Q10: Customer Data Validation: Write a function which takes 2 parameters, data-denotes the data and
        detail-denotes if it is name address or phone number.Validate customer information based on
        following critirea. Ensure that names contain only letters and are properly capitalized, addresses do not
        contain special characters, and phone numbers follow a specific format (e.g., ###-###-####).*/

        public static void ValidateCustomerData(string data, string detail)
        {
            switch (detail.ToLower())
            {
                case "name":
                    if (Regex.IsMatch(data, @"^[A-Z][a-zA-Z\s]*$"))
                        Console.WriteLine("Valid Name");
                    else
                        Console.WriteLine("Invalid Name");
                    break;

                case "address":
                    if (Regex.IsMatch(data, @"^[a-zA-Z0-9\s,]+$"))
                        Console.WriteLine("Valid Address");
                    else
                        Console.WriteLine("Invalid Address");
                    break;

                case "phone":
                    if (Regex.IsMatch(data, @"^\d{3}-\d{3}-\d{4}$"))
                        Console.WriteLine("Valid Phone Number");
                    else
                        Console.WriteLine("Invalid Phone Number");
                    break;

                default:
                    Console.WriteLine("Unknown Detail Type");
                    break;
            }
        }

        /* Q11: Address Formatting: Develop a function that takes an address as input (street, city, state, zip code)
        and formats it correctly, including capitalizing the first letter of each word and properly formatting the
        zip code.*/

        public static void FormatAddress(string street, string city, string state, string zip)
        {
            string formatted = $"{Capitalize(street)}, {Capitalize(city)}, {Capitalize(state)} - {zip}";
            Console.WriteLine("Formatted Address: " + formatted);
        }

        private static string Capitalize(string input)
        {
            string[] words = input.ToLower().Split(' ');
            for (int i = 0; i < words.Length; i++)
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            return string.Join(" ", words);
        }

        /* Q12: Order Confirmation Email: Create a program that generates an order confirmation email. The email
        should include details such as the customer's name, order number, delivery address, and expected
        delivery date.*/

        public static void GenerateOrderEmail(string name, string orderNo, string address, string deliveryDate)
        {
            Console.WriteLine($"\nDear {name},");
            Console.WriteLine($"Your order #{orderNo} has been confirmed.");
            Console.WriteLine($"Delivery Address: {address}");
            Console.WriteLine($"Expected Delivery: {deliveryDate}");
            Console.WriteLine("Thank you for shopping with us!\n");
        }

        /* Q13: Calculate Shipping Costs: Develop a function that calculates the shipping cost based on the distance
        between two locations and the weight of the parcel.You can use string inputs for the source and
        destination addresses.*/

        public static int GetDistance(string source, string destination)
        {
            Dictionary<string, Dictionary<string, int>> distanceMap = new Dictionary<string, Dictionary<string, int>>()
        {
            { "Chennai", new Dictionary<string, int> {
                { "Madurai", 460 }, { "Coimbatore", 500 }, { "Trichy", 330 }
            }},
            { "Madurai", new Dictionary<string, int> {
                { "Chennai", 460 }, { "Coimbatore", 210 }, { "Trichy", 130 }
            }},
            { "Coimbatore", new Dictionary<string, int> {
                { "Chennai", 500 }, { "Madurai", 210 }, { "Trichy", 220 }
            }},
            { "Trichy", new Dictionary<string, int> {
                { "Chennai", 330 }, { "Madurai", 130 }, { "Coimbatore", 220 }
            }}
        };

            if (distanceMap.ContainsKey(source) && distanceMap[source].ContainsKey(destination))
            {
                return distanceMap[source][destination];
            }
            else
            {
                return -1; 
            }
        }

        public static double CalculateShippingCost(string source, string destination, double weightInKg)
        {
            int distance = GetDistance(source, destination);

            if (distance == -1)
            {
                Console.WriteLine("Invalid source or destination.");
                return 0.0;
            }

            double costPerKmPerKg = 0.5;
            double totalCost = distance * weightInKg * costPerKmPerKg;

            return totalCost;
        }


        /* Q14: Password Generator: Create a function that generates secure passwords for courier system
        accounts.Ensure the passwords contain a mix of uppercase letters, lowercase letters, numbers, and
        special characters.*/

        public static void GeneratePassword(int length)
        {
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string special = "!@#$%^&*";
            string allChars = upper + lower + digits + special;

            Random rand = new Random();
            StringBuilder password = new StringBuilder();

            password.Append(upper[rand.Next(upper.Length)]);
            password.Append(lower[rand.Next(lower.Length)]);
            password.Append(digits[rand.Next(digits.Length)]);
            password.Append(special[rand.Next(special.Length)]);

            for (int i = 4; i < length; i++)
                password.Append(allChars[rand.Next(allChars.Length)]);

            Console.WriteLine("Generated Password: " + password);
        }

        /* Q15: Find Similar Addresses: Implement a function that finds similar addresses in the system. This can be
        useful for identifying duplicate customer entries or optimizing delivery routes.Use string functions to
        implement this.*/

        public static void FindSimilarAddresses(string[] addresses, string target)
        {
            Console.WriteLine($"Searching for similar addresses to: {target}\n");

            foreach (string address in addresses)
            {
                if (address.Contains(target, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine(address);
            }
        }
    }
}