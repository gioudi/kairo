using KAIROSV2.Business.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IProcesamientoArchivosDetManager
    {
        bool ActualizarPaDet(TProcesamientoArchivosDet paDet);
        bool BorrarPaDet(string idMapeo);
        Task<bool> BorrarPaDetAsync(string idMapeo);
        bool CrearPADet(TProcesamientoArchivosDet paDet);
        IEnumerable<TProcesamientoArchivosDet> ObtenerPADet();
        TProcesamientoArchivosDet ObtenerPADet(string idMapeo);
        Task<TProcesamientoArchivosDet> ObtenerPADetAsync(string idMapeo);
        List<string> GetColumnasArchivo(string idMapeo);
        IEnumerable<TProcesamientoArchivosDet> ObtenerPADetByKey(string idMapeo);
        IEnumerable<string> ObtenerTablasSistemaPAByKey(string idMapeo);
        IEnumerable<TProcesamientoArchivosDet> ObtenerPADetByKeyTabla(string idMapeo, string idTabla);
        public bool ProcesarArchivoTextoToBD(IFormFile archivo, char separador, string idMapeo);
        bool BorrarPaDetByKey(string idMapeo, string tablaActual);
    }
}