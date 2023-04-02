using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity.Authorization;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.AplicacionKairos)]
    public class HomeController : BaseController
    {
        public HomeController()
        {
            Area = "Inicio";
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            LogError(LogAcciones.Insertar, "Inicio", "", "Excepción no controlada", null);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
