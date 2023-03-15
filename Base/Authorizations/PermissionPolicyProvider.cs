using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Base.Authorizations
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "PERMISSION_CODE:";

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX)) {
                 var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                 policy.AddRequirements(new PermissionRequirement(policyName.Substring(POLICY_PREFIX.Length)));
                return Task.FromResult(policy.Build());
            }

             return Task.FromResult<AuthorizationPolicy>(null); 
        }
    }
}