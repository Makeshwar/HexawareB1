using System;
using CourierManagementSystem.Entities;
using CourierManagementSystem.Exceptions;
namespace CourierManagementSystem.dao
{
    public class CourierAdminServiceImpl : CourierUserServiceImpl, ICourierAdminService
    {
        private static int employeeIdSeed = 100;

        public int AddCourierStaff(Employee obj)
        {
            obj.EmployeeID = employeeIdSeed++;
            companyObj.EmployeeDetails.Add(obj);
            return obj.EmployeeID;
        }
    }
}