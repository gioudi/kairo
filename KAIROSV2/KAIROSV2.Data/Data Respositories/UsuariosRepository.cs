using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KAIROSV2.Data
{
    public class UsuariosRepository : DataRepositoryBase<TUUsuario>, IUsuariosRepository
    {
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
            return (from e in entityContext.TUUsuarioSet.Include(e => e.TUUsuarioImagen)
                    where e.IdUsuario == entity.IdUsuario
                    select e).FirstOrDefault();
        }

        public IEnumerable<TUUsuario> GetAll(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TUUsuarioSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public TUUsuario Get(string idUsuario, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TUUsuarioSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdUsuario == idUsuario);
            }
        }

        public bool Exists(string idUsuario)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TUUsuarioSet.Any(e => e.IdUsuario == idUsuario);
            }
        }
    }
}
