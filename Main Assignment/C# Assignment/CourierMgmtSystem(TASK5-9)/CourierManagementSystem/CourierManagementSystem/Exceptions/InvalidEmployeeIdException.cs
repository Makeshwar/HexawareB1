using System;

namespace CourierManagementSystem.Exceptions
{
    public class InvalidEmployeeIdException : Exception
    {
        public InvalidEmployeeIdException() : base("Invalid employee ID.") { }

        public InvalidEmployeeIdException(string message) : base(message) { }
    }
}