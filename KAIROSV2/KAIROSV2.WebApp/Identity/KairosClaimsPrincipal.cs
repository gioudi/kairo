using LightCore.Common.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace KAIROSV2.WebApp.Identity
{
    public class KairosClaimsPrincipal : ICurrentClaimsPrincipal
    {
        public ClaimsPrincipal CurrentClaimsPrincipal { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public KairosClaimsPrincipal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            CurrentClaimsPrincipal = _httpContextAccessor.HttpContext.User;
        }
    }
}
