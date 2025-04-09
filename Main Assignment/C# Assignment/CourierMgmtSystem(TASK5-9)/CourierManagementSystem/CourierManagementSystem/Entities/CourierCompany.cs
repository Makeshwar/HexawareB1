//TASK 5
//Courier Company Class

using System;
using System.Collections.Generic;

namespace CourierManagementSystem.Entities
{
    public class CourierCompany
    {
        private string companyName;
        private List<Courier> courierDetails;
        private List<Employee> employeeDetails;
        private List<Location> locationDetails;

        public CourierCompany()
        {
            courierDetails = new List<Courier>();
            employeeDetails = new List<Employee>();
            locationDetails = new List<Location>();
        }

        public CourierCompany(string companyName, List<Courier> courierDetails, List<Employee> employeeDetails, List<Location> locationDetails)
        {
            this.companyName = companyName;
            this.courierDetails = courierDetails;
            this.employeeDetails = employeeDetails;
            this.locationDetails = locationDetails;
        }

        public string CompanyName { get => companyName; set => companyName = value; }
        public List<Courier> CourierDetails { get => courierDetails; set => courierDetails = value; }
        public List<Employee> EmployeeDetails { get => employeeDetails; set => employeeDetails = value; }
        public List<Location> LocationDetails { get => locationDetails; set => locationDetails = value; }

        public override string ToString()
        {
            return $"CourierCompany [Name={companyName}, Couriers={courierDetails.Count}, Employees={employeeDetails.Count}, Locations={locationDetails.Count}]";
        }
    }
}
