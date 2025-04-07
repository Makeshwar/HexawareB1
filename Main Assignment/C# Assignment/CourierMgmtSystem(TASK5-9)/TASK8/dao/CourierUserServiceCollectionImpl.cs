using System;
using System.Collections.Generic;
using CourierManagementSystem.Entities;
using entities;
using myexceptions;

namespace CourierManagementSystem.dao
{
    public class CourierUserServiceCollectionImpl : ICourierUserService
    {
        public CourierCompanyCollection companyObj;
        private static int trackingSeed = 1000;

        private Dictionary<string, Courier> trackingMap;

        public CourierUserServiceCollectionImpl()
        {
            companyObj = new CourierCompanyCollection("Hexaware Courier");
            trackingMap = new Dictionary<string, Courier>();
        }

        public string PlaceOrder(Courier courierObj)
        {
            string trackingNumber = "TRK" + trackingSeed++;
            courierObj.TrackingNumber = trackingNumber;
            courierObj.Status = "YetToTransit";

            companyObj.CourierDetails.Add(courierObj);
            trackingMap[trackingNumber] = courierObj;

            return trackingNumber;
        }

        public string GetOrderStatus(string trackingNumber)
        {
            if (!trackingMap.ContainsKey(trackingNumber))
            {
                throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
            }

            return trackingMap[trackingNumber].Status;
        }

        public bool CancelOrder(string trackingNumber)
        {
            if (!trackingMap.ContainsKey(trackingNumber))
            {
                throw new TrackingNumberNotFoundException($"Cannot cancel. Tracking number {trackingNumber} not found.");
            }

            Courier c = trackingMap[trackingNumber];
            companyObj.CourierDetails.Remove(c);
            trackingMap.Remove(trackingNumber);
            return true;
        }

        public List<Courier> GetAssignedOrder(long courierStaffId)
        {
            List<Courier> assignedOrders = new List<Courier>();
            foreach (var courier in companyObj.CourierDetails)
            {
                if (courier.UserId == courierStaffId)
                {
                    assignedOrders.Add(courier);
                }
            }
            return assignedOrders;
        }
    }
}