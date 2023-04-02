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
    [PermissionsAuthorize(Permissions.SubModuloVehiculosTrailers)]
    public class TrailersController : BaseController
    {
        private const string Vista = "Trailers";
        private const string VistaGestion = "Gestión tráiler";
        private const string TablaTrailers = "T_Trailers";

        private readonly ITrailersManager _TrailersManager;

        public TrailersController(ITrailersManager TrailersManager)
        {
            Area = "Suministro y logística";
            _TrailersManager = TrailersManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaTrailers, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TTrailer>
            {
                Encabezados = new List<string>() { "Placa", "Acciones" },
                Entidades = _TrailersManager.ObtenerTrailers(),
                ActionsPermission = new ActionsPermission(User, Permissions.VehiculosTrailersAccionCN, Permissions.VehiculosTrailersAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosTrailersAccionCN)]
        public IActionResult NuevoTrailer()
        {
            var viewModel = new GestionTrailerViewModel()
            {
                Titulo = "Nuevo Tráiler",
                Accion = "Crear",
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionTrailer", viewModel);
        }


        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosTrailersAccionB)]
        public async Task<IActionResult> BorrarTrailer([FromBody] string placa)
        {
            var response = new MessageResponse();
            var trailer = await _TrailersManager.ObtenerTrailer(placa);

            if (trailer != null)
            {
                try
                {
                    var result = await _TrailersManager.BorrarTrailer(trailer);
                    response.Result = result;
                    if (result)
                        response.Message = "Tráiler eliminado correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Tráiler";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaTrailers, $"Tráiler {placa}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Tráiler";
                    LogError(LogAcciones.Eliminar, Vista, TablaTrailers, $"Tráiler {placa} no eliminado.", ex);
                }

            }
            else
            {
                response.Message = "No se encontró el Tráiler que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.VehiculosTrailersAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTrailer([FromForm] GestionTrailerViewModel addTrailerViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = _TrailersManager.CrearTrailer(addTrailerViewModel.ExtraerTrailer());
                    if (response.Result)
                    {
                        response.Message = "Tráiler creado correctamente";
                    }
                    else
                    {
                        response.Message = "La placa ya existe";
                    }

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTrailers, $"Tráiler {addTrailerViewModel?.Placa}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Tráiler";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaTrailers, response.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTrailers, $"No fue posible crear tráiler {addTrailerViewModel?.Placa}. {response?.Message}");
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.VehiculosTrailersAccionCN, Permissions.VehiculosTrailersAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None);

            return Json(permisos);
        }

    }
}
