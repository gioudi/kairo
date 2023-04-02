using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class PermisosRepository : DataRepositoryBase<TUPermiso>, IPermisosRepository
    {
        public IEnumerable<TUPermiso> GetPermissionByRol(string idRol)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TUPermisoSet.Where(e => e.TURolesPermisos.Any(i => i.Activo == true && i.IdRol == idRol))?.ToList();
            }
        }

        protected override TUPermiso GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TUPermisoSet
                         where e.IdPermiso == Convert.ToInt32(id)
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TUPermiso UpdateEntity(KAIROSV2DBContext entityContext, TUPermiso entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TUPermiso> Get(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TUPermisoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }
    }
}
