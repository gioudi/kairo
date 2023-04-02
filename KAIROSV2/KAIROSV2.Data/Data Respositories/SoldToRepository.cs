using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace KAIROSV2.Data
{
    public class SoldToRepository : DataRepositoryBase<TSoldTo>, ISoldToRepository
    {
        protected override TSoldTo GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TSoldTo UpdateEntity(KAIROSV2DBContext entityContext, TSoldTo entity)
        {
            return (from e in entityContext.TSoldToSet
                    where e.Sold_to == entity.Sold_to
                    select e).FirstOrDefault();
        }

        public IEnumerable<TSoldTo> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TSoldToSet.ToList();
            }
        }
    }
}
