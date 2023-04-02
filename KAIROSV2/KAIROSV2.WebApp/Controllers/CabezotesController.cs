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
    [PermissionsAuthorize(Permissions.SubModuloVehiculosCabezotes)]
    public class CabezotesController : BaseController
    {
        private const string Vista = "Cabezotes";
        private const string VistaGestion = "Gestión cabezote";
        private const string TablaCabezotes = "T_Cabezotes";

        private readonly ICabezotesManager _CabezotesManager;

        public CabezotesController(ICabezotesManager CabezotesManager)
        {
            Area = "Suministro y logística";
            _CabezotesManager = CabezotesManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaCabezotes, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TCabezote>
            {
                Encabezados = new List<string>() { "Placa", "Acciones" },
                Entidades = _CabezotesManager.ObtenerCabezotes(),
                ActionsPermission = new ActionsPermission(User, Permissions.VehiculosCabezotesAccionCN, Permissions.VehiculosCabezotesAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosCabezotesAccionCN)]
        public IActionResult NuevoCabezote()
        {
            var viewModel = new GestionCabezoteViewModel()
            {
                Titulo = "Nuevo Cabezote",
                Accion = "Crear",
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionCabezote", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosCabezotesAccionB)]
        public async Task<IActionResult> BorrarCabezote([FromBody] string placa)
        {
            var response = new MessageResponse();
            var cabezote = await _CabezotesManager.ObtenerCabezote(placa);

            if (cabezote != null)
            {
                try
                {
                    var result = await _CabezotesManager.BorrarCabezote(cabezote);
                    response.Result = result;
                    if (result)
                        response.Message = "Cabezote eliminado correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el cabezote";
                    
                    LogInformacion(LogAcciones.Eliminar, Vista, TablaCabezotes, $"Cabezote {placa}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el cabezote";
                    LogError(LogAcciones.Eliminar, Vista, TablaCabezotes, $"Cabezote {placa} no eliminado.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró el cabezote que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosCabezotesAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCabezote([FromForm] GestionCabezoteViewModel addCabezoteViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var cabezote = addCabezoteViewModel.ExtraerCabezote();
                    response.Result = _CabezotesManager.CrearCabezote(cabezote);
                    if (response.Result)
                    {
                        response.Message = "Cabezote creado correctamente";
                    }
                    else
                    {
                        response.Message = "La placa ya existe";
                    }

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaCabezotes, $"Cabezote {cabezote?.PlacaCabezote}. {response?.Message}");

                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el cabezote";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaCabezotes, response?.Message, ex);
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
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.VehiculosCabezotesAccionCN, Permissions.VehiculosCabezotesAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None);

            return Json(permisos);
        }
    }
}
