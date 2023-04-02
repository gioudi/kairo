using KAIROSV2.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITablasSistemaRepository
    {
        bool Existe(string NombreTabla);
        VDbTabla Obtener(string NombreTabla);
        VDbTabla Obtener(string NameTabla, params string[] includes);
        Task<VDbTabla> ObtenerAsync(string NombreTabla);
        IEnumerable<VDbTabla> ObtenerTodas();
        IEnumerable<VDbTabla> ObtenerTodas(params string[] includes);
        IEnumerable<string> ObtenerNombresTablas();
    }
}