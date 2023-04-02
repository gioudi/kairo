using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IDespachosManager
    {
        TDespacho ObtenerDespacho(string Id_Despacho);
        TDespacho ObtenerDespacho(string Id_Despacho, params string[] includes);
        Task<TDespacho> ObtenerDespachoAsync(string Id_Despacho);
        IEnumerable<TDespacho> ObtenerTodas();
        IEnumerable<TDespacho> ObtenerTodas(params string[] includes);
        Task<IEnumerable<TDespacho>> ObtenerTodasAsync();
        IEnumerable<TDespachosComponente> ObtenerDespachosComponentes(string Id_Terminal, string Id_Compañia, string Id_Producto ,DateTime Fecha_Corte);
        IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, string Id_Compañia, DateTime Fecha_Cierre);
        IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, List<string> Id_Compañia, DateTime Fecha_Corte);
        IEnumerable<DespachosConsolidadosDetalleDTO> CalcularDespachosConsolidadosDetalle(string Id_Terminal, string Id_Compañia , string Id_Producto, DateTime Fecha_Corte);
        IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, string Id_Compañia, DateTime Fecha_Corte);
        IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, List<string> Id_Compañia, DateTime Fecha_Corte);
        IEnumerable<DespachosDetalladosDetalleDTO> ObtenerDetalleDetalle(string Id_Despacho);
        ManagerResult CrearDespacho(TDespacho Despacho);
        Task<bool> CrearDespachoAsync(TDespacho Despacho);
        ManagerResult ActualizarDespacho(TDespacho Despacho);
        Task<bool> ActualizarDespachoAsync(TDespacho Despacho);
        ManagerResult ActualizarComponentesDespacho(IEnumerable<TDespachosComponente> despachosComponentes);
        bool BorrarDespacho(string Id_Despacho);
        Task<bool> BorrarDespachoAsync(string Id_Despacho);
        ManagerResult ActualizarEstadoDespacho(string Id_Despacho, bool estado);
        IEnumerable<TUUsuariosTerminalCompañia> ConsultarTerminalesCompañiasPorUsuario(string Id_Usuario);
        IEnumerable<DespachosConsolidadosDetalleDTO> ObtenerDetalleConsolidado(string Id_Terminal, string Id_Compañia, string Id_Producto, DateTime Fecha_Corte);
        IEnumerable<TTerminalesProductosReceta> ConsultarProductosAsociadosTerminal(string Id_Terminal);
        IEnumerable<TSoldTo> ObtenerSoldTos();
        IEnumerable<TShipTo> ObtenerShipTos();
        IEnumerable<FechasCorteDTO> ObtenerFechasCierrePorMes(string terminal, DateTime fecha);
        DateTime? ObtenerFechaCierreInicial();
        bool TieneFechaCierre(DateTime fechaCorte, string Id_Terminal);
        bool TieneFechaCierre(long? Id_Corte);

    }
}
