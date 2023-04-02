using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITerminalesRepository : IDataRepository<TTerminal>
    {
        IEnumerable<TTerminal> ObtenerTodas(params string[] includes);
        
        IEnumerable<TTerminal> ObtenerTodas();
        Task<IEnumerable<TTerminal>> ObtenerTodasAsync();

        IEnumerable<TTerminalesEstado> ObtenerEstadosTerminal();

        TTerminal Obtener(string IdTerminal, params string[] includes);
        
        TTerminal Obtener(string IdTerminal);

        Task<TTerminal> ObtenerAsync(string IdTerminal);

        bool Existe(string IdTerminal);
        Task <bool> ExisteAsync(string IdTerminal);
    }
}
