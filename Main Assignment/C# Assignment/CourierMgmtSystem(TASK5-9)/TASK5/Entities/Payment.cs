//TASK 5 Object Oriented Programming
//Payment Class

using System;
namespace CourierManagementSystem.Entities
{
	public class Payment
	{
        private long paymentID;
        private long courierID;
        private double amount;
        private DateTime paymentDate;

        public Payment() { }

        public Payment(long paymentId, long courierId, double amount, DateTime paymentDate)
        {
            this.paymentID = paymentId;
            this.courierID = courierId;
            this.amount = amount;
            this.paymentDate = paymentDate;
        }

        public long PaymentID { get => paymentID; set => paymentID = value; }
        public long CourierID { get => courierID; set => courierID = value; }
        public double Amount { get => amount; set => amount = value; }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }

        public override string ToString()
        {
            return $"Payment [ID={paymentID}, CourierID={courierID}, Amount={amount}, Date={paymentDate.ToShortDateString()}]";
        }
    }
}

