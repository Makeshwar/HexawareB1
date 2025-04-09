using System;
namespace InsuranceManagementSystem.entity
{
	public class Policy
	{
        private int policyId;
        private string policyNumber;
        private string policyType;
        private DateTime startDate;
        private DateTime endDate;

        public int PolicyId { get => policyId; set => policyId = value; }
        public string PolicyNumber { get => policyNumber; set => policyNumber = value; }
        public string PolicyType { get => policyType; set => policyType = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public Policy() { }

        public Policy(int policyId, string policyNumber, string policyType, DateTime startDate, DateTime endDate)
        {
            this.PolicyId = policyId;
            this.PolicyNumber = policyNumber;
            this.PolicyType = policyType;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"PolicyId: {PolicyId}, PolicyNumber: {PolicyNumber}, PolicyType: {PolicyType}, StartDate: {StartDate.ToString("yyyy-MM-dd")}, EndDate: {EndDate.ToString("yyyy-MM-dd")}");
        }
    }
}

