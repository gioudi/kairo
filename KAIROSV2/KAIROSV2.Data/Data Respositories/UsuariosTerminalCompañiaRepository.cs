using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KAIROSV2.Data
{
    public class UsuariosTerminalCompañiaRepository : DataRepositoryBase<TUUsuariosTerminalCompañia>, IUsuariosTerminalCompañiaRepository
    {
        protected override TUUsuariosTerminalCompañia GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TUUsuariosTerminalCompañia UpdateEntity(KAIROSV2DBContext entityContext, TUUsuariosTerminalCompañia entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TUUsuariosTerminalCompañia> Get(string idUsuario, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TUUsuariosTerminalCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.IdUsuario == idUsuario).ToList();
            }
        }

        public void RemoveAllByUser(string idUsuario)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_U_Usuarios_Terminal_Compañia] WHERE Id_Usuario = {idUsuario}");
            }
        }

        public void RemoveAll()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [T_U_Usuarios_Terminal_Compañia]");
            }
        }

    }
}
