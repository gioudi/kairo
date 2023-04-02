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
    public class CabezotesManager : ManagerBase, ICabezotesManager
    {
        private readonly ICabezotesRepository _CabezotesRepository;

        public CabezotesManager(ICabezotesRepository ConductoresRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _CabezotesRepository = ConductoresRepository;
        }

        public async Task<TCabezote> ObtenerCabezote(string Placa)
        {
            return await _CabezotesRepository.Get(Placa);
        }

        public IEnumerable<TCabezote> ObtenerCabezotes()
        {
            return _CabezotesRepository.GetAll();
        }

        public bool CrearCabezote(TCabezote Cabezotes)
        {
            if (_CabezotesRepository.Exists(Cabezotes.PlacaCabezote))
                return false;
            else
            {
                _CabezotesRepository.Add(Cabezotes);
                LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Cabezotes", "Cabezotes", "T_Cabezotes", "Cabezote " + Cabezotes?.PlacaCabezote + " creado");
            }

            return true;
        }


        public async Task<bool> BorrarCabezote(TCabezote Cabezotes)
        {
            if (!_CabezotesRepository.Exists(Cabezotes.PlacaCabezote))
                return false;
            else
            {
                _CabezotesRepository.Remove(Cabezotes);
                LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Cabezotes", "Cabezotes", "T_Cabezotes", "Cabezote " + Cabezotes?.PlacaCabezote + " eliminado");
            }

            return true;
        }

        public IEnumerable<TCabezote> ObtenerCabezote()
        {
            throw new NotImplementedException();
        }
    }

}
