using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Data.Contracts
{
    public interface IProveedoresRepository : IDataRepository<TProveedor>
    {
        bool Exists(string idProveedor);
        TProveedor Get(string idProveedor, params string[] includes);
        void RemoveAllPlantsBySupplier(string idProveedor);
        void RemoveAllProductsBySupplier(string idProveedor);
        void AddPlantsSupplier(IEnumerable<TProveedoresPlanta> entities);
        void AddProductsSupplier(IEnumerable<TProveedoresProducto> entities);
        void Remove(string id);
    }
}
