using System.Collections.Generic;
using CourierManagementSystem.Entities;

namespace entities
{
    public class CourierCompanyCollection
    {
        public string CompanyName { get; set; }
        public List<Courier> CourierDetails { get; set; }
        public List<Employee> EmployeeDetails { get; set; }
        public List<Location> LocationDetails { get; set; }

        public CourierCompanyCollection()
        {
            CourierDetails = new List<Courier>();
            EmployeeDetails = new List<Employee>();
            LocationDetails = new List<Location>();
        }

        public CourierCompanyCollection(string companyName) : this()
        {
            this.CompanyName = companyName;
        }
    }
}