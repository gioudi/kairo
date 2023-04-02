using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Data.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los proveedores de negocio
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para los proveedores, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir.
    /// </remarks>
    public class ProveedoresManager : ManagerBase, IProveedoresManager
    {
        private readonly IProveedoresRepository _proveedoresRepository;
        public ProveedoresManager(IProveedoresRepository proveedoresRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _proveedoresRepository = proveedoresRepository;
        }

        /// <summary>
        /// Obtiene todos los proveedores del sistema
        /// </summary>
        /// <returns>Proveedores</returns>
        public IEnumerable<TProveedor> ObtenerProveedores()
        {
            return _proveedoresRepository.Get();
        }

        /// <summary>
        /// Borra un proveedor del sistema
        /// </summary>
        /// <param name="idProveedor">Id del proveedor</param>
        /// <returns>True si se borro el proveedor, False si el proveedor no existe</returns>
        public bool BorrarProveedor(string idProveedor)
        {
            var result = false;

            if (_proveedoresRepository.Exists(idProveedor))
            {
                _proveedoresRepository.Remove(idProveedor);
                result = true;
                LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {idProveedor} eliminado.");
            }

            return result;
        }

        public TProveedor ObtenerProveedor(string idProveedor)
        {
            return _proveedoresRepository.Get(idProveedor, "TProveedoresProductos.IdTipoProductoNavigation", "TProveedoresPlanta");
        }

        public bool CrearProveedor(TProveedor proveedor)
        {
            var result = false;
            if (!_proveedoresRepository.Exists(proveedor.IdProveedor))
            {
                _proveedoresRepository.Add(proveedor);
                result = true;
                LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {proveedor?.IdProveedor} creado.");
            }
            return result;
        }

        public bool ActualizarProveedor(TProveedor proveedor)
        {
            if (!_proveedoresRepository.Exists(proveedor.IdProveedor))
                return false;
            else
            {
                proveedor.TProveedoresPlanta = null;
                proveedor.TProveedoresProductos = null;
                _proveedoresRepository.Update(proveedor);
                LogInformacion(LogAcciones.Actualizar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {proveedor?.IdProveedor} actualizado.");
            }
            return true;
        }

        public void ReemplazarProveedorPlantas(string idProveedor, IEnumerable<TProveedoresPlanta> plantas)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _proveedoresRepository.RemoveAllPlantsBySupplier(idProveedor);
                    LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {idProveedor} todas las plantas eliminadas.");

                    if (plantas != null)
                    {
                        _proveedoresRepository.AddPlantsSupplier(plantas);
                        LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {idProveedor} ({plantas?.Count()}) plantas creadas.");
                    }

                    scope.Complete();
                }                
            }
            catch (Exception ex)
            {
                LogError(LogAcciones.Actualizar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {idProveedor} error actualizando plantas.", ex);
                throw new PersistEntityException("Ocurrió un error actualizando las platas proveedor.");
            }            
        }

        public void ReemplazarProveedorProducots(string idProveedor, IEnumerable<TProveedoresProducto> productos)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _proveedoresRepository.RemoveAllProductsBySupplier(idProveedor);
                    LogInformacion(LogAcciones.Eliminar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores_Productos", $"Proveedor {idProveedor} se eliminan todos los proveedores.");

                    if (productos != null)
                    {
                        _proveedoresRepository.AddProductsSupplier(productos);
                        LogInformacion(LogAcciones.Insertar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores_Productos", $"Proveedor {idProveedor} ({productos?.Count()}) proveedores creados.");
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                LogError(LogAcciones.Actualizar, "Suministro y logística", "Proveedores", "Proveedores", "T_Proveedores", $"Proveedor {idProveedor} error actualizando proveedores.", ex);
                throw new PersistEntityException("Ocurrió un error actualizando los productos proveedor.");
            }
        }
    }
}
