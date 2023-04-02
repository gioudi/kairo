using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ITrailersManager
    {
        Task<TTrailer> ObtenerTrailer(string placa);
        IEnumerable<TTrailer> ObtenerTrailers();
        bool CrearTrailer(TTrailer placa);
        Task<bool> BorrarTrailer(TTrailer placa);
    }
}
