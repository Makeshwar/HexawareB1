using System;
using InsuranceManagementSystem.entity;

namespace InsuranceManagementSystem.dao
{
	public interface IPolicyService
	{
        bool CreatePolicy(Policy policy);
        Policy GetPolicy(int policyId);
        List<Policy> GetAllPolicies();
        bool UpdatePolicy(Policy policy);
        bool DeletePolicy(int policyId);
    }
}

