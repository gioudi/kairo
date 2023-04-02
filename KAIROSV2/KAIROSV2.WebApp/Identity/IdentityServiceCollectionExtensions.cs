using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddIdentityKAIROS<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
            => services.AddIdentityKAIROS<TUser, TRole>(setupAction: null);

        public static IdentityBuilder AddIdentityKAIROS<TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TUser : class
            where TRole : class
        {
            //// Services used by identity
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //})
            //.AddCookie(IdentityConstants.ApplicationScheme, o =>
            //{
            //    o.LoginPath = new PathString("/Account/Login");
            //})
            // .AddCookie(IdentityConstants.ExternalScheme, o =>
            // {
            //     o.Cookie.Name = IdentityConstants.ExternalScheme;
            //     o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            // })
            //  .AddCookie(IdentityConstants.TwoFactorRememberMeScheme, o =>
            //  {
            //      o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme;
            //      o.Events = new CookieAuthenticationEvents
            //      {
            //          OnValidatePrincipal = SecurityStampValidator.ValidateAsync<ITwoFactorSecurityStampValidator>
            //      };
            //  })
            //.AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
            //{
            //    o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
            //    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //});

            //// Hosting doesn't add IHttpContextAccessor by default
            //services.AddHttpContextAccessor();
            //// Identity services
            //services.TryAddScoped<SignInManager<TUser>>();

            //return services.AddIdentityCore<TUser>(setupAction);
            return services.AddIdentity<TUser, TRole>(setupAction);

        }
    }
}
