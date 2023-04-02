using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Business.Managers;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloDespachos)]
    public class DespachosController : BaseController
    {
        #region
        private const string Vista = "Despachos";
        private const string VistaGestion = "Gestión Despacho";
        private const string TablaDespachos = "T_Despachos";
        private readonly ITerminalesManager _TerminalesManager;
        private readonly IDespachosManager _DespachosManager;
        private readonly ITerminalesCompañiasManager _ITerminalesCompañiasManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;
        private readonly ITanquesManager _TanquesManager;
        private readonly IContadoresManager _ContadoresManager;
        private readonly ICompañiasManager _CompañiasManager;
        private readonly IProductosManager _ProductosManager;
        #endregion

        #region Constructores
        public DespachosController(ITerminalesManager TerminalesManager, IDespachosManager DespachosManager, ITerminalesCompañiasManager TerminalesCompañiasManager, 
            ILogManager logManager, IMapper mapper, IContadoresManager ContadoresManager, ITanquesManager TanquesManager, ICompañiasManager CompañiasManager,
            IProductosManager ProductosManager)
        {
            _TerminalesManager = TerminalesManager;
            _DespachosManager = DespachosManager;
            _ITerminalesCompañiasManager = TerminalesCompañiasManager;
            _logManager = logManager;
            _mapper = mapper;
            _TanquesManager = TanquesManager;
            _ContadoresManager = ContadoresManager;
            _CompañiasManager = CompañiasManager;
            _ProductosManager = ProductosManager;

        }
        #endregion

        #region Metodos IAction 	
        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaDespachos, $"Ingreso a vista {Vista}");

            var Terminales = ObtenerTerminalesPorUsuario();

            var DespachosViewModel = new DespachosViewModel()
            {
                FechaActual = DateTime.Now,
                Terminales = new List<SelectListItem>(),
                Compañias = new List<SelectListItem>(),
                DespachosConsolidados = new List<DespachosConsolidadosDTO>(),
                DespachosDetallados = new List<DespachosDetalladosDTO>(),
                ActionsPermission = new ActionsPermission(User, Permissions.DespachosAccionCN, Permissions.DespachosAccionB, Permissions.DespachosAccionE, Permissions.None, Permissions.None, Permissions.None)
            };

            Terminales?.ToList().ForEach(e => DespachosViewModel.Terminales.Add(new SelectListItem() { Text = e.Terminal, Value = e.IdTerminal }));
            
            return View( DespachosViewModel);
        }


        [HttpPost]
        public IActionResult BuscarDespachosConsolidados([FromBody] DespachosViewModel datosbuscar)
        {
            var response = new MessageResponse();
            response.Result = false;

            IEnumerable<DespachosConsolidadosDTO> Despachos = new List<DespachosConsolidadosDTO>();

            var FechaCorte = Convert.ToDateTime(FormatoFecha(datosbuscar.FechaCorte));

            if (datosbuscar.Compañia == "TODAS")
            {
                var Compañias = ObtenerCompañiasTerminal(datosbuscar.Terminal).Select(e => e.IdCompañia).ToList();
                Despachos = _DespachosManager.ObtenerDespachosConsolidados(datosbuscar.Terminal, Compañias, FechaCorte);
            }
            else
            {
                Despachos = _DespachosManager.ObtenerDespachosConsolidados(datosbuscar.Terminal, datosbuscar.Compañia, FechaCorte);
            }

            var viewModel = new DespachosViewModel()
            {
                DespachosConsolidados = Despachos
            };

            response.Result = true;
            response.Message = "Datos Consultados";
            
            return PartialView("_ListaConsolidado", viewModel);

        }

        [HttpPost]
        public IActionResult BuscarDespachosDetallados([FromBody] DespachosViewModel datosbuscar)
        {
            var response = new MessageResponse();
            response.Result = false;

            IEnumerable<DespachosDetalladosDTO> Despachos = new List<DespachosDetalladosDTO>();

            var Fecha_Corte = Convert.ToDateTime(FormatoFecha(datosbuscar.FechaCorte));

            if (datosbuscar.Compañia == "TODAS")
            {
                var Compañias = ObtenerCompañiasTerminal(datosbuscar.Terminal).Select(e => e.IdCompañia).ToList();
                Despachos = _DespachosManager.ObtenerDespachosDetallados(datosbuscar.Terminal, Compañias, Fecha_Corte);
            }
            else
            {
                Despachos = _DespachosManager.ObtenerDespachosDetallados(datosbuscar.Terminal, datosbuscar.Compañia, Fecha_Corte);
            }

            var viewModel = new DespachosViewModel()
            {
                DespachosDetallados = Despachos,
                PermiteEditar = true,
                ActionsPermission = new ActionsPermission(User, Permissions.DespachosAccionCN, Permissions.DespachosAccionB, Permissions.DespachosAccionE, Permissions.None, Permissions.None, Permissions.None)
            };

            response.Result = true;
            response.Message = "Datos Consultados";

            return PartialView("_ListaDetalle", viewModel);

        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.DespachosAccionE)]
        public IActionResult ActualizarEstadoDespacho([FromBody] DatosDespachoPeticion datosDespacho)
        {
            var response = new MessageResponse();

            var result = _DespachosManager.ActualizarEstadoDespacho(datosDespacho.Despacho, datosDespacho.Lectura); 
            response.Result = result.Result;
            response.Message = "Despacho actualizado";

            return Json(response);
        }

        [HttpPost]
        public IActionResult ObtenerProductosPorTerminal([FromBody] string Id_Terminal)
        {
            var TodosProductos = ObtenerProductosTerminal(Id_Terminal);
            var Productos = new List<SelectListItem>();
            TodosProductos?.ToList().ForEach(e => Productos.Add(new SelectListItem() { Text = e.NombreCorto, Value = e.IdProducto}));
            return Json(Productos?.OrderBy(e => e.Text)?.ToList());
        }

        [HttpPost]
        public IActionResult ConsultarCompañias([FromBody] string idTerminal)
        {
            var TodasCompañias = ObtenerCompañiasTerminal(idTerminal);
            var Compañias = new List<SelectListItem>();
            TodasCompañias?.ToList().ForEach(e => Compañias.Add(new SelectListItem() { Text = e.Nombre, Value = e.IdCompañia}));
            return Json(Compañias?.OrderBy(e => e.Text)?.ToList());
        }

        [HttpPost]
        public IActionResult ObtenerDetalleDetalle([FromBody] string idDespacho)
        {
            var response = new MessageResponse();
            response.Payload = _DespachosManager.ObtenerDetalleDetalle(idDespacho);
            response.Result = true;
            response.Message = "";
            return Json(response);
        }

        [HttpPost]
        public IActionResult ObtenerDetalleConsolidado([FromBody] DatosDespachoConsolidadoDetalle datosDespachoCD)
        {
            var response = new MessageResponse();
            response.Payload = _DespachosManager.ObtenerDetalleConsolidado(datosDespachoCD.Terminal, datosDespachoCD.Compañia,
                datosDespachoCD.IdProducto, Convert.ToDateTime(FormatoFecha(datosDespachoCD.Fecha_Corte)));
            response.Result = true;
            response.Message = "";
            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.DespachosAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearDespacho([FromForm] GestionDespachosViewModel addDespachoViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {

                    if (!_DespachosManager.TieneFechaCierre(addDespachoViewModel.FechaDespacho , addDespachoViewModel.Terminal))
                    {
                        var Despacho = addDespachoViewModel.ExtraerDespacho();

                        if (Despacho.TDespachosComponentes.Count() > 0)
                        {
                            Despacho.Id_Despacho = addDespachoViewModel.Terminal + addDespachoViewModel.Compañia + addDespachoViewModel.No_Orden + addDespachoViewModel.IdProducto + addDespachoViewModel.Compartimento;
                            var creationResponse = _DespachosManager.CrearDespacho(Despacho);
                            response.Result = creationResponse.Result;
                            if (response.Result)
                            {
                                response.Message = "Despacho creado correctamente";                                
                            }
                            else
                                response.Message = creationResponse.GetFailuresToString();
                            LogInformacion(LogAcciones.Insertar, VistaGestion, TablaDespachos, $"Despacho {addDespachoViewModel?.Id_Despacho}. {response?.Message}");
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = ("El despacho debe tener al menos un componente. Revise el producto o el volumen cargado");
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "La fecha de corte seleccionada ya tiene una fecha de cierre";
                    }


                }
                catch (Exception ex)
                {
                    response.Message = "Ocurrio un error creando el Despacho";
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
        [PermissionsAuthorize(Permissions.DespachosAccionCN)]
        public IActionResult NuevoDespacho([FromBody] DatosDespachoNuevoPeticion datosbuscar)
        {
            var Terminales = ObtenerTerminalesPorUsuario();
            var Compañias = ObtenerCompañiasTerminal(Terminales.Any() ? datosbuscar.Id_Terminal : "");
            var Contadores = _ContadoresManager.ObtenerContadores();
            var Tanques = _TanquesManager.ObtenerTanques();
            var Productos = ObtenerProductosTerminal(Terminales.Any() ? datosbuscar.Id_Terminal : "");
            var ShipTos = _DespachosManager.ObtenerShipTos();
            var SoldTos = _DespachosManager.ObtenerSoldTos();
                        
            var viewModel = new GestionDespachosViewModel()
            {
                Titulo = "Crear Despacho",
                Accion = "Crear",              
                Terminal = datosbuscar.Id_Terminal,
                Compañia = datosbuscar.Id_Compañia,
                //FechaDespacho = "",
                Lectura = false

            };

            //Contadores?.ToList().ForEach(e => viewModel.Contadores.Add(new SelectListItem() { Text = e.IdContador, Value = e.IdContador }));
            //Tanques?.ToList().ForEach(e => viewModel.Tanques.Add(new SelectListItem() { Text = e.IdTanque, Value = e.IdTanque}));
            Terminales?.ToList().ForEach(e => viewModel.Terminales.Add(new SelectListItem() { Text = e.Terminal, Value = e.IdTerminal }));
            Compañias?.ToList().ForEach(e => viewModel.Compañias.Add(new SelectListItem() { Text = e.Nombre, Value = e.IdCompañia }));
            Productos?.ToList().ForEach(e => viewModel.Productos.Add(new SelectListItem() { Text = e.NombreCorto, Value = e.IdProducto}));
            //ShipTos?.ToList().ForEach(e => viewModel.Ship_Tos.Add(new SelectListItem() { Text = e.Ship_to.ToString(), Value = e.Ship_to.ToString() }));
            SoldTos?.ToList().ForEach(e => viewModel.Sold_Tos.Add(new SelectListItem() { Text = e.Sold_to.ToString(), Value = e.Sold_to.ToString() }));

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionDespachos", viewModel);
        }


        [HttpPost]
        [PermissionsAuthorize(Permissions.DespachosAccionE)]
        public IActionResult EditarDespacho([FromBody] DatosDespachoPeticion datosDespacho)
        {
            var Despacho = _DespachosManager.ObtenerDespacho(datosDespacho.Despacho, "TDespachosComponentes");


            var Terminales = ObtenerTerminalesPorUsuario();
            var Compañias = ObtenerCompañiasTerminal(Terminales.Any() ? Terminales.First().IdTerminal : "");
            var Contadores = _ContadoresManager.ObtenerContadores();
            var Tanques = _TanquesManager.ObtenerTanques().Where(e => e.IdTerminal == Despacho.Id_Terminal).ToList();
            var Productos = ObtenerProductosTerminal(Terminales.Any() ? Despacho.Id_Terminal : "");
            var SoldTos = _DespachosManager.ObtenerSoldTos();

            var viewModel = new GestionDespachosViewModel();

            //Contadores?.ToList().ForEach(e => viewModel.Contadores.Add(new SelectListItem() { Text = e.IdContador, Value = e.IdContador }));
            //Tanques?.ToList().ForEach(e => viewModel.Tanques.Add(new SelectListItem() { Text = e.IdTanque, Value = e.IdTanque }));
            Terminales?.ToList().ForEach(e => viewModel.Terminales.Add(new SelectListItem() { Text = e.Terminal, Value = e.IdTerminal }));
            Compañias?.ToList().ForEach(e => viewModel.Compañias.Add(new SelectListItem() { Text = e.Nombre, Value = e.IdCompañia }));
            Productos?.ToList().ForEach(e => viewModel.Productos.Add(new SelectListItem() { Text = e.NombreCorto, Value = e.IdProducto }));
            //ShipTos?.ToList().ForEach(e => viewModel.Ship_Tos.Add(new SelectListItem() { Text = e.Ship_to.ToString(), Value = e.Ship_to.ToString() }));
            SoldTos?.ToList().ForEach(e => viewModel.Sold_Tos.Add(new SelectListItem() { Text = e.Sold_to.ToString(), Value = e.Sold_to.ToString() }));

            viewModel.Componentes = ExtraerComponentes(Despacho.TDespachosComponentes.ToList(), Productos);

            viewModel.Id_Despacho = Despacho.Id_Despacho;
            viewModel.FechaDespacho = Despacho.Fecha_Final_Despacho;
            viewModel.Terminal = Despacho.Id_Terminal;
            viewModel.Compañia = Despacho.Id_Compañia;
            viewModel.No_Orden = Despacho.No_Orden;
            viewModel.IdProducto = Productos.Any(e => e.IdProducto == Despacho.Id_Producto_Despacho) ? Despacho.Id_Producto_Despacho : "";
            viewModel.Observaciones = Despacho.Observaciones;
            viewModel.Placa_Cabezote = Despacho.Placa_Cabezote;
            viewModel.Placa_Trailer = Despacho.Placa_Trailer;
            viewModel.Volumen_Ordenado = Despacho.Volumen_Ordenado;
            viewModel.Volumen_Cargado = Despacho.Volumen_Cargado;
            viewModel.Cedula_Conductor = Despacho.Cedula_Conductor;
            viewModel.Sold_To = Despacho.Sold_To;
            viewModel.Cedula_Conductor = Despacho.Cedula_Conductor;
            viewModel.Compartimento = Despacho.Compartimento;

            viewModel.Lectura = true;
            viewModel.Titulo = "Editar Despacho";
            viewModel.Accion = "Actualizar";

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionDespachos", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.DespachosAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarDespacho([FromForm] GestionDespachosViewModel updateDespachoViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {

                try
                {
                    var anteriorDespacho = _DespachosManager.ObtenerDespacho(updateDespachoViewModel.Id_Despacho);
                    var Despacho = updateDespachoViewModel.ExtraerDespacho();
                    Despacho.Id_Corte = anteriorDespacho.Id_Corte;

                    if (!_DespachosManager.TieneFechaCierre(Despacho.Id_Corte))
                    {
                        var updateResponse = _DespachosManager.ActualizarComponentesDespacho(Despacho.TDespachosComponentes);
                        response.Result = updateResponse.Result;

                        if (response.Result)
                        {
                            try
                            {
                                var resultRecetas = _DespachosManager.ActualizarDespacho(Despacho);
                                response.Result = resultRecetas.Result;

                                response.Message = "Despacho actualizado";
                            }
                            catch (Exception ex)
                            {
                                response.Result = false;
                                response.Message = "Despacho actualizado, ocurrió un error persistiendo las recetas";
                            }

                        var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                        var anteriorData = JsonConvert.SerializeObject(anteriorDespacho, Formatting.None, serializeSettings);
                        var actualData = JsonConvert.SerializeObject(Despacho, Formatting.None, serializeSettings);
                        _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despacho", "Gestion Despacho", "T_Despachos", LogAcciones.Actualizar, $"Despacho {Despacho.Id_Despacho} actualizado. Datos antiguos {anteriorData} Datos nuevos {actualData}", LogPrioridades.Informacion);

                        }
                        else
                            response.Message = updateResponse.GetFailuresToString();
                    }

                    else
                    {
                        response.Result = false;
                        response.Message = "La fecha de corte seleccionada ya tiene una fecha de cierre";
                    }
                }

                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el Despacho";
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
        public IActionResult ObtenerRecetaActivaPorProductoTerminal([FromBody] DatosDespachoReceta datosbuscar)
        {
            var Contadores = _ContadoresManager.ObtenerContadores().ToList();
            var Tanques = _TanquesManager.ObtenerTanques().ToList();
            var ShipTos = _DespachosManager.ObtenerShipTos();

            var Componentes = ObtenerRecetaPorProductoTerminal(datosbuscar.IdTerminal, datosbuscar.IdProducto, Convert.ToDouble(datosbuscar.VolumenCargado));

            ViewData.TemplateInfo.HtmlFieldPrefix = "Componentes";

            _logManager.InsertarLogAsync("Admin", "Kairos2", "Operaciones", "Despachos", "Vista Gestión Despachos", "", LogAcciones.IngresoVista, "Ingreso a Vista Gestión Despachos", LogPrioridades.Informacion);
            return PartialView("_ListaComponentes", Componentes );
        }

        private IEnumerable<TTerminal> ObtenerTerminalesPorUsuario()
        {
            // Consulta Terminales por Usuario actual 
            var TerminalesUsuario = _DespachosManager.ConsultarTerminalesCompañiasPorUsuario(User.Claims.First().Value).Select(e => e.IdTerminal).Distinct().ToList();
            var Terminales = _TerminalesManager.ObtenerTerminales();
            return Terminales.Where(e => TerminalesUsuario.Contains(e.IdTerminal)).ToList();
        }

        private IEnumerable<TCompañia> ObtenerCompañiasTerminal(string Id_Terminal)
        {
            // Consulta Compañias para la primera terminal por Usuario actual
            var CompañiasUsuario = _DespachosManager.ConsultarTerminalesCompañiasPorUsuario(User.Claims.First().Value).Where(e => e.IdTerminal == Id_Terminal).Select(e => e.IdCompañia).Distinct().ToList();
            var Compañias = _CompañiasManager.ObtenerCompañias();
            return Compañias.Where(o => CompañiasUsuario.Contains(o.IdCompañia)).ToList();
        }

        private IEnumerable<TProducto> ObtenerProductosTerminal(string Id_Terminal)
        {
            // Consulta Productos para la terminal por Usuario actual
            var ProductosTerminal = _DespachosManager.ConsultarProductosAsociadosTerminal(Id_Terminal).Select(e => e.IdProducto).Distinct().ToList();
            return _ProductosManager.ObtenerProductos().Where(o => ProductosTerminal.Contains(o.IdProducto)).ToList();
        }

        private List<DespachosComponentesViewModel> ObtenerRecetaPorProductoTerminal(string Id_Terminal ,string Id_Producto, double Volumen_Cargado)
        {
            var ComponenteDespacho = new List<DespachosComponentesViewModel>();
            
            // Consulta Receta Activa para la terminal y producto Seleccionado 
            var RecetaExistente = _DespachosManager.ConsultarProductosAsociadosTerminal(Id_Terminal)
                .Where( e => e.IdProducto == Id_Producto  &&
                (e.FechaInicio < DateTime.Now) &&
                (e.FechaFin > DateTime.Now));

            var IdReceta = RecetaExistente.Count() > 0 ? RecetaExistente.First().IdReceta : "";

            if (IdReceta != "")
            {

                var Receta = _ProductosManager.ObtenerProductoConRecetas(Id_Producto).TProductosReceta.Where(e => e.IdReceta == IdReceta).First().TProductosRecetasComponentes;

                var Contadores = _ContadoresManager.ObtenerContadores().ToList();
                var Tanques = _TanquesManager.ObtenerTanques().ToList();
                var ShipTos = _DespachosManager.ObtenerShipTos();


                foreach (var ComponenteReceta in Receta)
                {
                    var Componente = new DespachosComponentesViewModel()
                    {
                        Lectura = false,
                        Producto_Componente = _ProductosManager.ObtenerProducto(ComponenteReceta.IdComponente).NombreCorto,
                        Volumen_Bruto = Volumen_Cargado * (ComponenteReceta.ProporcionComponente / 1000000),
                        Temperatura = 0,
                        Densidad = 0,
                        Factor = 1,
                        Volumen_Neto = Volumen_Cargado * (ComponenteReceta.ProporcionComponente / 1000000),
                        Tanque = "",
                        Contador = "",
                        Ship_To = 0

                    };

                    Tanques?.ToList().ForEach(e => Componente.Tanques.Add(new SelectListItem() { Text = e.IdTanque, Value = e.IdTanque }));
                    Contadores?.ToList().ForEach(e => Componente.Contadores.Add(new SelectListItem() { Text = e.IdContador, Value = e.IdContador }));
                    ShipTos?.ToList().ForEach(e => Componente.Ship_Tos.Add(new SelectListItem() { Text = e.Ship_to.ToString(), Value = e.Ship_to.ToString() }));

                    ComponenteDespacho.Add(Componente);
                }
            }
            
            
            return ComponenteDespacho;
        }

        private string FormatoFecha(string FechaOriginal )
        {
            var MesActual = FechaOriginal.Split("/")[1];
                        

            switch (MesActual)
            {
                case "ene":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "01");
                    break;
                case "feb":
                    FechaOriginal = FechaOriginal.Replace(MesActual,"02");
                    break;
                case "mar":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "03");
                    break;
                case "abr":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "04");
                    break;
                case "may":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "05");
                    break;
                case "jun":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "06");
                    break;
                case "jul":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "07");
                    break;
                case "ago":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "08");
                    break;
                case "sep":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "09");
                    break;
                case "oct":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "10");
                    break;
                case "nov":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "11");
                    break;
                case "dic":
                    FechaOriginal = FechaOriginal.Replace(MesActual, "12");
                    break;
                default:
                    FechaOriginal = FechaOriginal.Replace(MesActual, DateTime.Now.Month.ToString());
                    break;
            }

            return FechaOriginal;
        }

        private List<DespachosComponentesViewModel> ExtraerComponentes(IEnumerable<TDespachosComponente> Componentes, IEnumerable<TProducto> Productos)
        {
            var Contadores = _ContadoresManager.ObtenerContadores().ToList();
            var Tanques = _TanquesManager.ObtenerTanques().ToList();
            var ShipTos = _DespachosManager.ObtenerShipTos();

            var despachosComponentes = new List<DespachosComponentesViewModel>();

            foreach(var despacho in Componentes)
            {
                var viewModel = new DespachosComponentesViewModel()
                {
                    Lectura = true,
                    Contador = despacho.Contador,
                    Densidad = despacho.Densidad,
                    Producto_Componente = despacho.Id_Producto_Componente,
                    Ship_To = despacho.Ship_To,
                    Tanque = despacho.Tanque,
                    Temperatura = despacho.Temperatura,
                    Volumen_Bruto = despacho.Volumen_Bruto,
                    Volumen_Neto = despacho.Volumen_Neto,
                    Factor = Math.Round( despacho.Volumen_Neto / despacho.Volumen_Bruto, 5)

                };

                Contadores?.ToList().ForEach(e => viewModel.Contadores.Add(new SelectListItem() { Text = e.IdContador, Value = e.IdContador }));
                Tanques?.ToList().ForEach(e => viewModel.Tanques.Add(new SelectListItem() { Text = e.IdTanque, Value = e.IdTanque }));
                Productos?.ToList().ForEach(e => viewModel.Productos.Add(new SelectListItem() { Text = e.NombreCorto, Value = e.IdProducto }));
                ShipTos?.ToList().ForEach(e => viewModel.Ship_Tos.Add(new SelectListItem() { Text = e.Ship_to.ToString(), Value = e.Ship_to.ToString() }));

                despachosComponentes.Add(viewModel);
            }

            return despachosComponentes;
        }
        [HttpPost]
        public IActionResult ObtenerFechasCierreMes([FromBody] DatosConsultaPeticion peticion)
        {
            var fechasCierre = _DespachosManager.ObtenerFechasCierrePorMes(peticion.IdEntidad, peticion.Fecha)?.OrderBy(e => e.Fecha.Date).ThenByDescending(e => e.Cierre);
            return Json(fechasCierre);
        }

        [HttpPost]
        public IActionResult ObtenerFechaCierreInicial()
        {
            var fechaCierre = _DespachosManager.ObtenerFechaCierreInicial();

            return Json(fechaCierre);
        }
        #endregion
    }
}
