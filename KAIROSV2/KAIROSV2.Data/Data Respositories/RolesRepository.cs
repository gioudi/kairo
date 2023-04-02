using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Data
{
    public class RolesRepository : DataRepositoryBase<TURole>, IRolesRepository
    {
        protected override TURole GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TURoleSet
                         where e.IdRol == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TURole UpdateEntity(KAIROSV2DBContext entityContext, TURole entity)
        {
            return (from e in entityContext.TURoleSet
                    where e.IdRol == entity.IdRol
                    select e).FirstOrDefault();
        }

        public bool RolHasAssociatedUsers(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TURoleSet.Any(e => e.IdRol == id && e.TUUsuarios.Count > 0);
            }
        }

        public bool Exists(string idRol)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TURoleSet.Any(e => e.IdRol == idRol);
            }
        }
    }
}
