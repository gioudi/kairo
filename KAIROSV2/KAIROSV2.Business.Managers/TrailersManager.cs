using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    public class TrailersManager : ManagerBase, ITrailersManager
    {
        private readonly ITrailersRepository _TrailersRepository;

        public TrailersManager(ITrailersRepository TrailersRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _TrailersRepository = TrailersRepository;
        }

        public async Task<TTrailer> ObtenerTrailer(string Placa)
        {
            return await _TrailersRepository.Get(Placa);
        }

        public IEnumerable<TTrailer> ObtenerTrailers()
        {
            return _TrailersRepository.GetAll();
        }

        public bool CrearTrailer(TTrailer Trailers)
        {
            if (_TrailersRepository.Exists(Trailers.PlacaTrailer))
                return false;
            else
            {
                _TrailersRepository.Add(Trailers);
                LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Trailers", "Trailers", "T_Trailers", $"Trailer {Trailers?.PlacaTrailer} creado");
            }
            return true;
        }

        public async Task<bool> BorrarTrailer(TTrailer Trailers)
        {
            if (!_TrailersRepository.Exists(Trailers.PlacaTrailer))
                return false;
            else
            {
                _TrailersRepository.Remove(Trailers);
                LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Trailers", "Trailers", "T_Trailers", $"Tráiler {Trailers?.PlacaTrailer} eliminado");
            }
            return true;
        }

        public IEnumerable<TTrailer> ObtenerTrailer()
        {
            throw new NotImplementedException();
        }
    }

}
