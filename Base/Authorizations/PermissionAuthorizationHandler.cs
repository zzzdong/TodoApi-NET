using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Base.Authorizations
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var ok = false;
            foreach (var p in requirement.Permission)
            {
                if (context.User.HasClaim(ClaimTypes.Role, p))
                {
                    ok = true;
                    break;
                }
            }

            if (ok)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}