using AutoMapper;
using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloProveedores)]
    public class ProveedoresController : BaseController
    {
        private const string Vista = "Proveedores";
        private const string VistaGestion = "Gestión proveedor";
        private const string TablaProveedores = "T_Proveedores";
        private const string TablaProveedoresPlanta = "T_Proveedores_Plantas";
        private readonly IAuthorizationService _authorization;
        private readonly IProveedoresManager _proveedoresManager;

        public ProveedoresController(IAuthorizationService authorization, IProveedoresManager proveedoresManager)
        {
            Area = "Suministro y logística";
            _authorization = authorization;
            _proveedoresManager = proveedoresManager;
        }
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaProveedores, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TProveedor>
            {
                Encabezados = new List<string>() { "ID", "Nombre", "SICOM", "Acciones" },
                Entidades = _proveedoresManager.ObtenerProveedores(),
                ActionsPermission = new ActionsPermission(User, Permissions.ProveedoresAccionCN, Permissions.ProveedoresAccionB, Permissions.ProveedoresAccionE, Permissions.ProveedoresAccionVD, Permissions.None, Permissions.None)
            });
        }

        public IActionResult NuevoProveedor()
        {
            var viewModel = new GestionProveedorViewModel()
            {
                Titulo = "Nuevo Proveedor",
                Accion = "Crear",
                Tipos = new List<SelectListItem>()
                {
                    new SelectListItem("Aditivo", "5"),
                    new SelectListItem("Alcohol", "3"),
                    new SelectListItem("Biodiesel", "4"),
                    new SelectListItem("Diesel", "2"),
                    new SelectListItem("Gasolina", "1")
                }
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionProveedor", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ProveedoresAccionB)]
        public async Task<IActionResult> BorrarProveedor([FromBody] string idProveedor)
        {
            var response = new MessageResponse();
            try
            {
                response.Result = await Task.Run(() => _proveedoresManager.BorrarProveedor(idProveedor));
                if (response.Result)
                    response.Message = "Proveedor eliminado correctamente";
                else
                    response.Message = "No se encontró el proveedor que desea eliminar";

                LogInformacion(LogAcciones.Eliminar, Vista, TablaProveedores, $"Proveedor {idProveedor}. {response.Message}");
            }
            catch (DeleteCascadeException ex)
            {
                response.Message = ex.Message;
                LogError(LogAcciones.Eliminar, Vista, TablaProveedores, $"Proveedor {idProveedor} no eliminado.", ex);
            }
            catch (Exception ex)
            {
                response.Message = "Ocurrió un error inesperado, no fue posible borrar el proveedor";
                LogError(LogAcciones.Eliminar, Vista, TablaProveedores, $"Proveedor {idProveedor} no eliminado.", ex);
            }

            return Json(response);
        }

        [HttpPost]
        public IActionResult DatosProveedor([FromBody] DatosConsultaPeticion datosProveedor)
        {
            var permission = datosProveedor.Lectura ? Permissions.ProveedoresAccionVD : Permissions.ProveedoresAccionE;
            var AllowedPermission = _authorization.AuthorizeAsync(User, $"Permissions{permission}").Result.Succeeded;

            if (!AllowedPermission)
                return AccionNoPermitida("Datos Proveedor", "Lo sentimos no tienes los permisos necesarios para esta acción");

            var proveedor = _proveedoresManager.ObtenerProveedor(datosProveedor.IdEntidad);
            var viewModel = Mapper.Map<GestionProveedorViewModel>(proveedor);
            viewModel.Titulo = (datosProveedor.Lectura) ? "Detalle Proveedor" : "Editar Proveedor";
            viewModel.Accion = (datosProveedor.Lectura) ? "" : "Actualizar";
            viewModel.Lectura = datosProveedor.Lectura;
            viewModel.Tipos = new List<SelectListItem>()
                {
                    new SelectListItem("Aditivo", "5"),
                    new SelectListItem("Alcohol", "3"),
                    new SelectListItem("Biodiesel", "4"),
                    new SelectListItem("Diesel", "2"),
                    new SelectListItem("Gasolina", "1")
                };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionProveedor", viewModel);
        }


        [HttpPost]
        [PermissionsAuthorize(Permissions.ProveedoresAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProveedor([FromForm] GestionProveedorViewModel addProveedorViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var proveedor = Mapper.Map<TProveedor>(addProveedorViewModel);
                    proveedor.EditadoPor = "Admin";
                    proveedor.UltimaEdicion = DateTime.Now;
                    proveedor?.TProveedoresPlanta.ToList().ForEach(e => { e.EditadoPor = "Admin"; e.UltimaEdicion = DateTime.Now; e.IdProveedor = proveedor.IdProveedor; });
                    proveedor?.TProveedoresProductos.ToList().ForEach(e => { e.IdProveedor = proveedor.IdProveedor; });
                    response.Result = _proveedoresManager.CrearProveedor(proveedor);
                    if (response.Result)
                        response.Message = "Proveedor creado correctamente";
                    else
                        response.Message = "El proveedor que trata de crear ya existe.";

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaProveedores, $"Proveedor {proveedor?.IdProveedor}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Message = "Ocurrió un error al crear el proveedor";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaProveedores, response?.Message, ex);
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaProveedores, $"No fue posible crear proveedor {addProveedorViewModel?.IdProveedor}. {response?.Message}");
            }

            
            return Json(response);
        }

       
        [HttpPost]
        [PermissionsAuthorize(Permissions.ProveedoresAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarProveedor([FromForm] GestionProveedorViewModel updateProveedorViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var anteriorProveedor = _proveedoresManager.ObtenerProveedor(updateProveedorViewModel.IdProveedor);
                    var proveedor = Mapper.Map<TProveedor>(updateProveedorViewModel);
                    proveedor.EditadoPor = "Admin";
                    proveedor.UltimaEdicion = DateTime.Now;
                    var plantas = proveedor.TProveedoresPlanta;
                    var productos = proveedor.TProveedoresProductos;
                    response.Result = _proveedoresManager.ActualizarProveedor(proveedor);
                    if (response.Result)
                    {
                        try
                        {
                            plantas?.ToList().ForEach(e => { e.EditadoPor = "Admin"; e.UltimaEdicion = DateTime.Now; e.IdProveedor = proveedor.IdProveedor; });
                            _proveedoresManager.ReemplazarProveedorPlantas(proveedor.IdProveedor, plantas);

                            productos?.ToList().ForEach(e => { e.IdProveedor = proveedor.IdProveedor; });
                            _proveedoresManager.ReemplazarProveedorProducots(proveedor.IdProveedor, productos);

                            response.Message = "Proveedor actualizado correctamente";

                            LogInformacionActualizar(VistaGestion, TablaProveedores, $"Proveedor {updateProveedorViewModel?.IdProveedor}. {response?.Message}", anteriorProveedor, proveedor);
                        }
                        catch (PersistEntityException ex)
                        {
                            response.Message = ex.Message;
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaProveedores, $"Error persistiendo proveedor {proveedor?.IdProveedor}", ex);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Proveedor creado, fallo la actualización de plantas o productos";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaProveedoresPlanta, $"Error persistiendo proveedor {proveedor?.IdProveedor}",ex );
                        }
                    }
                    else
                        response.Message = "El Proveedor no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el Proveedor";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaProveedores, $"Proveedor {updateProveedorViewModel?.IdProveedor}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaProveedores, $"No fue posible actualizar proveedor {updateProveedorViewModel?.IdProveedor}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.ProveedoresAccionCN, Permissions.ProveedoresAccionB, Permissions.ProveedoresAccionE, Permissions.ProveedoresAccionVD, Permissions.None, Permissions.None);

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
