using System;
namespace InsuranceManagementSystem.entity
{
	public class Client
	{
        private int clientId;
        private string clientName;
        private string contactInfo;
        private Policy policy;

        public int ClientId { get => clientId; set => clientId = value; }
        public string ClientName { get => clientName; set => clientName = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
        public Policy Policy { get => policy; set => policy = value; }

        public Client() { }

        public Client(int clientId, string clientName, string contactInfo, Policy policy)
        {
            this.ClientId = clientId;
            this.ClientName = clientName;
            this.ContactInfo = contactInfo;
            this.Policy = policy;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ClientId: {ClientId}, ClientName: {ClientName}, ContactInfo: {ContactInfo}");
        }
    }
}

