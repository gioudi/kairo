using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Engines;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Managers;
using KAIROSV2.Data;
using KAIROSV2.Data.Contracts;
using KAIROSV2.Data.Data_Respositories;
using KAIROSV2.WebApp.Identity;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Support.JSONConverters;
using LightCore.Common.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace KAIROSV2.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<KAIROSV2DBContext>(options => options.UseSqlServer("Data Source=BOGSQL000;Initial Catalog=KAIROS2;User ID=kairos2;Password=Appl1c@t10n;"));
            services.AddDbContext<KAIROSV2DBContext>(options => options.UseSqlServer("Data Source=localhost;Initial Catalog=KAIROS2;User ID=sa;Password=STMS.2017;"));

            //Identity Types
            services.Configure<LDAPConfig>(Configuration.GetSection("Ldap"));
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionsPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionsAuthorizationHandler>();
            services.AddScoped<IADAuthenticationService, ADAuthenticationService>();
            services.AddScoped<IUserStore<TUUsuario>, CustomUserStore>();
            services.AddScoped<IRoleStore<TURole>, CustomerRolStore>();
            services.AddIdentityKAIROS<TUUsuario, TURole>()
                .AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();
            services.AddScoped<IUserClaimsPrincipalFactory<TUUsuario>, AddPermissionsToUserClaims>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddTransient<ITablasCorreccionRepository, TablasCorreccionRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAreasRepository, AreasRepository>();
            services.AddTransient<ICabezotesRepository, CabezotesRepository>();
            services.AddTransient<IConductoresRepository, ConductoresRepository>();
            services.AddTransient<ILineasRepository, LineasRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IPermisosRepository, PermisosRepository>();
            services.AddTransient<IProveedoresRepository, ProveedoresRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IRolesPermisosRepository, RolesPermisosRepository>();
            services.AddTransient<ITerminalCompañiaRepository, TerminalCompañiaRepository>();
            services.AddTransient<ITrailersRepository, TrailersRepository>();
            services.AddTransient<IUsuariosRepository, UsuariosRepository>();
            services.AddTransient<IUsuariosTerminalCompañiaRepository, UsuariosTerminalCompañiaRepository>();
            services.AddTransient<IProductosRepository, ProductosRepository>();
            services.AddTransient<ITipoProductoRepository, TipoProductoRepository>();
            services.AddTransient<IClaseProductoRepository, ClaseProductoRepository>();
            services.AddTransient<ITablasCorreccionManager, TablasCorreccionManager>();
            services.AddTransient<IAreasManager, AreasManager>();
            services.AddTransient<ICabezotesManager, CabezotesManager>();
            services.AddTransient<IConductoresManager, ConductoresManager>();
            services.AddTransient<ILineasManager, LineasManager>();
            services.AddTransient<ILogManager, LogManager>();
            services.AddTransient<IProductosManager, ProductosManager>();
            services.AddTransient<IProductosRepository, ProductosRepository>();
            services.AddTransient<IPermisosManager, PermisosManager>();
            services.AddTransient<IProveedoresManager, ProveedoresManager>();
            services.AddTransient<IRolesManager, RolesManager>();
            services.AddTransient<ITerminalesCompañiasManager, TerminalesCompañiasManager>();
            services.AddTransient<ITrailersManager, TrailersManager>();
            services.AddTransient<IConductoresRepository, ConductoresRepository>();
            services.AddTransient<IConductoresManager, ConductoresManager>();
            services.AddTransient<IAreasRepository, AreasRepository>();
            services.AddTransient<IAreasManager, AreasManager>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ILogManager, LogManager>();
            services.AddTransient<ICompañiasRepository, CompañiasRepository>();
            services.AddTransient<ICompañiasManager, CompañiasManager>();
            services.AddTransient<IUsuariosManager, UsuariosManager>();
            services.AddTransient<IProductosManager, ProductosManager>();
            services.AddTransient<IAtributosProductoRepository, AtributosProductoRepository>();
            services.AddTransient<IRecetasRepository, RecetasRepository>();

            services.AddTransient<ITerminalesManager, TerminalesManager>();
            services.AddTransient<ITerminalesRepository, TerminalesRepository>();
            services.AddTransient<IContadoresManager, ContadoresManager>();
            services.AddTransient<IContadoresRepository, ContadoresRepository>();
            services.AddTransient<IProductosRepository, ProductosRepository>();

            services.AddTransient<ITanquesManager, TanquesManager>();
            services.AddTransient<ITanquesRepository, TanquesRepository>();

            services.AddTransient<IProductosRepository, ProductosRepository>();

            services.AddTransient<IDespachosRepository, DespachosRepository>();
            services.AddTransient<IDespachosManager, DespachosManager>();
            services.AddTransient<IDespachosEngine, DespachosEngine>();

            services.AddTransient<IDespachosComponentesRepository, DespachosComponentesRepository>();

            services.AddTransient<ITASCortesRepository, TASCortesRepository>();

            services.AddTransient<IShipToRepository, ShipToRepository>();
            services.AddTransient<ISoldToRepository, SoldToRepository>();

            services.AddTransient<ITerminalesProductosRecetasRepository, TerminalesProductosRecetasRepository>();

            services.AddTransient<ITablasCorreccionEngine, TablasCorreccionEngine>();
            services.AddTransient<IPermisosEngine, PermisosEngine>();
            services.AddTransient<ICurrentClaimsPrincipal, KairosClaimsPrincipal>();

            services.AddTransient<IProcesamientoArchivosMstRepository, ProcesamientoArchivosMstRepository>();
            services.AddTransient<IProcesamientoArchivosMstManager, ProcesamientoArchivosMstManager>();

            services.AddTransient<IProcesamientoArchivosDetRepository, ProcesamientoArchivosDetRepository>();
            services.AddTransient<IProcesamientoArchivosDetManager, ProcesamientoArchivosDetManager>();

            services.AddTransient<ITablasSistemaRepository, TablasSistemaRepository>();
            services.AddTransient<IColumnasSistemaRepository, ColumnasSistemaRepository>();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.AddHttpContextAccessor();
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly(), System.Reflection.Assembly.GetAssembly(typeof(KAIROSV2.Business.Common.Profiles.ProveedorPlantaProfile)));
            services.AddTransient<IProductosEngine, ProductosEngine>();

            var customInfo = new CultureInfo("es-MX");
            customInfo.DateTimeFormat.AbbreviatedMonthNames = new string[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic", "" };
            customInfo.DateTimeFormat.AbbreviatedMonthGenitiveNames = new string[] { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic", "" };
            var supportedCultures = new[] { customInfo };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(customInfo);
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("es-US"), new CultureInfo("es-US") };
            });

            services.AddControllersWithViews(options =>
            options.ModelBinderProviders.RemoveType<DateTimeModelBinderProvider>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
                //AddJsonOptions(options => {
                //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                //});

            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly(), System.Reflection.Assembly.GetAssembly(typeof(KAIROSV2.Business.Common.Profiles.RecetasProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
