using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ICompañiasRepository : IDataRepository<TCompañia>
    {
        IEnumerable<TCompañia> ObtenerTodas(params string[] includes);

        IEnumerable<TCompañia> ObtenerTodas();
        Task<IEnumerable<TCompañia>> ObtenerTodasAsync();

        TCompañia Obtener(string IdCompañia, params string[] includes);

        public TCompañia Obtener(string IdCompañia);
        public Task<TCompañia> ObtenerAsync(string IdCompañia);

        bool Existe(string IdCompañia);
    }
}
