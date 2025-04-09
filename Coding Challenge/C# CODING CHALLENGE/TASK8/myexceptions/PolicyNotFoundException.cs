using System;
namespace InsuranceManagementSystem.myexceptions
{
	
        public class PolicyNotFoundException : Exception
        {
            public PolicyNotFoundException()
                : base("Policy not found with provided ID.") { }

            public PolicyNotFoundException(string message)
                : base(message) { }

            public PolicyNotFoundException(string message, Exception innerException)
                : base(message, innerException) { }
        }
    
}

