using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
   public interface ILineasManager
   {
        TLinea ObtenerLinea(string idLinea, string IdTerminal);
        TLinea ObtenerLinea(string idLinea, string IdTerminal, params string[] includes);
        IEnumerable<TLineasEstado> ObtenerEstadosLinea();
        Task<TLinea> ObtenerLineaAsync(string idLinea, string IdTerminal);
        IEnumerable<TLinea> ObtenerLineas();
        IEnumerable<TLinea> ObtenerLineas(params string[] includes);
        IEnumerable<LineaDTO> ObtenerProductoTerminalEstadoLineas();
       
        LineaDTO AgregarProductosTerminalesEstadosLinea(TLinea IdLinea, bool Consultar);
        Task<IEnumerable<TLinea>> ObtenerLineasAsync();
        bool CrearLinea(TLinea Linea);
        Task<bool> CrearLineaAsync(TLinea Linea);
        bool ActualizarLinea(TLinea Linea);
        Task<bool> ActualizarLineaAsync(TLinea Linea);
        bool BorrarLinea(string idLinea, string IdTerminal);
        Task<bool> BorrarLineaAsync(string idLinea, string IdTerminal);
        bool ActualizarEstadoLinea(string IdLinea, string IdTerminal, int estado);
        IEnumerable<TProducto> ObtenerProductos();
    }
}
