using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IProcesamientoArchivosMstRepository : IDataRepository<TProcesamientoArchivosMst>
    {
        bool Existe(string idMapeo);
        TProcesamientoArchivosMst Obtener(string idMapeo);
        TProcesamientoArchivosMst Obtener(string idMapeo, params string[] includes);
        Task<TProcesamientoArchivosMst> ObtenerAsync(string idMapeo);
        IEnumerable<TProcesamientoArchivosMst> ObtenerTodas();
        IEnumerable<TProcesamientoArchivosMst> ObtenerTodas(params string[] includes);
    }
}