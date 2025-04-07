using static CS_DAILYASSIGNMENT.Assignment3;

namespace CS_DAILYASSIGNMENT;

class Program
{
    public static void Main(string[] args)
    {

        //ASSIGNMENT 1

        Console.WriteLine("Assignment 1");
        Console.WriteLine("Enter your choice (1-5): ");
        Console.WriteLine("1. Check if two integers are equal");
        Console.WriteLine("2. Check if a number is positive or negative");
        Console.WriteLine("3. Perform arithmetic operations");
        Console.WriteLine("4. Print multiplication table");
        Console.WriteLine("5. Sum of two integers or triple if same");

        int choice1 = Convert.ToInt32(Console.ReadLine());

        switch (choice1)
        {
            case 1:
                Assignment1.CheckEquality();
                break;
            case 2:
                Assignment1.CheckSign();
                break;
            case 3:
                Assignment1.ArithmeticOperations();
                break;
            case 4:
                Assignment1.MultiplicationTable();
                break;
            case 5:
                Assignment1.SumOrTriple();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        Console.WriteLine();

        //ASSIGNMENT 2
        Console.WriteLine("Assignment 2 ");
        Console.WriteLine("Enter your choice (1-6): ");
        Console.WriteLine("1. Swap two numbers");
        Console.WriteLine("2. Display number four times");
        Console.WriteLine("3. Day of the week");
        Console.WriteLine("4. Array - Average, Min, Max");
        Console.WriteLine("5. Marks Analysis");
        Console.WriteLine("6. Copy array");

        int choice2 = Convert.ToInt32(Console.ReadLine());

        switch (choice2)
        {
            case 1:
                Assignment2.SwapNumbers();
                break;
            case 2:
                Assignment2.DisplayNumberFourTimes();
                break;
            case 3:
                Assignment2.DayOfWeek();
                break;
            case 4:
                Assignment2.ArrayAverageMinMax();
                break;
            case 5:
                Assignment2.AnalyzeMarks();
                break;
            case 6:
                Assignment2.CopyArray();
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }

        Console.WriteLine();

        //ASSIGNMENT 3

        Console.WriteLine("Assignment 3 ");
        Console.WriteLine("Enter your choice (1-4): ");
        Console.WriteLine("1. Strings Assignment");
        Console.WriteLine("2. Inheritance - Student Marks");
        Console.WriteLine("3. Interface - DayScholar / Resident");
        Console.WriteLine("4. User Defined Exception - Bank Transfer");

        int choice3 = int.Parse(Console.ReadLine());

        Console.WriteLine();

        switch (choice3)
        {
            case 1:
                Console.WriteLine("--- Strings Assignment ---");
                StringAssign.WordLength();
                StringAssign.ReverseWord();
                StringAssign.CompareWords();
                break;

            case 2:
                Console.WriteLine("--- Inheritance - Student ---");
                Student s = new Student(101, "Mandy", "B.Tech", 4, "CSE");
                s.GetMarks();
                s.DisplayResult();
                s.DisplayData();
                break;

            case 3:
                Console.WriteLine("--- Interface - IStudent ---");
                DayScholar ds = new DayScholar(1, "Ram", 30000);
                Resident rs = new Resident(2, "Rahul", 40000, 10000);
                ds.ShowDetails();
                rs.ShowDetails();
                break;

            case 4:
                Console.WriteLine("--- Bank Transfer Simulation ---");
                BankAccount acc = new BankAccount("Makesh", 10000);
                try
                {
                    Console.Write("Enter amount to transfer: ");
                    double amt = double.Parse(Console.ReadLine());
                    acc.Transfer(amt);
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        Console.WriteLine();
    }
}