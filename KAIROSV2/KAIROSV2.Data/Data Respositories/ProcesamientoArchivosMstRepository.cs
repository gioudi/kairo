using KAIROSV2.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAIROSV2.Data.Contracts;


namespace KAIROSV2.Data
{
    public class ProcesamientoArchivosMstRepository : DataRepositoryBase<TProcesamientoArchivosMst>, IProcesamientoArchivosMstRepository
    {
        protected override TProcesamientoArchivosMst GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TProcesamientoArchivosMstSet
                         where e.IdMapeo == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TProcesamientoArchivosMst UpdateEntity(KAIROSV2DBContext entityContext, TProcesamientoArchivosMst entity)
        {
            return (from e in entityContext.TProcesamientoArchivosMstSet
                    where e.IdMapeo == entity.IdMapeo
                    select e).FirstOrDefault();
        }

        public IEnumerable<TProcesamientoArchivosMst> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProcesamientoArchivosMstSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TProcesamientoArchivosMst> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosMstSet.ToList();
            }
        }


        public TProcesamientoArchivosMst Obtener(string idMapeo, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProcesamientoArchivosMstSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdMapeo == idMapeo);
            }
        }

        public TProcesamientoArchivosMst Obtener(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosMstSet.Where(e => e.IdMapeo == idMapeo).FirstOrDefault();
            }
        }

        public async Task<TProcesamientoArchivosMst> ObtenerAsync(string idMapeo)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosMstSet.Where(e => e.IdMapeo == idMapeo).FirstOrDefault();
            }
        }

        public bool Existe(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosMstSet.Any(e => e.IdMapeo == idMapeo);
            }
        }
    }
}

