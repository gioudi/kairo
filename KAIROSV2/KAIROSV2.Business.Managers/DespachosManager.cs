using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using System.Threading;
using KAIROSV2.Business.Entities.DTOs;
using System.Text.Json;
using Newtonsoft.Json;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los Despachos
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad Despacho, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir Despachos.
    /// </remarks>
    public class DespachosManager : IDespachosManager
    {
        private readonly IDespachosRepository _DespachosRepository;
        private readonly IDespachosComponentesRepository _DespachosComponentesRepository;
        private readonly ILogManager _logManager;
        private readonly IDespachosEngine _DespachosEngine;
        private readonly IUsuariosTerminalCompañiaRepository _usuariosTerminalCompañiaRepository;
        private readonly ITASCortesRepository _tasCortesRepository;
        private readonly ITerminalesProductosRecetasRepository _TerminalesProductosRecetaRepository;
        private readonly IShipToRepository _ShipToRepository;
        private readonly ISoldToRepository _SoldToRepository;
        private readonly IProductosRepository _ProductosRepository;


        public DespachosManager(IDespachosRepository DespachosRepository, IDespachosComponentesRepository DespachosComponentesRepository, ILogManager LogManager, IDespachosEngine DespachosEngine,
            IUsuariosTerminalCompañiaRepository usuariosTerminalCompañiaRepository, ITASCortesRepository tasCortesRepository,
            ITerminalesProductosRecetasRepository TerminalesProductosRecetaRepository, IShipToRepository ShipToRepository, 
            ISoldToRepository SoldToRepository, IProductosRepository ProductosRepository)
        {
            _DespachosRepository = DespachosRepository;
            _DespachosComponentesRepository = DespachosComponentesRepository;
            _logManager = LogManager;
            _DespachosEngine = DespachosEngine;
            _usuariosTerminalCompañiaRepository = usuariosTerminalCompañiaRepository;
            _tasCortesRepository = tasCortesRepository;
            _ShipToRepository = ShipToRepository;
            _SoldToRepository = SoldToRepository;
            _TerminalesProductosRecetaRepository = TerminalesProductosRecetaRepository;
            _ProductosRepository = ProductosRepository;
        }

        /// <summary>
        /// Obtiene el Despacho incluyendo su imagen
        /// </summary>
        /// <param name="Id_Despacho">Id del Despacho</param>
        /// <returns>Despacho</returns>
        public TDespacho ObtenerDespacho(string Id_Despacho)
        {
            TDespacho Despacho = new TDespacho();
            if (_DespachosRepository.Existe(Id_Despacho))
                Despacho = _DespachosRepository.Obtener(Id_Despacho);
            return Despacho;
        }

        public TDespacho ObtenerDespacho(string Id_Despacho, string[] parametros)
        {
            TDespacho Despacho = new TDespacho();
            if (_DespachosRepository.Existe(Id_Despacho))
                Despacho = _DespachosRepository.Obtener(Id_Despacho, parametros);
            return Despacho;
        }

        public Task<TDespacho> ObtenerDespachoAsync(string Id_Despacho)
        {
            try
            {
                if (_DespachosRepository.Existe(Id_Despacho))
                    return _DespachosRepository.ObtenerAsync(Id_Despacho);
                else
                    return null;
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Insertar, "Despacho " + Id_Despacho + "NO Creada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return null;
            }

        }

        /// <summary>
        /// Obtiene todos los Despachos del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Despachos del sistema</returns>
        public Task<IEnumerable<TDespacho>> ObtenerTodasAsync()
        {
            return _DespachosRepository.ObtenerTodasAsync();
        }

        public IEnumerable<TDespacho> ObtenerTodas(params string[] includes)
        {
            return _DespachosRepository.ObtenerTodas(includes);
        }
        
        public IEnumerable<TDespacho> ObtenerTodas()
        {
            return _DespachosRepository.ObtenerTodas();
        }

        public IEnumerable<TShipTo> ObtenerShipTos()
        {
            return _ShipToRepository.ObtenerTodas();
        }

        public IEnumerable<TSoldTo> ObtenerSoldTos()
        {
            return _SoldToRepository.ObtenerTodas();
        }

        public IEnumerable<DespachosConsolidadosDetalleDTO> CalcularDespachosConsolidadosDetalle(string Id_Terminal , string Id_Compañia , string Id_Producto,  DateTime Fecha_Corte)
        {
            var Consolidados = _DespachosEngine.CalcularDespachosConsolidadosDetalle(Id_Terminal, Id_Compañia, Id_Producto, Fecha_Corte);
            return Consolidados;
        }

        public IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, string Id_Compañia, DateTime Fecha_Corte)
        {
            var Consolidados = _DespachosEngine.ObtenerDespachosConsolidados(Id_Terminal, Id_Compañia, Fecha_Corte);
            return Consolidados;            
        }

        public IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, List<string> Id_Compañia, DateTime Fecha_Corte)
        {
            var Consolidados = _DespachosEngine.ObtenerDespachosConsolidados(Id_Terminal, Id_Compañia, Fecha_Corte);
            return Consolidados;
        }

        public IEnumerable<TDespachosComponente> ObtenerDespachosComponentes(string Id_Terminal, string Id_Compañia, string Id_Producto , DateTime Fecha_Corte)
        {
            var DespachosComponentes = _DespachosEngine.ObtenerDespachosComponentes(Id_Terminal, Id_Compañia, Id_Producto, Fecha_Corte);

            return DespachosComponentes;
        }

        public IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, string Id_Compañia, DateTime Fecha_Corte)
        {
            var Detallados = _DespachosEngine.ObtenerDespachosDetallados(Id_Terminal, Id_Compañia, Fecha_Corte );

            return Detallados;
        }

        public IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, List<string> Id_Compañia, DateTime Fecha_Corte)
        {
            var Detallados = _DespachosEngine.ObtenerDespachosDetallados(Id_Terminal, Id_Compañia, Fecha_Corte);

            return Detallados;
        }
        public IEnumerable<DespachosDetalladosDetalleDTO> ObtenerDetalleDetalle(string Id_Despacho)
        {
            var Detallados = _DespachosRepository.Obtener(Id_Despacho, "TDespachosComponentes");
            var Productos = _ProductosRepository.Get(new string[] { "TProductosAtributos" });
            var Componentes = new List<DespachosDetalladosDetalleDTO>();

            if (Detallados != null)
            {
                foreach (var Detalle in Detallados.TDespachosComponentes)
                {
                    var Detalles = new DespachosDetalladosDetalleDTO()
                    {
                        Componente = Productos.Any(e => e.IdProducto == Detalle.Id_Producto_Componente) ? Productos.First(e => e.IdProducto == Detalle.Id_Producto_Componente).NombreCorto : Detalle.Id_Producto_Componente,
                        Densidad = Detalle.Densidad,
                        Temperatura = Detalle.Temperatura,
                        Volumen_Bruto = Detalle.Volumen_Bruto,
                        Volumen_Neto = Detalle.Volumen_Neto
                    };

                    Componentes.Add(Detalles);
                }
            }

            return Componentes;
        }

        public IEnumerable<DespachosConsolidadosDetalleDTO> ObtenerDetalleConsolidado(string Id_Terminal, string Id_Compañia, string Id_Producto, DateTime Fecha_Corte)
        {
             return CalcularDespachosConsolidadosDetalle(Id_Terminal, Id_Compañia, Id_Producto , Fecha_Corte);            
        }


        public IEnumerable<TUUsuariosTerminalCompañia> ConsultarTerminalesCompañiasPorUsuario(string Id_Usuario)
        {
            return _usuariosTerminalCompañiaRepository.Get(Id_Usuario ,  "Id" );
        }

        public IEnumerable<TTerminalesProductosReceta> ConsultarProductosAsociadosTerminal(string Id_Terminal)
        {
            var productos = _TerminalesProductosRecetaRepository.Get("Id", "Id.TProductosRecetasComponentes").ToList();
            return productos.Where(e => e.IdTerminal == Id_Terminal).ToList();
        }

        public ManagerResult CrearDespacho(TDespacho Despacho)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            try
            {
                if (_DespachosRepository.Existe(Despacho.Id_Despacho))
                {
                    response.Result = false;
                    response.Failures.Add("El despacho que trata de crear ya existe");
                }
                else
                {
                    _DespachosRepository.Add(Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Insertar, "Despacho " + Despacho.Id_Despacho + " Creada.", LogPrioridades.Informacion);
                    response.Result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Insertar, "Despacho " + Despacho.Id_Despacho  + "NO Creada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                response.Result = false;
                response.Failures.Add("Algún componente ya existe para otro despacho. No se creo el despacho");
            }

            return response;
        }

        
        public async Task<bool> CrearDespachoAsync(TDespacho Despacho)
        {
            try
            {
                if (await _DespachosRepository.ExisteAsync(Despacho.Id_Despacho))
                    return false;
                else
                {
                    _DespachosRepository.Add(Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Insertar, "Despacho " + Despacho.Id_Despacho  + " Creada.", LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Insertar, "Despacho " + Despacho.Id_Despacho  + "NO Creada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }
                
        public ManagerResult ActualizarDespacho(TDespacho Despacho)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            try
            {
                if (!_DespachosRepository.Existe(Despacho.Id_Despacho))
                {
                    response.Failures.Add("El despacho que trata de actualizar no existe");
                    response.Result = false;
                }
                else
                {

                    var DatosAntiguos = _logManager.SerializarEntidad(_DespachosRepository.Obtener(Despacho.Id_Despacho));
                    var DatosNuevos = _logManager.SerializarEntidad(Despacho);
                    Despacho.TDespachosComponentes = null;
                    _DespachosRepository.Update(Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Despacho.Id_Despacho + " Actualizada. Datos Antiguos: " + DatosAntiguos + " Datos Nuevos: " + DatosNuevos, LogPrioridades.Informacion);
                    response.Result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Despacho.Id_Despacho  + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                response.Failures.Add("Ocurrio un problema al actualizar el despacho");
                response.Result = false;
            }

            return response;
        }


        public ManagerResult ActualizarComponentesDespacho(IEnumerable<TDespachosComponente> despachosComponentes)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            foreach (var Componente in despachosComponentes)
            {

                try
                {

                    if (!_DespachosComponentesRepository.Existe(Componente.No_Orden, Componente.Ship_To, Componente.Id_Producto_Componente, Componente.Compartimento, Componente.Tanque , Componente.Contador))
                    {
                        response.Failures.Add("El componentes que trata de actualizar no existe");
                        response.Result = false;
                    }
                    else
                    {

                        var DatosAntiguos = _logManager.SerializarEntidad(_DespachosComponentesRepository.Obtener(Componente.No_Orden , Componente.Ship_To, Componente.Id_Producto_Componente, Componente.Compartimento, Componente.Tanque, Componente.Contador ));
                        var DatosNuevos = _logManager.SerializarEntidad(Componente);

                        _DespachosComponentesRepository.Update(Componente);
                        
                        _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Componente.Id_Despacho + " Actualizada. Datos Antiguos: " + DatosAntiguos + " Datos Nuevos: " + DatosNuevos, LogPrioridades.Informacion);
                        response.Result = true;
                    }

                }
                catch (Exception e)
                {
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Componente.Id_Despacho + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                    response.Failures.Add("Ocurrio un problema al actualizar el componente");
                    response.Result = false;
                }
            }

            return response;
        }

        public ManagerResult ActualizarEstadoDespacho(string Id_Despacho, bool IdEstado)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            try
            {
                if (!_DespachosRepository.Existe(Id_Despacho))
                {
                    response.Failures.Add("El despacho que trata de actualizar no existe");
                    response.Result = false;
                }
                else
                {
                    var Despacho = ObtenerDespacho(Id_Despacho);
                    Despacho.Estado_Kairos = IdEstado;
                    _DespachosRepository.Update(Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Estado de Despacho " + Id_Despacho  + " Actualizada.", LogPrioridades.Informacion);
                    response.Result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Estado de Despacho " + Id_Despacho  + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                response.Failures.Add("Ocurrio un problema al actualizar el despacho");
                response.Result = false;
            }

            return response;
        }


        public async Task<bool> ActualizarDespachoAsync(TDespacho Despacho)
        {

            try
            {
                if (!await _DespachosRepository.ExisteAsync(Despacho.Id_Despacho))
                    return false;
                else
                {
                    var DatosAntiguos = _logManager.SerializarEntidad(_DespachosRepository.Obtener(Despacho.Id_Despacho));
                    var DatosNuevos = _logManager.SerializarEntidad(Despacho);                    
                    _DespachosRepository.Update(Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Despacho.Id_Despacho  + " Actualizada. Datos Antiguos: " + DatosAntiguos + " Datos Nuevos: " + DatosNuevos, LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Actualizar, "Despacho " + Despacho.Id_Despacho  + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }


            return true;
        }

        public bool BorrarDespacho(string Id_Despacho)
        {
            var result = false;
            try
            {
                if (_DespachosRepository.Existe(Id_Despacho))
                {
                    _DespachosRepository.Eliminar(Id_Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Eliminar, "Despacho " + Id_Despacho  + " Borrada.", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Eliminar, "Despacho " + Id_Despacho  + "NO Borrada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return result;
        }

        public async Task<bool> BorrarDespachoAsync(string Id_Despacho)
        {
            var result = false;

            try
            {
                if (await _DespachosRepository.ExisteAsync(Id_Despacho))
                {
                    _DespachosRepository.Eliminar(Id_Despacho);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Eliminar, "Despacho " + Id_Despacho  + " Borrada.", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Despachos", "", "T_Despachos", LogAcciones.Eliminar, "Despacho " + Id_Despacho  + "NO Borrada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return result;
        }

        public IEnumerable<FechasCorteDTO> ObtenerFechasCierrePorMes(string terminal, DateTime fecha)
        {
            List<FechasCorteDTO> fechas = new List<FechasCorteDTO>();
            IEnumerable<TTASCortes> cortes = _tasCortesRepository.ObtenerFechasCierrePorMes(fecha.Year, fecha.Month, terminal);
            foreach (TTASCortes corte in cortes)
            {
                fechas.Add(new FechasCorteDTO() { Fecha = corte.Fecha_Corte, Cierre = corte.Fecha_Cierre_Kairos.HasValue });
            }

            return fechas;
        }

        public DateTime? ObtenerFechaCierreInicial()
        {
            return _tasCortesRepository.ObtenerPrimerCierre()?.Fecha_Cierre_Kairos;
        }

        public bool TieneFechaCierre(DateTime fechaCorte, string Id_Terminal)
        {
            return _DespachosEngine.TieneFechaCierre(fechaCorte, Id_Terminal);
        }

        public bool TieneFechaCierre(long? Id_Corte)
        {
            return _DespachosEngine.TieneFechaCierre(Id_Corte);
        }
    }

}
