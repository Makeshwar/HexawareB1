namespace entity
{
    public class Employee
    {
        private int employeeId;

        private string name;

        private string department;

        private string email;

        private string password;

        public Employee() { }

        public Employee(int employeeId, string name, string department, string email, string password)
        {
            this.employeeId = employeeId;

            this.name = name;

            this.department = department;

            this.email = email;

            this.password = password;
        }

        public int EmployeeId

        { get => employeeId; set => employeeId = value; }

        public string Name

        { get => name; set => name = value; }

        public string Department

        { get => department; set => department = value; }

        public string Email

        { get => email; set => email = value; }

        public string Password

        { get => password; set => password = value; }
    }
}
