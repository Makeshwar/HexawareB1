using System;
using CourierManagementSystem.Entities;
using CourierManagementSystem.Exceptions;

namespace CourierManagementSystem.dao
{
    public class CourierAdminServiceCollectionImpl : CourierUserServiceCollectionImpl, ICourierAdminService
    {
        private static int employeeIdSeed = 100;

        public int AddCourierStaff(Employee obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Employee object cannot be null");

            obj.EmployeeID = employeeIdSeed++;
            companyObj.EmployeeDetails.Add(obj);
            return obj.EmployeeID;
        }
    }
}