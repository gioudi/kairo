using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ITanquesManager
    {
        TTanque ObtenerTanque(string idTanque, string IdTerminal);
        TTanque ObtenerTanque(string idTanque, string IdTerminal, params string[] includes);
        IEnumerable<TTanquesEstado> ObtenerEstadosTanque();
        Task<TTanque> ObtenerTanqueAsync(string idTanque, string IdTerminal);
        IEnumerable<TTanque> ObtenerTanques();
        IEnumerable<TTanque> ObtenerTanques(params string[] includes);
        IEnumerable<TanqueDTO> ObtenerProductoTerminalEstadoTanques();
        TanqueDetallesDTO AgregarDetallesTanque(string IdTanque, string IdTerminal);
        TanqueDTO AgregarProductosTerminalesEstadosTanque(TTanque Tanque, bool Consultar);
        Task<IEnumerable<TTanque>> ObtenerTanquesAsync();
        bool CrearTanque(TTanque Tanque);
        Task<bool> CrearTanqueAsync(TTanque Tanque);
        bool ActualizarTanque(TTanque Tanque);
        Task<bool> ActualizarTanqueAsync(TTanque Tanque);
        bool BorrarTanque(string idTanque, string IdTerminal);
        Task<bool> BorrarTanqueAsync(string idTanque, string IdTerminal);
        bool ActualizarEstadoTanque(string IdTanque, string IdTerminal, int estado);
        IEnumerable<TProducto> ObtenerProductos();

    }
}
