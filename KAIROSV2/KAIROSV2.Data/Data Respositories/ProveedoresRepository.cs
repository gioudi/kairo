using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using KAIROSV2.Business.Common.Exceptions;
using Microsoft.Data.SqlClient;

namespace KAIROSV2.Data
{
    public class ProveedoresRepository : DataRepositoryBase<TProveedor>, IProveedoresRepository
    {
        protected override TProveedor GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TProveedorSet
                         where e.IdProveedor == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TProveedor UpdateEntity(KAIROSV2DBContext entityContext, TProveedor entity)
        {
            return (from e in entityContext.TProveedorSet
                    where e.IdProveedor == entity.IdProveedor
                    select e).FirstOrDefault();
        }

        public bool Exists(string idProveedor)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProveedorSet.Any(e => e.IdProveedor == idProveedor);
            }
        }

        public TProveedor Get(string idProveedor, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProveedorSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdProveedor == idProveedor);
            }
        }

        public void RemoveAllPlantsBySupplier(string idProveedor)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Proveedores_Plantas] WHERE Id_Proveedor = {idProveedor}");
            }
        }

        public void RemoveAllProductsBySupplier(string idProveedor)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Proveedores_Productos] WHERE Id_Proveedor = {idProveedor}");
            }
        }

        public void AddPlantsSupplier(IEnumerable<TProveedoresPlanta> entities)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TProveedoresPlantaSet.AddRange(entities);
                entityContext.SaveChanges();
            }
        }

        public void AddProductsSupplier(IEnumerable<TProveedoresProducto> entities)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TProveedoresProductoSet.AddRange(entities);
                entityContext.SaveChanges();
            }
        }

        public void Remove(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var entity = GetEntity(entityContext, id);
                entityContext.Entry(entity).State = EntityState.Deleted;
                try
                {
                    entityContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException?.Number == 547)
                        throw new DeleteCascadeException("La entidad que desea borrar tiene datos relaciónados");
                    else
                        throw;
                }
            }
        }
    }
}
