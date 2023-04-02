using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IProcesamientoArchivosDetRepository : IDataRepository<TProcesamientoArchivosDet>
    {
        bool Existe(string idMapeo, string idTabla, string idCampo);
        bool Existe(string idMapeo, string idTabla);
        bool Existe(string idMapeo);
        TProcesamientoArchivosDet Obtener(string idMapeo);
        TProcesamientoArchivosDet Obtener(string idMapeo, params string[] includes);
        Task<TProcesamientoArchivosDet> ObtenerAsync(string idMapeo);
        IEnumerable<TProcesamientoArchivosDet> ObtenerTodas();
        IEnumerable<TProcesamientoArchivosDet> ObtenerTodas(params string[] includes);
        List<TProcesamientoArchivosDet> ObtenerByKey(string idMapeo);
        List<string> ObtenerColumnasArchivoByKey(string idMapeo);
        IEnumerable<string> ObtenerTablasSistemaByKey(string idMapeo);
        IEnumerable<TProcesamientoArchivosDet> ObtenerTablasSistemaByKeyTabla(string idMapeo, string idTabla);
        int IndexArchivoByKeyTabla(string idMapeo, string idTabla, string idCampo);
        string NombreColumnaArchivoByKeyTabla(string idMapeo, string idTabla, string idCampo);
        string InsertData(string sql);
    }
}