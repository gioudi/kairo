using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
   public interface IProductosManager
    {
        IEnumerable<TProducto> ObtenerProductos();
        bool BorrarProducto(string idProducto);
        void BorrarProductoTerminalReceta(string idProducto, string idTerminal);
        IEnumerable<TProductosTipo> ObtenerTiposProducto();
        IEnumerable<TProductosClase> ObtenerClasesProducto();
        TProducto ObtenerProductoConRecetas(string idProducto);
        TProducto ObtenerProducto(string idProducto);
        IEnumerable<ProductoTerminalDto> ObtenerProductosTerminalesRecetas(string idTerminal);
        ProductoTerminalDto ObtenerProductoRecetaAsignadoTerminal(string idTerminal, string idProducto);
        ManagerResult AsignarProductoRecetaATerminal(ProductoTerminalDto productoTerminal);
        ManagerResult CrearProducto(TProducto producto);
        ManagerResult ActualizarProducto(TProducto producto);
        bool AgregarActualizarAtributosProducto(IEnumerable<TProductosAtributo> atributos);
        ManagerResult AgregarActualizarRecetasProductoFormulario(IEnumerable<RecetaDTO> recetas, string productId);
        public IEnumerable<TProducto> ObtenerProductosPorClase(string idclase);
    }
}
