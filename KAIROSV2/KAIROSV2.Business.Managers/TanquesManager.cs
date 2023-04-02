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
    /// Maneja la logica de negocio para los Tanques
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad Tanque, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir Tanques.
    /// </remarks>
    public class TanquesManager : ITanquesManager
    {
        private readonly ITanquesRepository _TanquesRepository;
        private readonly IProductosRepository _ProductosRepository;
        private readonly ILogManager _logManager;

        public TanquesManager(ITanquesRepository TanquesRepository, ILogManager LogManager, IProductosRepository ProductosRepository)
        {
            _TanquesRepository = TanquesRepository;
            _ProductosRepository = ProductosRepository;
            _logManager = LogManager;
        }

        /// <summary>
        /// Obtiene el Tanque incluyendo su imagen
        /// </summary>
        /// <param name="idTanque">Id del Tanque</param>
        /// <returns>Tanque</returns>
        public TTanque ObtenerTanque(string idTanque, string IdTerminal)
        {
            TTanque Tanque = new TTanque();
            if (_TanquesRepository.Existe(idTanque, IdTerminal))
                Tanque = _TanquesRepository.Obtener(idTanque, IdTerminal);
            return Tanque;
        }

        public IEnumerable<TTanquesEstado> ObtenerEstadosTanque()
        {
            return _TanquesRepository.ObtenerEstadosTanque().ToList();

        }

        public TTanque ObtenerTanque(string idTanque, string IdTerminal, string[] parametros)
        {
            TTanque Tanque = new TTanque();
            if (_TanquesRepository.Existe(idTanque, IdTerminal))
                Tanque = _TanquesRepository.Obtener(idTanque, IdTerminal, parametros);
            return Tanque;
        }

        public Task<TTanque> ObtenerTanqueAsync(string idTanque, string IdTerminal)
        {
            try
            {
                if (_TanquesRepository.Existe(idTanque, IdTerminal))
                    return _TanquesRepository.ObtenerAsync(idTanque, IdTerminal);
                else
                    return null;
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Insertar, "Tanque " + idTanque + "no creado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return null;
            }

        }

        /// <summary>
        /// Obtiene todos los Tanques del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Tanques del sistema</returns>
        public Task<IEnumerable<TTanque>> ObtenerTanquesAsync()
        {
            return _TanquesRepository.ObtenerTodasAsync();
        }


        public IEnumerable<TProducto> ObtenerProductos()
        {
            return _ProductosRepository.ObtenerProductos();
        }

        public IEnumerable<TTanque> ObtenerTanques(params string[] includes)
        {
            return _TanquesRepository.ObtenerTodas(includes);
        }

        public TanqueDTO AgregarProductosTerminalesEstadosTanque(TTanque Tanque, bool Consultar)
        {
            if (Consultar)
                Tanque = _TanquesRepository.Obtener(Tanque.IdTanque, Tanque.IdTerminal, new string[3] { "IdEstadoNavigation", "IdTerminalNavigation", "IdProductoNavigation" });

            var TipoProductos = _ProductosRepository.ObtenerTiposProducto();

            var TanqueCompleto = new TanqueDTO()
            {
                ClaseTanque = Tanque.ClaseTanque,
                ClaseColor = Tanque.ClaseTanque.Equals("FISICO", StringComparison.OrdinalIgnoreCase) ? "blue" : (Tanque.ClaseTanque.Equals("VIRTUAL", StringComparison.OrdinalIgnoreCase) ? "green" : "gray"),
                Estado = Tanque.IdEstadoNavigation.Descripcion,
                EstadoColor = Tanque.IdEstadoNavigation.ColorHex,
                Producto = Tanque.IdProductoNavigation.NombreCorto,
                Tanque = Tanque.IdTanque,
                Terminal = Tanque.IdTerminalNavigation.Terminal,
                IdTerminal = Tanque.IdTerminal,
                IconoProducto = TipoProductos.Where(e => e.IdTipo == Tanque.IdProductoNavigation.IdTipo).FirstOrDefault().Icono,
                IconoColor = TipoProductos.Where(e => e.IdTipo == Tanque.IdProductoNavigation.IdTipo).FirstOrDefault().Color,
            };

            return TanqueCompleto;
        }


        public TanqueDetallesDTO AgregarDetallesTanque(string IdTanque, string IdTerminal)
        {
            var Tanque = _TanquesRepository.Obtener(IdTanque, IdTerminal, new string[4] { "IdEstadoNavigation", "IdTerminalNavigation", "IdProductoNavigation", "IdTanquesPantallaFlotanteNavigation" });

            var TanqueCompleto = new TanqueDetallesDTO()
            {
                AforadoPor = Tanque.AforadoPor,
                AlturaMaximaAforo = Tanque.AlturaMaximaAforo,
                CapacidadNominal = Tanque.CapacidadNominal,
                CapacidadOperativa = Tanque.CapacidadOperativa,
                FechaAforo = Tanque.FechaAforo,
                Observaciones = Tanque.Observaciones,
                PantallaFlotante = Tanque.PantallaFlotante,
                TipoTanque = Tanque.TipoTanque,
                VolumenNoBombeable = Tanque.VolumenNoBombeable,
                Tanque = Tanque.IdTanque,
                Terminal = Tanque.IdTerminalNavigation.Terminal
                
            };

            if (Tanque.IdTanquesPantallaFlotanteNavigation != null)
            {
                TanqueCompleto.DensidadAforo = Tanque.IdTanquesPantallaFlotanteNavigation.DensidadAforo;
                TanqueCompleto.NivelCorreccionFinal = Tanque.IdTanquesPantallaFlotanteNavigation.NivelCorreccionFinal;
                TanqueCompleto.NivelCorreccionInicial = Tanque.IdTanquesPantallaFlotanteNavigation.NivelCorreccionInicial;
                TanqueCompleto.GalonesPorGrado = Tanque.IdTanquesPantallaFlotanteNavigation.GalonesPorGrado;
            }

            return TanqueCompleto;
        }

        public IEnumerable<TanqueDTO> ObtenerProductoTerminalEstadoTanques()
        {
            var Tanques = _TanquesRepository.ObtenerTodas(new string[3] { "IdEstadoNavigation", "IdTerminalNavigation", "IdProductoNavigation" });
            List<TanqueDTO> TanquesProductos = new List<TanqueDTO>();

            foreach (var Tanque in Tanques)
            {
                var TanqueProducto = AgregarProductosTerminalesEstadosTanque(Tanque, false);
                TanquesProductos.Add(TanqueProducto);
            }

            return TanquesProductos;

        }


        public IEnumerable<TTanque> ObtenerTanques()
        {
            return _TanquesRepository.ObtenerTodas();
        }



        /// <summary>
        /// Crean el Tanque en el sistema
        /// </summary>
        /// <param name="Tanque">Entidad Tanque para crear</param>
        /// <returns>True si creo el Tanque, Flase si el Tanque ya existe</returns>
        public bool CrearTanque(TTanque Tanque)
        {
            try
            {
                if (_TanquesRepository.Existe(Tanque.IdTanque, Tanque.IdTerminal))
                    return false;
                else
                {
                    _TanquesRepository.Add(Tanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Insertar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " creado.", LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Insertar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + "no creado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }

        public async Task<bool> CrearTanqueAsync(TTanque Tanque)
        {
            try
            {
                if (await _TanquesRepository.ExisteAsync(Tanque.IdTanque, Tanque.IdTerminal))
                    return false;
                else
                {
                    _TanquesRepository.Add(Tanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Insertar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " creado.", LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Insertar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + "no creado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }


        public bool ActualizarTanque(TTanque Tanque)
        {
            try
            {
                if (!_TanquesRepository.Existe(Tanque.IdTanque, Tanque.IdTerminal))
                    return false;
                else
                {

                    var DatosAntiguos = _logManager.SerializarEntidad(_TanquesRepository.Obtener(Tanque.IdTanque, Tanque.IdTerminal));
                    var DatosNuevos = _logManager.SerializarEntidad(Tanque);

                    _TanquesRepository.BorrarPantallaFlotante(Tanque.IdTanque, Tanque.IdTerminal);
                    _TanquesRepository.Update(Tanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " actualizado. Datos Antiguos: " + DatosAntiguos + " Datos Nuevos: " + DatosNuevos, LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " no actualizado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }

        public bool ActualizarEstadoTanque(string IdTanque, string IdTerminal, int IdEstado)
        {
            try
            {
                if (!_TanquesRepository.Existe(IdTanque, IdTerminal))
                    return false;
                else
                {
                    var Tanque = ObtenerTanque(IdTanque, IdTerminal);
                    Tanque.IdEstado = IdEstado;
                    _TanquesRepository.Update(Tanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Estado de Tanque " + IdTanque + " para la Terminal " + Tanque.IdTerminal + " actualizado.", LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Estado de Tanque " + IdTanque + " para la Terminal " + IdTerminal + " no actualizado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }


        public async Task<bool> ActualizarTanqueAsync(TTanque Tanque)
        {

            try
            {
                if (!await _TanquesRepository.ExisteAsync(Tanque.IdTanque, Tanque.IdTerminal))
                    return false;
                else
                {
                    var DatosAntiguos = _logManager.SerializarEntidad(_TanquesRepository.Obtener(Tanque.IdTanque, Tanque.IdTerminal));
                    var DatosNuevos = _logManager.SerializarEntidad(Tanque);
                    _TanquesRepository.BorrarPantallaFlotante(Tanque.IdTanque, Tanque.IdTerminal);
                    _TanquesRepository.Update(Tanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " actualizado. Datos Antiguos: " + DatosAntiguos + " Datos Nuevos: " + DatosNuevos, LogPrioridades.Informacion);
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Actualizar, "Tanque " + Tanque.IdTanque + " para la Terminal " + Tanque.IdTerminal + " no actualizado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }


            return true;
        }

        public bool BorrarTanque(string idTanque, string IdTerminal)
        {
            var result = false;
            try
            {
                if (_TanquesRepository.Existe(idTanque, IdTerminal))
                {
                    _TanquesRepository.Remove(idTanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Eliminar, "Tanque " + idTanque + " para la Terminal " + IdTerminal + " eliminado.", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Eliminar, "Tanque " + idTanque + " para la Terminal " + IdTerminal + "no eliminado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return result;
        }

        public async Task<bool> BorrarTanqueAsync(string idTanque, string IdTerminal)
        {
            var result = false;

            try
            {
                if (await _TanquesRepository.ExisteAsync(idTanque, IdTerminal))
                {
                    _TanquesRepository.Remove(idTanque);
                    _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Eliminar, "Tanque " + idTanque + " para la Terminal " + IdTerminal + " eliminado.", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Tanques", "", "T_Tanques", LogAcciones.Eliminar, "Tanque " + idTanque + " para la Terminal " + IdTerminal + "no eliminado. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return result;
        }
    }

}
