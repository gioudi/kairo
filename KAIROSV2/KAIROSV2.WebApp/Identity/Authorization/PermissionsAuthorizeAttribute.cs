using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity.Authorization
{
    internal class PermissionsAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Permissions";

        public PermissionsAuthorizeAttribute(Permissions permissionId) => PermissionId = permissionId;

        // Get or set the permission property by manipulating the underlying Policy property
        public Permissions PermissionId
        {
            get
            {
                if (Enum.TryParse<Permissions>(Policy.Substring(POLICY_PREFIX.Length), out var permissionId))
                {
                    return permissionId;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}