using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Data.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Managers
{
    public class LineasManager : ManagerBase, ILineasManager
    {
        private readonly ILineasRepository _lineasRepository;
        private readonly IProductosRepository _productosRepository;

        public LineasManager(ILineasRepository lineasRepository,IProductosRepository productosRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _lineasRepository = lineasRepository;
            _productosRepository = productosRepository;

        }

        /// <summary>
        /// Obtiene el Tanque incluyendo su imagen
        /// </summary>
        /// <param name="idTanque">Id del Tanque</param>
        /// <returns>Tanque</returns>
        public TLinea ObtenerLinea(string idLinea, string IdTerminal)
        {
            TLinea Linea = new TLinea();
            if (_lineasRepository.Existe(idLinea, IdTerminal))
                Linea = _lineasRepository.Obtener(idLinea, IdTerminal);
            return Linea;
        }

        public IEnumerable<TLineasEstado> ObtenerEstadosLinea()
        {
            return _lineasRepository.ObtenerEstadosLinea().ToList();

        }

        public TLinea ObtenerLinea(string idLinea, string IdTerminal, string[] parametros)
        {
            TLinea Linea = new TLinea();
            if (_lineasRepository.Existe(idLinea, IdTerminal))
                Linea = _lineasRepository.Obtener(idLinea, IdTerminal, parametros);
            return Linea;
        }

        public Task<TLinea> ObtenerLineaAsync(string idLinea, string IdTerminal)
        {
            try
            {
                if (_lineasRepository.Existe(idLinea, IdTerminal))
                    return _lineasRepository.ObtenerAsync(idLinea, IdTerminal);
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// Obtiene todos los Tanques del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Tanques del sistema</returns>
        public Task<IEnumerable<TLinea>> ObtenerLineasAsync()
        {
            return _lineasRepository.ObtenerTodasAsync();
        }


        public IEnumerable<TProducto> ObtenerProductos()
        {
            return _productosRepository.ObtenerProductos();
        }

        public IEnumerable<TLinea> ObtenerLineas(params string[] includes)
        {
            return _lineasRepository.ObtenerTodas(includes);
        }

        public LineaDTO AgregarProductosTerminalesEstadosLinea(TLinea Linea, bool Consultar)
        {
            if (Consultar)
                Linea = _lineasRepository.Obtener(Linea.IdLinea, Linea.IdTerminal, new string[3] { "IdEstadoNavigation", "IdTerminalNavigation", "IdProductoNavigation" });

            var TipoProductos = _productosRepository.ObtenerTiposProducto();

            var LineaCompleta = new LineaDTO()
            {
                
                Estado = Linea.IdEstadoNavigation.Descripcion,
                EstadoColor = Linea.IdEstadoNavigation.ColorHex,
                Producto = Linea.IdProductoNavigation.NombreCorto,
                Idlinea = Linea.IdLinea,
                Terminal = Linea.IdTerminalNavigation.Terminal,
                IdTerminal = Linea.IdTerminal,
                Capacidad = Linea.Capacidad,
                DensidadAforo = Linea.DensidadAforo,
                Observaciones = Linea.Observaciones
               
            };

            return LineaCompleta;
        }

        public IEnumerable<LineaDTO> ObtenerProductoTerminalEstadoLineas()
        {
            var Lineas = _lineasRepository.ObtenerTodas(new string[3] { "IdEstadoNavigation", "IdTerminalNavigation", "IdProductoNavigation" });
            List<LineaDTO> LineasProductos = new List<LineaDTO>();

            foreach (var Linea in Lineas)
            {
                var LineaProducto = AgregarProductosTerminalesEstadosLinea(Linea, false);
                LineasProductos.Add(LineaProducto);
            }

            return LineasProductos;

        }

        public IEnumerable<TLinea> ObtenerLineas()
        {
            return _lineasRepository.ObtenerTodas();
        }

        public bool CrearLinea(TLinea Linea)
        {
            try
            {
                if (_lineasRepository.Existe(Linea.IdLinea, Linea.IdTerminal))
                    return false;
                else
                {
                    _lineasRepository.Add(Linea);
                    LogInformacion(LogAcciones.Insertar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} creada");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} no actualizada.", e);
                return false;
            }

            return true;
        }

        public async Task<bool> CrearLineaAsync(TLinea Linea)
        {
            try
            {
                if (await _lineasRepository.ExisteAsync(Linea.IdLinea, Linea.IdTerminal))
                    return false;
                else
                {
                    _lineasRepository.Add(Linea);
                    LogInformacion(LogAcciones.Insertar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} creada");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Lineas {Linea.IdLinea} - Terminal {Linea.IdTerminal} no creada.", e);
                return false;
            }

            return true;
        }


        public bool ActualizarLinea(TLinea Linea)
        {
            try
            {
                if (!_lineasRepository.Existe(Linea.IdLinea, Linea.IdTerminal))
                    return false;
                else
                {
                    _lineasRepository.Update(Linea);
                    LogInformacion(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} actualizada.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} no actualizada.", e);
                return false;
            }

            return true;
        }

        public bool ActualizarEstadoLinea(string IdLinea, string IdTerminal, int IdEstado)
        {
            try
            {
                if (!_lineasRepository.Existe(IdLinea, IdTerminal))
                    return false;
                else
                {
                    var Linea = ObtenerLinea(IdLinea, IdTerminal);
                    Linea.Id_Estado = IdEstado;
                    _lineasRepository.Update(Linea);
                    LogInformacion(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} actualizada.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Estado de Linea {IdLinea} para terminal " + IdTerminal + " no actualizada", e);
                return false;
            }

            return true;
        }


        public async Task<bool> ActualizarLineaAsync(TLinea Linea)
        {

            try
            {
                if (!await _lineasRepository.ExisteAsync(Linea.IdLinea, Linea.IdTerminal))
                    return false;
                else
                {                    
                    _lineasRepository.Update(Linea);
                    LogInformacion(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} actualizada.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {Linea.IdLinea} - Terminal {Linea.IdTerminal} no actualizada.", e);
                return false;
            }


            return true;
        }

        public bool BorrarLinea(string idLinea, string IdTerminal)
        {
            var result = false;
            try
            {
                if (_lineasRepository.Existe(idLinea, IdTerminal))
                {
                    _lineasRepository.Remove(idLinea);
                    LogInformacion(LogAcciones.Eliminar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {idLinea} - Terminal {IdTerminal} eliminada.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {idLinea} - Terminal {IdTerminal} no eliminada.", e);
                return false;
            }

            return result;
        }

        public async Task<bool> BorrarLineaAsync(string idLinea, string IdTerminal)
        {
            var result = false;

            try
            {
                if (await _lineasRepository.ExisteAsync(idLinea, IdTerminal))
                {
                    _lineasRepository.Remove(idLinea);
                    LogInformacion(LogAcciones.Eliminar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {idLinea} - Terminal {IdTerminal} eliminada.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Almacenamiento", "Lineas", "Lineas", "T_Lineas", $"Linea {idLinea} - Terminal {IdTerminal} no eliminada.", e);
                return false;
            }

            return result;
        }
    }
}
