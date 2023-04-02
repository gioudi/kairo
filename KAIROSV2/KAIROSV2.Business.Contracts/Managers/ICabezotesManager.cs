using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ICabezotesManager
    {
        Task<TCabezote> ObtenerCabezote(string placa);
        IEnumerable<TCabezote> ObtenerCabezotes();
        bool CrearCabezote(TCabezote cabezote);
        Task<bool> BorrarCabezote(TCabezote Cabezotes);
    }
}
