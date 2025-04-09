using System;
namespace InsuranceManagementSystem.entity
{
	public class Claim
	{
        private int claimId;
        private string claimNumber;
        private DateTime dateFiled;
        private decimal claimAmount;
        private string status;
        private Policy policy;
        private Client client;

        public int ClaimId { get => claimId; set => claimId = value; }
        public string ClaimNumber { get => claimNumber; set => claimNumber = value; }
        public DateTime DateFiled { get => dateFiled; set => dateFiled = value; }
        public decimal ClaimAmount { get => claimAmount; set => claimAmount = value; }
        public string Status { get => status; set => status = value; }
        public Policy Policy { get => policy; set => policy = value; }
        public Client Client { get => client; set => client = value; }
      

        public Claim() { }

        public Claim(int claimId, string claimNumber, DateTime dateFiled, decimal claimAmount, string status, Policy policy, Client client)
        {
            this.ClaimId = claimId;
            this.ClaimNumber = claimNumber;
            this.DateFiled = dateFiled;
            this.ClaimAmount = claimAmount;
            this.Status = status;
            this.Policy = policy;
            this.Client = client;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ClaimId: {ClaimId}, ClaimNumber: {ClaimNumber}, DateFiled: {DateFiled.ToString("yyyy-MM-dd")}, ClaimAmount: {ClaimAmount}, Status: {Status}");
            
        }
    }
}

