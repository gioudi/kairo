using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity.Authorization
{
    internal class PermissionsAuthorizationHandler : AuthorizationHandler<PermissionsRequirement>
    {
        // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            var permissionsClaim =
                context.User.Claims.SingleOrDefault(c => c.Type == "http://schemas.primax.co/identity/claims/permissions");

            if (permissionsClaim == null)
                return Task.CompletedTask;

            //TODO: if allowed
            var usersPermissions = permissionsClaim.Value.DecompressPermissionsFromString();
            if (usersPermissions.Contains((int)requirement.PermissionId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}