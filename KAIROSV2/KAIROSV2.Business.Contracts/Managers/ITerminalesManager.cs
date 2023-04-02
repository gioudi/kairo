using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ITerminalesManager
    {
        TTerminal ObtenerTerminal(string idTerminal);
        TTerminal ObtenerTerminal(string idTerminal, params string[] includes);
        IEnumerable<TTerminalesEstado> ObtenerEstadosTerminal();
        Task <TTerminal> ObtenerTerminalAsync(string idTerminal);
        IEnumerable<TTerminal> ObtenerTerminales();
        IEnumerable<TTerminal> ObtenerTerminales(params string[] includes);
        Task <IEnumerable<TTerminal>> ObtenerTerminalesAsync();
        bool CrearTerminal(TTerminal Terminal);
        bool ActualizarTerminal(TTerminal Terminal);
        Task<bool> ActualizarTerminalAsync(TTerminal Terminal);
        bool BorrarTerminal(string idTerminal);
        Task<bool> BorrarTerminalAsync(string idTerminal);
        bool ActualizarEstadoTerminal(string IdTerminal, int estado);

    }
}
