using System;
using CourierManagementSystem.Entities;
using entities;
using myexceptions;

namespace CourierManagementSystem.dao
{
    public class CourierAdminServiceImpl : CourierUserServiceImpl, ICourierAdminService
    {
        private static int employeeIdSeed = 100;

        public int AddCourierStaff(Employee obj)
        {
            obj.EmployeeID = employeeIdSeed++;

            Console.WriteLine("Simulated: Employee added to array.");

            return obj.EmployeeID;
        }
    }
}