using Microsoft.AspNetCore.Authorization;

namespace Base.Authorizations
{
    public class PermissionRequireAttribute: AuthorizeAttribute
    {
        const string POLICY_PREFIX = "PERMISSION_CODE:";

        public PermissionRequireAttribute(string permission) 
        {
            this.Permission = permission;
            this.Policy = $"{POLICY_PREFIX}{permission}";
        }

        public string Permission { get; private set; }
    }
}