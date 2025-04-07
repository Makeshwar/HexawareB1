using System;

namespace myexceptions
{
    public class InvalidEmployeeIdException : Exception
    {
        public InvalidEmployeeIdException() : base("Invalid employee ID.") { }

        public InvalidEmployeeIdException(string message) : base(message) { }
    }
}