using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Engines
{
    public interface IDespachosEngine
    {
        IEnumerable<DespachosConsolidadosDetalleDTO> CalcularDespachosConsolidadosDetalle(string Id_Terminal, string Id_Compañia, string Id_Producto, DateTime FechaCierre);
        IEnumerable<TDespachosComponente> ObtenerDespachosComponentes(string Id_Terminal, string Id_Compañia, string Id_Producto , DateTime FechaCierre);
        IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, string Id_Compañia, DateTime FechaCierre);
        IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, List<string> Id_Compañia, DateTime FechaCierre);
        IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, string Id_Compañia, DateTime FechaCierre);
        IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal,  List<string> Id_Compañia, DateTime FechaCierre);
        bool TieneFechaCierre(DateTime FechaCorte, string Id_Terminal);
        bool TieneFechaCierre(long? Id_Corte);
    }
}
