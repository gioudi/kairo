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
    public class AccountRepository : DataRepositoryBase<TUUsuario>, IAccountRepository
    {
        #region UserStore
        public async Task AddAsync(TUUsuario usuario, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                await entityContext.TUUsuarioSet.AddAsync(usuario, cancellationToken);
                await entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TUUsuario> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return await entityContext.TUUsuarioSet.Include(e => e.TUUsuarioImagen).FirstOrDefaultAsync(e => e.IdUsuario == id, cancellationToken);
            }
        }

        public async Task<IList<TUUsuario>> GetByRolAsync(string rolId, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return await entityContext.TUUsuarioSet.Where(e => e.RolId == rolId).ToListAsync(cancellationToken);
            }
        }

        public async Task<TUUsuario> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return await entityContext.TUUsuarioSet.Include(e => e.TUUsuarioImagen).FirstOrDefaultAsync(e => e.Nombre == name, cancellationToken);
            }
        }

        public async Task RemoveAsync(TUUsuario usuario, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TUUsuarioSet.Remove(usuario);
                await entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public Task UpdateAsync(TUUsuario usuario, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Entry(usuario).State = EntityState.Modified;
                return entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public Task UpdateUserRoleAsync(TUUsuario usuario, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TUUsuarioSet.Attach(usuario);
                entityContext.Entry(usuario).Property(x => x.RolId).IsModified = true;
                return entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public IQueryable<TUUsuario> GetIqueryable()
        {
            var entityContext = new KAIROSV2DBContext();
            return entityContext.TUUsuarioSet;
        }

        protected override TUUsuario GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TUUsuarioSet
                         where e.IdUsuario == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TUUsuario UpdateEntity(KAIROSV2DBContext entityContext, TUUsuario entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region RolStore
        public async Task AddRoleAsync(TURole role, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                await entityContext.TURoleSet.AddAsync(role, cancellationToken);
                await entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task RemoveRoleAsync(TURole role, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TURoleSet.Remove(role);
                await entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TURole> FindRoleByIdAsync(string id, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return await entityContext.TURoleSet.Include(e => e.TURolesPermisos).FirstOrDefaultAsync(e => e.IdRol == id, cancellationToken);
            }
        }

        public async Task<TURole> FindRoleByNameAsync(string name, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return await entityContext.TURoleSet.Include(e => e.TURolesPermisos).FirstOrDefaultAsync(e => e.Nombre == name, cancellationToken);
            }
        }

        public Task UpdateRoleAsync(TURole role, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Entry(role).State = EntityState.Modified;
                return entityContext.SaveChangesAsync(cancellationToken);
            }
        }
        #endregion

    }
}
