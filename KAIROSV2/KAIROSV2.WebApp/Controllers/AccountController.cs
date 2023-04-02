using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Identity;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        private const string Vista = "Login";
        private const string TablaUsuarios = "T_U_Usuarios";

        private readonly SignInManager<TUUsuario> _signInManager;
        private readonly IADAuthenticationService _adAuthenticationService;

        public AccountController(SignInManager<TUUsuario> signInManager, 
            IADAuthenticationService adAuthenticationService)
        {
            Area = "Login";
            _signInManager = signInManager;
            _adAuthenticationService = adAuthenticationService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if(_signInManager.IsSignedIn(User))
            {
                //_logManager.InsertarLogAsync("Admin", "Kairos2", "Operaciones", "Productos", "Vista Productos", "", LogAcciones.IngresoVista, "Ingreso a Vista Productos", LogPrioridades.Informacion);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new LoginViewModel
                {
                    ReturnUrl = returnUrl
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await UserManager.FindByIdAsync(loginViewModel.UserName);

            if (user != null)
            {
                await _signInManager.SignInAsync(user, false);                
                if (_adAuthenticationService.ValidateCredentials(loginViewModel.UserName, loginViewModel.Password))                    
                {
                    Logger.LogInformacion(LogAcciones.IngresoSistema, user.IdUsuario, "Kairos2", Area, "Account", TablaUsuarios, Vista, "Inicio de sesión exitoso");
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            loginViewModel.Validar = "false";
            return View(loginViewModel);
        }

        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            LogInformacion(LogAcciones.IngresoSistema, TablaUsuarios, Vista, "Cerrar sesión");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
