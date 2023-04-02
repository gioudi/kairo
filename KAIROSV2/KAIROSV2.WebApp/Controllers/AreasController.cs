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
    [PermissionsAuthorize(Permissions.SubModuloAreas)]
    public class AreasController : BaseController
    {
        private const string Vista = "Áreas";
        private const string VistaGestion = "Gestión área";
        private const string TablaAreas = "T_Areas";
        private readonly IAreasManager _AreasManager;

        public AreasController(IAreasManager AreasManager)
        {
            Area = "Configuración";
            _AreasManager = AreasManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaAreas, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TArea>
            {
                Encabezados = new List<string>() { "Id Área", "Área", "Acciones" },
                Entidades = _AreasManager.ObtenerAreas(),
                ActionsPermission = new ActionsPermission(User, Permissions.AreasAccionCN, Permissions.AreasAccionB, Permissions.AreasAccionE, Permissions.None, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.AreasAccionCN)]
        public IActionResult NuevaArea()
        {
            
            var viewModel = new GestionAreaViewModel() {
                Titulo = "Crear Área",
                Accion = "Crear",
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionArea", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.AreasAccionB)]
        public async Task<IActionResult> BorrarArea([FromBody] string idArea)
        {
            var response = new MessageResponse();
            var Area = await _AreasManager.ObtenerAreaAsync(idArea);

            if (Area != null)
            {
                try
                {
                    var result = await _AreasManager.BorrarAreaAsync(idArea);
                    response.Result = true;
                    if (response.Result)
                        response.Message = "Área eliminada correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Área";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaAreas, $"Área {idArea}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Área";
                    LogError(LogAcciones.Eliminar, Vista, TablaAreas, $"Área {idArea} no eliminada.", ex);
                }

            }
            else
            {
                response.Message = "No se encontró el Área que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.AreasAccionE)]
        public IActionResult DatosArea([FromBody] DatosConsultaPeticion datosArea)
        {
            var Area = _AreasManager.ObtenerArea(datosArea.IdEntidad);
            

            var viewModel = new GestionAreaViewModel()
            {
                Titulo = (datosArea.Lectura) ? "Detalle Área" : "Editar Área",
                Accion = (datosArea.Lectura) ? "" : "Actualizar",
                Lectura = datosArea.Lectura,
                
            };
            viewModel.AsignarArea(Area);

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionArea", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.AreasAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearArea([FromForm] GestionAreaViewModel addAreaViewModel)
        {
            var response = new MessageResponse();

            if(ModelState.IsValid)
            {
                try
                {
                    var area = addAreaViewModel.ExtraerArea();
                    response.Result = _AreasManager.CrearArea(area);
                    if (response.Result)
                    {
                        try
                        {
                            response.Message = "Área creado correctamente";
                            LogInformacion(LogAcciones.Insertar, VistaGestion, TablaAreas, $"Área {area?.IdArea}. {response?.Message}");
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Área creado, fallo la creación de terminales y compañías";
                            LogError(LogAcciones.Insertar, VistaGestion, TablaAreas, response?.Message, ex);
                        }
                    }
                        
                    else
                        response.Message = "El Área ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Área";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaAreas, response?.Message, ex);
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
        [ValidateAntiForgeryToken]
        [PermissionsAuthorize(Permissions.AreasAccionE)]
        public async Task<IActionResult> ActualizarArea([FromForm] GestionAreaViewModel updateAreaViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var areaAnterior = _AreasManager.ObtenerArea(updateAreaViewModel.IdArea);
                    var area = updateAreaViewModel.ExtraerArea();
                    response.Result = _AreasManager.ActualizarArea(area);
                    if (response.Result)
                    {
                        try
                        {
                            response.Message = "Área actualizado correctamente";
                            LogInformacionActualizar(VistaGestion, TablaAreas, $"Área {updateAreaViewModel?.Area}. {response?.Message}", areaAnterior, area);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Área creado, fallo la actualización de terminales y compañías";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaAreas, $"Área {updateAreaViewModel?.IdArea}, no fue posible actualizar.", ex);
                        }
                    }
                    else
                        response.Message = "El Área no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible modificar el Área";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaAreas, $"Área {updateAreaViewModel?.IdArea}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Área";
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaAreas, $"No fue posible actualizar área {updateAreaViewModel?.IdArea}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.AreasAccionCN, Permissions.AreasAccionB, Permissions.AreasAccionE, Permissions.None, Permissions.None, Permissions.None);

            return Json(permisos);
        }
    }
}
