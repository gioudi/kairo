using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Identity.Authorization;
using Microsoft.AspNetCore.Authorization;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloRoles)]
    public class RolesController : BaseController
    {
        private const string Vista = "Roles";
        private const string VistaGestion = "Gestión rol";
        private const string TablaRoles = "T_U_Roles";
        private const string TablaRolesPermisos = "T_U_Roles_Permisos";

        private readonly IRolesManager _rolesManager;
        private readonly IPermisosManager _permisosManager;
        private readonly IAuthorizationService _authorization;
        public RolesController(IAuthorizationService authorization, IRolesManager rolesManager, IPermisosManager permisosManager)
        {
            Area = "Administración";
            _authorization = authorization;
            _rolesManager = rolesManager;
            _permisosManager = permisosManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaRoles, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TURole>
            {
                Encabezados = new List<string>() { "Id Rol", "Nombre", "Descripción", "Acciones" },
                Entidades = _rolesManager.ObtenerRoles(),
                ActionsPermission = new ActionsPermission(User, Permissions.RolesAccionCN, Permissions.RolesAccionB, Permissions.RolesAccionE, Permissions.RolesAccionVD, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.RolesAccionCN)]
        public IActionResult NuevoRol()
        {
            var permisos = _permisosManager.ObtenerBaseJerarquiaPermisos();
            var viewModel = new GestionRolViewModel()
            {
                Titulo = "Nuevo Rol",
                Accion = "Crear",
                Permisos = permisos
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionRol", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.RolesAccionB)]
        public async Task<IActionResult> BorrarRol([FromBody] string idRol)
        {
            var response = new MessageResponse();

            try
            {
                response.Result = _rolesManager.BorrarRol(idRol);
                if (response.Result)
                    response.Message = "Rol eliminado correctamente";
                else
                    response.Message = "No se encontró el Rol que desea eliminar";

                LogInformacion(LogAcciones.Eliminar, Vista, TablaRoles, $"Rol {idRol}. {response?.Message}");
            }
            catch (ValidationException ex)
            {
                response.Message = ex.Message;
                LogError(LogAcciones.Eliminar, Vista, TablaRoles, $"Rol {idRol} no eliminado.", ex);
            }
            catch (Exception ex)
            {
                response.Message = "Ocurrió un error no es posible borrar el rol";
                LogError(LogAcciones.Eliminar, Vista, TablaRoles, $"Rol {idRol} no eliminado.", ex);
            }

            return Json(response);
        }

        [HttpPost]
        public IActionResult DatosRol([FromBody] DatosConsultaPeticion datosRol)
        {
           
            var permission = datosRol.Lectura ? Permissions.RolesAccionVD : Permissions.RolesAccionE;
            var AllowedPermission = _authorization.AuthorizeAsync(User, $"Permissions{permission}").Result.Succeeded;

            if (!AllowedPermission)
                return AccionNoPermitida("Datos Rol", "Lo sentimos no tienes los permisos necesarios para esta acción");

            if (datosRol.IdEntidad == "Administrador" && !datosRol.Lectura)
                return AccionNoPermitida("Editar Rol", "Lo sentimos no es posible editar este rol");

            var rol = _rolesManager.ObtenerRol(datosRol.IdEntidad);
            var permisos = _permisosManager.ObtenerBaseJerarquiaPermisosRol(datosRol.IdEntidad);

            var viewModel = new GestionRolViewModel()
            {
                Titulo = (datosRol.Lectura) ? "Detalle Rol" : "Editar Rol",
                Accion = (datosRol.Lectura) ? "" : "Actualizar",
                IdRol = rol.IdRol,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Lectura = datosRol.Lectura,
                Permisos = permisos
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionRol", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.RolesAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearRol([FromForm] GestionRolViewModel addRolViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = _rolesManager.CrearRol(addRolViewModel.ExtraerRol());
                    if (response.Result)
                    {
                        try
                        {
                            addRolViewModel.EnableParentsPermissions();
                            _permisosManager.AgregarNuevosPermisosParaRol(addRolViewModel.IdRol, addRolViewModel.Permisos);
                            response.Message = "Rol creado correctamente";
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Rol creado, fallo la creación de permisos";
                            LogError(LogAcciones.Insertar, VistaGestion, TablaRoles, response.Message, ex);
                        }
                    }
                    else
                        response.Message = "El rol ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el rol";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaRoles, response.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaRoles, $"No fue posible crear rol {addRolViewModel?.IdRol}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.RolesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarRol([FromForm] GestionRolViewModel updateRolViewModel)
        {
            var result = Request.Form;
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {

                try
                {
                    var anteriorRol = _rolesManager.ObtenerRol(updateRolViewModel.IdRol);
                    var rol = updateRolViewModel.ExtraerRol();
                    response.Result = _rolesManager.ActualizarRol(rol);
                    if (response.Result)
                    {
                        try
                        {
                            updateRolViewModel.EnableParentsPermissions();
                            _permisosManager.AgregarNuevosPermisosParaRol(updateRolViewModel.IdRol, updateRolViewModel.Permisos);
                            response.Message = "Rol actualizado correctamente";
                            LogInformacionActualizar(VistaGestion, TablaRoles, $"Rol {updateRolViewModel?.IdRol}. {response?.Message}", anteriorRol, rol);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Rol creado, fallo la actualización de terminales y compañías.";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaRolesPermisos, $"{response?.Message} Rol:{rol?.IdRol}", ex);
                        }
                    }
                    else
                        response.Message = "El rol no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el rol";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaRoles, $"Rol {updateRolViewModel?.IdRol}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el rol";
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaRoles, $"No fue posible actualizar rol {updateRolViewModel?.IdRol}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.RolesAccionCN, Permissions.RolesAccionB, Permissions.RolesAccionE, Permissions.RolesAccionVD, Permissions.None, Permissions.None);

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
