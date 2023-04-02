using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Data.Contracts;
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
    [PermissionsAuthorize(Permissions.SubModuloCompañías)]
    public class CompañiasController : BaseController
    {
        private const string Vista = "Compañías";
        private const string VistaGestion = "Gestión compañías";
        private const string TablaCompañias = "T_Compañías";
        private readonly IAuthorizationService _authorization;
        private readonly ICompañiasManager _CompañiasManager;
        
        public CompañiasController(IAuthorizationService authorization, ICompañiasManager CompañiasManager)
        {
            Area = "Configuración";
            _authorization = authorization;
            _CompañiasManager = CompañiasManager;            
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaCompañias, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TCompañia>
            {
                Encabezados = new List<string>() { "Id Compañia", "Compañia", "Acciones" },
                Entidades = _CompañiasManager.ObtenerCompañias(),
                ActionsPermission = new ActionsPermission(User, Permissions.CompañiasAccionCN, Permissions.CompañiasAccionB, Permissions.CompañiasAccionE, Permissions.CompañiasAccionVD, Permissions.None, Permissions.None)
            });

        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.CompañiasAccionCN)]
        public IActionResult NuevaCompañia()
        {
            var viewModel = new GestionCompañiaViewModel() {
                Titulo = "Crear Compañía",
                Accion = "Crear",
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionCompañia", viewModel);
        }

        [HttpPost]
        public IActionResult DatosCompañia([FromBody] DatosCompañiaPeticion datosCompañia)
        {
            var permission = datosCompañia.Lectura ? Permissions.CompañiasAccionVD : Permissions.CompañiasAccionE;
            var AllowedPermission = _authorization.AuthorizeAsync(User, $"Permissions{permission}").Result.Succeeded;

            if (!AllowedPermission)
                return AccionNoPermitida("Datos Compañías", "Lo sentimos no tienes los permisos necesarios para esta acción");

            var Compañia = _CompañiasManager.ObtenerCompañia(datosCompañia.Compañia);
            
            var viewModel = new GestionCompañiaViewModel()
            {
                Titulo = (datosCompañia.Lectura) ? "Detalle Compañía" : "Editar Compañía",
                Accion = (datosCompañia.Lectura) ? "" : "Actualizar",
                Lectura = datosCompañia.Lectura,
                
            };
            viewModel.AsignarCompañia(Compañia);

            return PartialView("_GestionCompañia", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.CompañiasAccionB)]
        public async Task<IActionResult> BorrarCompañia([FromBody] string idCompañia)
        {
            var response = new MessageResponse();
            var Compañia = await _CompañiasManager.ObtenerCompañiaAsync(idCompañia);

            if (Compañia != null)
            {
                try
                {
                    var result = await _CompañiasManager.BorrarCompañiaAsync(idCompañia);
                    response.Result = true;
                    if (response.Result)
                        response.Message = "Compañía eliminada correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar la Compañía";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaCompañias, $"Compañía {idCompañia}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar la Compañía";
                    LogError(LogAcciones.Eliminar, Vista, TablaCompañias, $"Compañía {idCompañia} no eliminada.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró la Compañía que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.CompañiasAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCompañia([FromForm] GestionCompañiaViewModel addCompañiaViewModel)
        {
            var response = new MessageResponse();

            if(ModelState.IsValid)
            {
                try
                {
                    var compañia = addCompañiaViewModel.ExtraerCompañia();
                    response.Result = _CompañiasManager.CrearCompañia(compañia);
                    if (response.Result)
                    {
                        try
                        {
                            response.Message = "Compañía creada correctamente";
                            LogInformacion(LogAcciones.Insertar, VistaGestion, TablaCompañias, $"Producto {compañia?.IdCompañia}. {response?.Message}");
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Compañía creada, fallo la creación de Compañías y compañías";
                            LogError(LogAcciones.Insertar, VistaGestion, TablaCompañias, response?.Message, ex);
                        }
                    }

                    else
                        response.Message = "La Compañía ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear la Compañía";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaCompañias, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.CompañiasAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarCompañia([FromForm] GestionCompañiaViewModel updateUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var anteriorCompañia = _CompañiasManager.ObtenerCompañia(updateUserViewModel.IdCompañia);
                    var compañia = updateUserViewModel.ExtraerCompañia();
                    response.Result = _CompañiasManager.ActualizarCompañia(compañia);
                    if (response.Result)
                    {
                        try
                        {
                            response.Message = "Compañía actualizada correctamente";
                            LogInformacionActualizar(VistaGestion, TablaCompañias, $"Producto {updateUserViewModel?.IdCompañia}. {response?.Message}", anteriorCompañia, compañia);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Compañía creada, fallo la actualización de Compañías y compañías";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaCompañias, $"Compañía {compañia?.IdCompañia}. {response?.Message}", ex);
                        }
                    }
                    else
                        response.Message = "El Compañía no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar la Compañía";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaCompañias, $"Compañía {updateUserViewModel?.IdCompañia}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar la Compañía";
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaCompañias, $"{response?.Message} {updateUserViewModel?.IdCompañia}");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.CompañiasAccionCN, Permissions.CompañiasAccionB, Permissions.CompañiasAccionE, Permissions.CompañiasAccionVD, Permissions.None, Permissions.None);

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
