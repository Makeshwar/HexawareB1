using System.Collections.Generic;
using CourierManagementSystem.Entities;

namespace CourierManagementSystem.dao
{
    public interface ICourierUserService
    {
        string PlaceOrder(Courier courierObj);

        string GetOrderStatus(string trackingNumber);

        bool CancelOrder(string trackingNumber);

        List<Courier> GetAssignedOrder(int courierStaffId);
    }
}