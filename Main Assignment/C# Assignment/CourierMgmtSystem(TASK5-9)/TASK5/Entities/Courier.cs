using System;

namespace CourierManagementSystem.Entities
{
    public class Courier
    {
        private int courierID;
        private string senderName;
        private string senderAddress;
        private string receiverName;
        private string receiverAddress;
        private double weight;
        private string status;
        private string trackingNumber;
        private DateTime sentDate;       
        private DateTime deliveryDate;
        private int deliveryLocationId;
        private int serviceId;
        private int userId;
        private int assignedStaffId;

        public Courier()

        {
            status = "In Transit";
            sentDate = DateTime.Now;
            DeliveryDate = DateTime.Now.AddDays(3);
        }

        public Courier(int courierID, string senderName, string senderAddress, string receiverName,
            string receiverAddress, double weight, string status,string TrackingNumber, DateTime sentDate, DateTime deliveryDate,int DeliveryLocationId, int ServiceId, int userId, int assignedStaffId)
        {
            this.courierID = courierID;
            this.senderName = senderName;
            this.senderAddress = senderAddress;
            this.receiverName = receiverName;
            this.receiverAddress = receiverAddress;
            this.weight = weight;
            this.status = status;
            this.sentDate = sentDate;         
            this.deliveryDate = deliveryDate;
            this.deliveryLocationId = DeliveryLocationId;
            this.serviceId = ServiceId;
            this.userId = userId;
            this.assignedStaffId = assignedStaffId;
            this.trackingNumber = TrackingNumber;
        }

        public int CourierID { get => courierID; set => courierID = value; }
        public string SenderName { get => senderName; set => senderName = value; }
        public string SenderAddress { get => senderAddress; set => senderAddress = value; }
        public string ReceiverName { get => receiverName; set => receiverName = value; }
        public string ReceiverAddress { get => receiverAddress; set => receiverAddress = value; }
        public double Weight { get => weight; set => weight = value; }
        public string Status { get => status; set => status = value; }
        public DateTime SentDate { get => sentDate; set => sentDate = value; }      
        public DateTime DeliveryDate { get => deliveryDate; set => deliveryDate = value; }
        public int DeliveryLocationID { get => deliveryLocationId; set => deliveryLocationId = value; }
        public int ServiceID { get => serviceId; set => serviceId = value; }
        public string TrackingNumber { get; set; }
        public int UserId { get => userId; set => userId = value; }
        public int AssignedStaffId { get => assignedStaffId; set => assignedStaffId = value; }

        public override string ToString()
        {
            return $"Courier [ID={courierID}, Sender={senderName}, Receiver={receiverName}, Tracking={trackingNumber}, Status={status}]";
        }
    }
}