using Microsoft.AspNetCore.Authorization;

namespace Base.Authorizations
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public IEnumerable<string> Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            this.Permission = permission.Split(',');
        }
    }
}