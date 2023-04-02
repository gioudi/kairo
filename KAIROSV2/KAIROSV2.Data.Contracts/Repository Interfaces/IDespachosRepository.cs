using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IDespachosRepository : IDataRepository<TDespacho>
    {
        TDespacho Obtener(string id);
        TDespacho Obtener(string id, params string[] includes);
        Task<TDespacho> ObtenerAsync(string Id_Despacho);
        
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, string Id_Compañia, List<long> Id_Corte, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, string Id_Compañia, List<long> Id_Corte);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, string Id_Compañia, long Id_Corte, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, List<long> Id_Corte, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, long Id_Corte);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, long Id_Corte, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCorte(string Id_Terminal, long Id_Corte, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaSinCorte(string Id_Terminal, string Id_Compañia, DateTime UltimaFechaCorte, DateTime FechaSeleccionada, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaSinCorte(string Id_Terminal, List<string> Id_Compañia, DateTime UltimaFechaCorte, DateTime FechaSeleccionada, params string[] includes);
        IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, List<long> Id_Corte);
        Task<IEnumerable<TDespacho>> ObtenerDespachosFiltrosAsync(string Id_Terminal, string Id_Compañia, long Id_Corte, params string[] includes);
        IEnumerable<TDespachosComponente> ObtenerDespachosComponentesPorIds(IEnumerable<string> Despachos);
        IEnumerable<string> ObtenerIdsDespachosTerminalProducto(string Id_Terminal, string Id_Producto);
        IEnumerable<string> ObtenerIdsDespachosTerminalCompaniaProducto(string Id_Terminal, string Id_Compania, string Id_Producto);
        IEnumerable<string> ObtenerIdsDespachosTerminalCompañiaProductoCorte(string Id_Terminal, string Id_Compañia, string Id_Producto , List<long> Id_Corte);
        IEnumerable<string> ObtenerIdsDespachosTerminalCorte(string Id_Terminal, List<long> Id_Corte);
        public IEnumerable<TDespacho> ObtenerDespachosAPI(string searchQuery);
        IEnumerable<TDespacho> ObtenerDespachosAPI(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords);
        IEnumerable<TDespacho> ObtenerTodas();
        IEnumerable<TDespacho> ObtenerTodas(params string[] includes);
        Task<IEnumerable<TDespacho>> ObtenerTodasAsync(params string[] includes);
        Task<IEnumerable<TDespacho>> ObtenerTodasAsync();
        IEnumerable<TDespacho> Obtener(params string[] includes);
        bool Existe(string id);
        Task<bool> ExisteAsync(string Id_Despacho);
        void Eliminar(string id);
    }
}
