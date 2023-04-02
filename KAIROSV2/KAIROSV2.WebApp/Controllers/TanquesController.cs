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
    [PermissionsAuthorize(Permissions.SubModuloTanques)]
    public class TanquesController : BaseController
    {
        #region Variables 	
        private const string Vista = "Tanques";
        private const string VistaGestion = "Gestión tanque";
        private const string TablaTanques = "T_Tanques";
        private readonly ITanquesManager _TanquesManager;
        private readonly ITerminalesManager _TerminalesManager;
        #endregion

        #region Constructores	
        public TanquesController(ITanquesManager TanquesManager, ITerminalesManager TerminalesManager)
        {
            Area = "Almacenamiento";
            _TanquesManager = TanquesManager;
            _TerminalesManager = TerminalesManager;
        }
        #endregion

        #region Metodos IAction 	
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaTanques, $"Ingreso a vista {Vista}");
            return View(new ListViewModel<TanqueDTO>
            {
                Encabezados = new List<string>() { "Terminal", "Tanque", "Imagen", "Producto", "Clase", "Estado", "Acciones" },
                Entidades = _TanquesManager.ObtenerProductoTerminalEstadoTanques(),
                ActionsPermission = new ActionsPermission(User, Permissions.TanquesAccionCN, Permissions.TanquesAccionB, Permissions.TanquesAccionE, Permissions.TanquesAccionVD, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionCN)]
        public IActionResult NuevoTanque()
        {
            var viewModel = new GestionTanqueViewModel()
            {
                Titulo = "Nuevo Tanque",
                Accion = "Crear",
                EstadosTanque = _TanquesManager.ObtenerEstadosTanque().Select(e => e.Descripcion),
                Terminales = _TerminalesManager.ObtenerTerminales().Select(e => e.Terminal).ToList(),
                Productos = _TanquesManager.ObtenerProductos().Select(e => e.NombreCorto).ToList(),
                FechaAforo = DateTime.Now.Date,
                Lectura = false
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionTanque", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionB)]
        public async Task<IActionResult> BorrarTanque([FromBody] DatosTanquePeticion datosTanque)
        {
            var response = new MessageResponse();
            var Tanque = await _TanquesManager.ObtenerTanqueAsync(datosTanque.Tanque, datosTanque.IdTerminal);
            if (Tanque != null)
            {
                try
                {
                    response.Result = await _TanquesManager.BorrarTanqueAsync(datosTanque.Tanque, datosTanque.IdTerminal);
                    if (response.Result)
                        response.Message = "Tanque eliminado correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Tanque";
                    LogInformacion(LogAcciones.Eliminar, Vista, TablaTanques, $"Tanque {datosTanque?.Tanque } - Terminal {datosTanque?.IdTerminal}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Tanque";
                    LogError(LogAcciones.Eliminar, Vista, TablaTanques, $"Tanque {datosTanque?.Tanque} - Terminal {datosTanque?.IdTerminal} no eliminado.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró el Tanque que desea borrar";
            }
            return Json(response);

        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionE)]
        public IActionResult EditarTanque([FromBody] DatosTanquePeticion datosTanque)
        {
            var TanqueActual = _TanquesManager.ObtenerTanque(datosTanque.Tanque, datosTanque.IdTerminal, new String[4] { "IdTerminalNavigation", "IdEstadoNavigation", "IdProductoNavigation", "IdTanquesPantallaFlotanteNavigation" });
            var viewModel = new GestionTanqueViewModel()
            {
                Titulo = "Editar Tanque",
                Accion = "Actualizar",
                Lectura = true,
                IdTanque = datosTanque.Tanque,
                Terminal = TanqueActual.IdTerminalNavigation.Terminal,
                Estado = TanqueActual.IdEstadoNavigation.Descripcion,
                Producto = TanqueActual.IdProductoNavigation.NombreCorto,
                ClaseTanque = TanqueActual.ClaseTanque,
                TipoTanque = TanqueActual.TipoTanque,
                CapacidadNominal = TanqueActual.CapacidadNominal,
                CapacidadOperativa = TanqueActual.CapacidadOperativa,
                AlturaMaximaAforo = TanqueActual.AlturaMaximaAforo,
                AforadoPor = TanqueActual.AforadoPor,
                VolumenNoBombeable = TanqueActual.VolumenNoBombeable,
                FechaAforo = TanqueActual.FechaAforo,
                Observaciones = TanqueActual.Observaciones,
                PantallaFlotante = TanqueActual.PantallaFlotante,
                DensidadAforo = TanqueActual.IdTanquesPantallaFlotanteNavigation != null ? TanqueActual.IdTanquesPantallaFlotanteNavigation.DensidadAforo : 0,
                GalonesPorGrado = TanqueActual.IdTanquesPantallaFlotanteNavigation != null ? TanqueActual.IdTanquesPantallaFlotanteNavigation.GalonesPorGrado : 0,
                NivelCorreccionFinal = TanqueActual.IdTanquesPantallaFlotanteNavigation != null ? TanqueActual.IdTanquesPantallaFlotanteNavigation.NivelCorreccionFinal : 0,
                NivelCorreccionInicial = TanqueActual.IdTanquesPantallaFlotanteNavigation != null ? TanqueActual.IdTanquesPantallaFlotanteNavigation.NivelCorreccionInicial : 0,
                EstadosTanque = _TanquesManager.ObtenerEstadosTanque().Select(e => e.Descripcion),
                Terminales = _TerminalesManager.ObtenerTerminales().Select(e => e.Terminal).ToList(),
                Productos = _TanquesManager.ObtenerProductos().Select(e => e.NombreCorto).ToList(),
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionTanque", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionVD)]
        public IActionResult DetalleTanque([FromBody] DatosTanquePeticion datosTanque)
        {
            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");

            var response = new MessageResponse();
            TanqueDetallesDTO TanqueActual = _TanquesManager.AgregarDetallesTanque(datosTanque.Tanque, datosTanque.IdTerminal);
            response.Message = "";
            response.Payload = TanqueActual;
            response.Result = true;
            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTanque([FromForm] GestionTanqueViewModel addTanqueViewModel)
        {
            var response = new MessageResponse();
            if (ModelState.IsValid)
            {
                try
                {
                    var IdProducto = _TanquesManager.ObtenerProductos().Where(e => e.NombreCorto == addTanqueViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _TanquesManager.ObtenerEstadosTanque().Where(e => e.Descripcion == addTanqueViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _TerminalesManager.ObtenerTerminales().Where(e => e.Terminal == addTanqueViewModel.Terminal).FirstOrDefault().IdTerminal;
                    var Tanque = (addTanqueViewModel.ExtraerTanque(IdTerminal, IdProducto, IdEstado));
                    response.Result = await _TanquesManager.CrearTanqueAsync(Tanque);
                    if (response.Result)
                    {
                        response.Message = "Tanque creado correctamente";
                        response.Result = true;
                        response.Payload = _TanquesManager.AgregarProductosTerminalesEstadosTanque(Tanque, true);
                    }
                    else
                        response.Message = "El Tanque ya existe";
                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTanques, $"Tanque {addTanqueViewModel?.IdTanque} - Terminal {addTanqueViewModel?.IdTerminal}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Tanque";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaTanques, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTanques, $"No fue posible crear tanque {addTanqueViewModel?.IdTanque} - Terminal {addTanqueViewModel?.IdTerminal}. {response?.Message}");
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarTanque([FromForm] GestionTanqueViewModel updateTanqueViewModel)
        {
            var response = new MessageResponse();
            if (ModelState.IsValid)
            {
                //TODO: Actualizar	
                try
                {
                    var IdProducto = _TanquesManager.ObtenerProductos().Where(e => e.NombreCorto == updateTanqueViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _TanquesManager.ObtenerEstadosTanque().Where(e => e.Descripcion == updateTanqueViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _TerminalesManager.ObtenerTerminales().Where(e => e.Terminal == updateTanqueViewModel.Terminal).FirstOrDefault().IdTerminal;
                    var anteriorTanque = _TanquesManager.ObtenerTanque(updateTanqueViewModel.IdTanque, updateTanqueViewModel.IdTerminal);
                    var Tanque = (updateTanqueViewModel.ExtraerTanque(IdTerminal, IdProducto, IdEstado));
                    response.Result = await _TanquesManager.ActualizarTanqueAsync(Tanque);
                    if (response.Result)
                    {
                        response.Message = "El Tanque fue actualizada correctamente";
                        response.Result = true;
                        response.Payload = _TanquesManager.AgregarProductosTerminalesEstadosTanque(Tanque, true);
                    }
                    else
                        response.Message = "El Tanque no existe";
                    LogInformacionActualizar(VistaGestion, TablaTanques, $"Tanque {updateTanqueViewModel?.IdTanque} - Terminal {updateTanqueViewModel?.IdTerminal}. {response?.Message}", anteriorTanque, Tanque);
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Tanque";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaTanques, $"Error persistiendo tanque {updateTanqueViewModel?.IdTanque} - Terminal {updateTanqueViewModel?.IdTerminal}", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Tanque";
            }
            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TanquesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarTanquestado([FromForm] GestionTanqueViewModel ActualizarTanqueViewModel)
        {
            var response = new MessageResponse();
            if (ModelState.IsValid)
            {
                //TODO: Actualizar	
                try
                {
                    var IdProducto = _TanquesManager.ObtenerProductos().Where(e => e.NombreCorto == ActualizarTanqueViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _TanquesManager.ObtenerEstadosTanque().Where(e => e.Descripcion == ActualizarTanqueViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _TerminalesManager.ObtenerTerminales().Where(e => e.Terminal == ActualizarTanqueViewModel.Terminal).FirstOrDefault().IdTerminal;
                    var anteriorTanque = _TanquesManager.ObtenerTanque(ActualizarTanqueViewModel.IdTanque, ActualizarTanqueViewModel.IdTerminal);
                    var tanque = ActualizarTanqueViewModel.ExtraerTanque(IdTerminal, IdProducto, IdEstado);
                    response.Result = await _TanquesManager.ActualizarTanqueAsync(tanque);
                    if (response.Result)
                    {
                        response.Message = "Tanque actualizada correctamente";
                    }
                    else
                        response.Message = "El Tanque no existe";
                    LogInformacionActualizar(VistaGestion, TablaTanques, $"Tanque {ActualizarTanqueViewModel?.IdTanque} - Terminal {ActualizarTanqueViewModel?.IdTerminal}. {response?.Message}", anteriorTanque, tanque);
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Tanque";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaTanques, $"Error persistiendo el tanque: {ActualizarTanqueViewModel?.IdTanque} - Terminal {ActualizarTanqueViewModel?.IdTerminal}", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Tanque";
            }
            return Json(response);
        }

        private IEnumerable<HelperSelectString> ObtenerListadoProductos()
        {
            return _TanquesManager.ObtenerProductos().Select(a => new HelperSelectString()
            {
                Id = a.IdProducto,
                Descripcion = a.NombreCorto
            });
        }

        private IEnumerable<HelperSelectString> ObtenerListadoTerminales()
        {
            return _TerminalesManager.ObtenerTerminales().Select(a => new HelperSelectString()
            {
                Id = a.IdTerminal,
                Descripcion = a.Terminal
            });
        }

        private IEnumerable<HelperSelectInt> ObtenerListadoEstados()
        {
            return _TanquesManager.ObtenerEstadosTanque().Select(a => new HelperSelectInt()
            {
                Id = a.IdEstado,
                Descripcion = a.Descripcion
            });
        }
        #endregion
    }
}