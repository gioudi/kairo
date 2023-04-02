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
    public class ConductoresRepository : DataRepositoryBase<TConductor>, IConductoresRepository
    {
        protected override TConductor GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TConductorSet
                         where e.Cedula == Convert.ToInt32(id)
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TConductor UpdateEntity(KAIROSV2DBContext entityContext, TConductor entity)
        {
            return (from e in entityContext.TConductorSet
                    where e.Cedula == entity.Cedula
                    select e).FirstOrDefault();
        }

        public IEnumerable<TConductor> GetAll(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TConductorSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }



        

        public async Task<TConductor> Get(int Cedula, params string[] includes)
        {
            using ( KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TConductorSet.AsQueryable();
                foreach (string include in includes)
                {
                     query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.Cedula == Cedula);
            }
        }

        public bool Exists(int Cedula)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TConductorSet.Any(e => e.Cedula == Cedula);
            }
        }

        public async Task<TConductor> Get(int Cedula)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TConductorSet.Where(e => e.Cedula == Cedula).FirstOrDefault();
            }
        }

        public TConductor ObtenerConductor(int Cedula)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TConductorSet.Where(e => e.Cedula == Cedula).FirstOrDefault();
            }
        }
        public IEnumerable<TConductor> GetAll()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TConductorSet.Where(e => e.Cedula > 0).ToList();
            }
        }
    }
}
