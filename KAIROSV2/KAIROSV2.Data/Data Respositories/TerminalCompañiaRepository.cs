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
    public class TerminalCompañiaRepository : DataRepositoryBase<TTerminalCompañia>, ITerminalCompañiaRepository
    {
        protected override TTerminalCompañia GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TTerminalCompañia UpdateEntity(KAIROSV2DBContext entityContext, TTerminalCompañia entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TTerminalCompañia> Get(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public void RemoveCompaniesByTerminal(string idTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Terminal_Compañias] WHERE Id_Terminal = {idTerminal}");
            }
        }

        public void RemoveCompanyByTerminal(string idTerminal , string idCompañia)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Terminal_Compañias] WHERE Id_Terminal = {idTerminal} AND Id_Compañia = {idCompañia}" );
            }
        }
        public IEnumerable<TTerminalCompañia> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TTerminalCompañia> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalCompañiaSet.ToList();
            }
        }

        public async Task<IEnumerable<TTerminalCompañia>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalCompañiaSet.ToList();
            }
        }

        public TTerminalCompañia Obtener(string IdTerminal, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalCompañiaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdTerminal == IdTerminal);
            }
        }

        public IEnumerable<TTerminalCompañia> Obtener(string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalCompañiaSet.Where(e => e.IdTerminal == IdTerminal).ToList();
            }
        }

        public async Task<TTerminalCompañia> ObtenerAsync(string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalCompañiaSet.Where(e => e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }

        public bool Existe(string IdTerminal, string IdCompañia)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalCompañiaSet.Any(e => e.IdTerminal == IdTerminal && e.IdCompañia == IdCompañia);
            }
        }
    }
}
