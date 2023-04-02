using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface ITerminalCompañiaRepository : IDataRepository<TTerminalCompañia>
    {
        IEnumerable<TTerminalCompañia> Get(params string[] includes);
        void RemoveCompaniesByTerminal(string idTerminal);
        void RemoveCompanyByTerminal(string idTerminal, string idCompañia);
        IEnumerable<TTerminalCompañia> ObtenerTodas(params string[] includes);
        IEnumerable<TTerminalCompañia> ObtenerTodas();
        Task<IEnumerable<TTerminalCompañia>> ObtenerTodasAsync();
        IEnumerable< TTerminalCompañia> Obtener(string IdTerminal);
        TTerminalCompañia Obtener(string IdTerminal, params string[] includes);
        Task<TTerminalCompañia> ObtenerAsync(string IdTerminal);
        bool Existe(string IdTerminal, string IdCompañia);
    }
}
