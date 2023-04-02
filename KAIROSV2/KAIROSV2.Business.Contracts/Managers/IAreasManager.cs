using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IAreasManager
    {
        TArea ObtenerArea(string idArea);
        Task <TArea> ObtenerAreaAsync(string idArea);
        IEnumerable<TArea> ObtenerAreas();
        bool CrearArea(TArea Area);
        bool ActualizarArea(TArea Area);
        bool BorrarArea(string idArea);
        Task<bool> BorrarAreaAsync(string idArea);

    }
}
