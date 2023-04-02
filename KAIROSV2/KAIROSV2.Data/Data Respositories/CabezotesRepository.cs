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
    public class CabezotesRepository : DataRepositoryBase<TCabezote>, ICabezotesRepository
    {
        protected override TCabezote GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TCabezoteSet
                         where e.PlacaCabezote == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TCabezote UpdateEntity(KAIROSV2DBContext entityContext, TCabezote entity)
        {
            return (from e in entityContext.TCabezoteSet
                    where e.PlacaCabezote == entity.PlacaCabezote
                    select e).FirstOrDefault();
        }

        public IEnumerable<TCabezote> GetAll(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TCabezoteSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TCabezote> GetAll()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {

                return entityContext.TCabezoteSet.ToList();
            }
        }

        public async Task<TCabezote> Get(string placa)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TCabezoteSet.AsQueryable();

                return query.First(e => e.PlacaCabezote == placa);
            }
        }

        public bool Exists(string placa)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TCabezoteSet.Any(e => e.PlacaCabezote == placa);
            }
        }
    }
}
