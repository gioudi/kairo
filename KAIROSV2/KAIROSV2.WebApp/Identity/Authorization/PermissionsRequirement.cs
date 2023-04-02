using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity.Authorization
{
    internal class PermissionsRequirement : IAuthorizationRequirement
    {
        public Permissions PermissionId { get; private set; }

        public PermissionsRequirement(Permissions permissionId) { PermissionId = permissionId; }
    }
}
