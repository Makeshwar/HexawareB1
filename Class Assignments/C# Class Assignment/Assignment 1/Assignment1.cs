using System;
namespace CS_DAILYASSIGNMENT
{
	public class Assignment1
	{
        // Q 1. Write a C# Sharp program to accept two integers and check whether they are equal or not.

        public static void CheckEquality()
        {
            Console.Write("Input 1st number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input 2nd number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            if (num1 == num2)
                Console.WriteLine($"{num1} and {num2} are equal");
            else
                Console.WriteLine($"{num1} and {num2} are not equal");
        }

        // Q 2. Write a C# Sharp program to check whether a given number is positive or negative. 

        public static void CheckSign()
        {
            Console.Write("Input a number: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num >= 0)
                Console.WriteLine($"{num} is a positive number");
            else
                Console.WriteLine($"{num} is a negative number");
        }

        // Q 3. Write a C# Sharp program that takes two numbers as input and performs all operations (+,-,*,/) on them and displays the result of that operation. 

        public static void ArithmeticOperations()
        {
            Console.Write("Input first number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input operation (+, -, *, /): ");
            char op = Convert.ToChar(Console.ReadLine());

            Console.Write("Input second number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case '+':
                    Console.WriteLine($"{a} + {b} = {a + b}");
                    break;
                case '-':
                    Console.WriteLine($"{a} - {b} = {a - b}");
                    break;
                case '*':
                    Console.WriteLine($"{a} * {b} = {a * b}");
                    break;
                case '/':
                    if (b != 0)
                        Console.WriteLine($"{a} / {b} = {(float)a / b}");
                    else
                        Console.WriteLine("Cannot divide by zero.");
                    break;
                default:
                    Console.WriteLine("Invalid operator.");
                    break;
            }
        }

        // Q 4. Write a C# Sharp program that prints the multiplication table of a number as input.

        public static void MultiplicationTable()
        {
            Console.Write("Enter the number: ");
            int num = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"{num} * {i} = {num * i}");
            }
        }

        // Q 5.  Write a C# program to compute the sum of two given integers. If two values are the same, return the triple of their sum.

        public static void SumOrTriple()
        {
            Console.Write("Input 1st number: ");
            int x = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input 2nd number: ");
            int y = Convert.ToInt32(Console.ReadLine());

            int sum = x + y;
            if (x == y)
                Console.WriteLine($"Values are equal. Triple of sum: {3 * sum}");
            else
                Console.WriteLine($"Sum: {sum}");
        }
    }
}

