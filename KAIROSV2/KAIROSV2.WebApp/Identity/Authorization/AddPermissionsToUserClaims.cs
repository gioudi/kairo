using KAIROSV2.Business.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity.Authorization
{
    public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<TUUsuario>
    {
        private readonly RoleManager<TURole> _roleManager;

        public AddPermissionsToUserClaims(UserManager<TUUsuario> userManager, IOptions<IdentityOptions> optionsAccessor,
            RoleManager<TURole> roleManager)
            : base(userManager, optionsAccessor)
        {
            _roleManager = roleManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUUsuario user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var role = await _roleManager.FindByIdAsync(user.RolId);
            var permissions = await _roleManager.GetClaimsAsync(role);
            identity.AddClaim(permissions.FirstOrDefault());
            return identity;
        }
    }

}