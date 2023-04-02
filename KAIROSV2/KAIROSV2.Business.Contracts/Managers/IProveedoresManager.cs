using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface IProveedoresManager
    {
        IEnumerable<TProveedor> ObtenerProveedores();
        bool BorrarProveedor(string idProveedor);
        TProveedor ObtenerProveedor(string idProveedor);
        bool CrearProveedor(TProveedor proveedor);
        bool ActualizarProveedor(TProveedor proveedor);
        void ReemplazarProveedorPlantas(string idProveedor, IEnumerable<TProveedoresPlanta> plantas);
        void ReemplazarProveedorProducots(string idProveedor, IEnumerable<TProveedoresProducto> productos);
    }
}
