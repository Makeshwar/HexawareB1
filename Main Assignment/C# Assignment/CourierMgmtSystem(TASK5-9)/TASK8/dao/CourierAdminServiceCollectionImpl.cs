using System;
using CourierManagementSystem.Entities;
using entities;
using myexceptions;

namespace CourierManagementSystem.dao
{
    public class CourierAdminServiceCollectionImpl : CourierUserServiceCollectionImpl, ICourierAdminService
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