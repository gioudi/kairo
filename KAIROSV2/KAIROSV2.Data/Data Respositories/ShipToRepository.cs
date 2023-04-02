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
    public class ShipToRepository : DataRepositoryBase<TShipTo>, IShipToRepository
    {
        protected override TShipTo GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TShipTo UpdateEntity(KAIROSV2DBContext entityContext, TShipTo entity)
        {
            return (from e in entityContext.TShipToSet
                    where e.Ship_to == entity.Ship_to
                    select e).FirstOrDefault();
        }

        public IEnumerable<TShipTo> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TShipToSet.ToList();
            }
        }
    }
}
