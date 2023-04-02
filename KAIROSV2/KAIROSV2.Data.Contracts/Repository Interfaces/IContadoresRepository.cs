using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IContadoresRepository : IDataRepository<TContador>
    {
        IEnumerable<TContador> ObtenerTodas(params string[] includes);

        IEnumerable<TContador> ObtenerTodas();
        Task<IEnumerable<TContador>> ObtenerTodasAsync();        

        TContador Obtener(string IdContador, params string[] includes);

        TContador Obtener(string IdContador);

        Task<TContador> ObtenerAsync(string IdContador);

        bool Existe(string IdContador);
        Task<bool> ExisteAsync(string IdContador);
        //IEnumerable<TContadoresTipo> ObtenerTipos();
        //IEnumerable<TContadoresEstados> ObtenerEstados();

    }
}
