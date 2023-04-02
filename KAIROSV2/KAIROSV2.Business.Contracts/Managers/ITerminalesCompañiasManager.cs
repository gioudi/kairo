using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ITerminalesCompañiasManager
    {
        IEnumerable<TerminalCompañiaDTO> ObtenerBaseJerarquiaTerminalesCompañiaUsuario(string idUsuario);
        IEnumerable<TerminalCompañiaDTO> ObtenerBaseJerarquiaTerminalesCompañia();
        void AgregarNuevoTerminalesCompañiaParaUsuario(string idUsuario, IEnumerable<TerminalCompañiaDTO> terminalCompañia);
        void BorrarTerminalesCompañiaParaUsuario(string idUsuario);
        string AsignarCompañiasATerminal(string IdTerminal, IEnumerable<TTerminalCompañia> terminalCompañias);
        string AsignarCompañiaATerminal(string IdTerminal, TTerminalCompañia Compañia);
        string EliminarCompañiasATerminal(string idTerminal);
        string EliminarCompañiaATerminal(string idTerminal , string idCompañia);
        IEnumerable<TTerminalCompañia> ObtenerTerminalCompañias();
        IEnumerable<TTerminalCompañia> ObtenerTerminalCompañias(string idTerminal);
        Task<TTerminalCompañia> ObtenerTerminalCompañiasAsync(string idTerminal);
        //Task<IEnumerable<TTerminalCompañia>> ObtenerTerminalCompañiasAsync(string idTerminal);
        TTerminalCompañia ObtenerTerminalCompañias(string idTerminal, string[] parametros);
    }
}
