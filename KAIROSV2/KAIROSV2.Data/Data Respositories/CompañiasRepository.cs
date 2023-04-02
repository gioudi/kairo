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
    public class CompañiasRepository : DataRepositoryBase<TCompañia>, ICompañiasRepository
    {
        protected override TCompañia GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TCompañiaSet
                         where e.IdCompañia == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TCompañia UpdateEntity(KAIROSV2DBContext entityContext, TCompañia entity)
        {
            return (from e in entityContext.TCompañiaSet
                    where e.IdCompañia == entity.IdCompañia
                    select e).FirstOrDefault();
        }

        public IEnumerable<TCompañia> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TCompañia> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCompañiaSet.ToList();
            }
        }

        public async Task<IEnumerable<TCompañia>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCompañiaSet.ToList();
            }
        }

        public TCompañia Obtener(string IdCompañia, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdCompañia == IdCompañia);
            }
        }

        public TCompañia Obtener(string IdCompañia)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCompañiaSet.Where(e => e.IdCompañia == IdCompañia).FirstOrDefault();
            }
        }

        public async Task<TCompañia> ObtenerAsync(string IdCompañia)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCompañiaSet.Where(e => e.IdCompañia == IdCompañia).FirstOrDefault();
            }
        }

        public bool Existe(string IdCompañia)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCompañiaSet.Any(e => e.IdCompañia == IdCompañia);
            }
        }
    }
}
