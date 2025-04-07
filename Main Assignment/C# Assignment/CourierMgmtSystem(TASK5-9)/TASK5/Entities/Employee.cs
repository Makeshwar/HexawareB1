using System;
namespace CourierManagementSystem.Entities
{
	public class Employee
	{
        private int employeeID;
        private string employeeName;
        private string email;
        private string contactNumber;
        private string role;
        private double salary;

        public Employee() { }

        public Employee(int id, string name, string email, string contact, string role, double salary)
        {
            this.employeeID = id;
            this.employeeName = name;
            this.email = email;
            this.contactNumber = contact;
            this.role = role;
            this.salary = salary;
        }

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public string Email { get => email; set => email = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string Role { get => role; set => role = value; }
        public double Salary { get => salary; set => salary = value; }

        public override string ToString()
        {
            return $"Employee [ID={employeeID}, Name={employeeName}, Role={role}, Email={email}]";
        }
    }
}

