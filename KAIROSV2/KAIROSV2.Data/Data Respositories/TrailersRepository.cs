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
    public class TrailersRepository : DataRepositoryBase<TTrailer>, ITrailersRepository
    {
        protected override TTrailer GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TTrailerSet
                         where e.PlacaTrailer == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TTrailer UpdateEntity(KAIROSV2DBContext entityContext, TTrailer entity)
        {
            return (from e in entityContext.TTrailerSet
                    where e.PlacaTrailer == entity.PlacaTrailer
                    select e).FirstOrDefault();
        }

        public IEnumerable<TTrailer> GetAll(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTrailerSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TTrailer> GetAll()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {

                return entityContext.TTrailerSet.ToList();
            }
        }

        public async Task<TTrailer> Get(string placa)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTrailerSet.AsQueryable();

                return query.First(e => e.PlacaTrailer == placa);
            }
        }

        public bool Exists(string placa)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTrailerSet.Any(e => e.PlacaTrailer == placa);
            }
        }
    }
}
