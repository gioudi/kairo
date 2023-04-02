using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ICompañiasManager
    {
        TCompañia ObtenerCompañia(string idCompañia);
        Task<TCompañia> ObtenerCompañiaAsync(string idCompañia);
        IEnumerable<TCompañia> ObtenerCompañias();
        Task<IEnumerable<TCompañia>> ObtenerCompañiasAsync();
        bool CrearCompañia(TCompañia Compañia);
        bool ActualizarCompañia(TCompañia Compañia);
        bool BorrarCompañia(string idCompañia);
        Task<bool> BorrarCompañiaAsync(string idCompañia);

    }
}
