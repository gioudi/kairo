using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class AreasRepository : DataRepositoryBase<TArea>, IAreasRepository
    {
        protected override TArea GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TAreaSet
                         where e.IdArea == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TArea UpdateEntity(KAIROSV2DBContext entityContext, TArea entity)
        {
            return (from e in entityContext.TAreaSet
                    where e.IdArea == entity.IdArea
                    select e).FirstOrDefault();
        }

        public IEnumerable<TArea> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TAreaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TArea> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TAreaSet.ToList();
            }
        }

        public TArea Obtener(string idArea, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TAreaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdArea == idArea);
            }
        }

        public TArea Obtener(string idArea)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TAreaSet.Where(e => e.IdArea == idArea).FirstOrDefault();
            }
        }

        public async Task<TArea> ObtenerAsync(string idArea)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TAreaSet.Where(e => e.IdArea == idArea).FirstOrDefault();
            }
        }        

        public bool Existe(string idArea)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TAreaSet.Any(e => e.IdArea == idArea);
            }
        }
    }
}
