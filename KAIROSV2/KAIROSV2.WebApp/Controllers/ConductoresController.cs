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
    [PermissionsAuthorize(Permissions.SubModuloConductores)]
    public class ConductoresController : BaseController
    {
        private const string Vista = "Conductores";
        private const string VistaGestion = "Gestión conductor";
        private const string TablaConductores = "T_Conductores";

        private readonly IConductoresManager _ConductoresManager;

        public ConductoresController(IConductoresManager ConductoresManager)
        {
            Area = "Suministro y logística";
            _ConductoresManager = ConductoresManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaConductores, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TConductor>
            {
                Encabezados = new List<string>() {  "Nombre", "Cedula", "Acciones" },
                Entidades = _ConductoresManager.ObtenerConductores(),
                ActionsPermission = new ActionsPermission(User, Permissions.ConductoresAccionCN, Permissions.ConductoresAccionB, Permissions.ConductoresAccionE, Permissions.None, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ConductoresAccionCN)]
        public IActionResult NuevoConductor()
        {
            var viewModel = new GestionConductorViewModel()
            {
                Titulo = "Nuevo Conductor",
                Accion = "Crear",
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionConductor", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ConductoresAccionB)]
        public async Task<IActionResult> BorrarConductor([FromBody] int Cedula)
        {
            var response = new MessageResponse();
            var Conductor = await _ConductoresManager.ObtenerConductorAsync(Cedula);

            if (Conductor != null)
            {
                try
                {
                    var result = await _ConductoresManager.BorrarConductor(Conductor);
                    response.Result = result;
                    if (result)
                        response.Message = "Conductor eliminar correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Conductor";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaConductores, $"Conductor {Cedula}. {response.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Conductor";
                    LogError(LogAcciones.Eliminar, Vista, TablaConductores, $"Conductor {Cedula} no eliminado.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró el Conductor que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ConductoresAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearConductor([FromForm] GestionConductorViewModel addConductorViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var conductor = addConductorViewModel.ExtraerConductor();
                    response.Result = _ConductoresManager.CrearConductor(conductor);

                    if (response.Result)
                        response.Message = "Conductor creado correctamente";
                    else
                        response.Message = "El Conductor ya existe";

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaConductores, $"Conductor {conductor?.Cedula}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Conductor";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaConductores, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaConductores, $"No fue posible crear conductor {addConductorViewModel?.Cedula}. {response?.Message}");
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionsAuthorize(Permissions.ConductoresAccionE)]
        public async Task<IActionResult> ActualizarConductor([FromForm] GestionConductorViewModel updateConductorViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var anteriorConductor = _ConductoresManager.ObtenerConductor(updateConductorViewModel.Cedula);
                    var conductor = updateConductorViewModel.ExtraerConductor();
                    response.Result = _ConductoresManager.ActualizarConductor(conductor);
                    if (response.Result)
                        response.Message = "Conductor actualizado correctamente";

                    else
                        response.Message = "El Conductor no existe";

                    LogInformacionActualizar(VistaGestion, TablaConductores, $"Conductor {updateConductorViewModel?.Cedula}. {response?.Message}", anteriorConductor, conductor);
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible modificar el conductor";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaConductores, $"{response?.Message}", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el conductor";
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaConductores, $"{response?.Message} {updateConductorViewModel?.Cedula}");
            }

            return Json(response);
        }


        [HttpPost]
        public IActionResult DatosConductor([FromBody] DatosConsultaPeticion datosConductor)
        {
            var conductor = _ConductoresManager.ObtenerConductor(datosConductor.IdNumEntidad);

            var viewModel = new GestionConductorViewModel()
            {
                Titulo = "Editar Conductor",
                Accion = "Actualizar"
            };
            viewModel.AsignarConductor(conductor);

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionConductor", viewModel);
        }

    }
}
