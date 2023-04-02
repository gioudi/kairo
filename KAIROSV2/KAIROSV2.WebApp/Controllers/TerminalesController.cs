using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Data.Contracts;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloTerminales)]
    public class TerminalesController : BaseController
    {
        #region Variables 
        private const string Vista = "Terminales";
        private const string VistaGestion = "Gestión terminal";
        private const string TablaTerminales = "T_Terminales";
        private readonly IProductosManager _productosManager;
        private readonly ITerminalesManager _TerminalesManager;
        private readonly ITerminalesCompañiasManager _TerminalesCompañiasManager;
        private readonly ICompañiasManager _CompañiasManager;
        private readonly IAreasManager _AreasManager;

        #endregion 

        #region Constructores
        public TerminalesController(ITerminalesManager TerminalesManager, 
            ICompañiasManager CompañiasManager, 
            ITerminalesCompañiasManager TerminalesCompañiasManager , 
            IAreasManager AreasManager,
            IProductosManager productosManager)
        { 
            _TerminalesManager = TerminalesManager;
            _CompañiasManager = CompañiasManager;
            _TerminalesCompañiasManager = TerminalesCompañiasManager;
            _AreasManager = AreasManager;
            _productosManager = productosManager;
        }

        #endregion

        #region Metodos IAction 
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaTerminales, $"Ingreso a vista {Vista}");

            return View("Index", new ListViewModel<TTerminal>
            {
                Entidades = _TerminalesManager.ObtenerTerminales(new string[2] { "IdEstadoNavigation", "IdAreaNavigation" }),
                ActionsPermission = new ActionsPermission(User, Permissions.TerminalesAccionCN, Permissions.TerminalesAccionB, Permissions.TerminalesAccionE, Permissions.TerminalesAccionVD, Permissions.None, Permissions.None)
            });


            //return View("GestionTerminalTest", new TerminalCompletaViewModel
            //{
            //    Terminal = _TerminalesManager.ObtenerTerminal("PRIMAX", "IdEstadoNavigation"),
            //    Compañias = ObtenerCompañiasAsignadas(),
            //    TerminalCompañias = _TerminalesCompañiasManager.ObtenerTerminalCompañias("PRIMAX"),
            //    Productos = new List<TProducto>(),
            //    TerminalCompañiasProductos = new List<TTerminalCompañiasProducto>()
            //});

            //return View("GestionTerminal", new GestionTerminalViewModel
            //{
            //    Terminal = _TerminalesManager.ObtenerTerminal("PRIMAX", new String [2] { "IdEstadoNavigation" , "IdAreaNavigation" }),
            //    EstadosTerminal = _TerminalesManager.ObtenerEstadosTerminal(),
            //    Areas = _AreasManager.ObtenerAreas().Select(e => e.Area).ToList(),
            //    Compañias = ObtenerCompañiasAsignadas(),
            //    TerminalCompañias = _TerminalesCompañiasManager.ObtenerTerminalCompañias("PRIMAX"),
            //    Productos = new List<TProducto>(),
            //    TerminalCompañiasProductos = new List<TTerminalCompañiasProducto>()
            //}) ;
        }
                

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionCN)]
        public IActionResult NuevaTerminal()
        {
            var viewModel = new GestionTerminalViewModel()
            {
                Titulo = "Nueva Terminal",
                Accion = "",
                EstadosTerminal = _TerminalesManager.ObtenerEstadosTerminal().Select(e => e.Descripcion),
                Areas = _AreasManager.ObtenerAreas().Select(e => e.Area).ToList(),
                Terminal = new TTerminal(),
                Compañias = _CompañiasManager.ObtenerCompañias(),
                Productos = _productosManager.ObtenerProductosTerminalesRecetas(""),
                TerminalCompañias = new List<TerminalCompañiaViewModel>(),
                Lectura = true
            };

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionTerminal", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionE)]
        public IActionResult EditarTerminal([FromBody] DatosTerminalPeticion datosTerminal)
        {
            //var Terminal = _TerminalesManager.ObtenerTerminal(datosTerminal.Terminal, new String[1] { "TTerminalCompañia" });
            


            var viewModel = new GestionTerminalViewModel()
            {
                Titulo = "Editar Terminal",
                Accion = "Actualizar",
                Lectura = false,
                IdTerminal = datosTerminal.Terminal,
                Terminal = _TerminalesManager.ObtenerTerminal(datosTerminal.Terminal, new String[3] { "IdEstadoNavigation", "IdAreaNavigation" , "TTerminalCompañia" }),
                EstadosTerminal = _TerminalesManager.ObtenerEstadosTerminal().Select(e => e.Descripcion),
                Areas = _AreasManager.ObtenerAreas().Select(e => e.Area).ToList(),
                Compañias = ObtenerCompañiasAsignadas(datosTerminal.Terminal),
                TerminalCompañias = ExtraerCompañias( _TerminalesCompañiasManager.ObtenerTerminalCompañias(datosTerminal.Terminal)),
                Productos = _productosManager.ObtenerProductosTerminalesRecetas(datosTerminal.Terminal),
                TerminalCompañiasProductos = new List<TTerminalCompañiasProducto>()
            };
            //viewModel.AsignarTerminal(Terminal);

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionTerminal", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionB)]
        public async Task<IActionResult> BorrarTerminal([FromBody] string idTerminal)
        {
            var response = new MessageResponse();
            var Terminal = await _TerminalesManager.ObtenerTerminalAsync(idTerminal);

            if (Terminal != null)
            {
                try
                {
                    response.Message = EliminarCompañiasATerminal(idTerminal);
                    if (response.Message == "")
                    {
                        response.Result = await _TerminalesManager.BorrarTerminalAsync(idTerminal);

                        if (response.Result)
                            response.Message = "Terminal eliminado correctamente";
                        else
                            response.Message = "Ocurrió un error inesperado, no fue posible eliminar el Terminal";

                        LogInformacion(LogAcciones.Eliminar, Vista, TablaTerminales, $"Terminal {idTerminal}. {response?.Message}");
                    }
                    else
                        response.Message = "No fue posible borrar las compañías de la Terminal";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Ocurrió un error no es posible borrar el Terminal";
                    LogError(LogAcciones.Eliminar, Vista, TablaTerminales, $"Terminal {idTerminal} no eliminado.", ex);
                }

            }
            else
            {
                response.Message = "No se encontró el Terminal que desea borrar";
            }

            return Json(response);
        }

        [HttpPost]
        public IActionResult ModalHabilitarCompania([FromBody] DatosTerminalPeticion datosTerminal)
        {

            var Compañias = _CompañiasManager.ObtenerCompañias();

            var viewModel = new GestionTerminalCompañiaViewModel()
            {
                Titulo = "Nueva Terminal",
                Accion = "Crear",
                Estado = "Activo",
                IdCompañia = datosTerminal.Compañia,
                Compañia = Compañias.First(e => e.IdCompañia == datosTerminal.Compañia).Nombre,
                CompañiasAgrupadora = new List<SelectListItem>(),
                IdTerminal = datosTerminal.Terminal,
                Lectura = false

            };

            Compañias?.ToList().ForEach(e => viewModel.CompañiasAgrupadora.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = e.Nombre, Value = e.IdCompañia }));
            LogInformacion(LogAcciones.IngresoVista, "Gestión compañía", "", "Ingresó a la vista Gestión compañía");
            return PartialView("_GestionCompania" , viewModel);
        }


        [HttpPost]
        public IActionResult DatosProductoTerminal([FromBody] DatosTerminalProductoPeticion terminalProductoPeticion)
        {
            ViewBag.Lectura = terminalProductoPeticion.Lectura;
            ViewBag.Accion = "Actualizar";
            var productoAsignado = _productosManager.ObtenerProductoRecetaAsignadoTerminal(terminalProductoPeticion.IdTerminal, terminalProductoPeticion.IdProducto);
            return PartialView("_GestionRecetas", productoAsignado);
        }

        [HttpPost]
        public IActionResult AsignarProducto([FromBody] DatosTerminalProductoPeticion terminalProductoPeticion)
        {
            ViewBag.Lectura = false;
            ViewBag.Accion = "Crear";
            var productoAsignado = _productosManager.ObtenerProductoRecetaAsignadoTerminal(terminalProductoPeticion.IdTerminal, terminalProductoPeticion.IdProducto);
            return PartialView("_GestionRecetas", productoAsignado);
        }

        [HttpPost]
        public IActionResult DesasignarProducto([FromBody] DatosTerminalProductoPeticion terminalProductoPeticion)
        {
            var response = new MessageResponse();
            try
            {
                _productosManager.BorrarProductoTerminalReceta(terminalProductoPeticion.IdProducto, terminalProductoPeticion.IdTerminal);
                response.Result = true;
                response.Message = "Producto des-asignado correctamente";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = "Ocurrió un error inesperado, no fue posible des-asignar el producto";
            }

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAsignacionProducto([FromForm] ProductoTerminalDto addProductoTerminalViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _productosManager.AsignarProductoRecetaATerminal(addProductoTerminalViewModel);
                    response.Result = result.Result;
                    if (response.Result) 
                        response.Message = "Asignación creada correctamente";
                    else
                        response.Message = result.GetFailuresToString();                    
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear la asignación";
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarAsignacionProducto([FromForm] ProductoTerminalDto addProductoTerminalViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _productosManager.AsignarProductoRecetaATerminal(addProductoTerminalViewModel);
                    response.Result = result.Result;
                    if (response.Result)
                        response.Message = "Asignación actualizada correctamente";
                    else
                        response.Message = result.GetFailuresToString();
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar la asignación";
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
            }

            return Json(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTerminal([FromForm] GestionTerminalViewModel addTerminalViewModel)
        {
            var response = new MessageResponse();

            if(ModelState.IsValid)
            {
                try
                {
                    response.Result = CrearSoloTerminal(addTerminalViewModel.ExtraerTerminal(_TerminalesManager.ObtenerEstadosTerminal(), _AreasManager.ObtenerAreas()));
                    if (response.Result)
                    {
                        try
                        {
                            response.Message = AsignarCompañiasATerminal(addTerminalViewModel.IdTerminal, addTerminalViewModel.TerminalCompañias);
                            if (response.Message == "")
                            {
                                response.Message = "Terminal creado correctamente";
                                response.Result = true;
                            }
                            else
                            {
                                response.Message = "No fue posible asociar las compañías a la Terminal: " + response.Message;
                                response.Result = false;
                            }

                            LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}");
                        }
                        catch (Exception ex)
                        {
                            response.Result = false;
                            response.Message = "Terminal creado, fallo la creación de terminales y compañías";
                            LogError(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}", ex);
                        }
                    }
                        
                    else
                        response.Message = "La Terminal ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Terminal";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}", ex);
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
        [PermissionsAuthorize(Permissions.TerminalesAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSoloTerminal([FromForm] GestionTerminalViewModel addTerminalViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = CrearSoloTerminal(addTerminalViewModel.ExtraerTerminal(_TerminalesManager.ObtenerEstadosTerminal(), _AreasManager.ObtenerAreas()));
                    if (response.Result)
                    {
                        response.Message = "Terminal creado correctamente";
                    }

                    else
                        response.Message = "La Terminal ya existe";

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Terminal";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algún dato";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"No fue posible crear terminal {addTerminalViewModel?.IdTerminal} - compañía {addTerminalViewModel?.IdCompañiaOperadora}. {response?.Message}");
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarTerminal([FromForm] GestionTerminalViewModel updateUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var anteriorTerminal = _TerminalesManager.ObtenerTerminal(updateUserViewModel.IdTerminal);
                    var terminal = updateUserViewModel.ExtraerTerminal(_TerminalesManager.ObtenerEstadosTerminal(), _AreasManager.ObtenerAreas());
                    response.Result = ActualizarSoloTerminal(terminal);
                    if (response.Result)
                    {
                        try
                        {
                            AsignarCompañiasATerminal(updateUserViewModel.IdTerminal, updateUserViewModel.TerminalCompañias);
                            response.Message = "Terminal actualizada correctamente";

                            LogInformacionActualizar(VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. {response?.Message}", anteriorTerminal, terminal);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Terminal creado, fallo la actualización de terminales y compañías";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminales, $"Error persistiendo terminal {updateUserViewModel?.IdTerminal} - compañía {updateUserViewModel?.IdCompañiaOperadora}", ex);
                        }
                    }
                    else
                        response.Message = "El Terminal no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Terminal";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Terminal";
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarSoloTerminal([FromForm] GestionTerminalViewModel updateUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    var anteriorTerminal = _TerminalesManager.ObtenerTerminal(updateUserViewModel.IdTerminal);
                    var Terminal = updateUserViewModel.ExtraerTerminal(_TerminalesManager.ObtenerEstadosTerminal(), _AreasManager.ObtenerAreas());
                    Terminal.IdEstado = _TerminalesManager.ObtenerEstadosTerminal().Where(e => e.Descripcion == updateUserViewModel.Estado).FirstOrDefault().IdEstado;
                    Terminal.IdArea = _AreasManager.ObtenerAreas().Where(e => e.Area == updateUserViewModel.Area).FirstOrDefault().IdArea;
                    response.Result = ActualizarSoloTerminal(Terminal);
                    if (response.Result)
                    {
                        response.Message = "La Terminal fue actualizada correctamente";
                    }
                    else
                        response.Message = "El Terminal no existe";

                    LogInformacionActualizar(VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. {response?.Message}", anteriorTerminal, Terminal);
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Terminal";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. No fue posible actualizar", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Terminal";
            }

            return Json(response);
        }



        [HttpPost]
        [PermissionsAuthorize(Permissions.TerminalesAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarTerminalEstado([FromForm] GestionTerminalViewModel updateUserViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                //TODO: Actualizar
                try
                {
                    response.Result = ActualizarEstadoTerminal(updateUserViewModel.IdTerminal , _TerminalesManager.ObtenerEstadosTerminal().FirstOrDefault (e => e.Descripcion == updateUserViewModel.Estado).IdEstado);
                    if (response.Result)
                    {
                        try
                        {
                            AsignarCompañiasATerminal(updateUserViewModel.IdTerminal, updateUserViewModel.TerminalCompañias);
                            response.Message = "Terminal actualizada correctamente";

                            LogInformacionActualizar(VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. {response.Message}", null, null);
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Terminal creado, fallo la actualización de terminales y compañías";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. No fue posible actualizar", ex);
                        }
                    }
                    else
                        response.Message = "El Terminal no existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Terminal";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminales, $"Terminal {updateUserViewModel?.IdTerminal} - Compañía {updateUserViewModel?.IdCompañiaOperadora}. No fue posible actualizar", ex);
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el Terminal";
            }

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarCompañiaATerminal([FromForm] GestionTerminalCompañiaViewModel addTerminalViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {

                try
                {
                    response.Message = AsignarCompañiaATerminal(addTerminalViewModel.IdTerminal, addTerminalViewModel.ExtraerCompañia());
                    if (response.Message == "")
                    {
                        response.Message = "Compañías asociadas a la Terminal correctamente";
                        response.Result = true;
                    }
                    else
                    {
                        response.Message = "No se pudo asignar las Compañías a la Terminal " + response.Message;
                        response.Result = false;
                    }

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaTerminales, $"Terminal {addTerminalViewModel?.IdTerminal} - Compañía {addTerminalViewModel?.IdCompañia}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Fallo la asignación de compañías a la Terminal";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaTerminales, response?.Message, ex);
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
        [PermissionsAuthorize(Permissions.TerminalesAccionB)]
        public async Task<IActionResult> EliminarCompañiaDeTerminal([FromBody] DatosTerminalPeticion datosTerminal)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {

                try
                {
                    response.Message = EliminarCompañiaATerminal(datosTerminal.Terminal , datosTerminal.Compañia);
                    if (response.Message == "")
                    {
                        response.Message = "Compañías asociadas a la Terminal correctamente";
                        response.Result = true;
                    }

                    //else
                    //{
                    //    response.Message = "No se pudo eliminar las Compañias a la Terminal: " + response.Message;
                    //    response.Result = false;
                    //}
                    LogInformacion(LogAcciones.Eliminar, Vista, TablaTerminales, $"Terminal {datosTerminal?.Terminal} - Compañía {datosTerminal?.Compañia}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Fallo al eliminar las compañías a la Terminal";
                    LogError(LogAcciones.Eliminar, Vista, TablaTerminales, $"Terminal {datosTerminal?.Terminal} - Compañía {datosTerminal?.Compañia} no eliminado.", ex);
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
        public IActionResult RecargarVistaListaCompania([FromBody] DatosTerminalPeticion datosTerminal)
        {
            var viewModel = new GestionTerminalViewModel()
            {
                Titulo = "Editar Terminal",
                Accion = "Actualizar",
                Lectura = datosTerminal.Lectura,
                IdTerminal = datosTerminal.Terminal,
                Compañias = ObtenerCompañiasAsignadas(datosTerminal.Terminal),                
                TerminalCompañias = ExtraerCompañias( _TerminalesCompañiasManager.ObtenerTerminalCompañias(datosTerminal.Terminal))

            };

            return PartialView("_ListaCompanias", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.TerminalesAccionCN, Permissions.TerminalesAccionB, Permissions.TerminalesAccionE, Permissions.TerminalesAccionVD, Permissions.None, Permissions.None);

            return Json(permisos);
        }
        #endregion

        #region Metodos 
        private IEnumerable<TCompañia> ObtenerCompañiasAsignadas(string IdTerminal)
        {
            var TodasCompañias = _CompañiasManager.ObtenerCompañias();
            var CompañiasAsignadas = _TerminalesCompañiasManager.ObtenerTerminalCompañias(IdTerminal).Select( e => e.IdCompañia);

            var Compañias = TodasCompañias.Where( e => CompañiasAsignadas.Contains( e.IdCompañia));
            var CompañiasDisponibles = TodasCompañias.Except(Compañias);

            return CompañiasDisponibles;
        }

        public bool ActualizarEstadoTerminal(string IdTerminal, int IdEstado)
        {
            return _TerminalesManager.ActualizarEstadoTerminal(IdTerminal, IdEstado);
        }

        public string AsignarCompañiasATerminal(string  IdTerminal , IEnumerable<TTerminalCompañia> TerminalCompañias  )
        {
            return _TerminalesCompañiasManager.AsignarCompañiasATerminal( IdTerminal,  TerminalCompañias);
        }

        public string AsignarCompañiaATerminal(string IdTerminal, TTerminalCompañia TerminalCompañias)
        {
            return _TerminalesCompañiasManager.AsignarCompañiaATerminal(IdTerminal, TerminalCompañias);
        }

        public string EliminarCompañiasATerminal(string IdTerminal)
        {
            return _TerminalesCompañiasManager.EliminarCompañiasATerminal(IdTerminal);
        }

        public string EliminarCompañiaATerminal(string IdTerminal , string IdCompañia)
        {
            return _TerminalesCompañiasManager.EliminarCompañiaATerminal(IdTerminal, IdCompañia );
        }

        public bool ActualizarSoloTerminal(TTerminal UnicaTerminal)
        {
            return _TerminalesManager.ActualizarTerminal(UnicaTerminal);
        }

        public bool CrearSoloTerminal(TTerminal UnicaTerminal)
        {
            return _TerminalesManager.CrearTerminal(UnicaTerminal);
        }

        private IEnumerable<TerminalCompañiaViewModel> ExtraerCompañias(IEnumerable<TTerminalCompañia> Companias)
        {
            List<TerminalCompañiaViewModel> TerminalCompanias = new List<TerminalCompañiaViewModel>();
            var TodasCompanias = _CompañiasManager.ObtenerCompañias();

            foreach (var Compania in Companias)
            {
                var Comp = new TerminalCompañiaViewModel() 
                {
                    IdCompañia = Compania.IdCompañia,
                    Compañia = TodasCompanias.Any(e => e.IdCompañia == Compania.IdCompañia) ? TodasCompanias.First(e => e.IdCompañia == Compania.IdCompañia).Nombre : "NO EXISTE",
                    CompañiaAgrupadora = TodasCompanias.Any(e => e.IdCompañia == Compania.IdCompañiaAgrupadora) ? TodasCompanias.First(e => e.IdCompañia == Compania.IdCompañiaAgrupadora).Nombre : "NO EXISTE",
                    Estado = Compania.Estado,
                    IdCompañiaAgrupadora = Compania.IdCompañiaAgrupadora,
                    IdTerminal = Compania.IdTerminal,
                    PorcentajePropiedad = Compania.PorcentajePropiedad,
                    SicomCompañiaTerminal = Compania.SicomCompañiaTerminal,
                    Socia = Compania.Socia,
                };

                TerminalCompanias.Add(Comp);
            }

            return TerminalCompanias;
        }

        #endregion


    }
}
