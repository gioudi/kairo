using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KAIROSV2.Data
{
    public class AtributosProductoRepository : DataRepositoryBase<TProductosAtributo>, IAtributosProductoRepository
    {
        protected override TProductosAtributo GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TProductosAtributo UpdateEntity(KAIROSV2DBContext entityContext, TProductosAtributo entity)
        {
            return (from e in entityContext.TProductosAtributoSet
                    where e.IdProducto == entity.IdProducto &&
                          e.IdAtributo == entity.IdAtributo
                    select e).FirstOrDefault();
        }

        public bool Exists(string idProducto, int idAtributo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductosAtributoSet.Any(e => e.IdProducto == idProducto && e.IdAtributo == idAtributo);
            }
        }

        public TProductosAtributo Get(string idProducto, int idAtributo, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProductosAtributoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdProducto == idProducto && e.IdAtributo == idAtributo);
            }
        }

        public IEnumerable<TProductosAtributo> Get(string idProducto)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductosAtributoSet.Where(e => e.IdProducto == idProducto).ToList();
            }
        }

    }
}
