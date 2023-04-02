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
using KAIROSV2.Business.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los Terminales
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad Terminal, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir Terminales.
    /// </remarks>
    public class TerminalesManager : ManagerBase, ITerminalesManager
    {
        private readonly ITerminalesRepository _TerminalesRepository;

        public TerminalesManager(ITerminalesRepository TerminalesRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _TerminalesRepository = TerminalesRepository;
        }

        /// <summary>
        /// Obtiene el Terminal incluyendo su imagen
        /// </summary>
        /// <param name="idTerminal">Id del Terminal</param>
        /// <returns>Terminal</returns>
        public TTerminal ObtenerTerminal(string idTerminal)
        {
            TTerminal Terminal = new TTerminal();
            if (_TerminalesRepository.Existe(idTerminal))
                Terminal = _TerminalesRepository.Obtener(idTerminal);
            return Terminal;
        }

        public IEnumerable<TTerminalesEstado> ObtenerEstadosTerminal()
        {
            return _TerminalesRepository.ObtenerEstadosTerminal().ToList();
            
        }

        public TTerminal ObtenerTerminal(string idTerminal , string[] parametros)
        {
            TTerminal Terminal = new TTerminal();
            if (_TerminalesRepository.Existe(idTerminal))
                Terminal = _TerminalesRepository.Obtener(idTerminal, parametros);
            return Terminal;
        }

        public Task<TTerminal> ObtenerTerminalAsync(string idTerminal)
        {            
            try
            {
                if (_TerminalesRepository.Existe(idTerminal))
                    return _TerminalesRepository.ObtenerAsync(idTerminal);
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }                       
            
        }

        /// <summary>
        /// Obtiene todos los Terminales del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Terminales del sistema</returns>
        public Task< IEnumerable<TTerminal>> ObtenerTerminalesAsync()
        {
            return _TerminalesRepository.ObtenerTodasAsync();
        }

        public IEnumerable<TTerminal> ObtenerTerminales(params string[] includes)
        {
            return _TerminalesRepository.ObtenerTodas(includes);
        }

        public IEnumerable<TTerminal> ObtenerTerminales()
        {
            return _TerminalesRepository.ObtenerTodas();
        }



        /// <summary>
        /// Crean el Terminal en el sistema
        /// </summary>
        /// <param name="Terminal">Entidad Terminal para crear</param>
        /// <returns>True si creo el Terminal, Flase si el Terminal ya existe</returns>
        public bool CrearTerminal(TTerminal Terminal)
        {
            try
            {
                if (_TerminalesRepository.Existe(Terminal.IdTerminal))
                    return false;
                else
                {
                    _TerminalesRepository.Add(Terminal);
                    LogInformacion(LogAcciones.Insertar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal?.IdTerminal} creado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal?.IdTerminal} no creado.", e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del Terminal
        /// </summary>
        /// <param name="Terminal">Entidad Terminal para actualizar</param>
        /// <returns>True si se actualizo el Terminal, False si no existe el Terminal</returns>
        public bool ActualizarTerminal(TTerminal Terminal)
        {
            try
            {
                if (!_TerminalesRepository.Existe(Terminal.IdTerminal))
                    return false;
                else
                { 
                    _TerminalesRepository.Update(Terminal);
                    LogInformacion(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal?.IdTerminal} actualizada.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal?.IdTerminal} no actualizada.", e);
                return false;
            }

            return true;
        }

        public bool ActualizarEstadoTerminal( string IdTerminal,  int IdEstado)
        {
            try
            {
                if (!_TerminalesRepository.Existe(IdTerminal))
                    return false;
                else
                {
                    var Terminal = ObtenerTerminal(IdTerminal);
                    Terminal.IdEstado = IdEstado;
                    //Terminal.IdEstado = _TerminalesRepository.ObtenerEstadosTerminal().Where(e => e.Descripcion == estado).FirstOrDefault().IdEstado;
                    _TerminalesRepository.Update(Terminal);
                    LogInformacion(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Estado de terminal {IdTerminal} actualizado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Estado de terminal {IdTerminal} no actualizado.", e);
                return false;
            }

            return true;
        }


        public async Task<bool> ActualizarTerminalAsync(TTerminal Terminal)
        {
            try
            {
                if (!await _TerminalesRepository.ExisteAsync(Terminal.IdTerminal))
                    return false;
                else
                {
                    _TerminalesRepository.Update(Terminal);
                    LogInformacion(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal.IdTerminal} actualizada.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {Terminal.IdTerminal} no actualizada.", e);
                return false;
            }
            
            return true;
        }

        public bool BorrarTerminal(string idTerminal)
        {
            var result = false;
            try
            {
                if (_TerminalesRepository.Existe(idTerminal))
                {
                    _TerminalesRepository.Remove(idTerminal);
                    LogInformacion(LogAcciones.Eliminar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {idTerminal} eliminada.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {idTerminal} no eliminada.", e);
                return false;
            }

            return result;
        }

        public async Task<bool> BorrarTerminalAsync(string idTerminal)
        {
            var result = false;

            try
            {
                if (await _TerminalesRepository.ExisteAsync(idTerminal))
                {
                    _TerminalesRepository.Remove(idTerminal);
                    LogInformacion(LogAcciones.Eliminar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {idTerminal} eliminada.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Terminales", "Terminales", "T_Terminales", $"Terminal {idTerminal} no eliminada.", e);
                return false;
            }

            return result;
        }
    }

}
