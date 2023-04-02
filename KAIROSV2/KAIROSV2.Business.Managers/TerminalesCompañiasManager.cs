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
    /// <summary>
    /// Maneja la logica de negocio para la relacion de terminales compañias
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para manejar las relaciones de terminales compañias en el sistema, asi como las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir.
    /// </remarks>
    public class TerminalesCompañiasManager : ManagerBase, ITerminalesCompañiasManager
    {
        private readonly ITerminalCompañiaRepository _terminalCompañiaRepository;
        private readonly IUsuariosTerminalCompañiaRepository _usuariosTerminalCompañiaRepository;

        public TerminalesCompañiasManager(ITerminalCompañiaRepository terminalCompañiaRepository,
                                          IUsuariosTerminalCompañiaRepository usuariosTerminalCompañiaRepository,
                                          IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _terminalCompañiaRepository = terminalCompañiaRepository;
            _usuariosTerminalCompañiaRepository = usuariosTerminalCompañiaRepository;
        }

        /// <summary>
        /// Obtiene todas las terminales/compañias y señala las que estan asociadas al usuario
        /// </summary>
        /// <remarks>
        /// Crea una jerarquia de terminales padre y compañias hijas señalando cuales de estas estan asociadas al usuario
        /// </remarks>
        /// <param name="idUsuario">Id de usuario</param>
        /// <returns>Jerarquia terminales compañia</returns>
        public IEnumerable<TerminalCompañiaDTO> ObtenerBaseJerarquiaTerminalesCompañiaUsuario(string idUsuario)
        {
            var baseTerminalesCompañia = _terminalCompañiaRepository.Get("IdCompañiaNavigation", "IdTerminalNavigation");
            var terminalesCompañiaUsuario = _usuariosTerminalCompañiaRepository.Get(idUsuario);
            var resultado = new List<TerminalCompañiaDTO>(baseTerminalesCompañia?.Count() ?? 0);

            var grupo = baseTerminalesCompañia.GroupBy(e => e.IdTerminalNavigation);

            foreach (var terminal in grupo)
            {
                var terminalDTO = new TerminalCompañiaDTO();
                terminalDTO.EsTerminal = true;
                terminalDTO.Habilitada = terminalesCompañiaUsuario.Any(e => e.IdTerminal == terminal.Key.IdTerminal);
                terminalDTO.IdEntidad = terminal.Key.IdTerminal;
                terminalDTO.Nombre = terminal.Key.Terminal;
                terminalDTO.Compañias = new List<TerminalCompañiaDTO>(terminal.Count());
                foreach (var compañia in terminal)
                {
                    var compañiaDTO = new TerminalCompañiaDTO();
                    compañiaDTO.EsTerminal = false;
                    compañiaDTO.Habilitada = terminalesCompañiaUsuario.Any(e => e.IdTerminal == compañia.IdTerminal && e.IdCompañia == compañia.IdCompañia);
                    compañiaDTO.IdEntidad = compañia.IdCompañia;
                    compañiaDTO.Nombre = compañia.IdCompañiaNavigation.Nombre;
                    compañiaDTO.IdEntidadPadre = compañia.IdTerminal;
                    terminalDTO.Compañias.Add(compañiaDTO);
                }
                resultado.Add(terminalDTO);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene todas la relacion de terminales/compañias
        /// </summary>
        /// <remarks>
        /// Crea una jerarquia de terminales padre y compañias hijas.
        /// </remarks>
        /// <returns>Jerarquia terminales compañia</returns>
        public IEnumerable<TerminalCompañiaDTO> ObtenerBaseJerarquiaTerminalesCompañia()
        {
            var baseTerminalesCompañia = _terminalCompañiaRepository.Get("IdCompañiaNavigation", "IdTerminalNavigation");
            var resultado = new List<TerminalCompañiaDTO>(baseTerminalesCompañia?.Count() ?? 0);

            var grupo = baseTerminalesCompañia.GroupBy(e => e.IdTerminalNavigation);

            foreach (var terminal in grupo)
            {
                var terminalDTO = new TerminalCompañiaDTO();
                terminalDTO.EsTerminal = true;
                terminalDTO.IdEntidad = terminal.Key.IdTerminal;
                terminalDTO.Nombre = terminal.Key.Terminal;
                terminalDTO.Compañias = new List<TerminalCompañiaDTO>(terminal.Count());
                foreach (var compañia in terminal)
                {
                    var compañiaDTO = new TerminalCompañiaDTO();
                    compañiaDTO.EsTerminal = false;
                    compañiaDTO.IdEntidad = compañia.IdCompañia;
                    compañiaDTO.Nombre = compañia.IdCompañiaNavigation.Nombre;
                    compañiaDTO.IdEntidadPadre = compañia.IdTerminal;
                    terminalDTO.Compañias.Add(compañiaDTO);
                }
                resultado.Add(terminalDTO);
            }

            return resultado;
        }

        /// <summary>
        /// Agrega la relacion de terminales/compañias para un usuario.
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <param name="terminalCompañia">Terminales/Compañia</param>
        public void AgregarNuevoTerminalesCompañiaParaUsuario(string idUsuario, IEnumerable<TerminalCompañiaDTO> terminalCompañia)
        {
            var listadoTemrinalesCompañias = new List<TUUsuariosTerminalCompañia>();
            var terminalesCompañiasHabilitadas = terminalCompañia.SelectMany(e => e.Compañias.Where(e => e.Habilitada == true));
            foreach (var item in terminalesCompañiasHabilitadas)
            {
                listadoTemrinalesCompañias.Add(new TUUsuariosTerminalCompañia()
                {
                    IdUsuario = idUsuario,
                    IdCompañia = item.IdEntidad,
                    IdTerminal = item.IdEntidadPadre,
                    Estado = "0",
                    EditadoPor = "Admin",
                    UltimaEdicion = DateTime.Now
                });
                LogInformacion(LogAcciones.Insertar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_U_Usuario_Terminal_Compañia", $"Usuario {idUsuario} - Terminal {item?.IdEntidadPadre} - Compañía {item?.IdEntidad} creado");
            }

            _usuariosTerminalCompañiaRepository.RemoveAllByUser(idUsuario);
            _usuariosTerminalCompañiaRepository.Add(listadoTemrinalesCompañias);
        }

        /// <summary>
        /// Borra todas las terminales/compañia asociadas a un usuario
        /// </summary>
        /// <param name="idUsuario">Id de usuario</param>
        public void BorrarTerminalesCompañiaParaUsuario(string idUsuario)
        {
            _usuariosTerminalCompañiaRepository.RemoveAllByUser(idUsuario);
            LogInformacion(LogAcciones.Eliminar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_U_Usuario_Terminal_Compañia", $"Terminales compañía para usuario {idUsuario} eliminadas");
        }

        public string EliminarCompañiasATerminal(string idTerminal)
        {
            try
            {
                _terminalCompañiaRepository.RemoveCompaniesByTerminal(idTerminal);
                LogInformacion(LogAcciones.Eliminar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañías para {idTerminal} eliminadas");
                return "";
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañías para {idTerminal} no eliminadas", e);
                return Logger.ManejoErrores(e);
            }
        }

        public string EliminarCompañiaATerminal(string idTerminal, string idCompañia)
        {
            try
            {
                _terminalCompañiaRepository.RemoveCompanyByTerminal(idTerminal, idCompañia);
                LogInformacion(LogAcciones.Eliminar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañía {idCompañia} - Terminal {idTerminal} eliminada");
                return "";
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañía {idCompañia} - Terminal {idTerminal} no eliminada", e);
                return Logger.ManejoErrores(e);
            }
        }

        public string AsignarCompañiasATerminal(string IdTerminal, IEnumerable<TTerminalCompañia> terminalCompañias)
        {
            try
            {
                _terminalCompañiaRepository.RemoveCompaniesByTerminal(IdTerminal);
                _terminalCompañiaRepository.Add(terminalCompañias);
                foreach (var Compañia in terminalCompañias)
                {
                    LogInformacion(LogAcciones.Insertar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañía {Compañia.IdCompañia} - Terminal {IdTerminal} eliminada");
                }
                return "";
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Error asignado ({terminalCompañias?.Count()}) compañías a terminal {IdTerminal}", e);
                return Logger.ManejoErrores(e);
            }
        }

        public string AsignarCompañiaATerminal(string IdTerminal, TTerminalCompañia Compañia)
        {
            try
            {
                if (!_terminalCompañiaRepository.Existe(IdTerminal, Compañia.IdCompañia))
                {
                    _terminalCompañiaRepository.Add(Compañia);
                    LogInformacion(LogAcciones.Insertar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañía {Compañia.IdCompañia} asignada a terminal {IdTerminal} eliminada");
                    return "";
                }
                else
                    return "Compañía ya esta asignada a la Terminal";
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Configuración", "TerminalesCompañias", "TerminalesCompañias", "T_Terminal_Compañias", $"Compañía {Compañia.IdCompañia} no se puede asignar a terminal {IdTerminal} eliminada",e);
                return Logger.ManejoErrores(e);
            }
        }

        //public IEnumerable<TTerminalCompañia> ObtenerTerminalCompañias()
        //{
        //    return _terminalCompañiaRepository.Get().ToList();
        //}

        public TTerminalCompañia ObtenerTerminalCompañias(string idTerminal, string[] parametros)
        {
            return _terminalCompañiaRepository.Obtener(idTerminal, parametros);
        }

        public Task<TTerminalCompañia> ObtenerTerminalCompañiasAsync(string idTerminal)
        {
            return _terminalCompañiaRepository.ObtenerAsync(idTerminal);
        }

        public IEnumerable<TTerminalCompañia> ObtenerTerminalCompañias(string idTerminal)
        {
            return _terminalCompañiaRepository.Obtener(idTerminal);
        }



        public Task<IEnumerable<TTerminalCompañia>> ObtenerTerminalCompañiasAsync()
        {
            return _terminalCompañiaRepository.ObtenerTodasAsync();
        }

        public IEnumerable<TTerminalCompañia> ObtenerTerminalCompañias()
        {
            return _terminalCompañiaRepository.ObtenerTodas().ToList();
        }


    }


}
