using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloUsuarios)]
    public class UsuariosController : BaseController
    {
        private const string Vista = "Usuarios";
        private const string VistaGestion = "Gestión usuario";
        private const string TablaUsuarios = "T_Usuarios";

        private readonly UserManager<TUUsuario> _userManager;
        private readonly IUsuariosManager _usuariosManager;
        private readonly ITerminalesCompañiasManager _terminalesCompañiasManager;
        private readonly IRolesManager _rolesManager;
        private readonly IADAuthenticationService _adAuthenticationService;
        private readonly IAuthorizationService _authorization;

        public UsuariosController(UserManager<TUUsuario> userManager,
                                  IUsuariosManager usuariosManager,
                                  ITerminalesCompañiasManager terminalesCompañiasManager,
                                  IRolesManager rolesManager,
                                  IAuthorizationService authorization,
                                  IADAuthenticationService adAuthenticationService)
        {
            Area = "Administración";
            _authorization = authorization;
            _userManager = userManager;
            _usuariosManager = usuariosManager;
            _terminalesCompañiasManager = terminalesCompañiasManager;
            _rolesManager = rolesManager;
            _adAuthenticationService = adAuthenticationService;
        }
       
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaUsuarios, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TUUsuario>
            {
                Encabezados = new List<string>() { "Imagen", "Id Usuario", "Nombre", "Rol", "Email", "Teléfono", "Acciones" },
                Entidades = _usuariosManager.ObtenerUsuarios(),
                ActionsPermission = new ActionsPermission(User, Permissions.UsuariosAccionCN, Permissions.UsuariosAccionB, Permissions.UsuariosAccionE, Permissions.UsuariosAccionVD, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.UsuariosAccionCN)]
        public IActionResult NuevoUsuario()
        {
            var terminalCompañias = _terminalesCompañiasManager.ObtenerBaseJerarquiaTerminalesCompañia();
            var viewModel = new GestionUsuarioViewModel() {
                Titulo = "Crear Usuario",
                Accion = "Crear",
                Roles = _rolesManager.ObtenerRoles()?.Select(e => e.IdRol)?.ToList(),
                TerminalCompañia = terminalCompañias?.ToList()
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionUsuario", viewModel);
        }

        [HttpPost]
        public IActionResult DatosUsuario([FromBody] DatosConsultaPeticion datosUsuario)
        {
            var permission = datosUsuario.Lectura ? Permissions.UsuariosAccionVD : Permissions.UsuariosAccionE;
            var AllowedPermission = _authorization.AuthorizeAsync(User, $"Permissions{permission}").Result.Succeeded;

            if (!AllowedPermission)
                return AccionNoPermitida("Datos Usuario", "Lo sentimos no tienes los permisos necesarios para esta acción");

            var usuario = _usuariosManager.ObtenerUsuario(datosUsuario.IdEntidad);
            var terminalesCompañia = _terminalesCompañiasManager.ObtenerBaseJerarquiaTerminalesCompañiaUsuario(datosUsuario.IdEntidad);

            var viewModel = new GestionUsuarioViewModel()
            {
                Titulo = (datosUsuario.Lectura) ? "Detalle Usuario" : "Editar Usuario",
                Accion = (datosUsuario.Lectura) ? "" : "Actualizar",
                Roles = _rolesManager.ObtenerRoles()?.Select(e => e.IdRol)?.ToList(),
                Lectura = datosUsuario.Lectura,
                TerminalCompañia = terminalesCompañia?.ToList()
            };
            viewModel.AsignarUsuario(usuario);

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionUsuario", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.UsuariosAccionB)]
        public async Task<IActionResult> BorrarUsuario([FromBody] string idUsuario)
        {
            var response = new MessageResponse();
            var usuario = await _userManager.FindByIdAsync(idUsuario);

            if (usuario != null)
            {
                try
                {
                    _terminalesCompañiasManager.BorrarTerminalesCompañiaParaUsuario(idUsuario);
                    IdentityResult result = await _userManager.DeleteAsync(usuario);
                    response.Result = result.Succeeded;
                    if (result.Succeeded)
                        response.Message = "Usuario eliminado correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el usuario";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaUsuarios, $"Usuario {idUsuario}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el usuario";
                    LogError(LogAcciones.Eliminar, Vista, TablaUsuarios, $"Usuario {idUsuario} no eliminado.", ex);
                }
                
            }
            else
            {
                response.Message = "No se encontró el usuario que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.UsuariosAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuario([FromForm] GestionUsuarioViewModel addUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = _usuariosManager.CrearUsuario(addUserViewModel.ExtraerUsuario());
                    if (response.Result)
                    {
                        try
                        {
                            _terminalesCompañiasManager.AgregarNuevoTerminalesCompañiaParaUsuario(addUserViewModel.IdUsuario, addUserViewModel.TerminalCompañia);
                            response.Message = "Usuario creado correctamente";
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Usuario creado, fallo la creación de terminales y compañías";
                            LogError(LogAcciones.Insertar, VistaGestion, TablaUsuarios, $"Usuario {addUserViewModel?.IdUsuario}. {response.Message}", ex);
                        }
                    }
                        
                    else
                        response.Message = "El usuario ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el usuario";
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.UsuariosAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarUsuario([FromForm] GestionUsuarioViewModel updateUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    response.Result = _usuariosManager.ActualizarUsuario(updateUserViewModel.ExtraerUsuario());
                    if (response.Result)
                    {
                        try
                        {
                            _terminalesCompañiasManager.AgregarNuevoTerminalesCompañiaParaUsuario(updateUserViewModel.IdUsuario, updateUserViewModel.TerminalCompañia);
                            response.Message = "Usuario actualizado correctamente";
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Usuario actualizado, fallo la actualización de terminales y compañías";
                        }
                    }
                    else
                        response.Message = "El usuario no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el usuario";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el usuario";
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerUsuariosActiveDirectory(string busqueda)
        {
            var usuarios = _adAuthenticationService.GetUsers(busqueda);

            return Json(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.UsuariosAccionCN, Permissions.UsuariosAccionB, Permissions.UsuariosAccionE, Permissions.UsuariosAccionVD, Permissions.None, Permissions.None);

            return Json(permisos);
        }

        private IActionResult AccionNoPermitida(string titulo, string mensaje)
        {
            ViewData["Titulo"] = titulo;
            ViewData["Mensaje"] = mensaje;
            return PartialView("_AccionNoPermitida");
        }
    }
}
