using KAIROSV2.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IColumnasSistemaRepository
    {
        Task<IEnumerable<VDbColumna>> ObtenerAsync(int idTabla);
        IEnumerable<VDbColumna> ObtenerColumnasTabla(int idTabla);
        IEnumerable<VDbColumna> ObtenerTodas();
        IEnumerable<VDbColumna> ObtenerTodas(params string[] includes);
    }
}