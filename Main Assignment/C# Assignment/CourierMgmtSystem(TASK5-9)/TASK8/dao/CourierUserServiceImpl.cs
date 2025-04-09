using System;
using System.Collections.Generic;
using CourierManagementSystem.Entities;
using CourierManagementSystem.Exceptions;

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
            return "TRK" + trackingSeed++;
        }

        public string GetOrderStatus(string trackingNumber)
        {
            throw new TrackingNumberNotFoundException("Tracking number not found.");
        }

        public bool CancelOrder(string trackingNumber)
        {
            throw new TrackingNumberNotFoundException("Cancel failed: tracking number not found.");
        }

        public List<Courier> GetAssignedOrder(int courierStaffId)
        {
            return new List<Courier>();
        }
    }
}