using System;
using System.Collections.Generic;
using System.Linq;
using CourierManagementSystem.Entities;
using myexceptions;

namespace CourierManagementSystem.dao
{
    public class CourierUserServiceCollectionImpl : ICourierUserService
    {
        public CourierCompanyCollection companyObj;

        public CourierUserServiceCollectionImpl()
        {
            companyObj = new CourierCompanyCollection("Hexaware Courier");
        }

        public string PlaceOrder(Courier courierObj)
        {
            companyObj.CourierDetails.Add(courierObj);

            return courierObj.TrackingNumber;
        }

        public string GetOrderStatus(string trackingNumber)
        {
            var courier = companyObj.CourierDetails
                .FirstOrDefault(c => c.TrackingNumber == trackingNumber);

            if (courier == null)
            {
                throw new TrackingNumberNotFoundException("Tracking number not found.");
            }

            return courier.Status;
        }

        public bool CancelOrder(string trackingNumber)
        {
            var courier = companyObj.CourierDetails
                .FirstOrDefault(c => c.TrackingNumber == trackingNumber);

            if (courier == null)
            {
                throw new TrackingNumberNotFoundException("Cancel failed: tracking number not found.");
            }

            if (courier.Status.ToLower() == "delivered")
            {
                return false; 
            }

            courier.Status = "Cancelled";
            return true;
        }

        public List<Courier> GetAssignedOrder(int courierStaffId)
        {
            return companyObj.CourierDetails
                .Where(c => c.AssignedStaffId == courierStaffId)
                .ToList();
        }
    }
}