using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloLineas)]
    public class LineasController : BaseController
    {
        private const string Vista = "Lineas";
        private const string VistaGestion = "Gestión lineas";
        private const string TablaLineas = "T_Lineas";

        private readonly ILineasManager _LineasManager;
        private readonly ITerminalesManager _terminalesManager;
        private readonly IProductosManager _productoManager;
        public LineasController(ILineasManager lineasManager, ITerminalesManager terminalesManager)
        {
            Area = "Almacenamiento";
            _LineasManager = lineasManager;
            _terminalesManager = terminalesManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaLineas, $"Ingreso a vista {Vista}");

            return View("Index", new LineasViewModel
            {
                Lineas = _LineasManager.ObtenerProductoTerminalEstadoLineas(),
                ActionsPermission = new ActionsPermission(User, Permissions.LineasAccionCN, Permissions.LineasAccionB, Permissions.LineasAccionE, Permissions.None, Permissions.None, Permissions.None)
            });


        }

        [HttpPost]
        public IActionResult NuevaLinea()
        {

            var viewModel = new GestionLIneaViewModel()
            {
                Titulo = "Nueva Línea",
                Accion = "Crear",
                EstadosLinea = _LineasManager.ObtenerEstadosLinea().Select(e => e.Descripcion),
                Terminales = _terminalesManager.ObtenerTerminales().Select(e => e.Terminal).ToList(),
                Productos = _LineasManager.ObtenerProductos().Select(e => e.NombreCorto).ToList(),
                Lectura = false
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionLinea", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.LineasAccionB)]
        public async Task<IActionResult> BorrarLinea([FromBody] DatosLineaPeticion datosLinea)
        {
            var response = new MessageResponse();
            var Linea = await _LineasManager.ObtenerLineaAsync(datosLinea.IdLinea, datosLinea.IdTerminal);

            if (Linea != null)
            {
                try
                {
                    response.Result = await _LineasManager.BorrarLineaAsync(datosLinea.IdLinea, datosLinea.IdTerminal);

                    if (response.Result)
                        response.Message = "Línea eliminada correctamente";
                    else
                        response.Message = "Ocurrió un error inesperado, no fue posible eliminar la Línea";

                    LogInformacion(LogAcciones.Eliminar, Vista, TablaLineas, $"Linea {datosLinea?.IdLinea} - Terminal {datosLinea?.IdTerminal}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar la Línea";
                    LogError(LogAcciones.Eliminar, Vista, TablaLineas, $"Linea {datosLinea?.IdLinea} - Terminal {datosLinea?.IdTerminal} no eliminada.", ex);
                }
            }
            else
            {
                response.Message = "No se encontró la Línea que desea borrar";
            }

            return Json(response);
        }

        private IEnumerable<HelperSelectString> ObtenerListadoProductos()
        {
            return _LineasManager.ObtenerProductos().Select(a => new HelperSelectString()
            {
                Id = a.IdProducto,
                Descripcion = a.NombreCorto
            });
        }

        private IEnumerable<HelperSelectString> ObtenerListadoTerminales()
        {
            return _terminalesManager.ObtenerTerminales().Select(a => new HelperSelectString()
            {
                Id = a.IdTerminal,
                Descripcion = a.Terminal
            });
        }

        private IEnumerable<HelperSelectInt> ObtenerListadoEstados()
        {
            return _LineasManager.ObtenerEstadosLinea().Select(a => new HelperSelectInt()
            {
                Id = a.IdEstado,
                Descripcion = a.Descripcion
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.LineasAccionE)]
        public IActionResult EditarLinea([FromBody] DatosLineaPeticion datosLinea)
        {
            var LineaActual = _LineasManager.ObtenerLinea(datosLinea.IdLinea, datosLinea.IdTerminal, new String[3] { "IdTerminalNavigation", "IdEstadoNavigation", "IdProductoNavigation" });

            var viewModel = new GestionLIneaViewModel()
            {
                Titulo = "Editar Línea",
                Accion = "Actualizar",
                Lectura = true,

                IdLinea = datosLinea.IdLinea,
                Terminal = LineaActual.IdTerminalNavigation.Terminal,
                Estado = LineaActual.IdEstadoNavigation.Descripcion,
                Producto = LineaActual.IdProductoNavigation.NombreCorto,
                Capacidad = LineaActual.Capacidad,
                DensidadAforo = LineaActual.DensidadAforo,
                Observaciones = LineaActual.Observaciones,


                EstadosLinea = _LineasManager.ObtenerEstadosLinea().Select(e => e.Descripcion),
                Terminales = _terminalesManager.ObtenerTerminales().Select(e => e.Terminal).ToList(),
                Productos = _LineasManager.ObtenerProductos().Select(e => e.NombreCorto).ToList(),

            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionLinea", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionsAuthorize(Permissions.LineasAccionCN)]
        public async Task<IActionResult> CrearLinea([FromForm] GestionLIneaViewModel addLineaViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var IdProducto = _LineasManager.ObtenerProductos().Where(e => e.NombreCorto == addLineaViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _LineasManager.ObtenerEstadosLinea().Where(e => e.Descripcion == addLineaViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _terminalesManager.ObtenerTerminales().Where(e => e.Terminal == addLineaViewModel.Terminal).FirstOrDefault().IdTerminal;

                    var Linea = (addLineaViewModel.ExtraerLinea(IdTerminal, IdProducto, IdEstado));

                    response.Result = await _LineasManager.CrearLineaAsync(Linea);
                    if (response.Result)
                    {
                        response.Message = "Línea creada correctamente";
                        response.Result = true;
                        response.Payload = _LineasManager.AgregarProductosTerminalesEstadosLinea(Linea, true);
                    }

                    else
                        response.Message = "La Línea ya existe";

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaLineas, $"Linea {addLineaViewModel?.IdLinea} - Terminal {addLineaViewModel?.Terminal}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear la Línea";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaLineas, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaLineas, $"No fue posible crear linea {addLineaViewModel?.IdLinea} - terminal {addLineaViewModel?.Terminal}. {response?.Message}");
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionsAuthorize(Permissions.LineasAccionE)]
        public async Task<IActionResult> ActualizarLinea([FromForm] GestionLIneaViewModel updateLineaViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {

                    var IdProducto = _LineasManager.ObtenerProductos().Where(e => e.NombreCorto == updateLineaViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _LineasManager.ObtenerEstadosLinea().Where(e => e.Descripcion == updateLineaViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _terminalesManager.ObtenerTerminales().Where(e => e.Terminal == updateLineaViewModel.Terminal).FirstOrDefault().IdTerminal;

                    var anteriorLinea = _LineasManager.ObtenerLinea(updateLineaViewModel.IdLinea, updateLineaViewModel.Terminal);
                    var Linea = (updateLineaViewModel.ExtraerLinea(IdTerminal, IdProducto, IdEstado));

                    response.Result = await _LineasManager.ActualizarLineaAsync(Linea);
                    if (response.Result)
                    {
                        response.Message = "La Línea fue actualizada correctamente";
                        response.Result = true;
                        response.Payload = _LineasManager.AgregarProductosTerminalesEstadosLinea(Linea, true);
                    }
                    else
                        response.Message = "La Línea no existe";

                    LogInformacionActualizar(VistaGestion, TablaLineas, $"Linea {updateLineaViewModel?.IdLinea} - Terminal {updateLineaViewModel?.Terminal}. {response?.Message}", anteriorLinea, Linea);
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear la Línea";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaLineas, $"Error persistiendo linea {updateLineaViewModel?.IdLinea} - Terminal {updateLineaViewModel?.Terminal}", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar la Línea";
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaLineas, $"No fue posible actualizar linea {updateLineaViewModel?.IdLinea} - Terminal {updateLineaViewModel?.Terminal}. {response?.Message}");
            }

            return Json(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionsAuthorize(Permissions.LineasAccionE)]
        public async Task<IActionResult> ActualizarLineastado([FromForm] GestionLIneaViewModel ActualizarLineaViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var IdProducto = _LineasManager.ObtenerProductos().Where(e => e.NombreCorto == ActualizarLineaViewModel.Producto).FirstOrDefault().IdProducto;
                    var IdEstado = _LineasManager.ObtenerEstadosLinea().Where(e => e.Descripcion == ActualizarLineaViewModel.Estado).FirstOrDefault().IdEstado;
                    var IdTerminal = _terminalesManager.ObtenerTerminales().Where(e => e.Terminal == ActualizarLineaViewModel.Terminal).FirstOrDefault().IdTerminal;

                    response.Result = await _LineasManager.ActualizarLineaAsync(ActualizarLineaViewModel.ExtraerLinea(IdTerminal, IdProducto, IdEstado));
                    if (response.Result)
                    {
                        response.Message = "Línea actualizada correctamente";
                    }
                    else
                        response.Message = "La Línea no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear la Línea";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar la Línea";
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.LineasAccionCN, Permissions.LineasAccionB, Permissions.LineasAccionE, Permissions.None, Permissions.None, Permissions.None);

            return Json(permisos);
        }



    }

}
