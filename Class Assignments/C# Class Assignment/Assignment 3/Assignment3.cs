using System;
using System.ComponentModel;
using static CS_DAILYASSIGNMENT.Assignment3;

namespace CS_DAILYASSIGNMENT
{
    public class Assignment3
    {

        // Strings Assignment :

        public class StringAssign
        {

            // Q 1.	Write a program in C# to accept a word from the user and display the length of it.

            public static void WordLength()
            {
                Console.Write("Enter a word: ");
                string word = Console.ReadLine();
                Console.WriteLine("Length of the word: " + word.Length);
            }

            // Q 2.	Write a program in C# to accept a word from the user and display the reverse of it. 

            public static void ReverseWord()
            {
                Console.Write("Enter a word: ");
                string word = Console.ReadLine();
                string reverse = "";
                for (int i = word.Length - 1; i >= 0; i--)
                    reverse += word[i];
                Console.WriteLine("Reversed word: " + reverse);
            }

            // Q 3.	Write a program in C# to accept two words from user and find out if they are same.

            public static void CompareWords()
            {
                Console.Write("Enter first word: ");
                string word1 = Console.ReadLine();
                Console.Write("Enter second word: ");
                string word2 = Console.ReadLine();
                if (word1.Equals(word2, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine("Words are the same.");
                else
                    Console.WriteLine("Words are different.");
            }
        }

        // Inheritance :

        /* Q 1. Create a class called student which has data members like rollno, name, class, Semester, branch, int[] marks = new int marks[5] (marks of 5 subjects )

        -Pass the details of student like rollno, name, class, SEM, branch in constructor

        -For marks write a method called GetMarks() and give marks for all 5 subjects

        -Write a method called displayresult, which should calculate the average marks

        -If marks of any one subject is less than 35 print result as failed

        -If marks of all subject is >35,but average is < 50 then also print result as failed

        -If avg > 50 then print result as passed.

        -Write a DisplayData() method to display all object members values.*/

        public class Student
        {
            private int rollNo;
            private string name;
            private string className;
            private int semester;
            private string branch;
            private int[] marks = new int[5];

            public Student(int rollNo, string name, string className, int semester, string branch)
            {
                this.rollNo = rollNo;
                this.name = name;
                this.className = className;
                this.semester = semester;
                this.branch = branch;
            }

            public void GetMarks()
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"Enter marks for subject {i + 1}: ");
                    marks[i] = int.Parse(Console.ReadLine());
                }
            }

            public void DisplayResult()
            {
                int total = 0;
                bool failed = false;
                foreach (int mark in marks)
                {
                    if (mark < 35)
                        failed = true;
                    total += mark;
                }

                double avg = total / 5.0;

                if (failed || avg < 50)
                    Console.WriteLine("Result: Failed");
                else
                    Console.WriteLine("Result: Passed");
            }

            public void DisplayData()
            {
                Console.WriteLine($"Roll No: {rollNo}, Name: {name}, Class: {className}, Semester: {semester}, Branch: {branch}");
            }
        }

        // Interface

        /* Q 1.  Create an Interface IStudent with StudentId, Name and Fees as Properties, void ShowDetails() as its method. Create 2 classes Dayscholar and Resident that implements the interface Properties and Methods. (Fees for day scholar will be different from resident)
        resident student will also have accommodation fees */

        interface IStudent
        {
            int StudentId { get; set; }
            string Name { get; set; }
            double Fees { get; set; }
            void ShowDetails();
        }

        public class DayScholar : IStudent
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public double Fees { get; set; }

            public DayScholar(int id, string name, double fees)
            {
                StudentId = id;
                Name = name;
                Fees = fees;
            }

            public void ShowDetails()
            {
                Console.WriteLine($"DayScholar - ID: {StudentId}, Name: {Name}, Fees: {Fees}");
            }
        }

        public class Resident : IStudent
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public double Fees { get; set; }
            public double AccommodationFees { get; set; }

            public Resident(int id, string name, double fees, double accFees)
            {
                StudentId = id;
                Name = name;
                Fees = fees;
                AccommodationFees = accFees;
            }

            public void ShowDetails()
            {
                Console.WriteLine($"Resident - ID: {StudentId}, Name: {Name}, Total Fees: {Fees + AccommodationFees}");
            }
        }

        // User Defined Exception :

        // Q 1. You are working for ABC Bank Ltd.  on the funds Transfer module. You need to handle the situation when the customer wishes to transfer more money than he has in his/her account

        public class InsufficientFundsException : Exception
        {
            public InsufficientFundsException(string message) : base(message) { }
        }

        public class BankAccount
        {
            public string CustomerName { get; set; }
            public double Balance { get; set; }

            public BankAccount(string name, double balance)
            {
                CustomerName = name;
                Balance = balance;
            }

            public void Transfer(double amount)
            {
                if (amount > Balance)
                {
                    throw new InsufficientFundsException("Transfer failed: Not enough balance.");
                }
                else
                {
                    Balance -= amount;
                    Console.WriteLine($"Transfer successful. Remaining Balance: {Balance}");
                }
            }
        }
    }
}


