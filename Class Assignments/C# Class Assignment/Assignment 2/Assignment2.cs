using System;
namespace CS_DAILYASSIGNMENT
{
	public class Assignment2
	{

        // Q 1. Write a C# Sharp program to swap two numbers.

        public static void SwapNumbers()
        {
            Console.Write("Enter a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter b: ");
            int b = Convert.ToInt32(Console.ReadLine());

            int temp = a;
            a = b;
            b = temp;

            Console.WriteLine($"After swapping: a = {a}, b = {b}");
        }

        // Q 2. Write a C# program that takes a number as input and displays it four times in a row (separated by blank spaces), and then four times in the next row, with no separation. You should do it twice: Use the console. Write and use {0}.


        public static void DisplayNumberFourTimes()
        {
            Console.Write("Enter a digit: ");
            int value = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("{0} {0} {0} {0}", value);
            Console.WriteLine("{0}{0}{0}{0}", value);

            Console.WriteLine("{0} {0} {0} {0}", value);
            Console.WriteLine("{0}{0}{0}{0}", value);
        }

        // Q 3. Write a C# Sharp program to read any day number as an integer and display the name of the day as a word.


        public static void DayOfWeek()
        {
            Console.Write("Enter day number (1-7): ");
            int day = Convert.ToInt32(Console.ReadLine());

            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            if (day >= 1 && day <= 7)
                Console.WriteLine("Day is: " + days[day - 1]);
            else
                Console.WriteLine("Invalid day number");
        }

        //  Arrays  :

        // Q 1.    Write a  Program to assign integer values to an array  and then print the following


        public static void ArrayAverageMinMax()
        {
            int[] numbers = new int[10];
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Enter mark {i + 1}: ");
                numbers[i] = Convert.ToInt32(Console.ReadLine());
            }
            int sum = 0, min = numbers[0], max = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
                if (numbers[i] < min)
                    min = numbers[i];
                if (numbers[i] > max)
                    max = numbers[i];
            }

            double average = (double)sum / numbers.Length;

            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");
        }

        // Q 2.	Write a program in C# to accept ten marks and display the following


        public static void AnalyzeMarks()
        {
            int[] marks = new int[10];
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Enter mark {i + 1}: ");
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }

            // Calculate total, average, min, max
            int total = 0, min = marks[0], max = marks[0];
            for (int i = 0; i < marks.Length; i++)
            {
                total += marks[i];
                if (marks[i] < min)
                    min = marks[i];
                if (marks[i] > max)
                    max = marks[i];
            }

            double average = (double)total / marks.Length;

           

            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Minimum: {min}");
            Console.WriteLine($"Maximum: {max}");

            Array.Sort(marks);
            Console.WriteLine("Marks Ascending: " + string.Join(", ", marks));

            Array.Reverse(marks);
            Console.WriteLine("Marks Descending: " + string.Join(", ", marks));
        }

        // Q 3.  Write a C# Sharp program to copy the elements of one array into another array.(do not use any inbuilt functions)

        public static void CopyArray()
        {
            int[] original = new int[5];
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter number {i + 1}: ");
                original[i] = Convert.ToInt32(Console.ReadLine());
            }
            int[] copy = new int[original.Length];

            for (int i = 0; i < original.Length; i++)
            {
                copy[i] = original[i];
            }

            Console.WriteLine("Copied array: " + string.Join(", ", copy));
        }
    }
}

