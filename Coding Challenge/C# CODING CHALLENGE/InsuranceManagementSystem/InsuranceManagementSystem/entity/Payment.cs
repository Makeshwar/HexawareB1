using System;
namespace InsuranceManagementSystem.entity
{
	public class Payment
	{
        private int paymentId;
        private DateTime paymentDate;
        private decimal paymentAmount;
        private Client client;

        public int PaymentId { get => paymentId; set => paymentId = value; }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
        public decimal PaymentAmount { get => paymentAmount; set => paymentAmount = value; }
        public Client Client { get => client; set => client = value; }

        public Payment() { }

        public Payment(int paymentId, DateTime paymentDate, decimal paymentAmount, Client client)
        {
            this.PaymentId = paymentId;
            this.PaymentDate = paymentDate;
            this.PaymentAmount = paymentAmount;
            this.Client = client;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"PaymentId: {PaymentId}, PaymentDate: {PaymentDate.ToString("yyyy-MM-dd")}, PaymentAmount: {PaymentAmount}");
        }
    }
}

