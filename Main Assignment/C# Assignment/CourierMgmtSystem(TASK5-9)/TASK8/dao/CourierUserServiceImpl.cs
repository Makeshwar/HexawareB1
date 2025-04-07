using System;
using System.Collections.Generic;
using CourierManagementSystem.Entities;
using entities;
using myexceptions;

namespace CourierManagementSystem.dao
{
    public class CourierUserServiceImpl : ICourierUserService
    {
        protected CourierCompany companyObj;
        private static int trackingSeed = 1000;

        public CourierUserServiceImpl()
        {
            companyObj = new CourierCompany(); 
        }

        public string PlaceOrder(Courier courierObj)
        {
            string trackingNumber = "TRK" + trackingSeed++;
            courierObj.TrackingNumber = trackingNumber;
            courierObj.Status = "YetToTransit";

           
            Console.WriteLine("Simulated: Courier added to CourierCompany object array.");

            return trackingNumber;
        }

        public string GetOrderStatus(string trackingNumber)
        {
            throw new TrackingNumberNotFoundException("Tracking number not found (object array simulation).");
        }

        public bool CancelOrder(string trackingNumber)
        {
            throw new TrackingNumberNotFoundException("Cancel failed: tracking number not found.");
        }

        public List<Courier> GetAssignedOrder(long courierStaffId)
        {
            return new List<Courier>(); // Simulated empty list
        }
    }
}