using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ILineasRepository: IDataRepository<TLinea>
    {
        IEnumerable<TLinea> ObtenerTodas(params string[] includes);

        IEnumerable<TLinea> ObtenerTodas();
        Task<IEnumerable<TLinea>> ObtenerTodasAsync();

        IEnumerable<TLineasEstado> ObtenerEstadosLinea();

        TLinea Obtener(string IdLinea, string IdTerminal, params string[] includes);

        TLinea Obtener(string IdLinea, string IdTerminal);

        Task<TLinea> ObtenerAsync(string IdLinea, string IdTerminal);

        bool Existe(string IdLinea, string IdTerminal);
        Task<bool> ExisteAsync(string IdLinea, string IdTerminal);
    }
}
