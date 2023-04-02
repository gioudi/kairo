using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using KAIROSV2.Business.Common.Exceptions;

namespace KAIROSV2.Data
{
    public class ProductosRepository : DataRepositoryBase<TProducto>, IProductosRepository
    {
        public TProducto Get(string id, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProductoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.IdProducto == id).FirstOrDefault();
            }
        }

        public IEnumerable<TProducto> Get(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProductoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TProducto> GetByType(string type, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProductoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.IdTipoNavigation.IdTipo == type).ToList();
            }
        }

        public IEnumerable<TProducto> GetByClass(string classId, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProductoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                if (classId == "1")
                    query = query.Where(e => e.IdClaseNavigation.Descripcion == "Base");
                else if (classId == "2")
                    query = query.Where(e => e.IdClaseNavigation.Descripcion == "Base" || e.IdClaseNavigation.Descripcion == "Mezcla");
                else if (classId == "3")
                    query = query.Where(e => e.IdClaseNavigation.Descripcion == "Mezcla" || e.IdClaseNavigation.Descripcion == "PreMezcla");

                return query.ToList();
            }
        }

        public bool Exists(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductoSet.Any(e => e.IdProducto == id);
            }
        }

        public bool ExistsNombreERP(string nombreERP)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductoSet.Any(e => e.NombreErp == nombreERP);
            }
        }

        public bool ExistsNombreCorto(string nombreCorto)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductoSet.Any(e => e.NombreCorto == nombreCorto);
            }
        }

        public bool IsAdditive(string idProduct)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductoSet.Any(e => e.IdProducto == idProduct && e.IdTipo == "5");
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
                        throw new DeleteCascadeException("La entidad que desea borrar tiene datos relacionados");
                    else
                        throw;
                }
            }
        }

        protected override TProducto GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TProductoSet
                         where e.IdProducto == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TProducto UpdateEntity(KAIROSV2DBContext entityContext, TProducto entity)
        {
            return (from e in entityContext.TProductoSet
                    where e.IdProducto == entity.IdProducto
                    select e).FirstOrDefault();
        }

        public IEnumerable<TProductosTipo> ObtenerTiposProducto()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductosTipoSet.ToList();
            }
        }

        public IEnumerable<TProducto> ObtenerProductos()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductoSet.ToList();
            }
        }

    }
}
