using KAIROSV2.Business.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IProcesamientoArchivosMstManager
    {
        bool ActualizarPaMst(TProcesamientoArchivosMst paMst);
        bool BorrarPaMst(string idMapeo);
        Task<bool> BorrarPaMstAsync(string idMapeo);
        bool CrearPAMst(TProcesamientoArchivosMst paMst);
        IEnumerable<TProcesamientoArchivosMst> ObtenerPAMst();
        TProcesamientoArchivosMst ObtenerPAMst(string idMapeo);
        Task<TProcesamientoArchivosMst> ObtenerPAMstAsync(string idMapeo);
        public bool ProcesarArchivoTexto(IFormFile archivo, char separador, out List<string> encabezadosColumnas, out List<List<string>> muestraData);
        bool GetColumnasTablaSistema(string currTabla, out List<VDbColumna> columnasSis);
        bool GetTablasSistema(out List<string> tablasSis);
        bool Existe(string idMapeo);
    }
}