using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
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
    [PermissionsAuthorize(Permissions.SubModuloContadores)]
    public class ContadoresController : BaseController
    {
        #region Variables 
        private const string Vista = "Contadores";
        private const string VistaGestion = "Gestión contador";
        private const string TablaContadores = "T_Contadores";

        private readonly IContadoresManager _ContadoresManager;
        #endregion 

        #region Constructores
        public ContadoresController(IContadoresManager ContadoresManager)
        {
            Area = "Almacenamiento";
            _ContadoresManager = ContadoresManager;
        }

        #endregion

        #region Metodos IAction 
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaContadores, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TContador>
            {
                Encabezados = new List<string>() { "Contador", "Acciones" },
                Entidades = _ContadoresManager.ObtenerContadores(),
                ActionsPermission = new ActionsPermission(User, Permissions.ContadoresAccionCN, Permissions.ContadoresAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None)
            });

        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ContadoresAccionCN)]
        public IActionResult NuevoContador()
        {

            var viewModel = new GestionContadorViewModel()
            {
                Titulo = "Nuevo Contador",
                Accion = "Crear",
                Lectura = false
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionContador", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ContadoresAccionB)]
        public async Task<IActionResult> BorrarContador([FromBody] DatosContadorPeticion datosContador)
        {
            var response = new MessageResponse();
            var Contador = await _ContadoresManager.ObtenerContadorAsync(datosContador.Contador);

            if (Contador != null)
            {
                try
                {
                    response.Result = await _ContadoresManager.BorrarContadorAsync(datosContador.Contador);

                    if (response.Result)
                        response.Message = "contador eliminado correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Contador";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaContadores, $"Contador {datosContador?.Contador}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Contador";
                    LogError(LogAcciones.Eliminar, Vista, TablaContadores, $"Contador {datosContador?.Contador} no eliminado.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró el Contador que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ContadoresAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearContador([FromForm] GestionContadorViewModel addContadorViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var Contador = (addContadorViewModel.ExtraerContador());

                    response.Result = await _ContadoresManager.CrearContadorAsync(Contador);
                    if (response.Result)
                    {
                        response.Message = "Contador creado correctamente";
                        response.Payload = Contador;
                    }

                    else
                        response.Message = "El contador ya existe";

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaContadores, $"Contador {Contador?.IdContador}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Contador";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaContadores, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato. Revise si el formato esta correcto";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaContadores, $"No fue posible crear contador {addContadorViewModel?.IdContador}. {response?.Message}");
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.ContadoresAccionCN, Permissions.ContadoresAccionB, Permissions.None, Permissions.None, Permissions.None, Permissions.None);

            return Json(permisos);
        }
        #endregion


    }
}
