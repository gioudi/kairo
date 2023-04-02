using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KAIROSV2.Data
{
    public class RolesPermisosRepository : DataRepositoryBase<TURolesPermiso>, IRolesPermisosRepository
    {
        public IEnumerable<TURolesPermiso> Get(string idRol, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TURolesPermisoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.IdRol == idRol).ToList();
            }
        }
        public IEnumerable<TURolesPermiso> GetEnabled(string idRol, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TURolesPermisoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.IdRol == idRol && e.Activo).ToList();
            }
        }
        public void RemoveAllByRol(string idRol)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_U_Roles_Permisos] WHERE Id_Rol = {idRol}");
            }
        }

        protected override TURolesPermiso GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TURolesPermiso UpdateEntity(KAIROSV2DBContext entityContext, TURolesPermiso entity)
        {
            throw new NotImplementedException();
        }

    }
}
